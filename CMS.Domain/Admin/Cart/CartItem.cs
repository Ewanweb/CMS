using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Admin.Products;

namespace CMS.Domain.Admin.Cart
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName{ get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public decimal TotalPrice => Price * Quantity;

        public CartItem()
        {
            
        }

        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Price = product.Price;
            Quantity = 1;
            Image = product.Image;
        }
    }
}
