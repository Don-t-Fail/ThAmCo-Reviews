using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.Models
{
    public class Account
    {
        public int Id { get; set; }
        public bool IsStaff { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
