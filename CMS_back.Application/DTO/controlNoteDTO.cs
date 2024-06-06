using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class controlNoteDTO
    {
        [Required]
        public string Description { get; set; }
    }
}
