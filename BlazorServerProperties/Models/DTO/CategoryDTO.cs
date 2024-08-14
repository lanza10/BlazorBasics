using System.ComponentModel.DataAnnotations;

namespace BlazorServerProperties.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The category name is mandatory")]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
