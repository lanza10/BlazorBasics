using Microsoft.AspNetCore.Components.Forms;

namespace BlazorServerProperties.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public FileUpload(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }
        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            { 
                var fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid() + fileInfo.Extension;
                var folderDirectory = $"{_environment.WebRootPath}\\propertiesImages";
                var path = Path.Combine(_environment.WebRootPath, "propertiesImages", fileName);
                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                memoryStream.WriteTo(fs);

                var url = $"{_configuration.GetValue<string>("ServerUrl")}";
                var fullPath = $"{url}propertiesImages/{fileName}";
                return fullPath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var path = $"{_environment.WebRootPath}\\propertiesImages\\{fileName}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
