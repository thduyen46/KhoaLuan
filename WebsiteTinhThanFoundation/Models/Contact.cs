using System.ComponentModel.DataAnnotations;

namespace WebsiteTinhThanFoundation.Models
{
    public class Contact : EntityBase
    {
        public string FullName { get; set; }
        [Phone]
        public string NumberPhone { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
