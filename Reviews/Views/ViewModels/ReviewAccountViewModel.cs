using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.Models.ViewModels
{
    public class ReviewAccountViewModel
    {
        [Display(Name = "Review ID")]
        public int Id { get; set; }
        [Display(Name = "Purchase ID")]
        public int PurchaseId { get; set; }
        [Display(Name = "Review Is Visible?")]
        public bool IsVisible { get; set; }
        [Display(Name = "Account ID")]
        public int AccountId { get; set; }
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [Display(Name = "Rating")]
        public int Rating { get; set; }
        [Display(Name = "Review Content")]
        public string Content { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}
