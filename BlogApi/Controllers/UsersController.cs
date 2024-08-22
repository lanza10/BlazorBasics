using System.Net;
using AutoMapper;
using BlogApi.Models;
using BlogApi.Models.Dtos.Users;
using BlogApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(IUserRepository userRepo, IMapper mapper) : ControllerBase
    {
        protected ApiResponses Responses = new();

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var validUniqueUser = userRepo.IsUniqueUser(userRegisterDto.Username);
            if (!validUniqueUser)
            {
                Responses.StatusCode = HttpStatusCode.BadRequest;
                Responses.IsSuccess = false;
                Responses.ErrorMessages.Add("The username already exists");
                return BadRequest(Responses);
            }

            var user = await userRepo.Register(userRegisterDto);
            if (user == null)
            {
                Responses.StatusCode = HttpStatusCode.BadRequest;
                Responses.IsSuccess = false;
                Responses.ErrorMessages.Add("Error occurred while trying to register");
                return BadRequest(Responses);
            }
            Responses.StatusCode = HttpStatusCode.OK;
            Responses.IsSuccess = true;
            return Ok(Responses);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var loginResponse = await userRepo.Login(userLoginDto);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                Responses.StatusCode = HttpStatusCode.BadRequest;
                Responses.IsSuccess = false;
                Responses.ErrorMessages.Add("Username or password are incorrect");
                return BadRequest(Responses);
            }
            Responses.StatusCode = HttpStatusCode.OK;
            Responses.IsSuccess = true;
            Responses.Result = loginResponse;
            return Ok(Responses);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetUsers()
        {
            var usersList = userRepo.GetUsers();

            var usersDtoList = new List<UserDto>();

            foreach (var user in usersList)
            {
                usersDtoList.Add(mapper.Map<UserDto>(user));
            }
            return Ok(usersDtoList);
        }

        [Authorize]
        [HttpGet("{id:int}",Name ="GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int id)
        {
            var user = userRepo.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
    }
}
