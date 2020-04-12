using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFoodStapleEx.Controllers
{
    public class UsersController : Controller
    {
        [Authorize(Roles="Admin,User")]
        public IActionResult User()
        {
            return View();
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult YourOrder()
        {
            return View();
        }
    }
}