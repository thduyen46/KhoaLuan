using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Services.Interface;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Areas.Admin.Controllers
{
    [Authorize]
    [Authorize(Policy = Constants.Policies.RequireAdmin)]
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogArticleService _blogArticleService;
        private readonly ILogger<BlogController> _logger;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;
        public BlogController(IBlogArticleService blogArticleService, ILogger<BlogController> logger, IUserService userService, 
            ITagService tagService)
        {
            _blogArticleService = blogArticleService;
            _logger = logger;
            _userService = userService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await _blogArticleService.GetAllAsync();
                this.AddToastrMessage("Tải dữ liệu thành công", Enums.ToastrMessageType.Success);
                return View(model);
            }catch (Exception ex)
            {
                this.AddToastrMessage("Đã có lỗi xảy ra", Enums.ToastrMessageType.Error);
                _logger.LogError
                    (ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Create()
        {
            var tags = await _tagService.GetAllAsync();
            ViewData["TagList"] = string.Join(", ", tags.Select(x => x.Name).ToList());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogArticle model)
        {
            try
            {
                var user = await _userService.GetUser();
                if(user == null) {
                    this.AddToastrMessage("Vui lòng đăng nhập lại và thử lại", Enums.ToastrMessageType.Error);
                    return RedirectToAction(nameof(Create));
                }
                //Console.WriteLine("AA: " + model.HagTags);
                await _blogArticleService.Add(model, user.Id);
                return RedirectToAction(nameof(Index));
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            var tags = await _tagService.GetAllAsync();
            ViewData["TagList"] = string.Join(", ", tags.Select(x => x.Name).ToList());
            return View(model);
        }
    }
}
