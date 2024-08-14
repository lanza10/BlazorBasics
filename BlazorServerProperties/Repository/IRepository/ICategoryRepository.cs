using BlazorServerProperties.Models.DTO;

namespace BlazorServerProperties.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<CategoryDTO>> GetAllCategory();
        public Task<CategoryDTO> GetCategoryById(int id);
        public Task<CategoryDTO> CreateCategory(CategoryDTO category);
        public Task<CategoryDTO> UpdateCategory(int id, CategoryDTO category);
        public Task<CategoryDTO> ExistCategoryName(string name);
        public Task<int> DeleteCategory(int id);
        //public Task<IEnumerable<CategoryDTO>> GetDropDownCategories();
    }
}
