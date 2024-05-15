using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Repository.UnitOfWork;
using WebsiteTinhThanFoundation.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace WebsiteTinhThanFoundation.Services
{
    public class RoleService : IRoleService
    {
        public IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleService(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<ICollection<IdentityRole>> GetRoles()
            => await _unitOfWork.RoleRepository.GetAllAsync();

        public async Task<List<string>> GetUserRoles(ApplicationUser user)
            => new List<string>(await _userManager.GetRolesAsync(user));
    }
}
