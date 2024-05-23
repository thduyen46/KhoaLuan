using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _service;

        public ContactController(ILogger<ContactController> logger, IContactService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add(Contact model)
        {
            try{
                await _service.Add(model);
                this.AddToastrMessage("Liên hệ thành công", Enums.ToastrMessageType.Success);
                return RedirectToAction("Contact", "Home");
            }catch(Exception ex)
            {
                this.AddToastrMessage("Đã có lỗi xảy ra", Enums.ToastrMessageType.Error);
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Contact", "Home", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}