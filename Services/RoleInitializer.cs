using Microsoft.AspNetCore.Identity;
using Qualification.Data;
using System.Threading.Tasks;

namespace Qualification.Services
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "qwe123";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("employee") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("employee"));
            }
            if (await roleManager.FindByNameAsync("employer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("employer"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
