using AutoMapper;
using BlazorServerProperties.Data;
using BlazorServerProperties.Models;
using BlazorServerProperties.Models.DTO;
using BlazorServerProperties.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerProperties.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            try
            {
                var categoriesDto = 
                     _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
                return  categoriesDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            try
            {
                var categoryDto =
                    _mapper.Map<Category, CategoryDTO>(await _db.Categories.FirstOrDefaultAsync(c => c.Id == id));
                return categoryDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO category)
        {
            var cat = _mapper.Map<CategoryDTO, Category>(category);
            cat.CreationDate = DateTime.Now;
            var createdCat = await _db.Categories.AddAsync(cat);
            await _db.SaveChangesAsync();
            return _mapper.Map<Category, CategoryDTO>(createdCat.Entity);
        }

        public async Task<CategoryDTO> UpdateCategory(int id, CategoryDTO category)
        {
            try
            {
                if (id != category.Id)
                {
                    return null;
                }
                //Valid to update
                var dbCat = await _db.Categories.FindAsync(id);
                var cat = _mapper.Map<CategoryDTO, Category>(category, dbCat);
                cat.UpdateDate = DateTime.Now;
                var updatedCat =  _db.Categories.Update(cat);
                await _db.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDTO>(updatedCat.Entity);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CategoryDTO> ExistCategoryName(string name)
        {
            try
            {
                var categoryDto =
                    _mapper.Map<Category, CategoryDTO>(await _db.Categories
                        .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == name.ToLower()));
                return categoryDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DeleteCategory(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat == null)
            {
                return 0;
            }
            _db.Categories.Remove(cat);
            return await _db.SaveChangesAsync();

        }
    }
}
