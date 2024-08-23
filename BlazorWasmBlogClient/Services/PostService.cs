using System.Text;
using BlazorWasmBlogClient.Helpers;
using BlazorWasmBlogClient.Models;
using BlazorWasmBlogClient.Services.IServices;
using Newtonsoft.Json;

namespace BlazorWasmBlogClient.Services
{
    public class PostService(HttpClient client) : IPostService
    {
        private readonly HttpClient _client = client;

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var response = await _client.GetAsync($"{Init.BaseUrlApi}api/posts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<IEnumerable<Post>>(content);
                return posts;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.Message);
            }
        }

        public async Task<Post> GetPost(int id)
        {
            var response = await _client.GetAsync($"{Init.BaseUrlApi}api/posts/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var post = JsonConvert.DeserializeObject<Post>(content);
                return post;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.Message);
            }
        }

        public async Task<Post> CreatePost(Post p)
        {
            var content = JsonConvert.SerializeObject(p);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{Init.BaseUrlApi}api/posts", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.Message);
            }
            
        }

        public async Task<Post> UpdatePost(int id, Post p)
        {
            var content = JsonConvert.SerializeObject(p);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync($"{Init.BaseUrlApi}api/posts/{id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.Message);
            }
        }

        public async Task<bool> DeletePost(int id)
        {
            var response = await _client.DeleteAsync($"{Init.BaseUrlApi}api/posts/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.Message);
            }
        }

        public async Task<string> UploadImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync($"{Init.BaseUrlApi}api/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }

            var postImage = Path.Combine($"{Init.BaseUrlApi}", postContent);
            return postImage;
        }
    }
}
