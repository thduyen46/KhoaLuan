using Microsoft.EntityFrameworkCore.Internal;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.UnitOfWork;
using WebsiteTinhThanFoundation.Services.Interface;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Services
{
    public class RegisteredvolunteerService : IRegisteredvolunteerService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public RegisteredvolunteerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AcceptContact(Guid Id)
        {
            var model = await _unitOfWork.RegisteredVolunteerRepository.GetAsync(x => x.Id == Id);
            if(model != null)
            {
                model.IsContacted = true;
                _unitOfWork.RegisteredVolunteerRepository.Update(model);
                await _unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task Add(Registeredvolunteers model)
        {
            model.CreatedOn = DateTime.UtcNow.ToTimeZone();
            _unitOfWork.RegisteredVolunteerRepository.Add(model);
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> CountAsync()
            => await _unitOfWork.RegisteredVolunteerRepository.CountAsync();

        public async Task<ICollection<Registeredvolunteers>> GetAllAsync()
            => (await _unitOfWork.RegisteredVolunteerRepository.GetAllAsync());

        public async Task<Registeredvolunteers?> GetByIdAsync(Guid? Id)
            => await _unitOfWork.RegisteredVolunteerRepository.GetAsync(x => x.Id == Id);
    }
}
