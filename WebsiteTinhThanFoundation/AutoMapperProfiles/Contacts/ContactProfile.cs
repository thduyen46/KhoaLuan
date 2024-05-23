using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.AutoMapperProfiles.Contacts
{
    public class ContactProfile : Profile
    {
        public ContactProfile() { 
            CreateMap<ContactDTO, Contact>().ReverseMap();
        }
    }
}