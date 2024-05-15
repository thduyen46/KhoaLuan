using Microsoft.EntityFrameworkCore.Query;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.UnitOfWork;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Services
{
    public class BlogArticleCommentService : IBlogArticleCommentService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public BlogArticleCommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<BlogArticleComment>> GetAllAsync(Func<IQueryable<BlogArticleComment>, IIncludableQueryable<BlogArticleComment, object>>? includes = null)
            => await _unitOfWork.BlogArticleCommentRepository.GetAllAsync(include: includes);

        public async Task<ICollection<BlogArticleComment>> GetByBlogArticleId(Guid BlogId, Func<IQueryable<BlogArticleComment>, IIncludableQueryable<BlogArticleComment, object>>? includes = null)
            => await _unitOfWork.BlogArticleCommentRepository.GetAllAsync(x => x.BlogArticleId == BlogId, include: includes);

        public  BlogArticleComment? GetById(Guid Id)
            => _unitOfWork.BlogArticleCommentRepository.Get(x => x.Id == Id);

        public async Task<BlogArticleComment?> GetByIdAsync(Guid Id, Func<IQueryable<BlogArticleComment>, IIncludableQueryable<BlogArticleComment, object>>? includes = null)
            => await _unitOfWork.BlogArticleCommentRepository.GetAsync(x => x.Id == Id, include: includes);

        public async Task Add(BlogArticleComment model, Guid BlogArticleId)
        {
            model.BlogArticleId = BlogArticleId;
            model.CreatedOn = DateTime.UtcNow.ToTimeZone();
            _unitOfWork.BlogArticleCommentRepository.Add(model);
            await _unitOfWork.CommitAsync();
        }
    }
}
