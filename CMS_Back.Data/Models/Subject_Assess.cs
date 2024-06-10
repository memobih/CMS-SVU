using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
    public class Subject_Assess
    {
        //[Key]
        //public string Id { get; set; } = Guid.NewGuid().ToString();

        public string SubjectID { get; set; }
        public Subject Subject { get; set; }
        public string AssessID { get; set; }
        public Assess Assess { get; set; }
        public double? MAX_Degree { get; set; }
        public double? MIN_Degree { get; set; }

    }
}
