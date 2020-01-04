using System.Collections.Generic;

namespace Reviews.Models
{
    public class Account
    {
        public int Id { get; set; }
        public bool IsStaff { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}