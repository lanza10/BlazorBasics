using System.ComponentModel.DataAnnotations;

namespace BlazorCRUD.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The book's title is mandatory")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The book's description is mandatory")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The book's author is mandatory")]
        public string Author { get; set; }
        [Required(ErrorMessage = "The book's amount of pages is mandatory")]
        public int Pages { get; set; }
        [Required(ErrorMessage = "The book's price is mandatory")]
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
