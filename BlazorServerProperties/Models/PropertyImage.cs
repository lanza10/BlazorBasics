using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorServerProperties.Models
{
    public class PropertyImage
    {
        [Key]
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string PropertyImageUrl { get; set; }
        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }
        
    }
}
