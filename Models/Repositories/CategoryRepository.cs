using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFoodStapleEx.Data;
using TheFoodStapleEx.Models.Interfaces;

namespace TheFoodStapleEx.Models.Repositories
{
    public class CategoryRepository:InterCategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Category> Categories => _applicationDbContext.Categories;
    }
}
