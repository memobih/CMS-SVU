using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Models
{
	public class Control_Note
	{
		[Key, MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Description { get; set; }

        //[Column(TypeName = "Date")]
        public DateOnly? WriteDate { get; set; }
		public string WriteByID { get; set; }
		public ApplicationUser WriteBy { get; set; }

		public string ControlID { get; set; }
		public Control Control { get; set; }

	}
}
