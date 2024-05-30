using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface IContactService
    {
        public Task<int> CountAsync();
        public Task Add(Contact model);
        public Task<bool> AcceptContact(Guid Id);
        public Task<ICollection<Contact>> GetAllAsync();
        public Task<Contact?> GetByIdAsync(Guid Id);
        public Task<Dictionary<int, int>> GetRegistrationCountByMonthAsync();
        public Task<MemoryStream> ExportData();
        Task<bool> Delete(Guid contactId);
    }
}
