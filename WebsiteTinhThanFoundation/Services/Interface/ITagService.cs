using Microsoft.EntityFrameworkCore.Query;
using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.Services.Interface
{
    public interface ITagService
    {
        public Task<Tag?> GetByAsync(Guid? Id);
        public Tag? GetById(Guid? Id);
        public Task<ICollection<Tag>> GetAllAsync(Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>? includes = null);
        public Task<ICollection<Tag>> GetFeatureAsync(int? take, Func<IQueryable<Tag>, IQueryable<Tag>>? includes = null);
        public Task Add(BlogArticle entry, IEnumerable<string> tags);
        public Task Update(Tag model);
        public Task Delete(Guid? Id);
    }
}
