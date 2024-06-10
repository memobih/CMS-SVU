using CMS_back.Models;

namespace CMS_back.DTO
{
    public class ControlTaskResultDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public Question IsDone { get; set; }
        public DateOnly CreationDate { get; set; }
        public ICollection<UserResultForTaskDdto> Users { get; set; }
    }
}
