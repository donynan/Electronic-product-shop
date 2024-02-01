using AutoMapper;
using MyShop.Application.DTO.Brand;
using MyShop.Application.Exceptions;
using MyShop.Application.Services.Interface;
using MyShop.Domain.Contracts;
using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services
{
    public class BrandService:IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        private readonly IMapper _mapper;
        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }


        public async Task<BrandDTO> CreateAsync(CreateBrandDTO createbrandDTO)
        {
            var validator=new CreateBrandDTOValidator();
            var validationresult= await validator.ValidateAsync(createbrandDTO);

            if (validationresult.Errors.Any())
            {
                throw new BadRequestException("Invalid Brand Input", validationresult);
            }
            var brand = _mapper.Map<Brand>(createbrandDTO);

            var createdEntity = await _brandRepository.CreateAsync(brand);

            var entity = _mapper.Map<BrandDTO>(createdEntity);

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(x => x.Id == id);
            await _brandRepository.DeleteAsync(brand);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllAsync()
        {
            var brands = await _brandRepository.GetAllAsync();

            return _mapper.Map<List<BrandDTO>>(brands);
        }

        public async Task<BrandDTO> GetByIdAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(x => x.Id == id);

            return _mapper.Map<BrandDTO>(brand);

        }

        public async Task UpdateAsync(UpdateBrandDTO updatebrandDTO)
        {
            var brand = _mapper.Map<Brand>(updatebrandDTO);


            await _brandRepository.UpdateAsync(brand);
        }
    }
}
