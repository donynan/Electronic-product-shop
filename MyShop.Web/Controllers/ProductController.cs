using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Common;
using MyShop.Application.DTO.Category;
using MyShop.Application.Services.Interface;
using static MyShop.Application.ApplicationConstants.CommonMessaage;
using System.Net;
using MyShop.Application.DTO.Product;
using MyShop.Application.InputModels;
using Microsoft.AspNetCore.Authorization;

namespace MyShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        protected APIResponse _response;

        public ProductController(IProductService productService)
        {
            _productService = productService;
            _response = new APIResponse();
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("Filter")]
        public async Task<ActionResult<APIResponse>> Getfilter(int? categoryid,int? brandid)
        {


            try
            {
                var categories = await _productService.GetAllByFilterAsync(categoryid,brandid);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = categories;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }
            return _response;
        }

        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<APIResponse>> GetPagination(PaginationInputModel paginationInputModel)
        {


            try
            {
                var categories = await _productService.GetPagination(paginationInputModel);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = categories;
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
        public async Task<ActionResult<APIResponse>> Get()
        {


            try
            {
                var categories = await _productService.GetAllAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = categories;
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
                var category = await _productService.GetByIdAsync(id);

                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessage.RecordNotFound;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = category;

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
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateProductDTO dTO)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.AddError(ModelState.ToString());
                    _response.DisplayMessage = CommonMessage.CreateOperationFailed;

                    return Ok(_response);
                }
                var result = await _productService.CreateAsync(dTO);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.CreateOperationSuccess;

                _response.Result = result;
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
        public async Task<ActionResult> Update([FromBody] UpdateProductDTO product)
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

                await _productService.UpdateAsync(product);

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


            var result = await _productService.GetByIdAsync(id);
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
            await _productService.DeleteAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            _response.DisplayMessage = CommonMessage.DeleteOperationSuccess;
            return Ok(_response);
        }
    }
}
