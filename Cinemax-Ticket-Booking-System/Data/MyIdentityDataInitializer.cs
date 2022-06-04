using Microsoft.AspNetCore.Identity;

namespace Cinemax_Ticket_Booking_System.Data
{
    public class MyIdentityDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        // name - poprawny adres email
        // password - min 8 znaków, mała i duża litera, cyfra i znak specjalny
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        public static void SeedOneUser(UserManager<IdentityUser> userManager, string name, string password, string role = null)
        {
            if (userManager.FindByNameAsync(name).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = name,
                    Email = name
                };
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded && role != null)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            SeedOneUser(userManager, "admin@login", "aUpass1!", "Admin");
            SeedOneUser(userManager, "guest@login", "123456");
        }
    }
}
