using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.Models.ViewModels
{
    public class ReviewCreateViewModel
    {
        [Display(Name = "Review ID")]
        public int Id { get; set; }
        [Display(Name = "Purchase ID")]
        public int PurchaseId { get; set; }
        [Display(Name = "Product Rating"), Range(0,5)]
        public int Rating { get; set; }
        [Display(Name = "Review Content")]
        public string Content { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}
