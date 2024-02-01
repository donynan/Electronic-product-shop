using MyShop.Application.InputModels;
using MyShop.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services.Interface
{
    public interface IPaginationService<T,S> where T : class
    {
        PaginationVM<T> GetPagination(List<S> source,PaginationInputModel pagination);
    }
}
