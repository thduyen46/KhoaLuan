using Microsoft.EntityFrameworkCore.Query;
using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface IBlogArticleCommentService
    {
        public Task<BlogArticleComment?> GetByIdAsync(Guid Id, Func<IQueryable<BlogArticleComment>, IIncludableQueryable<BlogArticleComment, object>>? includes = null);
        public BlogArticleComment? GetById(Guid Id);
        public Task<ICollection<BlogArticleComment>> GetAllAsync(Func<IQueryable<BlogArticleComment>, IIncludableQueryable<BlogArticleComment, object>>? includes = null);
        public Task<ICollection<BlogArticleComment>> GetByBlogArticleId(Guid BlogId, Func<IQueryable<BlogArticleComment>, IIncludableQueryable<BlogArticleComment, object>>? includes = null); 
        public Task Add(BlogArticleComment model, Guid BlogArticleId);

    }
}
