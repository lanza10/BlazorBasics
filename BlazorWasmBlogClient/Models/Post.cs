using System.ComponentModel.DataAnnotations;

namespace BlazorWasmBlogClient.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Post title is mandatory")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Post description is mandatory")]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Post tags is mandatory")]
        public string Tags { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }
    }
}
