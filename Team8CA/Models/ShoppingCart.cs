using System;
using System.Collections.Generic;
using Team8CA.DataAccess;

namespace Team8CA.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext db;

        public string ShoppingCartId { get; set; }

        public virtual List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext appDbContext)
        {
            db = appDbContext;
        }
        public string CustomerId { get; set; }

        public DateTime OrderCreationTime { get; set; }

        public DateTime OrderTime { get; set; }

        public bool IsCheckOut { get; set; }

        public ShoppingCart()
        {
            ShoppingCartItems = new List<ShoppingCartItem>();
        }
    }
}

