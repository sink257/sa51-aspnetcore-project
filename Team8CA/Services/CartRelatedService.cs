using System;
using System.Collections.Generic;
using System.Linq;
using Team8CA.DataAccess;
using Team8CA.Models;

namespace Team8CA.Services
{
    public class CartRelatedService
    {
        private readonly AppDbContext db;

        private readonly ShoppingCart _shoppingcart;

        public CartRelatedService(AppDbContext appDbContext, ShoppingCart shoppingcart)
        {
            db = appDbContext;
            _shoppingcart = shoppingcart;
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

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return _shoppingcart.ShoppingCartItems = db.ShoppingCartItem.Where(x => x.ShoppingCartId == _shoppingcart.ShoppingCartId).ToList();
        }

        public double GetCartTotal()
        {
            double total = db.ShoppingCartItem.Where(x => x.ShoppingCartId == _shoppingcart.ShoppingCartId).Select(x => x.Products.ProductPrice * x.Quantity).Sum();

            return total;
        }

        public void ClearCart(string customerid)
        {
            var cartitems = db.ShoppingCartItem.Where(x => x.CustomerId == customerid);
            var cart = db.ShoppingCart.Where(x => x.CustomerId == customerid);
            db.ShoppingCartItem.RemoveRange(cartitems);
            db.ShoppingCart.RemoveRange(cart);
            db.SaveChanges();
        }

        public void CheckoutCart(string customerid)
        {
            ShoppingCart shoppingcart = db.ShoppingCart.FirstOrDefault(x => x.CustomerId == customerid && _shoppingcart.IsCheckOut == false);
            shoppingcart.IsCheckOut = true;
            shoppingcart.OrderTime = DateTime.Now;
            db.ShoppingCart.Update(shoppingcart);
            db.SaveChanges();

            Order orders = db.Order.FirstOrDefault(x => x.CustomerId == customerid && x.CheckOutComplete == false);

            if (orders == null)
            {
                Order neworder = new Order()
                {
                    CustomerId = customerid,
                    OrderDate = DateTime.Now,
                    CheckOutComplete = false,
                    OrderTotal = GetCartTotal()
                };
                db.Order.Add(neworder);
            }
            db.SaveChanges();

            Order orderid = db.Order.FirstOrDefault(x => x.CustomerId == customerid && x.CheckOutComplete == false);
            List<ShoppingCartItem> shoppingcartitems = db.ShoppingCartItem.Where(x => x.CustomerId == customerid).ToList();
            foreach (ShoppingCartItem shoppingcartitem in shoppingcartitems)
            {
                OrderDetails neworderdetails = new OrderDetails
                {
                    Quantity = shoppingcartitem.Quantity,
                    Price = shoppingcartitem.Products.ProductPrice,
                    ProductId = shoppingcartitem.Products.ProductId,
                    OrderId = orderid.OrderId,
                };
                db.OrderDetails.Add(neworderdetails);

                for (int i = 0; i < shoppingcartitem.Quantity; i++)
                {
                    string code = orderid.OrderId + " " + shoppingcartitem.ProductsId + " " + Guid.NewGuid().ToString();
                    ActivationCodes codes = new ActivationCodes(orderid.OrderId, code, shoppingcartitem.ProductsId);
                    db.ActivationCodes.Add(codes);
                }
            }
            orderid.CheckOutComplete = true;
            db.ShoppingCart.Update(shoppingcart);
            db.SaveChanges();
        }
    }
}