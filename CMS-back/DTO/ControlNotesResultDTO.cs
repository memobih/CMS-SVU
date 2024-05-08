using CMS_back.Models;

namespace CMS_back.DTO
{
    public class ControlNotesResultDTO
    {
        public string Description { get; set; }
        public DateTime? WriteDate { get; set; }
        public UserResultDto WriteBy { get; set; }
    }
}
