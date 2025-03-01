using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common;

namespace CMS.Application.Services
{
    public interface IProductGalleryService
    {
        Task<ServiceResult> AddProductGallery(int productId, List<IFormFile> images);
        Task<ServiceResult> DeleteProductGallery(int imageId);
    }
}
