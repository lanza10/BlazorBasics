using System.ComponentModel.DataAnnotations;

namespace BlazorServerProperties.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }
    }
}
