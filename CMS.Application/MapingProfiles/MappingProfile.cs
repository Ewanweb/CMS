using AutoMapper;
using CMS.Application.Pages.Dtos;
using CMS.Application.Products.Dtos;
using CMS.Domain.Admin.Pages;
using CMS.Domain.Admin.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CMS.Application.MapingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product Mapping
            CreateMap<ProductDTO, Product>().ReverseMap();

            CreateMap<EditProductDto, Product>().ReverseMap();

            // Page Mapping
            CreateMap<Page, PageDto>().ReverseMap();

        }
    }
}
