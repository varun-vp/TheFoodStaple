using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoodStapleEx.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Item> PreferredItems { get; set; }
    }
}
