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
        public Guid Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}
