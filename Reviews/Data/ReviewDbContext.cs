using Microsoft.EntityFrameworkCore;
using Reviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.Data
{
    public class ReviewDbContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet <Review> Review { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ReviewsDb;");
        }
    }
}
