using System.ComponentModel.DataAnnotations;

namespace BlazorServerProperties.Models.DTO
{
    public class PropertyDTO 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name is mandatory")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Name length must be between 5 and 30 chars")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The description is mandatory")]
        [StringLength(300, MinimumLength = 20, ErrorMessage = "Description length must be between 20 and 300 chars")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The area is mandatory")]
        [Range(1, 500, ErrorMessage = "You must insert a valid number")]
        public int Area { get; set; }
        [Required(ErrorMessage = "The amount of rooms is mandatory")]
        [Range(1, 10, ErrorMessage = "You must insert a valid number")]
        public int Rooms { get; set; }
        [Required(ErrorMessage = "The amount of bathrooms is mandatory")]
        [Range(1, 10, ErrorMessage = "You must insert a valid number")]
        public int Bathrooms { get; set; }
        [Required(ErrorMessage = "The amount of parking spaces is mandatory")]
        [Range(1, 10, ErrorMessage = "You must insert a valid number")]
        public int Parking { get; set; }
        [Required(ErrorMessage = "The price is mandatory")]
        public double Price { get; set; }
        [Required]
        public bool Active { get; set; }

        public int CategoryId { get; set; }
        public virtual ICollection<PropertyImage> Images { get; set; }
        public List<string> ImagesUrl { get; set; }
    }
}
