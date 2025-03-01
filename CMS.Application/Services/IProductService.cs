using CMS.Application.Common;
using CMS.Application.Products.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Admin.Products;

namespace CMS.Application.Services
{
    public interface IProductService
    {
        Task<string> HandleImageUpload(IFormFile? imageUpload, bool isGallery = false);
        Task<ServiceResult> CreateProduct(ProductDTO productDto);
        Task<ServiceResult> EditProduct(EditProductDto productDto);
        Task<ServiceResult> DeleteProduct(Product? product);
    }
}
