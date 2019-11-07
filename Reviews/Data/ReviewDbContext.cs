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
            }
        }
}
}
