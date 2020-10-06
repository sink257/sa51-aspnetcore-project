using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class PurchasedItems
    {
        [MaxLength(36)]
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual ShoppingCart Cart { get; set; }
        public virtual DateTime CheckoutTime { get; set; }

        public virtual List<ActivationCodes> ActivationCodes { set; get; }
        public PurchasedItems()
        {
            ActivationCodes = new List<ActivationCodes>();
        }

        public PurchasedItems(int cartId, int productId)
        {
            ShoppingCartId = cartId;
            ProductId = productId;
            Quantity = 1;
            ActivationCodes = new List<ActivationCodes>();
        }
    }
}
