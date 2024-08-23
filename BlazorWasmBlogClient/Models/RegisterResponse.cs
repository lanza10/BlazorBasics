namespace BlazorWasmBlogClient.Models
{
    public class RegisterResponse
    {
        public bool CorrectRegister { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
