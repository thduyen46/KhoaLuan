using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Services;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Areas.Admin.Controllers
{
    [Authorize]
    [Authorize(Policy = Constants.Policies.RequireStaff)]
    [Area("admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _service;
        private readonly ILogger<ContactController> _logger;
        public ContactController(IContactService service, ILogger<ContactController> logger)
        {
            _service = service;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> AcceptVolunteer(Guid Id)
        {
            try
            {
                if (await _service.AcceptContact(Id))
                {
                    this.AddToastrMessage("Bạn vừa xác nhận đã liên lạc", Enums.ToastrMessageType.Success);
                    return RedirectToAction(nameof(Index));
                }
                this.AddToastrMessage("Xác nhận không thành công.", Enums.ToastrMessageType.Error);
                return RedirectToAction(nameof(Index));
            }
            catch
            (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Export(CancellationToken cancellationToken)
        {
            // query data from database  
            var stream = await _service.ExportData();
            string excelName = $"Người liên lạc-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
