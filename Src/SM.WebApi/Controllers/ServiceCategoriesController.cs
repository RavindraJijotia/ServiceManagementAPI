using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SM.Application.Interfaces;
using SM.Application.Models;
using SM.Application.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SM.WebApi.Controllers
{
    [Route("api/ServiceCategories")]
    [ApiController]
    public class ServiceCategoriesController : ControllerBase
    {
        private readonly IServiceCategoryService _serviceCategoryService;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ServiceCategoriesController> _logger;

        public ServiceCategoriesController(IServiceCategoryService serviceCategoryService, 
            LinkGenerator linkGenerator, 
            ILogger<ServiceCategoriesController> logger)
        {
            _serviceCategoryService = serviceCategoryService;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IList<ServiceCategoryDetailModel>>> GetServiceCategories([FromQuery] ServiceCategoryParameters serviceCategoryParameters)
        {
            try
            {
                var result = await _serviceCategoryService.GetAllServiceCategoriesAsync(serviceCategoryParameters);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceCategoryDetailModel>> GetServiceCategory(int id, bool includeServices = false)
        {
            try
            {
                var result = await _serviceCategoryService.GetServiceCategoryAsync(id, includeServices);
                if (result == null) return NotFound();

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceCategoryModel>> CreateServiceCategory(ServiceCategoryModel model)
        {
            try
            {
                var existing = await _serviceCategoryService.GetServiceCategoryByNameAsync(model.Name);
                if (existing != null)
                    return BadRequest($"Service Category {model.Name} already exist.");
                
                var result = await _serviceCategoryService.AddServiceCategoryAsync(model);

                var location = _linkGenerator.GetPathByAction("Get", "ServiceCategories", new { id = result.Id });

                return Created(location, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceCategoryModel>> UpdateServiceCategory(int id, ServiceCategoryModel model)
        {
            try
            {
                var serviceCategory = await _serviceCategoryService.GetServiceCategoryAsync(id);

                if (serviceCategory == null)
                    return NotFound();

                var serviceCategoryExist = await _serviceCategoryService.GetServiceCategoryByNameAsync(model.Name);

                if (serviceCategoryExist != null && serviceCategoryExist.Id != id)
                    return BadRequest($"Service Category {model.Name} already exist.");

                model.Id = id;

                return await _serviceCategoryService.UpdateServiceCategory(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DisableServiceCategory(int id)
        {
            try
            {
                if (await _serviceCategoryService.GetServiceCategoryAsync(id) == null)
                    return NotFound();

                if (await _serviceCategoryService.DisableServiceCategoryAsync(id))
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest("Failed to delete record.");
        }
        
    }
}