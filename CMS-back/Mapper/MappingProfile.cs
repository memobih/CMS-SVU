using AutoMapper;
using CMS_back.DTO;
using CMS_back.Models;

namespace CMS_back.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Faculity, FacultyResultDto>().ReverseMap();
            CreateMap<Control, ControlResultDto>().ReverseMap();
            CreateMap<Subject, subjectResultDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserResultDto>().ReverseMap();
            CreateMap<Control_Note, controlNoteDTO>().ReverseMap();
            CreateMap<Control_Note, ControlNotesResultDTO>().ReverseMap();
            CreateMap<Control_Task, controlTaskDTO>().ReverseMap();
            CreateMap<ControlUsers,UserWithHisControlDTO>().ReverseMap();  
            CreateMap<Faculity_Node, FacultyNodeDTO>().ReverseMap();
        }
    }
}
