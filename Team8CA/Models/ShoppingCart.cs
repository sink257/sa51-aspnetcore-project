using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Schema;
using Team8CA.DataAccess;

namespace Team8CA.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext db;

        [Key]
        public string ShoppingCartId { get; set; } //this is the cartID

        public virtual List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public string CustomerId { get; set; } //to get customerID

        //public double TotalValue { get; set; }

        public DateTime OrderCreationTime { get; set; }

        public DateTime OrderTime { get; set; }

        public bool IsCheckOut { get; set; } //if checkout or not

        public ShoppingCart()
        {
            ShoppingCartItems = new List<ShoppingCartItem>();
        }

        public void AddToCart(Products product, int productid, int quantity, string customerId, string sessionID)
        {

            if (sessionID == null)
            {
                ShoppingCartItem shoppingCartItem = db.ShoppingCartItem.FirstOrDefault(
                    x => x.Products.ProductId == product.ProductId && x.ShoppingCartId == "0" && x.ShoppingCart.IsCheckOut == false);
                ShoppingCart shoppingcart = db.ShoppingCart.FirstOrDefault(
                    x => x.CustomerId == customerId && x.ShoppingCartId == "0" && x.IsCheckOut == false);
                if (shoppingcart == null)
                {
                    ShoppingCart shoppingcarts = new ShoppingCart
                    {
                        ShoppingCartId = "0",
                        CustomerId = customerId,
                        OrderCreationTime = DateTime.Now,
                        IsCheckOut = false
                    };
                    db.ShoppingCart.Add(shoppingcarts);
                }
                db.SaveChanges();

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = "0",
                        CustomerId = customerId,
                        ProductsId = productid,
                        Products = product,
                        Quantity = quantity,
                    };
                    db.ShoppingCartItem.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Quantity++;
                }
                db.SaveChanges();
            }
            else
            {
                ShoppingCartItem shoppingCartItem = db.ShoppingCartItem.FirstOrDefault(
                    x => x.Products.ProductId == product.ProductId && x.ShoppingCartId == customerId && x.ShoppingCart.IsCheckOut == false);
                ShoppingCart shoppingcart = db.ShoppingCart.FirstOrDefault(
                    x => x.CustomerId == customerId && x.IsCheckOut == false);
                if (shoppingcart == null)
                {
                    ShoppingCart shoppingcarts = new ShoppingCart
                    {
                        ShoppingCartId = customerId,
                        CustomerId = customerId,
                        OrderCreationTime = DateTime.Now,
                        IsCheckOut = false
                    };
                    db.ShoppingCart.Add(shoppingcarts);
                }
                db.SaveChanges();

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = customerId,
                        CustomerId = customerId,
                        Products = product,
                        Quantity = quantity,
                    };
                    db.ShoppingCartItem.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Quantity++;
                }
                db.SaveChanges();
            }
        }

        public void RemoveFromCart(Products product, string customerId, string sessionID)
        {
            if (sessionID == null)
            {
                ShoppingCartItem shoppingCartItem = db.ShoppingCartItem.FirstOrDefault(
                    x => x.Products.ProductId == product.ProductId && x.ShoppingCartId == "0");

                if (shoppingCartItem != null)
                {
                    if (shoppingCartItem.Quantity > 1)
                    {
                        shoppingCartItem.Quantity--;
                    }
                    else
                    {
                        db.ShoppingCartItem.Remove(shoppingCartItem);
                    }
                }
                db.SaveChanges();
            }
            else
            {
                ShoppingCartItem shoppingCartItem = db.ShoppingCartItem.FirstOrDefault(
                    x => x.Products.ProductId == product.ProductId && x.ShoppingCartId == customerId);

                if (shoppingCartItem != null)
                {
                    if (shoppingCartItem.Quantity > 1)
                    {
                        shoppingCartItem.Quantity--;
                    }
                    else
                    {
                        db.ShoppingCartItem.Remove(shoppingCartItem);
                    }
                }
                db.SaveChanges();
            }
        }

        public void RemoveRow(Products product, string customerId, string sessionID)
        {
            ShoppingCartItem shoppingCartItem = db.ShoppingCartItem.FirstOrDefault(
                x => x.Products.ProductId == product.ProductId && x.ShoppingCartId == customerId);

            db.ShoppingCartItem.Remove(shoppingCartItem);
            db.SaveChanges();
        }

        public ICollection<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = db.ShoppingCartItem.Where(x => x.ShoppingCartId == ShoppingCartId).Include(y => y.Products).ToList());
        }

        public double GetCartTotal()
        {
            double total = db.ShoppingCartItem.Where(x => x.ShoppingCartId == ShoppingCartId).Select(x => x.Products.ProductPrice * x.Quantity).Sum();

            return total;
        }

        public void CheckoutCart (string customerid)
        {
            ShoppingCart shoppingcart = db.ShoppingCart.FirstOrDefault(x => x.CustomerId == customerid && !x.IsCheckOut);
            shoppingcart.IsCheckOut = true;
            shoppingcart.OrderTime = DateTime.Now;
            db.ShoppingCart.Update(shoppingcart);
            db.SaveChanges();

            Order orders = db.Order.FirstOrDefault(
                x => x.CustomerId == customerid);
            if (orders == null)
            {
                orders.CreateOrder(orders);
            }

            List<ShoppingCartItem> cartitems = db.ShoppingCartItem.Where(x => x.ShoppingCartId == shoppingcart.ShoppingCartId).ToList();
            foreach (var item in cartitems)
            {
                OrderDetails orderdetails = new OrderDetails()
                {
                    ProductId = item.ProductsId,
                    OrderId = orders.OrderId,
                    Quantity = item.Quantity,
                    Price = item.Products.ProductPrice
                };
                db.OrderDetails.Add(orderdetails);
                db.SaveChanges();
            }
        }
    }
}

