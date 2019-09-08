using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuitarFilter.Models
{
    public class FValueViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class FNameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FValueViewModel> Children { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        [Display(Name = "назва")]
        public string Name { get; set; }
        [Display(Name = "ціна", ResourceType = typeof(decimal))]
        public decimal Price { get; set; }
    }
    public class HomeViewModel
    {
        public List<FNameViewModel> Filters { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}