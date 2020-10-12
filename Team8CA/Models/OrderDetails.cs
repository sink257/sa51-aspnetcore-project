using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public virtual List<ActivationCodes> ActivationCodes { get; set; }
        public bool CheckOutComplete { get; set; }
        public OrderDetails()
        {

        }
    }
}
