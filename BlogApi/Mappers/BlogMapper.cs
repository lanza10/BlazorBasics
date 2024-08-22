using AutoMapper;
using BlogApi.Models;
using BlogApi.Models.Dtos.Posts;
using BlogApi.Models.Dtos.Users;

namespace BlogApi.Mappers
{
    public class BlogMapper: Profile
    {
        public BlogMapper()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostCreateDto>().ReverseMap();
            CreateMap<Post, PostUpdateDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserLoginResponseDto>().ReverseMap();
        }
    }
}
