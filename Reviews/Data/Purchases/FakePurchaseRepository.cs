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

        public async Task InsertPurchase(Purchase purchase)
        {
            _purchases.Add(purchase);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}
