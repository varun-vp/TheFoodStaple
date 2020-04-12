using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoodStapleEx.Models
{
    public class IType
    {
        [Key]
        public int ITypeId { get; set; }
        [Required]
        public string ITypeName { get; set; }
        public string Description { get; set; }
        public virtual List<Item> Item{ get; set; }
    }
}
