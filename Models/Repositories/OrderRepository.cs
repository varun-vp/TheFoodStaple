using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFoodStapleEx.Data;
using TheFoodStapleEx.Models.Interfaces;

namespace TheFoodStapleEx.Models.Repositories
{
    public class OrderRepository:InterOrderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ShoppingCart _shoppingCart;


        public OrderRepository(ApplicationDbContext applicationDbContext, ShoppingCart shoppingCart)
        {
            _applicationDbContext = applicationDbContext;
            _shoppingCart = shoppingCart;
        }


        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _applicationDbContext.Orders.Add(order);
            _applicationDbContext.SaveChanges();
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    ItemId = shoppingCartItem.Item.ItemId,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Item.ItemPrice
                };

                _applicationDbContext.OrderDetails.Add(orderDetail);
            }
            _applicationDbContext.SaveChanges();
        }
    }
}
