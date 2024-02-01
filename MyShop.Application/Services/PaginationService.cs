using AutoMapper;
using MyShop.Application.InputModels;
using MyShop.Application.Services.Interface;
using MyShop.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services
{
    public class PaginationService<T, S> : IPaginationService<T, S> where T : class
    {
        private readonly IMapper _mapper;

        public PaginationService(IMapper IMapper)
        {
            _mapper = IMapper;


        }
        public PaginationVM<T> GetPagination(List<S> source, PaginationInputModel pagination)
        {
            var currentpage = pagination.PageNumber;
            var pagesize=pagination.PageSize;

            var totalNoOfRecords = source.Count;

            var totalpages=(int)Math.Ceiling(totalNoOfRecords/(double)pagesize);

            var result = source
                .Skip((pagination.PageNumber - 1) * (pagination.PageSize))
                .Take(pagination.PageSize)
                .ToList();
            var items = _mapper.Map<List<T>>(result);
            PaginationVM<T> paginationVM = new PaginationVM<T>(currentpage, totalpages, pagesize, totalNoOfRecords, items);

            return paginationVM;
        }
    }
}
