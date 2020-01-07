namespace Reviews.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int PurchaseRef { get; set; }
        public int ProductId { get; set; }
        public int AccountId { get; set; }

        public virtual Review Review { get; set; }
        public virtual Account Account { get; set; }
    }
}