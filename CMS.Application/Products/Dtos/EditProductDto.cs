using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Products.Dtos
{
    public class EditProductDto
    {
        public int Id { get; set; }

        [Required, MinLength(3, ErrorMessage = "حداقل 3 کاراکتر")]
        [DisplayName("نام محصول")]
        public string Name { get; set; }

        [DisplayName("اسلاگ")]
        public string Slug { get; set; }

        [Required, MinLength(10, ErrorMessage = "حداقل 10 کاراکتر")]
        [DisplayName("توضیحات")]
        public string Decription { get; set; }

        [Required]
        [DisplayName("قیمت")]
        public decimal Price { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "حداقل 1 دسته بندی را انتخاب کنید")]
        [DisplayName("دسته بندی")]
        public int CategoryId { get; set; }

        [DisplayName("دسته بندی")]
        public string? CategoryName { get; set; } // برای نمایش نام دسته‌بندی

        [DisplayName("تصویر")]
        public string? Image { get; set; } = "noimage.png";

        [DisplayName("آپلود تصویر")]
        public IFormFile? ImageUpload { get; set; }
    }
}
