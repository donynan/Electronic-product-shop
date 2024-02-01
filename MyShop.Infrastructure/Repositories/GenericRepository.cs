using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Common;
using MyShop.Domain.Contracts;
using MyShop.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext= applicationDbContext;


        }
        public async Task<T> CreateAsync(T entity)
        {
  var Added = await _applicationDbContext.Set<T>().AddAsync(entity);

            await _applicationDbContext.SaveChangesAsync();
            return Added.Entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _applicationDbContext.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _applicationDbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> condition)
        {
            return await _applicationDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(condition);
        }
    }
}
