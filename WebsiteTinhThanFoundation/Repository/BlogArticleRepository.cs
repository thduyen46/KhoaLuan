using Microsoft.EntityFrameworkCore;
using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Repository
{
    public class BlogArticleRepository : GenericRepository<BlogArticle>, IBlogArticleRepository
    {
        public BlogArticleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<BlogArticle>> GetAll(int pageSize = 10, int? page = null)
        {
            var pageNumber = page ?? 1;
            var model = await _dbContext.BlogArticles.OrderBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return model;
        }
    }
}
