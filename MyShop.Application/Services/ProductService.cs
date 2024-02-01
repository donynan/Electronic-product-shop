using AutoMapper;
using MyShop.Application.DTO.Category;
using MyShop.Application.DTO.Product;
using MyShop.Application.InputModels;
using MyShop.Application.Services.Interface;
using MyShop.Application.ViewModel;
using MyShop.Domain.Contracts;
using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services
{
    public class ProductService : IProductService
    {
        
        private readonly IProductRepository _productRepository;

        private readonly IPaginationService<ProductDTO, Product> _pagerService;

    private readonly IMapper _mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper, IPaginationService<ProductDTO, Product> pagerService)
    {
        _productRepository = productRepository;
        _mapper = mapper;

            _pagerService = pagerService;
    }
        


    public async Task<ProductDTO> CreateAsync(CreateProductDTO createpostDTO)
    {
        var product = _mapper.Map<Product>(createpostDTO);

        var createdEntity = await _productRepository.CreateAsync(product);

        var entity = _mapper.Map<ProductDTO>(createdEntity);
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(x => x.Id == id);
        await _productRepository.DeleteAsync(product);
    }

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        var products = await _productRepository.GetAllProductAsync();

        return _mapper.Map<List<ProductDTO>>(products);
    }

        public async Task<IEnumerable<ProductDTO>> GetAllByFilterAsync(int? categoryid, int? brandid)
        {
            var data = await _productRepository.GetAllProductAsync();

            IEnumerable<Product> query = data;

            if (categoryid > 0) { 
            
             query= query.Where(x=>x.CategoryId==categoryid);
            
            }

            if(brandid > 0)
            {
                query= query.Where(x=>x.BrandId==brandid);
            }

            var result=_mapper.Map<List<ProductDTO>>(query);

            return result;
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetDetailsIdAsync( id);

        return _mapper.Map<ProductDTO>(product);

    }

        public async Task<PaginationVM<ProductDTO>> GetPagination(PaginationInputModel paginationInputModel)
        {
            var source = await _productRepository.GetAllProductAsync();
            var result = _pagerService.GetPagination(source, paginationInputModel);
            return result;
        }

        public async Task UpdateAsync(UpdateProductDTO updatecategoryDTO)
    {
        var product = _mapper.Map<Product>(updatecategoryDTO);


        await _productRepository.UpdateAsync(product);
    }
}
}
