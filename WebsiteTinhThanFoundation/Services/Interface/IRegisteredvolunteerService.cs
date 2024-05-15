using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface IRegisteredvolunteerService
    {
        public Task<int> CountAsync();
        public Task Add(Registeredvolunteers model);
        public Task<bool> AcceptContact(Guid Id);
        public Task<ICollection<Registeredvolunteers>> GetAllAsync();
        public Task<Registeredvolunteers?> GetByIdAsync(Guid? Id);
    }
}
