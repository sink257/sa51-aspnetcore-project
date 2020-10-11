using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Team8CA.DataAccess;

namespace Team8CA.Models
{
    public class Order
    {
        private readonly AppDbContext db;
        private readonly ShoppingCart shoppingcart;

        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Name { get; set; }

        public Order()
        {
        }

        public Order(AppDbContext db, ShoppingCart shoppingcart)
        {
            this.db = db;
            this.shoppingcart = shoppingcart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            order.OrderTotal = shoppingcart.GetCartTotal();
            db.Order.Add(order);
            db.SaveChanges();

            var shoppingcartitems = shoppingcart.GetShoppingCartItems();

            foreach (var shoppingcartitem in shoppingcartitems)
            {
                var orderdetails = new OrderDetails
                {
                    Quantity = shoppingcartitem.Quantity,
                    Price = shoppingcartitem.Products.ProductPrice,
                    ProductId = shoppingcartitem.Products.ProductId,
                    OrderId = order.OrderId
                };

                db.OrderDetails.Add(orderdetails);
            }
            db.SaveChanges();
        }
    }
}


