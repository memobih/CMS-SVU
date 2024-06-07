namespace CMS_back.Application.Interfaces
{
    public interface IUserResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? UserImage { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? FaculityLeaderID { get; set; }
        public string? FaculityEmployeeID { get; set; }
        public string? FaculityName { get; set; }
    }
}
