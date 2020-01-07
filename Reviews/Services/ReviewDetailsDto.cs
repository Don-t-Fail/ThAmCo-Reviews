namespace Reviews.Services
{
    public class ReviewDetailsDto
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public bool IsVisible { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
    }
}