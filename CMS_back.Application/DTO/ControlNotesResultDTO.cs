
namespace CMS_back.DTO
{
    public class ControlNotesResultDTO
    {
        public string Description { get; set; }
        public DateOnly? WriteDate { get; set; }
        public LeaderResultDto WriteBy { get; set; }
    }
}
