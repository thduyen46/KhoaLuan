using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebsiteTinhThanFoundation.Models;

namespace WebsiteTinhThanFoundation.AutoMapperProfiles.Volunteers
{
    public class VolunteerProfile : Profile
    {
        public VolunteerProfile(){
            CreateMap<VolunteerDTO, Registeredvolunteers>().ReverseMap();
        }
    }
}