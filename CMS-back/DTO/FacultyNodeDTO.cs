using CMS_back.Reposatory.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.DTO
{
    public class FacultyNodeDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Order { get; set; }
        [Required]
        public string? Level { get; set; }
    }
}
