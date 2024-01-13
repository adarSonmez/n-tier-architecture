using Microsoft.AspNetCore.Http;

namespace Business.Utilities.File
{
    public interface IFileService
    {
        void SaveImageToServer(IFormFile? file, out string fileName);
        Task<string> FileSaveToFtp(IFormFile? file);
        byte[]? ConvertFileToByteArray(IFormFile? file, out string fileName);
    }
}
