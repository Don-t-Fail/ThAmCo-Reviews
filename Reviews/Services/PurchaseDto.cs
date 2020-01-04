using System;

namespace Reviews.Services
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int AddressId { get; set; }
        public int AccountId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}