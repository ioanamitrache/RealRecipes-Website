using Microsoft.AspNetCore.Identity;

namespace MVCRecipesSite.Data
{
    public class SeedDataIdentity
    {
        private const string adminEmail = "admin@test.com";
        private const string adminPassword = "Secret1234$";

        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser user = await userManager.FindByEmailAsync(adminEmail);

            if(user == null)
            {
                user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                await userManager.CreateAsync(user, adminPassword);
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string roleName = "DeleteRecipesRole";
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));

            var adminWithRoleEmail = "adminrole@test.com";
            var adminWithRole = await userManager.FindByEmailAsync(adminWithRoleEmail);

            if(adminWithRole == null)
            {
                adminWithRole = new IdentityUser
                {
                    UserName = adminWithRoleEmail,
                    Email = adminWithRoleEmail
                };
                await userManager.CreateAsync(adminWithRole, adminPassword);
                await userManager.AddToRoleAsync(adminWithRole, roleName);
            }

        }
    }
}
