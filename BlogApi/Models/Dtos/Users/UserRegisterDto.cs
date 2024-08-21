using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models.Dtos.Users
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username is mandatory")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }
    }
}
