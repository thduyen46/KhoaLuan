using WebsiteTinhThanFoundation.Controllers;
using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebsiteTinhThanFoundation.Areas.Admin.Controllers
{
    [Authorize]
    [Authorize(Policy = Constants.Policies.RequireVolunteer)]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardService _service;
        public HomeController(ILogger<HomeController> logger, IDashboardService service)
        {
            _service = service;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetDashboardView();
            return View(model);
        }
    }
}
