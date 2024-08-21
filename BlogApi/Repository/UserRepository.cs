using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Models.Dtos.Users;
using BlogApi.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Repository
{
    public class UserRepository(ApplicationDbContext db) : IUserRepository
    {
        private readonly ApplicationDbContext _db = db;
        private string _secretKey;

        public ICollection<User> GetUsers()
        {
            return _db.Users.OrderBy(u => u.Id).ToList();
        }

        public User GetUser(int id)
        {
            return _db.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool IsUniqueUser(string username)
        {
            var dbUser = _db.Users.FirstOrDefault(u=> u.Username == username);
            return dbUser == null;
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var encryptedPassword = GetMd5(userLoginDto.Password);
            var user = _db.Users.FirstOrDefault(
                u => u.Username.Equals(userLoginDto.Username, StringComparison.CurrentCultureIgnoreCase)
                && u.Password == encryptedPassword
                );
            //If user exists with that username anda password
            if (user == null)
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenManager = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    //new Claim(ClaimTypes.Roles, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenManager.CreateToken(tokenDescriptor);
            var userLoginResponseDto = new UserLoginResponseDto()
            {
                Token = tokenManager.WriteToken(token),
                User = user
            };
            return userLoginResponseDto;
        }

       

        public Task<User> Register(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }

        // Method to encrypt password using MD5 for Login and Register
        private static string GetMd5(string value)
        {
            var data = Encoding.UTF8.GetBytes(value);
            data = MD5.HashData(data);
            var resp = "";
            foreach (var t in data)
            {
                resp += t.ToString("x2").ToLower();
            }
            return resp;
        }
    }
}
