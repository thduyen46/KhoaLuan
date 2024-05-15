using Microsoft.AspNetCore.Mvc;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Areas.Admin.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IRegisteredvolunteerService _registeredvolunteerService;
        private readonly ILogger<VolunteerController> _logger;

        public async Task<IActionResult> Index()
        {
            var model = await _registeredvolunteerService.GetAllAsync();
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        public async Task<IActionResult> AcceptVolunteer(Guid Id)
        {
            try
            {
                if(await _registeredvolunteerService.AcceptContact(Id))
                {
                    this.AddToastrMessage("Bạn vừa xác nhận đã liên lạc", Enums.ToastrMessageType.Success);
                    return View(nameof(Index));
                }
                this.AddToastrMessage("Xác nhận không thành công.", Enums.ToastrMessageType.Error);
                return View(nameof(Index));
            }
            catch
            (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();
        }
    }
}
