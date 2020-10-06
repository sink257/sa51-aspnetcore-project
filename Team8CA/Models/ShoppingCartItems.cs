using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class ShoppingCartItems
    {
        public int Id { get; set; } //individual product ID

        public int ShoppingcartId { get; set; }

        public int ProductId { get; set; }

        public int QuantityPerItem { get; set; }

        public virtual Products Products { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartItems()
        {

        }

        public ShoppingCartItems(int ShoppingCartId, int productId)
        {
            ShoppingcartId = ShoppingCartId;
            ProductId = productId;
            QuantityPerItem = 0;
        }
    }
}
