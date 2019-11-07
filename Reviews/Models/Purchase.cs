using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AccountId { get; set; }

        public virtual Review Review { get; set; }
        public virtual Account Account { get; set; }
    }
}
