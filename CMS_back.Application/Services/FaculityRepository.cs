using AutoMapper;
using CMS_back.Application.DTO;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.IGenericRepository;
using CMS_back.Interfaces;
using CMS_back.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CMS_back.Services
{
    public class FaculityRepository : IFaculityRepository
    {
        private readonly CMSContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Faculity> _genericRepository;
        private readonly IGenericRepository<ACAD_YEAR> _acadYearRepo;

        public FaculityRepository(CMSContext context, IMapper mapper, IGenericRepository<Faculity> genericRepository,IGenericRepository<ACAD_YEAR> acadYearRepo)
        {
            _context = context;
            _mapper = mapper;
            _acadYearRepo = acadYearRepo;
            _genericRepository = genericRepository;
        }

        public async Task<FacultyResultDto> GetByIdAsync(string id)
        {
            var faculity = await _genericRepository.FindFirstAsync(f => f.Id == id, "Controls");
            if (faculity == null) throw new Exception("Invalid Faculity ID is Found.");
            var facultyDto = _mapper.Map<FacultyResultDto>(faculity);
            return facultyDto;
        }

        public async Task<IEnumerable<FacultyResultDto>> GetAllAsync()
        {
            var faculities = await _genericRepository.FindAsync(f => true, ["Controls", "Controls.UserCreator"]);
            if (faculities == null) throw new Exception("No Faculities are Exist");
            var facultiesResult = faculities.Select(_mapper.Map<FacultyResultDto>).ToList();
            return facultiesResult;
        }

        public async Task<bool> AddAsync(FacultyDTO facultyDTO)
        {
            var isExist = await _genericRepository.FindFirstAsync(f => f.Name == facultyDTO.Name);
            if (isExist != null) throw new Exception("Faculty is Already Exist");
            Faculity faculity = new Faculity()
            {
                Name = facultyDTO.Name,
                Code = facultyDTO.Code,
                Order = facultyDTO.Order,
            };
            var leader = _context.Users.FirstOrDefault(u => u.Id == facultyDTO.UserLeaderID);
            if (leader == null) throw new Exception("Must Enter Leader Of Faculity");
            faculity.UserLeader = leader;
            faculity.UserLeaderID = leader.Id;

            _genericRepository.Add(faculity);

            leader.Type = ConstsRoles.AdminFaculty;
            leader.FaculityLeaderID = faculity.Id;
            leader.FaculityLeader = faculity;

            if (await _context.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<List<FacultyNodeDTO>> GetFaculityNode(string FacultyNodeId)
        {
            var nodes = _context.Faculity_Node.Where(fn => fn.FaculityID == FacultyNodeId).ToList();
            if (nodes == null) throw new Exception("FaculityNode is Not Found");
            var faculityNode = _mapper.Map<List<FacultyNodeDTO>>(nodes);
            return faculityNode;
        }

        public async Task<IEnumerable<AcadYearDTO>> GetAllAcadYearAsync()
        {
            var acadYears = await _acadYearRepo.GetAllAsync();
            if (acadYears == null) throw new Exception("No AcademicYears Exist");
            var acadYearResult = _mapper.Map<IEnumerable<AcadYearDTO>>(acadYears);
            return acadYearResult;
        }
    }
}