using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Data.Purchases
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> GetAll();
        Task<Purchase> GetPurchase(int id);
        void InsertPurchase(Purchase purchase);
        void DeletePurchase(int id);
        void UpdatePurchase(Purchase purchase);
        Task<List<Purchase>> GetPurchaseByAccount(int accId);
        Task Save();
    }
}
