using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Areas.Admin.Controllers
{
    [Authorize]
    [Authorize(Policy = Constants.Policies.RequireAdmin)]
    [Area("admin")]
    public class VolunteerController : Controller
    {
        private readonly IRegisteredvolunteerService _registeredvolunteerService;
        private readonly ILogger<VolunteerController> _logger;
        public VolunteerController(IRegisteredvolunteerService registeredvolunteerService, ILogger<VolunteerController> logger)
        {
            _registeredvolunteerService = registeredvolunteerService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = (await _registeredvolunteerService.GetAllAsync());
            if (model == null)
            {
                model = new List<Registeredvolunteers>();
            }
            return View(model);
        }

        public async Task<IActionResult> AcceptVolunteer(Guid Id)
        {
            try
            {
                if (await _registeredvolunteerService.AcceptContact(Id))
                {
                    this.AddToastrMessage("Bạn vừa xác nhận đã liên lạc", Enums.ToastrMessageType.Success);
                    return RedirectToAction(nameof(Index));
                }
                this.AddToastrMessage("Xác nhận không thành công.", Enums.ToastrMessageType.Error);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Export(CancellationToken cancellationToken)
        {
            // query data from database  
            var stream = await _registeredvolunteerService.ExportData();
            string excelName = $"Tình nguyện viên-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
