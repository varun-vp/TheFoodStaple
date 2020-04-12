using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TheFoodStapleEx.Models.Interfaces;
using TheFoodStapleEx.Data;

namespace TheFoodStapleEx.Models.Repositories
{
    public class ItemRepository : InterItemRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ItemRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Item> Items => _applicationDbContext.Items.Include(c => c.Category);

        public IEnumerable<Item> PreferredItems => _applicationDbContext.Items.Where(p => p.IsPreferredItem).Include(c => c.Category);

        public Item GetItemById(int ItemId) => _applicationDbContext.Items.FirstOrDefault(p => p.ItemId == ItemId);

    }
}
