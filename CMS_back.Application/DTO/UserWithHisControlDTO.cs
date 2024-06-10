using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class UserWithHisControlDTO
    {
        public string JobType { get; set; }
        public ControlResultDto control { get; set; }
    }
}
