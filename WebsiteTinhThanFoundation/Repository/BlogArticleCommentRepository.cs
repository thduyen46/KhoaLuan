using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.Repository.Interface;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Repository
{
    public class BlogArticleCommentRepository : GenericRepository<BlogArticleComment>, IBlogArticleCommentRepository
    {
        public BlogArticleCommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<BlogCommentView>> FeatherComment(int take)
        {
            var model = await _dbContext.BlogArticleComments.OrderByDescending(x => x.CreatedOn).Include(x => x.BlogArticle).Select(x => new BlogCommentView(){
                Comment = x.Comment,
                FullName = x.FullName,
                UrlLinkBlog = x.BlogArticle.Permalink
            }).Take(take).ToListAsync();
            return model ?? new();
        }
    }
}
