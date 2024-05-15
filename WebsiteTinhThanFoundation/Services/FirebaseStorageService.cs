using Firebase.Auth;
using Firebase.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Text.Json;
using System.Web;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Services
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly string bucketName; 
        private readonly string apiKey; 
        private readonly string userName;
        private readonly string password;
        private readonly string tokenCredential;
        private readonly string _firebaseStorageUrl;
        public FirebaseStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            bucketName = _configuration["Firebase:BucketName"];
            apiKey = _configuration["Firebase:APIKey"];
            userName = _configuration["Firebase:AccEmail"];
            password = _configuration["Firebase:AccPassword"];
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var account = auth.SignInWithEmailAndPasswordAsync(userName, password).Result;
            tokenCredential = account.FirebaseToken;
            _firebaseStorageUrl = $"https://firebasestorage.googleapis.com/v0/b/{bucketName}/o";
        }
        public async Task<Uri> UploadFile(IFormFile upload)
        {
            var storage = string.Empty;
            if (upload != null && upload.Length > 0)
            {
                List<string> list = new()
                {
                    "image/bmp",
                    "image/gif",
                    "image/jpeg",
                    "image/png",
                    "image/svg+xml",
                    "image/tiff",
                    "image/webp"
                };
                if (list.Contains(upload.ContentType))
                {
                    var randomGuid = Guid.NewGuid();
                    using var stream = new MemoryStream();
                    await upload.CopyToAsync(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    var cancelToken = new CancellationTokenSource();
                    var objectName = $"{Path.GetFileNameWithoutExtension(upload.FileName)}-{randomGuid}{Path.GetExtension(upload.FileName)}";
                    // Tạo một client Firebase Storage
                    storage = await new FirebaseStorage(bucketName, new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = async () => await Task.FromResult(tokenCredential),
                        ThrowOnCancel = true
                    }).Child("Images").Child(objectName).PutAsync(stream, cancelToken.Token);
                }
            }
            return new Uri(storage);
        }
        public async Task<List<string>> ListFiles(string path = "")
        {
            var files = new List<string>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenCredential);
                    Console.WriteLine($"{_firebaseStorageUrl}?prefix={path}/&delimiter=/");
                    var response = await httpClient.GetAsync($"{_firebaseStorageUrl}?prefix={path}/&delimiter=/");
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var jsonDocument = JsonDocument.Parse(responseBody);
                    var items = jsonDocument.RootElement.GetProperty("items").EnumerateArray();
                    foreach (var item in items)
                    {
                        files.Add(_firebaseStorageUrl + "/" + HttpUtility.UrlEncode(item.GetProperty("name").GetString()) + "?alt=media");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return files;
        }

    }
    public class FirebaseStorageResponse
    {
        public List<FirebaseStorageItem> Items { get; set; }
    }

    public class FirebaseStorageItem
    {
        public string Name { get; set; }
    }

}
