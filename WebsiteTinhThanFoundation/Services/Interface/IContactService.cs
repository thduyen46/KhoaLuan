using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface IContactService
    {
        public Task<int> CountAsync();
        public Task Add(Contact model);
        public Task<bool> AcceptContact(Guid Id);
        public Task<List<Contact>> GetAllAsync();
        public Task<Contact?> GetByIdAsync(Guid Id);
    }
}
