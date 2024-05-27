using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebsiteTinhThanFoundation.AutoMapperProfiles.BlogProfiles;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Services.Interface;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Areas.Admin.Controllers
{
    [Authorize]
    [Authorize(Policy = Constants.Policies.RequireVolunteer)]
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogArticleService _blogArticleService;
        private readonly ILogger<BlogController> _logger;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public BlogController(IBlogArticleService blogArticleService, ILogger<BlogController> logger, IUserService userService, 
            ITagService tagService, IMapper mapper)
        {
            _blogArticleService = blogArticleService;
            _logger = logger;
            _userService = userService;
            _tagService = tagService;
            _mapper = mapper;
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

        public async Task<IActionResult> ChangeVisible(Guid blogId)
        {
            try
            {
                if(await _blogArticleService.ChangeVisible(blogId))
                    this.AddToastrMessage("Thay đổi trạng thái thành công", Enums.ToastrMessageType.Success);
                else
                    this.AddToastrMessage("Thay đổi không thành công", Enums.ToastrMessageType.Error);
                return RedirectToAction(nameof(Index));
            }catch(Exception ex)
            {
                this.AddToastrMessage("Thay đổi không thành công", Enums.ToastrMessageType.Error);
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid blogId)
        {
            try
            {
                var user = await _userService.GetUser();
                if(user == null) {
                    this.AddToastrMessage("Vui lòng đăng nhập lại và thử lại", Enums.ToastrMessageType.Error);
                    return RedirectToAction(nameof(Create));
                }
                if(await _blogArticleService.Delete(blogId, user.Id))
                    this.AddToastrMessage("Thay đổi trạng thái thành công", Enums.ToastrMessageType.Success);
                else
                    this.AddToastrMessage("Thay đổi không thành công", Enums.ToastrMessageType.Error);
                return RedirectToAction(nameof(Index));
            }catch(Exception ex)
            {
                this.AddToastrMessage("Thay đổi không thành công", Enums.ToastrMessageType.Error);
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid blogId)
        {
            var blogArticle = (await _blogArticleService.GetByAsync(blogId, x => x.Include(t => t.Tags)));
            if(blogArticle == null)
            {
                this.AddToastrMessage("Không tìm thấy dữ liệu", Enums.ToastrMessageType.Error);
                return RedirectToAction(nameof(Create));
            }
            var model = _mapper.Map<BlogArticleDTO>(blogArticle);
            List<string>? tagList = JsonConvert.DeserializeObject<List<TagModel>>(model.HagTags!)?.Select(x => x.Value).ToList();
            if (tagList != null)
            {
                ViewData["TagList"] = string.Join(",", tagList);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogArticleDTO model, Guid blogId)
        {
            try
            {
                if (model.BlogId != blogId)
                {
                    this.AddToastrMessage("Đã có lỗi xảy ra, vui lòng thử lại", Enums.ToastrMessageType.Error);
                    return RedirectToAction(nameof(Index));
                }
                var user = await _userService.GetUser();
                if (user == null)
                {
                    this.AddToastrMessage("Vui lòng đăng nhập lại và thử lại", Enums.ToastrMessageType.Error);
                    return RedirectToAction(nameof(Index));
                }
                await _blogArticleService.Update(model);
                this.AddToastrMessage("Chỉnh sửa bài viết thành công", Enums.ToastrMessageType.Success);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                this.AddToastrMessage("Đã có lỗi xảy ra từ máy chủ", Enums.ToastrMessageType.Error);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
