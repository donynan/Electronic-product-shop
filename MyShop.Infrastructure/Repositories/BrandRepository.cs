using MyShop.Domain.Contracts;
using MyShop.Domain.Models;
using MyShop.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand>,IBrandRepository
    {
        public BrandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task UpdateAsync(Brand brand)
        {



            if (brand != null)
            {


                _applicationDbContext.Update(brand);
                await _applicationDbContext.SaveChangesAsync();
            }



        }
    }
}
