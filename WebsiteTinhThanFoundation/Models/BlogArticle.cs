using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebsiteTinhThanFoundation.Data;

namespace WebsiteTinhThanFoundation.Models
{
    public class BlogArticle : EntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public bool Visible { get; set; } = true;
        public string BlogImage { get; set; }
        [JsonIgnore]
        [NotMapped]
        public IFormFile UploadFile { get; set; }
        public int Visits { get; set; } 
        public DateTime DatePost { get; set; }
        [Required(ErrorMessage = "Phải tạo url")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} dài {1} đến {2}")]
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "Chỉ dùng các ký tự [a-z0-9-]")]
        [Display(Name = "Url hiện thị")]
        public string Permalink { set; get; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? UserPost { get; set; }
        public DateTime DateUpdate { get; set; }
        public string? UserUpdateId { get; set; }
        [ForeignKey("UserUpdateId")]
        public ApplicationUser? UserUpdate { get; set; }
        public string? HagTags { get; set; }
        public bool IsDeleted {get; set;} = false;
        public string? UserRemove {get; set;}
        public virtual ICollection<BlogArticleTag> Tags { get; set; }
        public virtual ICollection<BlogArticleComment> BlogArticleComments { get; set; }
    }
}
