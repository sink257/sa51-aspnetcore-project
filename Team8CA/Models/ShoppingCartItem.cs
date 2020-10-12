using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public string ShoppingCartId { get; set; }

        public string CustomerId { get; set; }

        public int ProductsId { get; set; }

        public virtual Products Products { get; set;}

        public virtual ShoppingCart ShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}
