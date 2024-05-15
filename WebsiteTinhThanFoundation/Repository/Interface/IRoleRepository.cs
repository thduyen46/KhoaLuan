using Microsoft.AspNetCore.Identity;
using WebsiteTinhThanFoundation.Repository.GenericRepository;

namespace WebsiteTinhThanFoundation.Repository.Interface
{
    public interface IRoleRepository : IGenericRepository<IdentityRole>
    {
    }
}
