using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Repository
{
    public class BlogArticleTagRepository : GenericRepository<BlogArticleTag>, IBlogArticleTagRepository
    {
        public BlogArticleTagRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
