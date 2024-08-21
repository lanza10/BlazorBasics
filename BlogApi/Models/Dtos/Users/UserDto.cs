using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models.Dtos.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
