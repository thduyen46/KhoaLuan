using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface IUserService
    {
        public Task<ICollection<ApplicationUser>> GetUsers();
        public Task<int> CountAsync();
        public Task<ResponseListVM<UserInfoVM>> GetUsersWithRoles(int page = 1);
        public Task<ICollection<UserInfoVM>> GetUsersWithRoles();
        public Task<ApplicationUser?> GetUser(string userId);
        public Task<ApplicationUser?> GetUser();
    }
}
