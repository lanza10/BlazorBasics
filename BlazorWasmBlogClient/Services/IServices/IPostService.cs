using BlazorWasmBlogClient.Models;

namespace BlazorWasmBlogClient.Services.IServices
{
    public interface IPostService
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPost(int id);
        public Task<Post> CreatePost(Post p);
        public Task<Post> UpdatePost(int id, Post p);
        public Task<bool> DeletePost(int id);
        public Task<string> UploadImage(MultipartFormDataContent content);
    }
}
