using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.ViewModels
{
    public class BlogDetailVM
    {
        public ICollection<BlogArticle> BlogFeature { get; set; }
        public BlogArticle BlogDetail { get; set; }
    }
}
