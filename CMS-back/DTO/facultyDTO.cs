using CMS_back.Reposatory.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.DTO
{
    public class FacultyDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Order { get; set; }
        public FaculityType Type { get; set; }
        
        [ForeignKey("UserLeader")]
        public string UserLeaderID { get; set; }

    }
}
