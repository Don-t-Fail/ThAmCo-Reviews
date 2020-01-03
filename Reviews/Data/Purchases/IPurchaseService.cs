using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Services;

namespace Reviews.Data.Purchases
{
    public interface IPurchaseService
    {
        Task<List<PurchaseDto>> GetAll();
        Task<PurchaseDto> GetPurchase(int id);
    }
}
