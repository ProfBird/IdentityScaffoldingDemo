using IdentityScaffoldingDemo4.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityScaffoldingDemo4.Data
{
    public class SeedAdmin
    {
        private static RoleManager<IdentityRole> roleManager;
        private static UserManager<AppUser> userManager;
        // TODO: add static constructor

        // Note: email is serving as user ID
        public static async Task CreateUserAsync(IServiceProvider provider, string email, string password)
        {
            roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            userManager = provider.GetRequiredService<UserManager<AppUser>>();

            const string ADMIN_ROLE = "Admin";
            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(ADMIN_ROLE) == null)
                await roleManager.CreateAsync(new IdentityRole(ADMIN_ROLE));
            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync(email) == null)
            {
                var user = new AppUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded) await userManager.AddToRoleAsync(user, ADMIN_ROLE);
            }
        }
    }
}
