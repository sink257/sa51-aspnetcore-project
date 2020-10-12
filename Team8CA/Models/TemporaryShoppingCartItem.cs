using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class TemporaryShoppingCartItem
    {
        public int TemporaryShoppingCartItemId { get; set; }

        public string TemporaryShoppingCartId { get; set; }

        public string CustomerId { get; set; }

        public int ProductsId { get; set; }

        public virtual Products Products { get; set; }

        public virtual TemporaryShoppingCart TemporaryShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}
