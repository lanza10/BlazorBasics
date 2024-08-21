using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models.Dtos.Users
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username is mandatory")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }
    }
}
