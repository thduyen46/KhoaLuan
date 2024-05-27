using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using WebsiteTinhThanFoundation.AutoMapperProfiles.BlogProfiles;
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
        private readonly IFirebaseStorageService _firebaseStorage;
        private readonly IMapper _mapper;
        public BlogArticleService(IUnitOfWork unitOfWork, IFirebaseStorageService firebaseStorageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _firebaseStorage = firebaseStorageService;
            _mapper = mapper;
        }
        public async Task Add(BlogArticle model, string userId)
        {
            model.UserId = userId;
            model.UserUpdateId = userId;
            model.CreatedOn = DateTime.UtcNow.ToTimeZone();
            model.DateUpdate = DateTime.UtcNow.ToTimeZone();
            model.BlogImage = "";
            if (model.UploadFile != null)
            {
                model.BlogImage = (await _firebaseStorage.UploadFile(model.UploadFile)).ToString();
            }
            model.Permalink = await GeneratePermalink(model.Title);
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

        private async Task<string> GeneratePermalink(string title)
        {
            string normalizedString = title.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(c);
                }
            }
            result.Replace(' ', '-');

            string permalink = Regex.Replace(result.ToString(), @"[^\w\s-]", "").ToLower();
            var isExist = (await _unitOfWork.BlogArticleRepository.GetAsync(x => x.Permalink.ToLower().Equals(permalink))) != null;
            if (isExist)
            {
                return permalink + Guid.NewGuid();
            }
            return permalink;
        }

        public async Task<bool> Delete(Guid? Id, string userId)
        {
            var blog = await GetByAsync(Id);
            if (blog == null)
                return false;
            blog.IsDeleted = true;
            blog.UserRemove = userId;
            _unitOfWork.BlogArticleRepository.Update(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> ChangeVisible(Guid? Id)
        {
            var blog = await GetByAsync(Id);
            if (blog == null)
                return false;
            blog.Visible = !blog.Visible;
            _unitOfWork.BlogArticleRepository.Update(blog);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<ICollection<BlogArticle>> GetAllAsync(string? keyword = null, string? tag = null, Func<IQueryable<BlogArticle>, IIncludableQueryable<BlogArticle, object>>? includes = null, bool? isActive = null)
        {
            ICollection<BlogArticle> model = new List<BlogArticle>();

            if (keyword != null && tag != null)
            {
                model = await _unitOfWork.BlogArticleRepository.GetAllAsync(x => x.Title.Contains(keyword) && !x.IsDeleted && (isActive != null ? x.Visible == isActive : true) && x.Tags.Any(q => q.Tag!.Name.Contains(tag)), x => x.Include(bt => bt.Tags).ThenInclude(t => t.Tag));
            }
            else if (keyword != null && tag == null)
            {
                model = await _unitOfWork.BlogArticleRepository.GetAllAsync(x => x.Title.Contains(keyword) && !x.IsDeleted && (isActive != null ? x.Visible == isActive : true), includes);
            }
            else if (keyword == null && tag != null)
            {
                model = await _unitOfWork.BlogArticleRepository.GetAllAsync(x => x.Tags.Any(q => q.Tag!.Name.Contains(tag)) && !x.IsDeleted && (isActive != null ? x.Visible == isActive : true), x => x.Include(bt => bt.Tags).ThenInclude(t => t.Tag));
            }
            else
            {
                model = await _unitOfWork.BlogArticleRepository.GetAllAsync(x => (isActive == null || x.Visible == isActive) && !x.IsDeleted, includes);
            }
            return model;
        }

        public async Task<BlogArticle?> GetByAsync(Guid? Id, Func<IQueryable<BlogArticle>, IIncludableQueryable<BlogArticle, object>>? includes = null)
            => await _unitOfWork.BlogArticleRepository.GetAsync(x => x.Id == Id, includes);

        public BlogArticle? GetById(Guid? Id)
            => _unitOfWork.BlogArticleRepository.Get(x => x.Id == Id);

        public async Task Update(BlogArticleDTO model)
        {
            var blogArticle = await _unitOfWork.BlogArticleRepository.GetAsync(x => x.Id == model.BlogId);
            if (blogArticle == null)
            {
                throw new Exception($"BlogID {model.BlogId} is not found.");
            }
            if (model.Title != blogArticle.Title)
            {
                blogArticle.Permalink = await GeneratePermalink(model.Title);
            }
            if (blogArticle.HagTags != model.HagTags)
            {
                List<string>? tagList = JsonConvert.DeserializeObject<List<TagModel>>(blogArticle.HagTags!)?.Select(x => x.Value).ToList();
                if (tagList != null && tagList.Count > 0)
                {
                    await AddTags(blogArticle, tagList);
                }
            }
            _mapper.Map(model, blogArticle);
            model.UploadFile = blogArticle.UploadFile;
            if (blogArticle.UploadFile != null)
            {
                blogArticle.BlogImage = (await _firebaseStorage.UploadFile(blogArticle.UploadFile)).ToString();
            }
            blogArticle.DateUpdate = DateTime.UtcNow.ToTimeZone();
            _unitOfWork.BlogArticleRepository.Update(blogArticle);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ICollection<BlogArticle>> GetFeatureAsync(int take = 5)
            => await _unitOfWork.BlogArticleRepository.GetAllAsync(orderBy: x => x.OrderBy(x => x.CreatedOn), take: take);

        public async Task<BlogArticle?> GetByPermalink(string? permalink, Func<IQueryable<BlogArticle>, IIncludableQueryable<BlogArticle, object>>? includes = null)
        {
            var model = await _unitOfWork.BlogArticleRepository.GetAsync(x => x.Permalink == permalink, includes);
            if (model != null)
            {
                model.Visits++;
                await _unitOfWork.CommitAsync();
            }
            return model;
        }
    }
}
