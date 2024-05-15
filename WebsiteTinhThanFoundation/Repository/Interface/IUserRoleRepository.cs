using WebsiteTinhThanFoundation.Repository.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace WebsiteTinhThanFoundation.Repository.Interface
{
    public interface IUserRoleRepository : IGenericRepository<IdentityUserRole<string>>
    {
    }
}
