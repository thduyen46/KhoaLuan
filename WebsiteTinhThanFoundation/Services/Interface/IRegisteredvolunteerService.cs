using Microsoft.AspNetCore.Mvc;
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
        public Task<ICollection<Registeredvolunteers>> GetNotContactAsync();
        public Task<Registeredvolunteers?> GetByIdAsync(Guid? Id);
        public Task<MemoryStream> ExportData();
        Task<bool> DeleteAsync(int id);
    }
}
