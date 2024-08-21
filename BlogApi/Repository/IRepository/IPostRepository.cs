using BlogApi.Models;

namespace BlogApi.Repository.IRepository
{
    public interface IPostRepository
    { 
        ICollection<Post> GetPosts();
        Post GetPost(int id);
        bool PostExists(string title);
        bool PostExists(int id);
        bool CreatePost(Post p);
        bool UpdatePost(Post p);
        bool DeletePost(Post p);
        bool Save();
    }
}
