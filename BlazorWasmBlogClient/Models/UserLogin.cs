using System.ComponentModel.DataAnnotations;

namespace BlazorWasmBlogClient.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Username is mandatory")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }
    }
}
