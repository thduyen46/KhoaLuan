using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;

namespace WebsiteTinhThanFoundation.Repository.Interface
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        public Task<Dictionary<int, int>> GetRegistrationCountByMonthAsync();
    }
}
