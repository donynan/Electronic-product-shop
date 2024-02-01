using MaxiShop.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Application.Services;
using MyShop.Application.Services.Interface;
using MyShop.Domain.Contracts;
using MyShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection services)
        {
            
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
