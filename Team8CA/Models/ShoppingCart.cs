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
        private readonly AppDbContext _appDbContext;

        public string ShoppingCartId { get; set; } //this is the cartID

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string CustomerId { get; set; } //to get customerID

        //public double TotalValue { get; set; }

        public DateTime OrderCreationTime { get; set; }

        public DateTime OrderTime {get;set;}

        public bool IsCheckOut { get; set; } //if checkout or not

        public ShoppingCart()
        {

        }

        //public static ShoppingCart GetCart(IServiceProvider services)
        //{
        //    ISession session = services.GetRequiredService<IHttpContextAccessor>
        //        ()?.HttpContext.Session;

        //    var context = services.GetService<AppDbContext>();
        //    string Id = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        //    session.SetString("CartId", Id);

        //    return new ShoppingCart(context)
        //    {
        //        ShoppingCartId = Id
        //    };
        //}

        public void AddToCart(Products product, int quantity, string customerId, string sessionID)
        {
            var shoppingCartItem = _appDbContext.ShoppingCartItem.FirstOrDefault(
                x => x.Product.Id == product.Id && x.ShoppingCartId == sessionID);
            var shoppingcart = _appDbContext.ShoppingCart.FirstOrDefault(
                x => x.CustomerId == customerId && x.ShoppingCartId == sessionID);

                if (shoppingcart == null)
                {
                    ShoppingCart shoppingcarts = new ShoppingCart
                    {
                        ShoppingCartId = sessionID,
                        CustomerId = customerId,
                        OrderCreationTime = DateTime.Now,
                        IsCheckOut = false
                    };
                    _appDbContext.ShoppingCart.Add(shoppingcarts);
                }
                _appDbContext.SaveChanges();

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = sessionID,
                        CustomerId = customerId,
                        Product = product,
                        Quantity = quantity,
                    };
                    _appDbContext.ShoppingCartItem.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Quantity++;
                }
                _appDbContext.SaveChanges();
            
        }

        public int RemoveFromCart(Products product)
        {
            var shoppingCartItem = _appDbContext.ShoppingCartItem.SingleOrDefault(
                x => x.Product.Id == product.Id && x.ShoppingCartId == ShoppingCartId);

            var newquantity = 0;

            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Quantity>1)
                {
                    shoppingCartItem.Quantity--;
                    newquantity = shoppingCartItem.Quantity;
                }
                else
                {
                    _appDbContext.ShoppingCartItem.Remove(shoppingCartItem);
                }
            }
            _appDbContext.SaveChanges();
            return newquantity;
        }

        public int AddQuantityToCart(Products product, string sessionID)
        {
            var shoppingCartItem = _appDbContext.ShoppingCartItem.SingleOrDefault(
                x => x.Product.Id == product.Id && x.ShoppingCartId == sessionID);

            var newquantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 0)
                {
                    shoppingCartItem.Quantity++;
                    newquantity = shoppingCartItem.Quantity;
                }
            }
            _appDbContext.SaveChanges();
            return newquantity;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _appDbContext.ShoppingCartItem.Where(x => x.ShoppingCartId == ShoppingCartId).Include(y => y.Product).ToList());
        }

        public void ClearCart()
        {
            var cartitems = _appDbContext.ShoppingCartItem.Where(x => x.ShoppingCartId == ShoppingCartId);
            _appDbContext.ShoppingCartItem.RemoveRange(cartitems);
            _appDbContext.SaveChanges();
        }

        public double GetCartTotal()
        {
            var total = _appDbContext.ShoppingCartItem.Where(x => x.ShoppingCartId == ShoppingCartId).Select(x => x.Product.ProductPrice * x.Quantity).Sum();

            return total;
        }
    }
}

