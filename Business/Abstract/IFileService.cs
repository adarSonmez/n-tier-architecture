using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IFileService
    {
        void SaveImage(IFormFile? file, out string fileName);
    }
}
