using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;

namespace WebsiteTinhThanFoundation.Repository.Interface
{
    public interface IBlogArticleRepository : IGenericRepository<BlogArticle>
    {
        public Task<ICollection<BlogArticle>> GetAll(int pageSize = 10, int? page = null);
            
    }
}
