using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.SmallCartView.ViewModels
{
    public class SmallCartViewModel
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string Image { get; set; }

        public decimal TotalPrice => Price * Quantity;
    }
}
