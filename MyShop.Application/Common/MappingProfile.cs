using AutoMapper;
using MyShop.Application.DTO.Brand;
using MyShop.Application.DTO.Category;
using MyShop.Application.DTO.Product;
using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Common
{
    public class MappingProfile:Profile
    {public MappingProfile()
        {
          
      
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();


            CreateMap<Brand, CreateBrandDTO>().ReverseMap();
            CreateMap<Brand, UpdateBrandDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();


            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.Category, opt => opt.MapFrom(source => source.Category.Name))
                .ForMember(x => x.Brand, opt => opt.MapFrom(source => source.Brand.Name));
        }
    }
    }

