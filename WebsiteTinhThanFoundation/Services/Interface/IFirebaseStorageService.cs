namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface IFirebaseStorageService
    {
        public Task<Uri> UploadFile(IFormFile file);
        public Task<List<string>> ListFiles(string path = "");
    }
}
