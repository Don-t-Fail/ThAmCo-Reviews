using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Data.Purchases
{
    public class FakePurchaseRepository : IPurchaseRepository
    {
        private List<Purchase> _purchases;

        public FakePurchaseRepository(List<Purchase> purchases)
        {
            _purchases = purchases;
        }

        public void DeletePurchase(int id)
        {
            var purchase = _purchases.Find(p => p.Id == id);
            _purchases.Remove(purchase);
        }

        public async Task<List<Purchase>> GetAll()
        {
            return await Task.FromResult(_purchases.ToList());
        }

        public Task<Purchase> GetPurchase(int id)
        {
            return Task.FromResult(_purchases.FirstOrDefault(p => p.Id == id));
        }

        public async Task<List<Purchase>> GetPurchaseByAccount(int accId)
        {
            return await Task.FromResult(_purchases.Where(p => p.AccountId == accId).ToList());
        }

        public void InsertPurchase(Purchase purchase)
        {
            _purchases.Add(purchase);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }

        public void UpdatePurchase(Purchase purchase)
        {
            throw new NotImplementedException();
        }
    }
}
