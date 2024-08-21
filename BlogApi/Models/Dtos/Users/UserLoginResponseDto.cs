namespace BlogApi.Models.Dtos.Users
{
    public class UserLoginResponseDto
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
