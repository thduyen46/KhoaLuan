using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Enums;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Services.Interface;
using WebsiteTinhThanFoundation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GtaWebsiteTinhThanFoundationV.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(IUserService userService, ILogger<AccountController> logger, IRoleService roleService,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _logger = logger;
            _roleService = roleService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize]
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _userService.GetUsersWithRoles();
                this.AddToastrMessage("Tải dữ liệu thành công", ToastrMessageType.Success);
                return View(users);
            }
            catch (Exception ex)
            {
                this.AddToastrMessage("Đã có lỗi xayr ra", ToastrMessageType.Error);
                _logger.LogError(ex.Message.ToString());
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userService.GetUser(userId);
            var roles = await _roleService.GetRoles();

            if (user == null)
                return NotFound();

            var vm = new EditUserViewModel
            {
                User = user,
                Roles = new List<SelectListItem>()
            };
            foreach (var role in roles)
            {
                SelectListItem item = new()
                {
                    Text = role.Name,
                    Value = role.Id.ToString(),
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
                vm.Roles.Add(item);
            }
            return View(vm);
        }

        [Authorize]
        [Authorize(Roles = Constants.Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel data)
        {
            var user = await _userService.GetUser(data.User!.Id);
            try
            {
                if (user == null)
                {
                    return NotFound();
                }

                var roles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Không thể xóa vai trò hiện tại của người dùng");
                    return View(data);
                }
                result = await _userManager.AddToRolesAsync(user, data.Roles!.Where(x => x.Selected).Select(y => y.Text));
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Không thể thêm vai trò đã chọn cho người dùng");
                    return View(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("Edit", new { id = user!.Id });
        }

        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            TempData["ReturnUrl"] = returnUrl;
            UserInfoVM model = new();
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserInfoVM model)
        {
            try
            {
                var returnUrl = TempData["ReturnUrl"]?.ToString() ?? Url.Content("~/");
                model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, $"Vui lòng điền đủ thông tin.");
                    Console.WriteLine("Lỗi");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Người dùng đã đăng nhập.");
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi trong quá trình đăng nhập.");
                _logger.LogError(ex.Message.ToString());
            }
            return View(model);
        }
    }
}
