using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public int ShoppingCartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual Products Product { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartItem()
        { }

        public ShoppingCartItem(int shoppingCartid, int productid)
        {
            ShoppingCartId = shoppingCartid;
            ProductId = productid;
            Quantity = 1;
        }
    }
}
