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
            CreateMap<Control, ControlDTO>().ReverseMap();
            CreateMap<Subject, subjectResultDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserResultDto>().ReverseMap();
            CreateMap<ApplicationUser, UserResultForTaskDdto>().ReverseMap();
            CreateMap<Control_Note, controlNoteDTO>().ReverseMap();
            CreateMap<Control_Note, ControlNotesResultDTO>().ReverseMap();
            CreateMap<Control_Task, controlTaskDTO>().ReverseMap();
            CreateMap<Control_Task, ControlTaskResultDTO>().ForMember
                (dest => dest.Users, opt => opt.MapFrom(src =>src.UserTasks
                        .Where(ut => ut.UserTask != null && ut.Control_TaskID == src.Id)
                        .Select(ut => ut.UserTask)));
            CreateMap<ControlUsers, UserWithHisControlDTO>().ReverseMap();
            CreateMap<Faculity_Node, FacultyNodeDTO>().ReverseMap();
            CreateMap<ControlUsers, ControlUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, RegisterUserDto>().ReverseMap();
        }
    }
}
