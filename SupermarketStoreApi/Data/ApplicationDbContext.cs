using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupermarketStoreApi.Models;
using SupermarketStoreApi.Models.Auth;

namespace SupermarketStoreApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public DbSet<Prodotto> Prodotti { get; set; }
        public DbSet<Categoria> Categorie { get; set; }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Ordine> Ordini { get; set; }
        public DbSet<ProdottoCarrello> ProdottiCarrello { get; set; }
        public DbSet<ProdottoOrdine> ProdottiOrdine { get; set; }
        public DbSet<StatoOrdine> StatiOrdine { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Cliente)
                .WithOne(c => c.User)
                .HasForeignKey<Cliente>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.ProdottiCarrello)
                .WithOne(ci => ci.User)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Ordini)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Prodotto>()
                .HasMany(p => p.ProdottiCarrello)
                .WithOne(ci => ci.Prodotto)
                .HasForeignKey(ci => ci.ProdottoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Prodotto>()
                .HasMany(p => p.ProdottiOrdine)
                .WithOne(oi => oi.Prodotto)
                .HasForeignKey(oi => oi.ProdottoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Ordine>()
                .HasMany(o => o.ProdottoOrdini)
                .WithOne(oi => oi.Ordine)
                .HasForeignKey(oi => oi.OrdineId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Ordine>()
                .HasOne(o => o.StatoOrdini)
                .WithMany(os => os.Ordini)
                .HasForeignKey(o => o.StatoOrdineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Prodotto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Prodotti)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Ordine>()
                .Property(o => o.Totale)
                .HasPrecision(18, 2);

            builder.Entity<Prodotto>()
                .Property(p => p.PrezzoProdotto)
                .HasPrecision(18, 2);

            builder.Entity<ProdottoOrdine>()
                .Property(po => po.Prezzo)
                .HasPrecision(18, 2);


            builder.Entity<ApplicationUserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.ApplicationUser)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ur => ur.ApplicationRole)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<StatoOrdine>().HasData(
                new StatoOrdine { StatoOrdineId = 1, NomeStato = "In Preparazione" },
                new StatoOrdine { StatoOrdineId = 2, NomeStato = "Pronto" },
                new StatoOrdine { StatoOrdineId = 3, NomeStato = "Ritirato" },
                new StatoOrdine { StatoOrdineId = 4, NomeStato = "Annullato" }
            );

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = "1", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
                new ApplicationRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = "3", Name = "Seller", NormalizedName = "Seller" },
                new ApplicationRole { Id = "4", Name = "User", NormalizedName = "USER" }
            );
        }
    }
}
