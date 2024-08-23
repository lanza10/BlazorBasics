using BlazorWasmBlogClient.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmBlogClient.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthService.Exit();
            NavManager.NavigateTo("/");
        }

    }
}
