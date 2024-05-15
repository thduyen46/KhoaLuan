using Microsoft.AspNetCore.Identity;

namespace WebsiteTinhThanFoundation.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
