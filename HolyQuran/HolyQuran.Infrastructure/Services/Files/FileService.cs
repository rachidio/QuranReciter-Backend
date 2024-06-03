using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;

namespace HolyQuran.Infrastructure.Services.Files
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveFile(IFormFile file, string folderName)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public string SaveFileWithoutAsync(IFormFile file, string folderName)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    file.CopyTo(fileStream);
            }
            return fileName;
        }
        //public async Task<bool> DefaultImageAsync(CreateAdviserDto dto)
        //{
        //    using var dataStream = new MemoryStream();
        //    using (var stream = System.IO.File.OpenRead(Constant.DefaultImage.path2))
        //    {
        //        Constant.DefaultImage.File = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
        //        dto.Image = Constant.DefaultImage.File;
        //        await dto.Image.CopyToAsync(dataStream);
        //        return true;
        //    }
        //}
    }
}