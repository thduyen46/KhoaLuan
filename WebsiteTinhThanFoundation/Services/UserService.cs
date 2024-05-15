using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Repository.UnitOfWork;
using WebsiteTinhThanFoundation.Services.Interface;
using WebsiteTinhThanFoundation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebsiteTinhThanFoundation.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser?> GetUser()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            return user;
        }

        public async Task<ICollection<ApplicationUser>> GetUsers()
            => await _unitOfWork.UserRepository.GetAllAsync();

        public async Task<ResponseListVM<UserInfoVM>> GetUsersWithRoles(int page = 1)
        {

            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            var userroles = await _unitOfWork.UserRoleRepository.GetAllAsync();
            var roles = await _roleManager.Roles.AsNoTracking().ToListAsync();
            int pagesize = 10;
            int totalUsers = users.Count;
            int maxpage = (totalUsers / pagesize) + (totalUsers % 10 == 0 ? 0 : 1);
            int pagenumber = page < 0 ? 1 : page;
            var userWithRoles = new List<UserInfoVM>();
            foreach (var user in users)
            {
                var userRoles = userroles.Where(x => x.UserId == user.Id).Select(x => x.RoleId);
                var matchingRoles = roles.Where(r => userRoles.Contains(r.Id)).Select(r => r.Name).ToList();
                userWithRoles.Add(new UserInfoVM(user, matchingRoles));
            }
            var data = new ResponseListVM<UserInfoVM>()
            {
                ObjectListData = userWithRoles.ToList(),
                MaxPage = maxpage
            };
            return data;
        }
        public async Task<ICollection<UserInfoVM>> GetUsersWithRoles()
        {

            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            var userroles = await _unitOfWork.UserRoleRepository.GetAllAsync();
            var roles = await _roleManager.Roles.AsNoTracking().ToListAsync();
            var userWithRoles = new List<UserInfoVM>();
            foreach (var user in users)
            {
                var userRoles = userroles.Where(x => x.UserId == user.Id).Select(x => x.RoleId);
                var matchingRoles = roles.Where(r => userRoles.Contains(r.Id)).Select(r => r.Name).ToList();
                userWithRoles.Add(new UserInfoVM(user, matchingRoles));
            }
            return userWithRoles.ToList();
        }
        public async Task<ApplicationUser?> GetUser(string userId)
            => await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);

        public async Task<int> CountAsync()
            => await _unitOfWork.UserRepository.CountAsync();
    }
}
