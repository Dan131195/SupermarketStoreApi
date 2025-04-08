﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SupermarketStoreApi.Data;

#nullable disable

namespace SupermarketStoreApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250408162413_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Auth.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Seller",
                            NormalizedName = "Seller"
                        },
                        new
                        {
                            Id = "4",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Auth.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Auth.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaId"));

                    b.Property<string>("NomeCategoria")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorie");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Cliente", b =>
                {
                    b.Property<Guid>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodiceFiscale")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClienteId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Clienti");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Ordine", b =>
                {
                    b.Property<Guid>("OrdineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("OraRitiro")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("StatoOrdineId")
                        .HasColumnType("int");

                    b.Property<decimal>("Totale")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrdineId");

                    b.HasIndex("StatoOrdineId");

                    b.HasIndex("UserId");

                    b.ToTable("Ordini");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Prodotto", b =>
                {
                    b.Property<Guid>("ProdottoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("DescrizioneProdotto")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ImmagineProdotto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeProdotto")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PrezzoProdotto")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProdottoId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Prodotti");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.ProdottoCarrello", b =>
                {
                    b.Property<Guid>("ProdottoCarrelloId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProdottoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProdottoCarrelloId");

                    b.HasIndex("ProdottoId");

                    b.HasIndex("UserId");

                    b.ToTable("ProdottiCarrello");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.ProdottoOrdine", b =>
                {
                    b.Property<int>("ProdottoOrdineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdottoOrdineId"));

                    b.Property<Guid>("OrdineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Prezzo")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProdottoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantità")
                        .HasColumnType("int");

                    b.HasKey("ProdottoOrdineId");

                    b.HasIndex("OrdineId");

                    b.HasIndex("ProdottoId");

                    b.ToTable("ProdottiOrdine");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.StatoOrdine", b =>
                {
                    b.Property<int>("StatoOrdineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatoOrdineId"));

                    b.Property<string>("NomeStato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatoOrdineId");

                    b.ToTable("StatiOrdine");

                    b.HasData(
                        new
                        {
                            StatoOrdineId = 1,
                            NomeStato = "In Preparazione"
                        },
                        new
                        {
                            StatoOrdineId = 2,
                            NomeStato = "Pronto"
                        },
                        new
                        {
                            StatoOrdineId = 3,
                            NomeStato = "Ritirato"
                        },
                        new
                        {
                            StatoOrdineId = 4,
                            NomeStato = "Annullato"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.Auth.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Auth.ApplicationUserRole", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.Auth.ApplicationRole", "ApplicationRole")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SupermarketStoreApi.Models.Auth.ApplicationUser", "ApplicationUser")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApplicationRole");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Cliente", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.Auth.ApplicationUser", "User")
                        .WithOne("Cliente")
                        .HasForeignKey("SupermarketStoreApi.Models.Cliente", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Ordine", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.StatoOrdine", "StatoOrdini")
                        .WithMany("Ordini")
                        .HasForeignKey("StatoOrdineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SupermarketStoreApi.Models.Auth.ApplicationUser", "User")
                        .WithMany("Ordini")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatoOrdini");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Prodotto", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.Categoria", "Categoria")
                        .WithMany("Prodotti")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.ProdottoCarrello", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.Prodotto", "Prodotto")
                        .WithMany("ProdottiCarrello")
                        .HasForeignKey("ProdottoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupermarketStoreApi.Models.Auth.ApplicationUser", "User")
                        .WithMany("ProdottiCarrello")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prodotto");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.ProdottoOrdine", b =>
                {
                    b.HasOne("SupermarketStoreApi.Models.Ordine", "Ordine")
                        .WithMany("ProdottoOrdini")
                        .HasForeignKey("OrdineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupermarketStoreApi.Models.Prodotto", "Prodotto")
                        .WithMany("ProdottiOrdine")
                        .HasForeignKey("ProdottoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ordine");

                    b.Navigation("Prodotto");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Auth.ApplicationRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Auth.ApplicationUser", b =>
                {
                    b.Navigation("Cliente");

                    b.Navigation("Ordini");

                    b.Navigation("ProdottiCarrello");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Categoria", b =>
                {
                    b.Navigation("Prodotti");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Ordine", b =>
                {
                    b.Navigation("ProdottoOrdini");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.Prodotto", b =>
                {
                    b.Navigation("ProdottiCarrello");

                    b.Navigation("ProdottiOrdine");
                });

            modelBuilder.Entity("SupermarketStoreApi.Models.StatoOrdine", b =>
                {
                    b.Navigation("Ordini");
                });
#pragma warning restore 612, 618
        }
    }
}
