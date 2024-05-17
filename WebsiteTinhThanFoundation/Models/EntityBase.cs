using System.ComponentModel.DataAnnotations;
using WebsiteTinhThanFoundation.Helpers;

namespace WebsiteTinhThanFoundation.Models
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow.ToTimeZone();
        }

        [Key]
        [Display(Name = "Code", Order = 1)]
        public Guid Id { get; set; }
        [Display(Name = "Ngày đăng ký", Order = 2)]
        public DateTimeOffset CreatedOn { get; set; }
    }
}
