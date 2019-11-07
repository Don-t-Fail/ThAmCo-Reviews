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
                    new Account {Id = 1, IsStaff = false},
                    new Account { Id = 2, IsStaff = true}
                );

                builder.Entity<Purchase>().HasData(
                    new Purchase { Id = 1, AccountId = 1, ProductId = 1},
                    new Purchase { Id = 2, AccountId = 1, ProductId = 2}
                );

                builder.Entity<Review>().HasData(
                    new Review { Id = 1, IsVisible = true, Content = "This is a test review for product 1", PurchaseId = 1 },
                    new Review { Id = 2, IsVisible = true, Content = "This is a review for product 2", PurchaseId = 2 }
                );
            }
        }
}
}
