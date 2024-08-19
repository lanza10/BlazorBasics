using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorServerProperties.Models
{
    public class Property
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int Area { get; set; }
        [Required] public int Rooms { get; set; }
        [Required] public int Bathrooms { get; set; }
        [Required] public int Parking { get; set; }
        [Required] public double Price { get; set; }
        [Required] public bool Active { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<PropertyImage> Images { get; set; }
    }
}
