using AutoMapper;
using BlogApi.Models;
using BlogApi.Models.Dtos;

namespace BlogApi.Mappers
{
    public class BlogMapper: Profile
    {
        public BlogMapper()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostCreateDto>().ReverseMap();
            CreateMap<Post, PostUpdateDto>().ReverseMap();
        }
    }
}
