using System.Security.AccessControl;
using AutoMapper;
using BlazorServerProperties.Data;
using BlazorServerProperties.Models;
using BlazorServerProperties.Models.DTO;
using BlazorServerProperties.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerProperties.Repository
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PropertyImageRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyImageDTO>> GetPropImages(int idProp)
        {
            var images = await _db.PropertyImages.Where(p => p.PropertyId == idProp).ToListAsync();
            return _mapper.Map<IEnumerable<PropertyImage>, IEnumerable<PropertyImageDTO>>(images);
        }

        public async Task<int> DeletePropImageByImageId(int idImg)
        {
            var image = await _db.PropertyImages.FindAsync(idImg);
            _db.PropertyImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeletePropImageByPropId(int idProp)
        {
            var images = await _db.PropertyImages.Where(p => p.PropertyId == idProp).ToListAsync();
            _db.RemoveRange(images);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeletePropImageByUrl(string url)
        {
            var image = await _db.PropertyImages.FirstOrDefaultAsync
                (p => p.PropertyImageUrl.ToLower() == url.ToLower());
            if (image == null)
            {
                return 0;
            }
            _db.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> CreatePropImage(PropertyImageDTO propertyImage)
        {
            var image = _mapper.Map<PropertyImageDTO, PropertyImage>(propertyImage);
            await _db.AddAsync(image);
            return await _db.SaveChangesAsync();
        }
    }
}
