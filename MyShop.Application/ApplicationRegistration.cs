using MaxiShop.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Application.Common;
using MyShop.Application.Services;
using MyShop.Application.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application
{
    public static class ApplicationRegistration
    {  
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        
            services.AddScoped(typeof(IPaginationService<,>),typeof(PaginationService<,>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
