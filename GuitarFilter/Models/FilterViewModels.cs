using System;
using System.Collections.Generic;
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
}