using CMS_back.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class controlSubjectResultDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }

        public string? Code { get; set; }

        public int? Credit_Hours { get; set; }
        public Question? IsDone { get; set; }
        public Question? IsReview { get; set; }
    }
}
