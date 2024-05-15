using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTinhThanFoundation.Models
{
    public class BlogArticleTag : EntityBase
    {
        public Guid? BlogArticleId { get; set; }
        [ForeignKey(nameof(BlogArticleId))]
        public virtual BlogArticle? BlogArticle { get; set; }
        public Guid? TagId { get; set; }

        public virtual Tag? Tag { get; set; }
    }
}
