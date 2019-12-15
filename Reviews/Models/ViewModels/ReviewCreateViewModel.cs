using System.ComponentModel.DataAnnotations;

namespace Reviews.Models.ViewModels
{
    public class ReviewCreateViewModel
    {
        [Display(Name = "Review ID")]
        public int Id { get; set; }
        [Display(Name = "Purchase ID")]
        public int PurchaseId { get; set; }
        [Display(Name = "Product Rating"), Range(0, 5)]
        public int Rating { get; set; }
        [Display(Name = "Review Content")]
        public string Content { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}
