using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoodStapleEx.Models.Interfaces
{
    public interface InterCategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
