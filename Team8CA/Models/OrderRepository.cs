//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Team8CA.DataAccess;

//namespace Team8CA.Models
//{
//    public class OrderRepository : IOrderRepository
//    {
//        private readonly AppDbContext db;
//        private readonly ShoppingCart _shoppingCart;

//        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
//        {
//            db = appDbContext;
//            _shoppingCart = shoppingCart;
//        }

//        public void CreateOrder(Order order)
//        {
//            order.OrderDate = DateTime.Now;
//            order.OrderTotal = _shoppingCart.GetCartTotal();
//            db.Order.Add(order);
//            db.SaveChanges();

//            var shoppingcartitems = _shoppingCart.GetShoppingCartItems();

//            foreach (var shoppingcartitem in shoppingcartitems)
//            {
//                var orderdetails = new OrderDetails
//                {
//                    Quantity = shoppingcartitem.Quantity,
//                    Price = shoppingcartitem.Products.ProductPrice,
//                    ProductId = shoppingcartitem.Products.ProductId,
//                    OrderId = order.OrderId
//                };

//                db.OrderDetails.Add(orderdetails);
//            }
//            db.SaveChanges();
//        }
//    }
//}
