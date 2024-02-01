using MaxiShop.Application.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Common;
using MyShop.Application.InputModels;
using MyShop.Application.Services.Interface;

using System.Net;
using static MyShop.Application.ApplicationConstants. CommonMessaage;

namespace MyShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected APIResponse _response;

        public UserController(IAuthService authService)
        {
            _authService = authService;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<APIResponse>> Register(Register register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommonMessage.RegistrationFailed);
                    return _response;
                }

                var result = await _authService.Register(register);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                _response.DisplayMessage = CommonMessage.RegistrationSuccess;
                _response.Result = result;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<APIResponse>> Login(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommonMessage.RegistrationFailed);
                    return _response;
                }

                var result = await _authService.Login(login);

                if (result is string)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessage.LoginFailed;
                    _response.Result = result;
                    return _response;

                }

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.DisplayMessage = CommonMessage.LoginSuccess;
                _response.Result = result;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok(_response);
        }
    }

}

