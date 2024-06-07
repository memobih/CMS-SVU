using CMS_back.Models;
using Microsoft.AspNetCore.Http;

namespace CMS_back.Application.Helpers
{
    public interface IUserHelpers
    {
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<string> AddFileAsync(IFormFile file, string folderName);
        Task<bool> DeleteFileAsync(string imagePath, string folderName);
    }
}
