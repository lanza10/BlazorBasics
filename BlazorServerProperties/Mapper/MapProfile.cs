using AutoMapper;
using BlazorServerProperties.Models;
using BlazorServerProperties.Models.DTO;

namespace BlazorServerProperties.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Property, PropertyDTO>().ReverseMap();
            CreateMap<Category, DropDownCategoryDTO>().ReverseMap();
            CreateMap<PropertyImage, PropertyImageDTO>().ReverseMap();
        }
    }
}
