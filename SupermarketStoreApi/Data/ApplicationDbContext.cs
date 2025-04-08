using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupermarketStoreApi.Models.Auth;

namespace SupermarketStoreApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = "1", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
                new ApplicationRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = "3", Name = "Seller", NormalizedName = "Seller" },
                new ApplicationRole { Id = "4", Name = "User", NormalizedName = "USER" }
            );
        }
    }
}
