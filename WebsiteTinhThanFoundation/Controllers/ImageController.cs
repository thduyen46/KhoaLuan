using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Controllers
{
    public class ImageController : Controller
    {
        private readonly IFirebaseStorageService _storageService;
        public ImageController(IFirebaseStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload)
        {
            try
            {
                if (upload != null && upload.Length > 0)
                {
                    var pathUrl = await _storageService.UploadFile(upload);
                    return new JsonResult(pathUrl.ToString());
                }
                else
                {
                    return BadRequest(new { error = "Không có dữ liệu hoặc dữ liệu tải lên rỗng." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { error = "Đã xảy ra lỗi khi tải lên hình ảnh." });
            }
        }


        public async Task<IActionResult> UploadExplorer()
        {
            var dir = await _storageService.ListFiles("Images");
            ViewBag.FileInfo = dir;
            return View();
        }

        public async Task<IActionResult> Test(string id ="")
        {
            var a = await _storageService.ListFiles(id);
            return Ok(a);
        }

    }
}
