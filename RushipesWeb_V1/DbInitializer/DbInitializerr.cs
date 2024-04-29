using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RushipesWeb_V1.Data;
using RushipesWeb_V1.DbInitializer;
using RushipesWeb_V1.Models.User;
using RushipesWeb_V1.Utility;

namespace RushipesWeb.DbInitializer
{
    public class DbInitializerr : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RushipesDbContext _db;

        public DbInitializerr(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            RushipesDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (!_roleManager.RoleExistsAsync(SD.Role_User).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_SuperUser)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@rushipes.com",
                    Email = "admin@rushipes.com",
                    Nom = "VUONG",
                    Prenom = "Jean-Luc",
                }, "Admin@1234").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@rushipes.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
