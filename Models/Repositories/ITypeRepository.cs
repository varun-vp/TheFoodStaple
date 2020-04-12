using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFoodStapleEx.Data;
using TheFoodStapleEx.Models.Interfaces;

namespace TheFoodStapleEx.Models.Repositories
{
    public class ITypeRepository:InterITypeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ITypeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<IType> ITypes => _applicationDbContext.ITypes;

    }
}
