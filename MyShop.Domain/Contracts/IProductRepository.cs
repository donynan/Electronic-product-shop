using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Domain.Contracts
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<List<Product>> GetAllProductAsync();


        Task<Product> GetDetailsIdAsync(int id);
        Task UpdateAsync(Product product);
    }
}
