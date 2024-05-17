using AutoMapper;
using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.AutoMapperProfiles.TagProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile() { 
            CreateMap<TagDTO, Tag>().ForMember(x => x.Id, x => x.MapFrom(q => q.TagId))
                .ForMember(x => x.Name, x => x.MapFrom(q => q.TagName)).ReverseMap();
        }

    }
}
