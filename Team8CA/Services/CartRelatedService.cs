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

        public int AddToCart(int customerid, int productid, int quantity, int cartid)
        {
            using (var NewTrans = _db.Database.BeginTransaction())
            {
                try
                {
                    AddCartRow(customerid);
                    ShoppingCart cart = _db.ShoppingCart.FirstOrDefault(x => x.CustomerId == customerid && !x.IsCheckOut);
                    AddCartItems(productid, quantity, cart);
                    _db.SaveChanges();
                    NewTrans.Commit();
                    return NoOfCartItems(cartid);
                }
                catch (Exception e)
                {
                    NewTrans.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }

        //create new cart row if no cart created yet
        public void AddCartRow(int customerid)
        {
            ShoppingCart cart = _db.ShoppingCart.FirstOrDefault(x => x.CustomerId == customerid && !x.IsCheckOut);
            if (cart == null)
            {
                cart = new ShoppingCart(customerid);
                _db.ShoppingCart.Add(cart);
            }
            _db.SaveChanges();
        }

        //To add items to cart
        public void AddCartItems(int productid, int quantity, ShoppingCart cart)
        {
            Products product = _db.Products.First(x => x.Id == productid);
            ShoppingCartItem shoppingcartitem = _db.ShoppingCartItem.FirstOrDefault(x => x.ShoppingCartId == cart.Id && x.ProductId == productid);

            if (shoppingcartitem == null)
            {
                shoppingcartitem = new ShoppingCartItem(cart.Id, productid);
                _db.ShoppingCartItem.Add(shoppingcartitem);
            }
            else if (shoppingcartitem != null)
            {
                shoppingcartitem.Quantity += quantity;
            }
            if (shoppingcartitem.Quantity == 0)
            {
                _db.ShoppingCartItem.Remove(shoppingcartitem);
            }
            cart.TotalValue += quantity * product.ProductPrice;
            _db.SaveChanges();
        }

        //to pull out number of unique products in cart (for shoppingcart button)
        public int NoOfCartItems(int cartid)
        {
            List<ShoppingCartItem> shoppingcartitems = _db.ShoppingCartItem.Where(x => x.ShoppingCartId == cartid).ToList();
            return shoppingcartitems.Count;
        }

        public ShoppingCart RetrieveCustomerCart(int customerid)
        {
            var shoppingcart = _db.ShoppingCart.Where(x => x.CustomerId == customerid && !x.IsCheckOut).FirstOrDefault();
            if (shoppingcart != null)
            {
                shoppingcart.CartItems = _db.ShoppingCartItem.Where(x => x.ShoppingCartId == shoppingcart.Id).ToList<ShoppingCartItem>();
            }
            return shoppingcart ?? new ShoppingCart();
        }

        ////create new cart row
        //public void AddNewCart(int userId)
        //{
        //    ShoppingCart cart = _db.ShoppingCart.FirstOrDefault(x => x.CustomerId == userId && !x.IsCheckOut);
        //    if (cart == null)
        //    {
        //        cart = new ShoppingCart(userId);
        //        _db.ShoppingCart.Add(cart);
        //    }
        //    _db.SaveChanges();
        //}

        ////retrieve number of items in cart
        //public int GetNumberOfCartItems(int userId)
        //{
        //    return _db.ShoppingCart.FirstOrDefault(x => x.CustomerId == userId && !x.IsCheckOut)?.NoOfProductTypes ?? 0;
        //}

        //public void AddCartItem(int prdId, int quantity, ShoppingCart cart)
        //{
        //    Products product = _db.Products.First(x => x.Id == prdId);
        //    ShoppingCartItems cartItem = _db.ShoppingCartItem.FirstOrDefault(x => x.Id == cart.Id && x.ProductId == prdId);

        //    if (cartItem == null)
        //    {
        //        cartItem = new ShoppingCartItems(cart.Id, prdId);
        //        _db.ShoppingCartItem.Add(cartItem);
        //    }
        //    else
        //    {
        //        cartItem.QuantityPerItem += quantity;
        //    }

        //    //if (cartItem.QuantityPerItem == 0) //to remove item from shoppingcart if no quantity remaining (to delete for good implementation - show zero in shopping cart)
        //    //{
        //    //    _db.ShoppingCartItems.Remove(cartItem);
        //    //}
        //    cart.NoOfProductTypes += quantity;
        //    cart.SubTotal += cartItem.QuantityPerItem * product.ProductPrice;
        //    cart.Total += cart.SubTotal;
        //}

        //public void AddCartItemForSession(int userId, int prdId, int quantity, ShoppingCart cart)
        //{
        //    Products product = _db.Products.First(x => x.Id == prdId);
        //    ShoppingCartItems items = cart.ShoppingCartItems.FirstOrDefault(x => x.ProductId == prdId);


        //    if (items == null)
        //    {
        //        items = new ShoppingCartItems(cart.Id, prdId);
        //        items.Products = product;
        //        cart.ShoppingCartItems.Add(items);
        //    }
        //    else
        //    {
        //        items.QuantityPerItem += quantity;
        //    }
        //    _db.SaveChanges();

        //    List<ShoppingCartItems> cartitems = _db.ShoppingCartItem.Where(x => x.ProductId == prdId).ToList();
        //    if (cartitems == null)
        //    {
        //        ShoppingCart shoppingcart = new ShoppingCart
        //        {
        //            CustomerId = userId,
        //            ProductId = prdId,
        //            IsCheckOut = false,
        //            OrderCreationTime = DateTime.Now,
        //        };
        //    } //do a count of number of items in this list in controller for number of product types
        //}



        //public ShoppingCart GetCartForCustomer(int customerId)
        //{
        //    var cart = _db.ShoppingCart.Where(cart => cart.CustomerId == customerId && !cart.IsCheckOut).FirstOrDefault();
        //    if (cart != null)
        //    {
        //        cart.ShoppingCartItems = _db.ShoppingCartItem.Where(x => x.Id == cart.Id).ToList<ShoppingCartItems>();
        //    }
        //    return cart ?? new ShoppingCart();
        //}
    }
}
