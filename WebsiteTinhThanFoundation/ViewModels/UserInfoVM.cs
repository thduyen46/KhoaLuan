using WebsiteTinhThanFoundation.Data;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace WebsiteTinhThanFoundation.ViewModels
{
    public class UserInfoVM
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
        public virtual ICollection<string>? Roles { get; set; }
        public UserInfoVM()
        {
        }
        public UserInfoVM(ApplicationUser user)
        {
            this.FullName = user.FullName;
            this.UserId = user.Id;
        }

        public UserInfoVM(ApplicationUser user, List<string> roles)
        {
            this.FullName = user.FullName;
            this.UserId = user.Id;
            this.Roles = roles;
        }
    }
}
