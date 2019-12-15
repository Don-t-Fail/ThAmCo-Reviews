using Microsoft.EntityFrameworkCore;
using Reviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Reviews.Data
{
    public class ReviewDbContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<Review> Review { get; set; }

        private IHostingEnvironment HostEnv { get; }

        public ReviewDbContext(DbContextOptions<ReviewDbContext> options, IHostingEnvironment env) : base(options)
        {
            HostEnv = env;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                //Seed Data
                builder.Entity<Account>().HasData(
                    new Account { Id = 1, IsStaff = false},
                    new Account { Id = 2, IsStaff = true},
                    new Account { Id = 3, IsStaff = false},
                    new Account { Id = 4, IsStaff = false },
                    new Account { Id = 5, IsStaff = false },
                    new Account { Id = 6, IsStaff = false }
                );

                builder.Entity<Purchase>().HasData(
                    new Purchase { Id = 1, AccountId = 1, ProductId = 1},
                    new Purchase { Id = 2, AccountId = 1, ProductId = 2},
                    new Purchase { Id = 3, AccountId = 3, ProductId = 1},
                    new Purchase { Id = 4, AccountId = 4, ProductId = 1 },
                    new Purchase { Id = 5, AccountId = 3, ProductId = 3 },
                    new Purchase { Id = 6, AccountId = 5, ProductId = 1 },
                    new Purchase { Id = 7, AccountId = 6, ProductId = 1 }
                );

                builder.Entity<Review>().HasData(
                    new Review { Id = 1, IsVisible = true, Rating = 5, Content = "This is a test review for product 1", PurchaseId = 1 },
                    new Review { Id = 2, IsVisible = true, Rating = 2, Content = "This is a review for product 2", PurchaseId = 2 },
                    new Review { Id = 3, IsVisible = true, Rating = 3, Content = "This is a test review for product 1", PurchaseId = 3 },
                    new Review { Id = 4, IsVisible = false, Rating = 1, Content = "This is a test review for product 1", PurchaseId = 4 },
                    new Review { Id = 5, IsVisible = true, Rating = 3, Content = "This is a test review for product 3", PurchaseId = 5 },
                    new Review { Id = 6, IsVisible = true, Rating = 3, Content = "This is a test review for product 1", PurchaseId = 6 }
                );
            }
        }
}
}
