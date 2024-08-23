using BlazorWasmBlogClient.Models;
using BlazorWasmBlogClient.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmBlogClient.Pages.Authentication
{
    public partial  class Register
    {
        private readonly UserRegister _userRegister = new UserRegister();
        public bool IsProcessing { get; set; } = false;
        public bool ShowErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        private async Task RegisterUser()
        {
            ShowErrors = false;
            IsProcessing = true;
            var result = await AuthService.RegisterUser(_userRegister);
            IsProcessing = false;
            if (result.CorrectRegister)
            {
                NavManager.NavigateTo("/login");
            }
            else
            {
                Errors = result.Errors;
                ShowErrors = true;
            }

        }
    }
}
