﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Admin.Products;

namespace CMS.Domain.Admin.Categories
{
    public class Category : BaseDomain
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<Product> Products { get; set; }
    }
}
