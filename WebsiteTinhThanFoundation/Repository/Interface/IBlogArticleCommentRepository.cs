using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.GenericRepository;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Repository.Interface
{
    public interface IBlogArticleCommentRepository : IGenericRepository<BlogArticleComment>
    {
        public Task<List<BlogCommentView>> FeatherComment(int take);
    }
}
