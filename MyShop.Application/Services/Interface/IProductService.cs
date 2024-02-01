using MyShop.Application.DTO.Category;
using MyShop.Application.DTO.Product;
using MyShop.Application.InputModels;
using MyShop.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services.Interface
{
    public interface IProductService
    {
        Task<PaginationVM<ProductDTO>> GetPagination(PaginationInputModel paginationInputModel);
        Task<ProductDTO> GetByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<IEnumerable<ProductDTO>> GetAllByFilterAsync(int? categoryid,int? brandid);
        Task<ProductDTO> CreateAsync(CreateProductDTO categoryDTO);
        Task UpdateAsync(UpdateProductDTO updatecategoryDTO);
        Task DeleteAsync(int id);
    }
}
