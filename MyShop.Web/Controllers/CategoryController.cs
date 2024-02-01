using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Common;
using MyShop.Application.DTO.Category;
using MyShop.Application.Services.Interface;
using MyShop.Domain.Contracts;
using MyShop.Domain.Models;
using MyShop.Infrastructure.DbContexts;
using System.Net;
using static MyShop.Application.ApplicationConstants.CommonMessaage;

namespace MyShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
      
        private readonly ICategoryService _categoryService;
        protected APIResponse _response;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _response = new APIResponse();
        }

       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task <ActionResult<APIResponse>> Get()
        {
            

            try
            {
                var categories = await _categoryService.GetAllAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = categories;
            }
            catch(Exception )
            {
                _response.StatusCode=HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }
            return _response;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
      
        [HttpGet]
        [Route("Details")]
        public async Task <ActionResult<APIResponse>> Get(int id)
        {
           


            try
            {
                var category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage=CommonMessage.RecordNotFound;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result=category;

                return Ok(_response);
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }

            return Ok (_response);



        }


        [ProducesResponseType(StatusCodes.Status200OK)]
     
        [HttpPost]
        public async Task <ActionResult<APIResponse>> Create([FromBody] CreateCategoryDTO dTO)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    _response.AddError(ModelState.ToString());
                    _response.DisplayMessage = CommonMessage.CreateOperationFailed;

                    return Ok(_response);
                }
                var result = await _categoryService.CreateAsync(dTO);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.CreateOperationSuccess;

                _response.Result = result;
            }
            catch (Exception )
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
        public async Task <ActionResult> Update([FromBody] UpdateCategoryDTO category)
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

                await _categoryService.UpdateAsync(category);

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
        public async Task <ActionResult<APIResponse>> Delete(int id)
        {
            

            var result = await _categoryService.GetByIdAsync(id);
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
           await _categoryService.DeleteAsync( id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            _response.DisplayMessage = CommonMessage.DeleteOperationSuccess;
            return Ok(_response);
        }

    }
}
