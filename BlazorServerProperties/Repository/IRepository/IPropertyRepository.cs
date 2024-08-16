using BlazorServerProperties.Models.DTO;

namespace BlazorServerProperties.Repository.IRepository
{
    public interface IPropertyRepository
    {
        public Task<IEnumerable<PropertyDTO>> GetAllProperty();
        public Task<PropertyDTO> GetPropertyById(int id);
        public Task<PropertyDTO> CreateProperty(PropertyDTO property);
        public Task<PropertyDTO> UpdateProperty(int id, PropertyDTO property);
        public Task<PropertyDTO> ExistPropertyName(string name);
        public Task<int> DeleteProperty(int id);
    }
}
