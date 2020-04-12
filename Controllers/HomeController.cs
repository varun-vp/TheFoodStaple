using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheFoodStapleEx.Models;
using TheFoodStapleEx.Models.Interfaces;
using TheFoodStapleEx.Models.ViewModels;

namespace TheFoodStapleEx.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InterItemRepository _itemRepository;

        public HomeController(ILogger<HomeController> logger, InterItemRepository itemRepositrory)
        {
            _logger = logger;
            _itemRepository = itemRepositrory;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel();

            homeViewModel.PreferredItems = _itemRepository.PreferredItems;
            
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
