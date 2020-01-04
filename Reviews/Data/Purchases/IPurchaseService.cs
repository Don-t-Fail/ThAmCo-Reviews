using Reviews.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reviews.Data.Purchases
{
    public interface IPurchaseService
    {
        Task<List<PurchaseDto>> GetAll();

        Task<PurchaseDto> GetPurchase(int id);
    }
}