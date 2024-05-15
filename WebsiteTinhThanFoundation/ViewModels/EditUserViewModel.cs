using WebsiteTinhThanFoundation.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebsiteTinhThanFoundation.ViewModels
{
    public class EditUserViewModel
    {
        public ApplicationUser? User { get; set; }
        public IList<SelectListItem>? Roles { get; set; }
    }
}
