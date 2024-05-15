using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.Repository.Interface;
using Microsoft.AspNetCore.Identity;

namespace WebsiteTinhThanFoundation.Repository
{
    public class UserRoleRepository : GenericRepository<IdentityUserRole<string>>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
