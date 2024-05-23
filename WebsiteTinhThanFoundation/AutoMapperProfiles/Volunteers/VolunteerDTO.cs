using System.ComponentModel.DataAnnotations;

namespace WebsiteTinhThanFoundation.AutoMapperProfiles.Volunteers
{
    public class VolunteerDTO
    {
        [Display(Name = "Họ và tên", Order = 1)]
        public string FullName { get; set; }

        [EmailAddress]
        [Display(Name = "Email", Order = 2)]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Số điện thoại", Order = 3)]
        public string NumberPhone { get; set; }

        [Display(Name = "Địa chỉ", Order = 4)]
        public string Address { get; set; }
    }
}