using IdentityScaffoldingDemo4.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityScaffoldingDemo4.Data
{
    public class SeedAdmin
    {
        private static RoleManager<IdentityRole> roleManager;
        private static UserManager<AppUser> userManager;
        // TODO: add static constructor

        public static async Task CreateUser(IServiceProvider provider)
        {
            roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            userManager = provider.GetRequiredService<UserManager<AppUser>>();

            const string ADMIN = "Admin";
            await CreateRole(ADMIN);

            // TODO: Use user secrets to hide the password
            const string SECRET_PASSWORD = "Secret!123";
            await CreateUser("admin", "admin@example.com", SECRET_PASSWORD, ADMIN);
        }
        private static async Task CreateRole(string roleName)
        {
            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        private static async Task CreateUser(string name, string email, string password, string role)
        {
            // if username doesn't exist, create it and add to role if (await userManager.FindByNameAsync(username) == null) {
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded) await userManager.AddToRoleAsync(user, role);
        }
    }
}
