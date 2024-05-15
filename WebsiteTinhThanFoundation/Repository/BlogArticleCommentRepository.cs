using Microsoft.AspNetCore.Identity;
using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.Repository.Interface;

namespace WebsiteTinhThanFoundation.Repository
{
    public class BlogArticleCommentRepository : GenericRepository<BlogArticleComment>, IBlogArticleCommentRepository
    {
        public BlogArticleCommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
