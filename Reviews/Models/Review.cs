﻿namespace Reviews.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public bool IsVisible { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public int PurchaseRef { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}