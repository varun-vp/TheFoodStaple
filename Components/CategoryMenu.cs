using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFoodStapleEx.Models.Interfaces;

namespace TheFoodStapleEx.Components
{
    public class CategoryMenu:ViewComponent
    {
        InterCategoryRepository _categoryRepository;

        public CategoryMenu(InterCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var Categories = _categoryRepository.Categories.OrderBy(p => p.CategoryName);
            return View(Categories);
        }
    }
}
