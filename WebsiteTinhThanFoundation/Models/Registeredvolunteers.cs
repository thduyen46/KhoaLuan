using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinhThanFoundation.Models
{
    public class Registeredvolunteers : EntityBase
    {
        [Display(Name = "Họ và tên", Order = 3)]
        public string FullName { get; set; }

        [EmailAddress]
        [Display(Name = "Email", Order = 4)]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Số điện thoại", Order = 5)]
        public string NumberPhone { get; set; }

        [Display(Name = "Địa chỉ", Order = 6)]
        public string Addreass { get; set; }

        [Display(Name = "Trạng thái (1: Đã liên lạc, 0: Chưa liên lạc)", Order = 7)]
        public bool IsContacted { get; set; }
    }
}
