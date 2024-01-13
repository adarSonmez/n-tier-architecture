using Business.Abstract;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class FileManager : IFileService
    {
        public void SaveImage(IFormFile? image, out string fileName)
        {
            if (image == null)
            {
                fileName = "default.png";
                return;
            }

            var fileExtension = Path.GetExtension(image.FileName);
            fileName = Guid.NewGuid().ToString() + fileExtension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using var stream = new FileStream(path, FileMode.Create);
            image.CopyTo(stream);
        }
    }
}
