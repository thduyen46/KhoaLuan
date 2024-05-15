namespace WebsiteTinhThanFoundation.Models
{
    public class Tag : EntityBase
    {
        public Tag(
            string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public virtual ICollection<BlogArticleTag>? BlogArticles { get; set; }
    }
}
