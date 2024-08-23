using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using Blazored.LocalStorage;
using BlazorWasmBlogClient.Helpers;
using BlazorWasmBlogClient.Models;
using BlazorWasmBlogClient.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorWasmBlogClient.Services
{
    public class AuthService(
        HttpClient client,
        ILocalStorageService localStorageService,
        AuthenticationStateProvider authStateProv
        ) : IAuthService
    {
        public async Task<RegisterResponse> RegisterUser(UserRegister userRegister)
        {
            var content = JsonConvert.SerializeObject(userRegister);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{Init.BaseUrlApi}api/users/register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegisterResponse>(contentTemp);
            return response.IsSuccessStatusCode ? new RegisterResponse { CorrectRegister = true } : result;
        }

        public async Task<LoginResponse> Login(UserLogin userLogin)
        {
            var content = JsonConvert.SerializeObject(userLogin);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{Init.BaseUrlApi}api/users/login",bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = (JObject)JsonConvert.DeserializeObject(contentTemp);

            if (!response.IsSuccessStatusCode)
            {
                return new LoginResponse { IsSuccess = false };
            }

            var token = result["result"]["token"].Value<string>();
            var user = result["result"]["user"]["username"].Value<string>();
            await localStorageService.SetItemAsync(Init.LocalToken, token);
            await localStorageService.SetItemAsync(Init.LocalUserData, user);
            ((AuthStateProvider)authStateProv).NotifyUserLogged(token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new LoginResponse { IsSuccess = true };
        }

        public async Task Exit()
        {
            await localStorageService.RemoveItemAsync(Init.LocalToken);
            await localStorageService.RemoveItemAsync(Init.LocalUserData);
            ((AuthStateProvider)authStateProv).NotifyUserExit();
            client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
