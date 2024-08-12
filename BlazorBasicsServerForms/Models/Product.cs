using System.ComponentModel.DataAnnotations;

namespace BlazorBasicsServerForms.Models
{
    public class Product
    {
        [Required(ErrorMessage = "The Name of the product is mandatory")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Name must have between 5 and 10 chars")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Description of the product is mandatory")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Description must have between 5 and 100 chars")]
        public string Description { get; set; }
        [Range(1,20, ErrorMessage = "Amount must be a number between 1 and 20")]
        public int Amount { get; set; }
        [Range(typeof(bool),"true","true", ErrorMessage = "Product must be Active")]
        public bool Active { get; set; }
    }
}
