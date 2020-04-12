
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFoodStapleEx.Models;
using TheFoodStapleEx.Models.Interfaces;
using TheFoodStapleEx.Models.ViewModel;

namespace TheFoodStapleEx.Controllers
{
    public class ItemsController : Controller
    {
        private readonly InterCategoryRepository _categoryRepository;
        private readonly InterItemRepository _itemRepository;
        private readonly InterITypeRepository _typeRepository;

        public ItemsController(InterCategoryRepository categoryRepository, InterItemRepository itemRepository,InterITypeRepository typeRepository)
        {
            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
            _typeRepository = typeRepository;

        }
        
        public ViewResult List(string category)
        {

            //ItemListViewModel vm = new ItemListViewModel();
            //ViewBag.typeName = "type Drinks using ViewBag";
            //vm.Currenttype = "type Drinks";
            //vm.Items = _itemRepository.Items;

            string _category = category;
            IEnumerable<Item> items;
            string currentCategory = string.Empty;
            if(string.IsNullOrEmpty(category))
            {
                items = _itemRepository.Items.OrderBy(n => n.ItemId);
                currentCategory = "All of the above";
            }
            else
            {
                if(string.Equals("Organic",_category, StringComparison.OrdinalIgnoreCase))
                {
                    items = _itemRepository.Items.Where(p => p.Category.CategoryName.Equals("Organic")).OrderBy(n => n.ItemName);
                }
                else if (string.Equals("Inorganic", _category, StringComparison.OrdinalIgnoreCase))
                {
                    items = _itemRepository.Items.Where(p => p.Category.CategoryName.Equals("Inorganic")).OrderBy(n => n.ItemName);
                }
                else if (string.Equals("Fruit", _category, StringComparison.OrdinalIgnoreCase))
                {
                    items = _itemRepository.Items.Where(p => p.IType.ITypeName.Equals("Fruit")).OrderBy(n => n.ItemName);
                }
                else 
                {
                    items = _itemRepository.Items.Where(p => p.IType.ITypeName.Equals("Vegetable")).OrderBy(n => n.ItemName);
                }
                
                currentCategory = _category;
            }
            var itemListViewModel = new ItemListViewModel
            {
                Items = items,
                CurrentCategory = currentCategory
            };
            return View(itemListViewModel);
        }


        public ViewResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Item> items;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                items = _itemRepository.Items.OrderBy(p => p.ItemId);
            }
            //else if (string.Equals("Fruits", _searchString, StringComparison.OrdinalIgnoreCase))
            //{
            //    items = _itemRepository.Items.Where(p => p.ItemName.ToLower().Contains(_searchString.ToLower()));
            //}
            //else if (string.Equals("Organic", _searchString, StringComparison.OrdinalIgnoreCase))
            //{
            //    items = _itemRepository.Items.Where(p => p.Category.CategoryName.ToLower().Contains(_searchString.ToLower()));
            //}
            else 
            {
                items = _itemRepository.Items.Where(p => p.ItemName.ToLower().Contains(_searchString.ToLower()));
            }

            return View("~/Views/Items/List.cshtml", new ItemListViewModel { Items = items, CurrentCategory = _searchString.ToLower() });
        }



        //public ViewResult ListType(string type)
        //{

        //    //ItemListViewModel vm = new ItemListViewModel();
        //    //ViewBag.CategoryName = "Category Drinks using ViewBag";
        //    //vm.CurrentCategory = "Category Drinks";
        //    //vm.Items = _itemRepository.Items;

        //    string _type = type;
        //    IEnumerable<Item> items;
        //    string currentType = string.Empty;
        //    if (string.IsNullOrEmpty(type))
        //    {
        //        items = _itemRepository.Items.OrderBy(n => n.ItemId);
        //        currentType = "All of the above";
        //    }
        //    else
        //    {
        //        if (string.Equals("Fruits", _type, StringComparison.OrdinalIgnoreCase))
        //        {
        //            items = _itemRepository.Items.Where(p => p.Category.CategoryName.Equals("Fruits")).OrderBy(n => n.ItemName);
        //        }
        //        else
        //        {
        //            items = _itemRepository.Items.Where(p => p.Category.CategoryName.Equals("Vegetables")).OrderBy(n => n.ItemName);
        //        }
        //        currentType = _type;
        //    }
        //    var itemListViewModel = new ItemListViewModel
        //    {
        //        Items = items,
        //        CurrentType = currentType
        //    };
        //    return View(itemListViewModel);
        //}
    }
}