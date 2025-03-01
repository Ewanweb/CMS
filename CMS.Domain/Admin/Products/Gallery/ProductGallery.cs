using CMS.Domain.Admin.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Admin.Products.Gallery
{
    public class ProductGallery : BaseDomain
    {
        public int ProductId { get; set; }
        public string Image { get; set; } // فقط نام فایل را ذخیره کن
        public Product Product { get; set; }
    }
}
