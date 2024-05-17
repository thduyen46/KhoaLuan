using Microsoft.AspNetCore.Mvc;

namespace WebsiteTinhThanFoundation.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
