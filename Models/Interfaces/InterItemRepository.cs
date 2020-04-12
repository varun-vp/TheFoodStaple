using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoodStapleEx.Models.Interfaces
{
    public interface InterItemRepository
    {
        IEnumerable<Item> Items{ get; }
        IEnumerable<Item> PreferredItems { get; }
        Item GetItemById(int ItemId);
    }
}
