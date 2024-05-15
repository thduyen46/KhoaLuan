using Microsoft.AspNetCore.Identity;
using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Repository
{
    public class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
