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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task UpdateAsync(Category category)
        {

        

            if (category!=null  )
            {
               

                _applicationDbContext.Update(category);
                await _applicationDbContext.SaveChangesAsync();
            }
            
      
            
        }
    }
}
