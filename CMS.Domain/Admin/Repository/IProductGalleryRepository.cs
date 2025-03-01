using CMS.Domain.Admin.Categories;
using CMS.Domain.Admin.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Admin.Products.Gallery;

namespace CMS.Domain.Admin.Repository
{
    public interface IProductGalleryRepository : IRepository<ProductGallery>
    {
        Task<bool> AddGalleryImagesAsync(List<ProductGallery> galleryImages);
        Task<Product> GetProductByIdAsync(int id);
        Task<ProductGallery> GetGalleryImageByIdAsync(int imageId);
        Task<IEnumerable<ProductGallery>> GetAllGallery(int productId);
    }
}
