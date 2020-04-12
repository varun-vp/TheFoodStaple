using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoodStapleEx.Models
{
    /// <summary>
    /// 
    /// Stores the details of item 
    /// -->>Item Type 
    ///         ->Fruits 
    ///         ->Vegetables
    ///  -->>Category
    ///         ->Organic
    ///         ->Inorganic
    /// </summary>
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public String ItemName { get; set; }
        [Required]
        public float ItemPrice { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        /// <summary>
        /// -->>Store the value of item 
        ///     ->0 for not available 
        ///     ->1 for InStock
        /// </summary>            
        [Required]
        public bool InStock { get; set; }
        public bool IsPreferredItem { get; set; }
        [Required]
        public int ITypeId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IType IType { get; set; }
    }
}
