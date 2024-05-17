using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Areas.Admin.Controllers
{
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
            var model = (await _registeredvolunteerService.GetAllAsync()) ;
            if(model == null)
            {
                model = new List<Registeredvolunteers>();
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
            await Task.Yield();
            var list = await _registeredvolunteerService.GetAllAsync();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true, OfficeOpenXml.Table.TableStyles.Light16);
                workSheet.Cells.AutoFitColumns();
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"RegisterVolunteer-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
