using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Common;
using MyShop.Application.DTO.Category;
using MyShop.Application.Services.Interface;
using static MyShop.Application.ApplicationConstants.CommonMessaage;
using System.Net;
using MyShop.Application.DTO.Brand;
using MyShop.Application.Exceptions;

namespace MyShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        protected APIResponse _response;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
            _response = new APIResponse();
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> Get()
        {


            try
            {
                var brands = await _brandService.GetAllAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brands;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }
            return _response;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]

        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {



            try
            {
                var brands = await _brandService.GetByIdAsync(id);

                if (brands == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.RecordNotFound;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brands;

                return Ok(_response);
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
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateBrandDTO dTO)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                  
                    _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return Ok(_response);
                }
                var result = await _brandService.CreateAsync(dTO);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.CreateOperationSuccess;

                _response.Result = result;
            }

            catch (BadRequestException ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
        
                _response.DisplayMessage = CommonMessage.CreateOperationFailed;
                _response.AddWarning(ex.Message);
                _response.AddError(CommonMessage.SystemError);
                _response.Result = ex.ValidationsErrors;
            }
           
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
                _response.DisplayMessage = CommonMessage.CreateOperationFailed;
            }


            return Ok(_response);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateBrandDTO brandsDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.AddError(ModelState.ToString());
                    _response.DisplayMessage = CommonMessage.UpdateOperationFailed;

                    return Ok(_response);
                }

                await _brandService.UpdateAsync(brandsDto);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.UpdateOperationSuccess;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.AddError(ModelState.ToString());
                _response.DisplayMessage = CommonMessage.UpdateOperationFailed;
            }



            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {


            var result = await _brandService.GetByIdAsync(id);
            try
            {
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;

                    _response.DisplayMessage = CommonMessage.DeleteOperationFailed;

                    return Ok(_response);
                }
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.NotFound;

                _response.DisplayMessage = CommonMessage.DeleteOperationFailed;

            }
            await _brandService.DeleteAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            _response.DisplayMessage = CommonMessage.DeleteOperationSuccess;
            return Ok(_response);
        }
    }
}