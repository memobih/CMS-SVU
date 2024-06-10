using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class subjectDTO
    {
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public int Credit_Hours { get; set; }

        public string FaculityNodeID { get; set; }

    }
}
