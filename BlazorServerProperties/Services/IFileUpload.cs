using Microsoft.AspNetCore.Components.Forms;

namespace BlazorServerProperties.Services
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);
        bool DeleteFile(string fileName);
    }
}
