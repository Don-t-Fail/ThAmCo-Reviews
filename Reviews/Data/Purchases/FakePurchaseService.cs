using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Services;

namespace Reviews.Data.Purchases
{
    public class FakePurchaseService : IPurchaseService
    {
        private List<PurchaseDto> _purchases;

        public FakePurchaseService(List<PurchaseDto> purchases)
        {
            _purchases = purchases;
        }

        public async Task<List<PurchaseDto>> GetAll()
        {
            return await Task.FromResult(_purchases.ToList());
        }

        public async Task<PurchaseDto> GetPurchase(int id)
        {
            return await Task.FromResult(_purchases.FirstOrDefault(p => p.Id == id));
        }
    }
}
