using System.Web;
using BlazorWasmBlogClient.Models;
using BlazorWasmBlogClient.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmBlogClient.Pages.Authentication
{
    public partial class Login
    {
        private readonly UserLogin _userLogin = new UserLogin();
        public bool IsProcessing { get; set; } = false;
        public bool ShowErrors { get; set; }
        public string Error { get; set; }

        public string UrlReturn { get; set; }

        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        private async Task LogUser()
        {
            ShowErrors = false;
            IsProcessing = true;
            var result = await AuthService.Login(_userLogin);
            IsProcessing = false;
            if (result.IsSuccess)
            {
                var urlAbs = new Uri(NavManager.Uri);
                var queryParams = HttpUtility.ParseQueryString(urlAbs.ToString());
                UrlReturn = queryParams["returnUrl"];
                if (string.IsNullOrEmpty(UrlReturn))
                {
                    NavManager.NavigateTo("/");
                }
                else
                {
                    NavManager.NavigateTo("/" + UrlReturn);
                }
            }
            else
            {
                ShowErrors = true;
                Error = "Username and/or password incorrect";
                NavManager.NavigateTo("/login");
            }


        }
    }
}
