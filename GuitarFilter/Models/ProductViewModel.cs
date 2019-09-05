﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarFilter.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}