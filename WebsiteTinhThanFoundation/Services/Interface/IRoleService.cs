using WebsiteTinhThanFoundation.Data;
using Microsoft.AspNetCore.Identity;

namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface IRoleService
    {
        public Task<ICollection<IdentityRole>> GetRoles();
        public Task<List<string>> GetUserRoles(ApplicationUser user);
    }
}
