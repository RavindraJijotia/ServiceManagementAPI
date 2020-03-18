using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SM.Application.Interfaces;
using SM.Application.Models;
using SM.Application.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SM.WebApi.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceDetailService _serviceDetailService;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServiceDetailService serviceDetailService, 
            LinkGenerator linkGenerator,
            ILogger<ServicesController> logger)
        {
            _serviceDetailService = serviceDetailService;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IList<ServiceDetailModel>>> GetServices([FromQuery] ServiceDetailParameters serviceDetailParameters)
        {
            try
            {
                var result = await _serviceDetailService.GetAllServiceDetailsAsync(serviceDetailParameters);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IList<ServiceDetailModel>>> GetServiceDetail(int id)
        {
            try
            {
                var result = await _serviceDetailService.GetServiceDetailAsync(id);

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
        public async Task<ActionResult<ServiceDetailModel>> CreateService(ServiceCreateModel model)
        {
            try
            {
                var existing = await _serviceDetailService.GetServiceDetailByNameAsync(model.Name);
                if (existing != null)
                    return BadRequest($"Service {model.Name} already exist.");

                var result = await _serviceDetailService.AddServiceDetailAsync(model);

                var location = _linkGenerator.GetPathByAction("Get", "Service", new { id = result.Id });

                return Created(location, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceDetailModel>> UpdateService(int id, ServiceUpdateModel model)
        {
            try
            {
                if (await _serviceDetailService.GetServiceDetailAsync(id) == null)
                    return NotFound();

                var serviceExist = await _serviceDetailService.GetServiceDetailByNameAsync(model.Name);

                if (serviceExist != null && serviceExist.Id != id)
                    return BadRequest($"Service {model.Name} already exist.");

                return await _serviceDetailService.UpdateServiceDetail(id, model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DisableService(int id)
        {
            try
            {
                if (await _serviceDetailService.GetServiceDetailAsync(id) == null)
                    return NotFound();

                if (await _serviceDetailService.DisableServiceDetailAsync(id))
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