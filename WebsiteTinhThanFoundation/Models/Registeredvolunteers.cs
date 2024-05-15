using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinhThanFoundation.Models
{
    public class Registeredvolunteers : EntityBase
    {
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string NumberPhone { get; set; }
        public string Addreass { get; set; }
        public bool IsContacted { get; set; }
    }
}
