using AutoMapper;
using CMS_back.DTO;
using CMS_back.Models;

namespace CMS_back.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Faculity, FacultyResultDto>();
            CreateMap<Control, ControlResultDto>();
            CreateMap<Subject, subjectResultDTO>();
            CreateMap<ApplicationUser, UserResultDto>();
        }
    }
}
