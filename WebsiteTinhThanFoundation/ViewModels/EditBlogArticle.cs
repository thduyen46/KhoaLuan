using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.ViewModels
{
    public class EditBlogCommand 
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile UploadFile { get; set; }
        public virtual ICollection<BlogArticleTag> Tags { get; set; }
    }
}
