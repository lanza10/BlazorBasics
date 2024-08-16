using BlazorServerProperties.Models.DTO;

namespace BlazorServerProperties.Repository.IRepository
{
    public interface IPropertyImageRepository
    {
        public Task<IEnumerable<PropertyImageDTO>> GetPropImages(int idProp);
        public Task<int> DeletePropImageByImageId(int idImg);
        public Task<int> DeletePropImageByPropId(int idProp);
        public Task<int> DeletePropImageByUrl(string url);
        public Task<int> CreatePropImage(PropertyImageDTO propertyImage);
    }
}
