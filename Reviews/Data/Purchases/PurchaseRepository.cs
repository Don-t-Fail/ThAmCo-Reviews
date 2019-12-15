using Microsoft.EntityFrameworkCore;
using Reviews.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.Data.Purchases
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ReviewDbContext _context;

        public PurchaseRepository(ReviewDbContext context)
        {
            _context = context;
        }

        public void DeletePurchase(int id)
        {
            var purchase = _context.Purchase.FirstOrDefault(p => p.Id == id);
            _context.Purchase.Remove(purchase);
        }

        public async Task<List<Purchase>> GetAll()
        {
            return await _context.Purchase.ToListAsync();
        }

        public async Task<Purchase> GetPurchase(int id)
        {
            return await _context.Purchase.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Purchase>> GetPurchaseByAccount(int accId)
        {
            return await _context.Purchase.Where(p => p.AccountId == accId).ToListAsync();
        }

        public void InsertPurchase(Purchase purchase)
        {
            _context.Purchase.Add(purchase);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdatePurchase(Purchase purchase)
        {
            _context.Entry(purchase).State = EntityState.Modified;
        }
    }
}
