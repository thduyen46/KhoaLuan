using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTinhThanFoundation.Repository.UnitOfWork;
using WebsiteTinhThanFoundation.Services.Interface;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DashboardView> GetDashboardView()
        {
            DashboardView model = new();
            var contactMonthly = await _unitOfWork.ContactRepository.GetRegistrationCountByMonthAsync();
            if(contactMonthly != null)
            {
                model.NumberContactMonthly = contactMonthly.OrderBy(kv => kv.Key)
                                           .Select(kv => kv.Value)
                                           .ToList();
            }
            model.NumberContact = await _unitOfWork.ContactRepository.CountAsync();
            model.NumberVolunteer = await _unitOfWork.RegisteredVolunteerRepository.CountAsync();
            model.BlogCount = await _unitOfWork.BlogArticleRepository.CountAsync();

           model.CommentViews = await _unitOfWork.BlogArticleCommentRepository.FeatherComment(4);
            return model;
        }
    }
}