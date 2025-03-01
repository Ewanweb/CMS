using CMS.Domain.Admin.Categories;
using CMS.Domain.Admin.Products.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Admin.Products
{
    public class Product : BaseDomain
    {
        [Required, MinLength(3, ErrorMessage = "حداقل 3 کاراکتر")]
        [DisplayName("نام محصول")]
        public string Name { get; set; }

        [DisplayName("اسلاگ")]
        public string Slug { get; set; }

        [Required, MinLength(10, ErrorMessage = "حداقل 10 کاراکتر")]
        [DisplayName("توضیحات")]
        public string Decription { get; set; }

        [Required]
        [Column(TypeName = "decimal(8, 2)")]
        [DisplayName("قیمت")]
        public decimal Price { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "حداقل 1 دسته بندی را انتخاب کنید")]
        [DisplayName("دسته بندی")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [DisplayName("تصویر")]
        public string Image { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtention]
        public IFormFile ImageUpload { get; set; }

    }
}
