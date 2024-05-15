using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebsiteTinhThanFoundation.Enums
{
    public enum Gender
    {
        [Display(Name = "Nam")]
        Male = 0,
        [Display(Name = "Nữ")]
        Female = 1,
    }
}
