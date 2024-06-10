using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.DTO
{
    public class FacultyNodeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Order { get; set; }

        public string Level { get; set; }
    }
}
