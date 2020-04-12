using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFoodStapleEx.Models.Interfaces;

namespace TheFoodStapleEx.Components
{
    public class TypeMenu:ViewComponent
    {
        InterITypeRepository _typeRepository;

        public TypeMenu(InterITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var ITypes = _typeRepository.ITypes.OrderBy(p => p.ITypeName);
            return View(ITypes);
        }
    }
}
