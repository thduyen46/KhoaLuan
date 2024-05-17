using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinhThanFoundation.AutoMapperProfiles.BlogProfiles
{
    public class BlogArticleDTO 
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public string ImageUrl { get; set; }
        public string HagTags { get; set; }
        [NotMapped]
        public IFormFile UploadFile { get; set; }
    }
}
