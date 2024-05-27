using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface IDashboardService
    {
        public Task<DashboardView> GetDashboardView();
    }
}