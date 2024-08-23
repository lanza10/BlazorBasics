using BlazorWasmBlogClient.Models;

namespace BlazorWasmBlogClient.Services.IServices
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterUser(UserRegister userRegister);
        Task<LoginResponse> Login(UserLogin userLogin);
        Task Exit();
    }
}
