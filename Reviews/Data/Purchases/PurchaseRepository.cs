using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Data.Purchases
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ReviewDbContext _context;

        public PurchaseRepository(ReviewDbContext context)
        {
            _context = context;
        }
        public async Task InsertPurchase(Purchase purchase)
        {
            await _context.Purchase.AddAsync(purchase);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
