using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWasmBlogClient.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [Inject]
        private NavigationManager NavManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthProviderState { get; set; }

        bool NotAuthorized { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthProviderState;

            if (authState.User == null)
            {
                var returnUrl = NavManager.ToBaseRelativePath(NavManager.Uri);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    NavManager.NavigateTo("Login", true);
                }
                else
                {
                    NavManager.NavigateTo($"Login?returnUrl={returnUrl}", true);
                }
            }
            else
            {
                NotAuthorized = true;
            }
        }
    }
    
}