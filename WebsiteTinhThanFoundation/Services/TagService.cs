using Microsoft.EntityFrameworkCore.Query;
using System.Collections.ObjectModel;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.UnitOfWork;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Services
{
    public class TagService : ITagService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(BlogArticle entry, IEnumerable<string> tags)
        {
            var existingTags = await _unitOfWork.TagRepository.GetAllAsync();
            if (entry.Tags == null)
            {
                entry.Tags = new Collection<BlogArticleTag>();
            }

            foreach (var tag in entry.Tags.Where(t => !tags.Contains(t.Tag!.Name)).ToArray())
            {
                entry.Tags.Remove(tag);
            }

            foreach (var tag in tags.Where(t => !entry.Tags.Select(et => et.Tag!.Name).Contains(t)).ToArray())
            {
                var existingTag = existingTags.SingleOrDefault(t => t.Name.Equals(tag, StringComparison.OrdinalIgnoreCase));

                if (existingTag == null)
                {
                    existingTag = new Tag(tag);
                    existingTags.Add(existingTag);

                    _unitOfWork.TagRepository.Add(existingTag);
                }

                entry.Tags.Add(new BlogArticleTag()
                {
                    BlogArticleId = entry.Id,
                    TagId = existingTag.Id
                });
            }
        }

        public Task Delete(Guid? Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Tag>> GetAllAsync(Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>? includes = null)
            => await _unitOfWork.TagRepository.GetAllAsync(null, include: includes);

        public async Task<Tag?> GetByAsync(Guid? Id)
            => await _unitOfWork.TagRepository.GetAsync(x => x.Id == Id);

        public Tag? GetById(Guid? Id)
            => _unitOfWork.TagRepository.Get(x => x.Id == Id);

        public Task Update(Tag model)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Tag>> GetFeatureAsync(int? take, Func<IQueryable<Tag>, IQueryable<Tag>>? includes = null)
        {
            return await _unitOfWork.TagRepository.GetAllAsync(null, include: includes, orderBy: x => x.OrderBy(q => q.Id), take: take);
        }
    }
}
