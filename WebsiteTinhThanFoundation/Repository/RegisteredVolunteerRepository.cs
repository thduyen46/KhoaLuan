using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Repository
{
    public class RegisteredVolunteerRepository : GenericRepository<Registeredvolunteers>, IRegisteredVolunteerRepository
    {
        public RegisteredVolunteerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
