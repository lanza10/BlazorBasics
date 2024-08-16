using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorServerProperties.Models.DTO
{
    public class PropertyImageDTO
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string PropertyImageUrl { get; set; }
    }
}
