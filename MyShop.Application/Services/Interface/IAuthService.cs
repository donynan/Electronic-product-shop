using MaxiShop.Application.InputModels;
using Microsoft.AspNetCore.Identity;
using MyShop.Application.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services.Interface
{
    public interface IAuthService
    {
        Task<IEnumerable<IdentityError>> Register(Register register);
        Task<object> Login(Login login);
    }
}
