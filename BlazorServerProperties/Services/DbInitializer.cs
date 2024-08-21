using BlazorServerProperties.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerProperties.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            if (_db.Roles.Any(x => x.Name == Configurations.AdminRole)) return;
            _roleManager.CreateAsync(new IdentityRole(Configurations.AdminRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Configurations.PublisherRole)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin1@gmail.com",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
            }, "Admin123*").GetAwaiter().GetResult();

            var user = _db.Users.FirstOrDefault(u => u.Email == "admin1@gmail.com");
            _userManager.AddToRoleAsync(user, Configurations.AdminRole).GetAwaiter().GetResult();
        }
    }
}
