using AutoMapper;
using BlazorServerProperties.Data;
using BlazorServerProperties.Models;
using BlazorServerProperties.Models.DTO;
using BlazorServerProperties.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerProperties.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PropertyRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PropertyDTO>> GetAllProperty()
        {   //v 1.0
            //try
            //{
            //    var propertiesDto =
            //        _mapper.Map<IEnumerable<Property>, IEnumerable<PropertyDTO>>(_db.Properties);
            //    return propertiesDto;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            //v2.0
            //try
            //{
            //    var propertiesDto =
            //        _mapper.Map<IEnumerable<Property>, IEnumerable<PropertyDTO>>(_db.Properties
            //            .Include(x => x.Images));
            //    return propertiesDto;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            //v3.0
            try
            {
                var propertiesDto =
                    _mapper.Map<IEnumerable<Property>, IEnumerable<PropertyDTO>>(_db.Properties
                        .Include(x => x.Images).Include(c => c.Category));
                return propertiesDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PropertyDTO> GetPropertyById(int id)
        {
            try
            {
                var propertyDto =
                    _mapper.Map<Property, PropertyDTO>(await _db.Properties.FirstOrDefaultAsync(p => p.Id == id));
                return propertyDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PropertyDTO> CreateProperty(PropertyDTO property)
        {
            var prop = _mapper.Map<PropertyDTO, Property>(property);
            prop.CreationDate = DateTime.Now;
            var createdProp = await _db.Properties.AddAsync(prop);
            await _db.SaveChangesAsync();
            return _mapper.Map<Property, PropertyDTO>(createdProp.Entity);
        }

        public async Task<PropertyDTO> UpdateProperty(int id, PropertyDTO property)
        {
            try
            {
                if (id != property.Id)
                {
                    return null;
                }
                //Valid to update
                var dbProp = await _db.Properties.FindAsync(id);
                var prop = _mapper.Map<PropertyDTO, Property>(property, dbProp);
                prop.UpdateDate = DateTime.Now;
                var updatedProp = _db.Properties.Update(prop);
                await _db.SaveChangesAsync();
                return _mapper.Map<Property, PropertyDTO>(updatedProp.Entity);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PropertyDTO> ExistPropertyName(string name)
        {
            try
            {
                var propertyDto =
                    _mapper.Map<Property, PropertyDTO>(await _db.Properties
                        .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower()));
                return propertyDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DeleteProperty(int id)
        {
            var prop = await _db.Properties.FindAsync(id);
            if (prop == null)
            {
                return 0;
            }
            _db.Properties.Remove(prop);
            return await _db.SaveChangesAsync();

        }
    }
}
