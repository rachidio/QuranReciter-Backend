using Microsoft.AspNetCore.Http;
 
namespace HolyQuran.Infrastructure.Services.Files
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string folderName);
        string SaveFileWithoutAsync(IFormFile file, string folderName);
       // Task<bool> DefaultImageAsync(CreateAdviserDto dto);
    }
}
