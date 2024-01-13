using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IFileService
    {
        void SaveImageToServer(IFormFile? file, out string fileName);
        Task<string> FileSaveToFtp(IFormFile? file);
        byte[]? ConvertFileToByteArray(IFormFile? file, out string fileName);
    }
}
