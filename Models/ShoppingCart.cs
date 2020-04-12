using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFoodStapleEx.Data;

namespace TheFoodStapleEx.Models
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _applicationDbContext;
        /// <summary>
        /// Inject the service of DbContext in Constructor
        /// </summary>
        private ShoppingCart(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public string ShoppingCartId { get; set; }

        /// <summary>
        /// --> ShoppingCartItems 
        /// That helps us to show the cart the details in the cart
        /// See the usage of this is ViewComponents
        /// </summary>
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        /// <summary>
        ///  Shopping CartCart create the session if Cart is empty
        /// </summary>
        
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        /// <summary>
        /// AddToCart method add the item in the cart
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public void AddToCart(Item item, int amount)
        {
            var shoppingCartItem =
                    _applicationDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Item.ItemId == item.ItemId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Item = item,
                    Amount = 1
                };

                _applicationDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _applicationDbContext.SaveChanges();
        }

        /// <summary>
        ///                      Remove the items from the Cart
        /// </summary>
        
        public int RemoveFromCart(Item item)
        {
            var shoppingCartItem =
                    _applicationDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Item.ItemId == item.ItemId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _applicationDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _applicationDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _applicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Item)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _applicationDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _applicationDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _applicationDbContext.SaveChanges();
        }

        public float GetShoppingCartTotal()
        {
            float total = _applicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Item.ItemPrice * c.Amount).Sum();
            return total;
        }

    }
}
