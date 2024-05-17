using AutoMapper;
using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.AutoMapperProfiles.BlogProfiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogArticleDTO, BlogArticle>().ForMember(x => x.Id, x => x.MapFrom(q => q.BlogId)).ReverseMap();
        }
    }
}
