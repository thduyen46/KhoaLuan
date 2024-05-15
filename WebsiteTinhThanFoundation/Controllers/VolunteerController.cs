using Microsoft.AspNetCore.Mvc;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IRegisteredVolunteerRepository _registeredVolunteerRepository;
        private readonly ILogger<VolunteerController> _logger;  
        public VolunteerController(IRegisteredVolunteerRepository registeredVolunteerRepository)
        {
            _registeredVolunteerRepository = registeredVolunteerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Registeredvolunteers model)
        {
            try
            {
                await _registeredVolunteerRepository.AddAsync(model);
                return RedirectToAction("Index", "Home");
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View(model);
        }
    }
}
