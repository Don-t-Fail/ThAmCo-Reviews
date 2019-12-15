using Reviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
