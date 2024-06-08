using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinhThanFoundation.Models
{
    public class Contact : EntityBase
    {
        public string FullName { get; set; }
        [Phone]
        public string NumberPhone { get; set; }
        [Column("Jod")]
        public string Title { get; set; }
        [Column("Message")]
        public string Content { get; set; }
        public bool IsContacted { get; set; }
    }
}
