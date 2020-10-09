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

        [Key]
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
        
        //public ShoppingCart (string customerid)
        //{
        //    ShoppingCartId = customerid;
        //    OrderCreationTime = DateTime.Now;
        //    IsCheckOut = false;
        //    CartItems = new List<ShoppingCartItem>();
        //}

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>
                ()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            string Id = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", Id);

            return new ShoppingCart(context)
            {
                ShoppingCartId = Id
            };
        }
        
        //public void AddProductsToCart(string customerId, Products product, int quantity)
        //{
        //    using (var trn = _appDbContext.Database.BeginTransaction())
        //    try
        //    {
        //        AddCart(customerId);
        //        ShoppingCart shoppingcart = _appDbContext.ShoppingCart.FirstOrDefault(x => x.CustomerId == customerId && !x.IsCheckOut);
        //        AddToCart(product, quantity);
        //        _appDbContext.SaveChanges();
        //        trn.Commit();
        //    }

        //    catch (Exception e)
        //    {
        //        trn.Rollback();
        //        throw new Exception(e.Message);
        //    }
        //}

        //public void AddCart(string customerId)
        //{
        //    ShoppingCart shoppingcart = _appDbContext.ShoppingCart.FirstOrDefault(x => x.CustomerId == customerId && !x.IsCheckOut);
        //    if (shoppingcart == null)
        //    {
        //        shoppingcart = new ShoppingCart(customerId);
        //        _appDbContext.ShoppingCart.Add(shoppingcart);
        //    }
        //    _appDbContext.SaveChanges();
        //}
                
        public void AddToCart(Products product, int quantity, string customerId)
        {
            var shoppingCartItem = _appDbContext.ShoppingCartItem.FirstOrDefault(
                x => x.Product.Id == product.Id && x.ShoppingCartId == ShoppingCartId);
            var shoppingcart = _appDbContext.ShoppingCart.FirstOrDefault(
                x => x.CustomerId == customerId && x.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Quantity = quantity,
            };

                if(shoppingcart == null)
                { 
                ShoppingCart shoppingcarts = new ShoppingCart
                {
                    ShoppingCartId = ShoppingCartId,
                    CustomerId = customerId,
                    OrderCreationTime = DateTime.Now,
                    IsCheckOut = false
                };
                    _appDbContext.ShoppingCart.Add(shoppingcarts);
                }

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

        public int AddQuantityToCart(Products product)
        {
            var shoppingCartItem = _appDbContext.ShoppingCartItem.SingleOrDefault(
                x => x.Product.Id == product.Id && x.ShoppingCartId == ShoppingCartId);

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
