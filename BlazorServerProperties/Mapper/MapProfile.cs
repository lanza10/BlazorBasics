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
        }
    }
}
