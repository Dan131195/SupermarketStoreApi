using Microsoft.AspNetCore.Identity;
using SupermarketStoreApi.Models.Auth;

namespace SupermarketStoreApi.Seeders
{
    public static class SuperAdminSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var roles = new[] { "SuperAdmin", "Admin", "Seller", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role, NormalizedName = role.ToUpper() });
                }
            }

            var superAdminEmail = "admin@email.com";
            var existingUser = await userManager.FindByEmailAsync(superAdminEmail);

            if (existingUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = superAdminEmail,
                    Email = superAdminEmail,
                    FirstName = "Super",
                    LastName = "Admin"
                };

                var result = await userManager.CreateAsync(user, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, new[] { "SuperAdmin" });
                }
            }
        }
    }
}
