using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteTinhThanFoundation.AutoMapperProfiles.Contacts
{
    public class ContactDTO
    {
        [Display(Name = "Họ và tên", Order = 1)]
        public string FullName { get; set; }

        [Phone]
        [Display(Name = "Số điện thoại", Order = 2)]
        public string NumberPhone { get; set; }
        [Display(Name = "Tiêu đề", Order = 3)]
        public string Title { get; set; }

        [Display(Name = "Nội dung", Order = 4)]
        public string Content { get; set; }
    }
}