using Microsoft.EntityFrameworkCore.Query;
using WebsiteTinhThanFoundation.AutoMapperProfiles.BlogProfiles;
using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface IBlogArticleService
    {
        public Task<BlogArticle?> GetByAsync(Guid? Id, Func<IQueryable<BlogArticle>, IIncludableQueryable<BlogArticle, object>>? includes = null);
        public BlogArticle? GetById(Guid? Id);
        public Task<BlogArticle?> GetByPermalink(string? permalink, Func<IQueryable<BlogArticle>, IIncludableQueryable<BlogArticle, object>>? includes = null);
        public Task<ICollection<BlogArticle>> GetAllAsync(string? keyword = null, string? tag = null, Func<IQueryable<BlogArticle>, IIncludableQueryable<BlogArticle, object>>? includes = null);
        public Task<ICollection<BlogArticle>> GetFeatureAsync(int take = 5);
        public Task Add(BlogArticle model, string userId);
        public Task Update(BlogArticleDTO model);
        public Task Delete(Guid? Id);

    }
}
