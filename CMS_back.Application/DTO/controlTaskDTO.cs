using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class controlTaskDTO
    {
        public string Description { get; set; }
        public List<string> UserTaskIds { get; set; }
    }
}
