using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team8CA.DataAccess;

namespace Team8CA.Models
{
    public class TemporaryShoppingCart
    {
        private readonly AppDbContext db;

        public string TemporaryShoppingCartId { get; set; }

        public virtual List<TemporaryShoppingCartItem> TemporaryShoppingCartItem { get; set; }

        public TemporaryShoppingCart(AppDbContext appDbContext)
        {
            db = appDbContext;
        }
        public string CustomerId { get; set; }

        public DateTime OrderCreationTime { get; set; }

        public DateTime OrderTime { get; set; }

        public bool IsCheckOut { get; set; }

        public TemporaryShoppingCart()
        {
            TemporaryShoppingCartItem = new List<TemporaryShoppingCartItem>();
        }
    }
}
