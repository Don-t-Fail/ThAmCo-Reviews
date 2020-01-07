﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reviews.Data;

namespace Reviews.Migrations
{
    [DbContext(typeof(ReviewDbContext))]
    [Migration("20200104020057_RemovedSeedDataForIntegrationTesting")]
    partial class RemovedSeedDataForIntegrationTesting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Reviews.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsStaff");

                    b.HasKey("Id");

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsStaff = false
                        },
                        new
                        {
                            Id = 2,
                            IsStaff = true
                        },
                        new
                        {
                            Id = 3,
                            IsStaff = false
                        },
                        new
                        {
                            Id = 4,
                            IsStaff = false
                        },
                        new
                        {
                            Id = 5,
                            IsStaff = false
                        },
                        new
                        {
                            Id = 6,
                            IsStaff = false
                        },
                        new
                        {
                            Id = 7,
                            IsStaff = false
                        });
                });

            modelBuilder.Entity("Reviews.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("Reviews.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<bool>("IsVisible");

                    b.Property<int>("PurchaseId");

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId")
                        .IsUnique();

                    b.ToTable("Review");
                });

            modelBuilder.Entity("Reviews.Models.Purchase", b =>
                {
                    b.HasOne("Reviews.Models.Account", "Account")
                        .WithMany("Purchases")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reviews.Models.Review", b =>
                {
                    b.HasOne("Reviews.Models.Purchase", "Purchase")
                        .WithOne("Review")
                        .HasForeignKey("Reviews.Models.Review", "PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
