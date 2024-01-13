using Business.Abstract;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Business.Concrete
{
    public class FileManager : IFileService
    {
        public void SaveImageToServer(IFormFile? image, out string fileName)
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

        public async Task<string> FileSaveToFtp(IFormFile? file)
        {
            var fileName = "default.png";

            if (file != null)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var ftpUrl = "ftp://yourserver.com";
                var ftpFolderPath = "/Uploads/";

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("username:password")));

                using var fileStream = file.OpenReadStream();
                var content = new StreamContent(fileStream);
                var response = await client.PutAsync(ftpUrl + ftpFolderPath + fileName, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error uploading file: {response.StatusCode}");
                }
            }

            return fileName;
        }

        // To be stored in database but not recommended
        public byte[]? ConvertFileToByteArray(IFormFile? file, out string fileName)
        {
            if (file == null)
            {
                fileName = "default.png";
                return null;
            }

            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();
            fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string fileString = Convert.ToBase64String(bytes);
            return bytes;
        }
    }
}
