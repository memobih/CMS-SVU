namespace CMS_back.DTO
{
    public class FacultyResultDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Order { get; set; }
        public string? FaculityTypeID { get; set; }
        
        public List<ControlResultDto>? controls { get; set; }
    }
}
