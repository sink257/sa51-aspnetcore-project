//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Threading.Tasks;
//using Team8CA.DataAccess;
//using Team8CA.Models;

//namespace Team8CA.Services
//{
//    public class CartRelatedService
//    {
//        private readonly AppDbContext _db;

//        public CartRelatedService(AppDbContext db)
//        {
//            _db = db;
//        }

//        public int AddToCart(int customerid, int productid, int quantity, int cartid)
//        {
//            using (var NewTrans = _db.Database.BeginTransaction())
//            {
//                try
//                {
//                    AddCartRow(customerid);
//                    ShoppingCart cart = _db.ShoppingCart.FirstOrDefault(x => x.CustomerId == customerid && !x.IsCheckOut);
//                    AddCartItems(productid, quantity, cart);
//                    _db.SaveChanges();
//                    NewTrans.Commit();
//                    return NoOfCartItems(cartid);
//                }
//                catch (Exception e)
//                {
//                    NewTrans.Rollback();
//                    throw new Exception(e.Message);
//                }
//            }
//        }

//        //create new cart row if no cart created yet
//        public void AddCartRow(int customerid)
//        {
//            ShoppingCart cart = _db.ShoppingCart.FirstOrDefault(x => x.CustomerId == customerid && !x.IsCheckOut);
//            if (cart == null)
//            {
//                cart = new ShoppingCart(customerid);
//                _db.ShoppingCart.Add(cart);
//            }
//            _db.SaveChanges();
//        }

//        //To add items to cart
//        public void AddCartItems(int productid, int quantity, ShoppingCart cart)
//        {
//            Products product = _db.Products.First(x => x.Id == productid);
//            ShoppingCartItem shoppingcartitem = _db.ShoppingCartItem.FirstOrDefault(x => x.ShoppingCartId == cart.Id && x.ProductId == productid);

//            if (shoppingcartitem == null)
//            {
//                shoppingcartitem = new ShoppingCartItem(cart.Id, productid);
//                _db.ShoppingCartItem.Add(shoppingcartitem);
//            }
//            else if (shoppingcartitem != null)
//            {
//                shoppingcartitem.Quantity += quantity;
//            }
//            if (shoppingcartitem.Quantity == 0)
//            {
//                _db.ShoppingCartItem.Remove(shoppingcartitem);
//            }
//            cart.TotalValue += quantity * product.ProductPrice;
//            _db.SaveChanges();
//        }

//        //to pull out number of unique products in cart (for shoppingcart button)
//        public int NoOfCartItems(int cartid)
//        {
//            List<ShoppingCartItem> shoppingcartitems = _db.ShoppingCartItem.Where(x => x.ShoppingCartId == cartid).ToList();
//            return shoppingcartitems.Count;
//        }

//        public ShoppingCart RetrieveCustomerCart(int customerid)
//        {
//            var shoppingcart = _db.ShoppingCart.Where(x => x.CustomerId == customerid && !x.IsCheckOut).FirstOrDefault();
//            if (shoppingcart != null)
//            {
//                shoppingcart.CartItems = _db.ShoppingCartItem.Where(x => x.ShoppingCartId == shoppingcart.Id).ToList<ShoppingCartItem>();
//            }
//            return shoppingcart ?? new ShoppingCart();
//        }

 
//    }
//}
