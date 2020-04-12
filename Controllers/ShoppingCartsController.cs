using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFoodStapleEx.Models;
using TheFoodStapleEx.Models.Interfaces;
using TheFoodStapleEx.Models.ViewModels;

namespace TheFoodStapleEx.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly InterItemRepository _itemRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartsController(InterItemRepository itemRepository, ShoppingCart shoppingCart)
        {
            _itemRepository = itemRepository;
            _shoppingCart = shoppingCart;
        }

        [Authorize]
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }


        public IActionResult AddToShoppingCart(int itemId)
        {
            var selectedItem = _itemRepository.Items.FirstOrDefault(p => p.ItemId == itemId);
            if (selectedItem != null)
            {
                _shoppingCart.AddToCart(selectedItem, 1);
            }
            return Redirect("~/Home/Index");
        }
        
        public RedirectToActionResult RemoveFromShoppingCart(int itemId)
        {
            var selectedItem = _itemRepository.Items.FirstOrDefault(p => p.ItemId == itemId);
            if (selectedItem != null)
            {
                _shoppingCart.RemoveFromCart(selectedItem);
            }
            return RedirectToAction("Index");
        }
        
    }
}