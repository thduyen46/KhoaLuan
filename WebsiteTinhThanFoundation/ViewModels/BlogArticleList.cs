using WebsiteTinhThanFoundation.Models;
using X.PagedList;

namespace WebsiteTinhThanFoundation.ViewModels
{
    public class BlogArticleList
    {
        public PagedList<BlogArticle> BlogArticles { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<BlogArticle> BlogFeature { get; set; }
    }
}
