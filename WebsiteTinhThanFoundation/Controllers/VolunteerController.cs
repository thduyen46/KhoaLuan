using Microsoft.AspNetCore.Mvc;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IRegisteredvolunteerService _service;
        private readonly ILogger<VolunteerController> _logger;
        public VolunteerController(IRegisteredvolunteerService serivce)
        {
            _service = serivce;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Registeredvolunteers model)
        {
            try
            {
                await _service.Add(model);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View(model);
        }
    }
}
