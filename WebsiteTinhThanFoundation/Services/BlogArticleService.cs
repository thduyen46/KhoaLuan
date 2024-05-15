using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.UnitOfWork;
using WebsiteTinhThanFoundation.Services.Interface;
using WebsiteTinhThanFoundation.ViewModels;

namespace WebsiteTinhThanFoundation.Services
{
    public class BlogArticleService : IBlogArticleService
    {
        public IUnitOfWork _unitOfWork;
        public BlogArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(BlogArticle model, string userId)
        {
            model.UserId = userId;
            model.UserUpdateId = userId;
            model.CreatedOn = DateTime.UtcNow.ToTimeZone();
            model.DateUpdate = DateTime.UtcNow.ToTimeZone();
            model.BlogImage = "test";
            model.Permalink = GeneratePermalink(model.Title) + Guid.NewGuid();
            List<string>? tagList = JsonConvert.DeserializeObject<List<TagModel>>(model.HagTags!)?.Select(x => x.Value).ToList();
            _unitOfWork.BlogArticleRepository.Add(model);
            if (tagList != null && tagList.Count > 0)
            {
                await AddTags(model, tagList);
            }
            await _unitOfWork.CommitAsync();
        }

        private async Task AddTags(BlogArticle entry, IEnumerable<string> tags)
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

        private string GeneratePermalink(string title)
        {
            string permalink = Regex.Replace(title, "[^a-zA-Z0-9-]", " ").ToLower();
            permalink = Regex.Replace(permalink, "\\s+", "-").Trim();
            return permalink;
        }

        public Task Delete(Guid? Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<BlogArticle>> GetAllAsync(string? keyword = null, string? tag = null, Func<IQueryable<BlogArticle>, IIncludableQueryable<BlogArticle, object>>? includes = null)
        {
            ICollection<BlogArticle> model = new List<BlogArticle>();  
                
            if(keyword != null && tag != null)
            {
                model = await _unitOfWork.BlogArticleRepository.GetAllAsync(x => x.Title.Contains(keyword) && x.Tags.Any(q => q.Tag!.Name.Contains(tag)), x => x.Include(bt => bt.Tags).ThenInclude(t => t.Tag));
            }
            else if(keyword != null && tag == null)
            {
                model = await _unitOfWork.BlogArticleRepository.GetAllAsync(x => x.Title.Contains(keyword), includes);
            }
            else if(keyword == null && tag != null)
            {
                model = await _unitOfWork.BlogArticleRepository.GetAllAsync(x => x.Tags.Any(q => q.Tag!.Name.Contains(tag)), x => x.Include(bt => bt.Tags).ThenInclude(t => t.Tag));
            }
            else
            {
                model = await _unitOfWork.BlogArticleRepository.GetAllAsync(null, includes);
            }
            return model;
        }

        public async Task<BlogArticle?> GetByAsync(Guid? Id)
            => await _unitOfWork.BlogArticleRepository.GetAsync(x => x.Id == Id);

        public BlogArticle? GetById(Guid? Id)
            => _unitOfWork.BlogArticleRepository.Get(x => x.Id == Id);

        public Task Update(BlogArticle model)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<BlogArticle>> GetFeatureAsync(int take = 5)
            => await _unitOfWork.BlogArticleRepository.GetAllAsync(orderBy: x => x.OrderBy(x => x.CreatedOn), take: take);

        public async Task<BlogArticle?> GetByPermalink(string? permalink, Func<IQueryable<BlogArticle>, IIncludableQueryable<BlogArticle, object>>? includes = null)
            => await _unitOfWork.BlogArticleRepository.GetAsync(x => x.Permalink == permalink, includes);
    }
}
