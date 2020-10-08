using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Team8CA.DataAccess;
using Team8CA.Models;

namespace Team8CA.Services
{
    public class CartRelatedService
    {
        private readonly AppDbContext _db;

        public CartRelatedService(AppDbContext db)
        {
            _db = db;
        }
        public int AddProductsToCart(int userId, int prdId, int quantity)
        {
            using (var trn = _db.Database.BeginTransaction())
            {
                try
                {
                    AddNewCart(userId);
                    ShoppingCart cart = _db.ShoppingCart.FirstOrDefault(x => x.CustomerId == userId && !x.IsCheckOut);
                    AddCartItem(prdId, quantity, cart);
                    _db.SaveChanges();
                    trn.Commit();
                    return cart.NoOfProductTypes;
                }
                catch (Exception e)
                {
                    trn.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }
        //create new cart row
        public void AddNewCart(int userId)
        {
            ShoppingCart cart = _db.ShoppingCart.FirstOrDefault(x => x.CustomerId == userId && !x.IsCheckOut);
            if (cart == null)
            {
                cart = new ShoppingCart(userId);
                _db.ShoppingCart.Add(cart);
            }
            _db.SaveChanges();
        }

        //retrieve number of items in cart
        public int GetNumberOfCartItems(int userId)
        {
            return _db.ShoppingCart.FirstOrDefault(x => x.CustomerId == userId && !x.IsCheckOut)?.NoOfProductTypes ?? 0;
        }

        public void AddCartItem(int prdId, int quantity, ShoppingCart cart)
        {
            Products product = _db.Products.First(x => x.Id == prdId);
            ShoppingCartItems cartItem = _db.ShoppingCartItem.FirstOrDefault(x => x.Id == cart.Id && x.ProductId == prdId);

            if (cartItem == null)
            {
                cartItem = new ShoppingCartItems(cart.Id, prdId);
                _db.ShoppingCartItem.Add(cartItem);
            }
            else
            {
                cartItem.QuantityPerItem += quantity;
            }

            //if (cartItem.QuantityPerItem == 0) //to remove item from shoppingcart if no quantity remaining (to delete for good implementation - show zero in shopping cart)
            //{
            //    _db.ShoppingCartItems.Remove(cartItem);
            //}
            cart.NoOfProductTypes += quantity;
            cart.SubTotal += cartItem.QuantityPerItem * product.ProductPrice;
            cart.Total += cart.SubTotal;
        }

        public void AddCartItemForSession(int userId, int prdId, int quantity, ShoppingCart cart)
        {
            Products product = _db.Products.First(x => x.Id == prdId);
            ShoppingCartItems items = cart.ShoppingCartItems.FirstOrDefault(x => x.ProductId == prdId);


            if (items == null)
            {
                items = new ShoppingCartItems(cart.Id, prdId);
                items.Products = product;
                cart.ShoppingCartItems.Add(items);
            }
            else
            {
                items.QuantityPerItem += quantity;
            }
            _db.SaveChanges();

            List<ShoppingCartItems> cartitems = _db.ShoppingCartItem.Where(x => x.ProductId == prdId).ToList();
            if (cartitems == null)
            {
                ShoppingCart shoppingcart = new ShoppingCart
                {
                    CustomerId = userId,
                    ProductId = prdId,
                    IsCheckOut = false,
                    OrderCreationTime = DateTime.Now,
                };
            } //do a count of number of items in this list in controller for number of product types
        }



        public ShoppingCart GetCartForCustomer(int customerId)
        {
            var cart = _db.ShoppingCart.Where(cart => cart.CustomerId == customerId && !cart.IsCheckOut).FirstOrDefault();
            if (cart != null)
            {
                cart.ShoppingCartItems = _db.ShoppingCartItem.Where(x => x.Id == cart.Id).ToList<ShoppingCartItems>();
            }
            return cart ?? new ShoppingCart();
        }
    }
}
