using Microsoft.EntityFrameworkCore;
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
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbcontext) : base(dbcontext) {
        
        
        
        
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
        return await _applicationDbContext.products.Include(x=>x.Category).Include(x=>x.Brand).AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetDetailsIdAsync(int id)
        {
            return await _applicationDbContext.products.Include(x => x.Category).Include(x => x.Brand).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
           _applicationDbContext.Update(product);
            await _applicationDbContext.SaveChangesAsync();

        }
    }
}
