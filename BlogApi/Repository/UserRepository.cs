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
    public class UserRepository(ApplicationDbContext db, IConfiguration config) : IUserRepository
    {
        private readonly string _secretKey = config.GetValue<string>("ApiSettings:Secret");

        public ICollection<User> GetUsers()
        {
            return db.Users.OrderBy(u => u.Id).ToList();
        }

        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool IsUniqueUser(string username)
        {
            var dbUser = db.Users.FirstOrDefault(u=> u.Username == username);
            return dbUser == null;
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var encryptedPassword = GetMd5(userLoginDto.Password);
            var user = db.Users.FirstOrDefault(
                u => u.Username.ToLower() == userLoginDto.Username.ToLower()
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

       

        public async Task<User> Register(UserRegisterDto userRegisterDto)
        {
            var encryptedPassword = GetMd5(userRegisterDto.Password);

            var user = new User()
            {
                Username = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                Name = userRegisterDto.Name,
                Password = userRegisterDto.Password
            };

            db.Users.Add(user);
            user.Password = encryptedPassword;
            await db.SaveChangesAsync();
            return user;
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
