using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository
{
    public class PostRepository(ApplicationDbContext db) : IPostRepository
    {
        private readonly ApplicationDbContext _db = db;

        public ICollection<Post> GetPosts()
        {
            return _db.Posts.OrderBy(p => p.Id).ToList();
        }

        public Post GetPost(int id)
        {
           return _db.Posts.FirstOrDefault(p => p.Id == id);
        }

        public bool PostExists(string title)
        {
            var value = _db.Posts.Any(p => p.Title.ToLower().Trim() == title.ToLower().Trim());
            return value;
        }

        public bool PostExists(int id)
        {
            var value = _db.Posts.Any(p => p.Id == id);
            return value;
        }

        public bool CreatePost(Post p)
        {
           p.CreationDate = DateTime.Now;
           _db.Posts.Add(p);
           return Save();
        }

        public bool UpdatePost(Post p)
        {
            p.UpdateDate = DateTime.Now;
            var imageFromDb = _db.Posts.AsNoTracking().FirstOrDefault(c => c.Id == p.Id);

            if (imageFromDb != null)
            {
                p.ImagePath ??= imageFromDb.ImagePath;
            }

            _db.Posts.Update(p);
            return Save();
        }

        public bool DeletePost(Post p)
        {
            _db.Posts.Remove(p);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
