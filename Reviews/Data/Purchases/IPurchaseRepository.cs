using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Data.Purchases
{
    public interface IPurchaseRepository
    {
        Task InsertPurchase(Purchase purchase);
        Task Save();
    }
}
