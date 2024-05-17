using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Repository
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
