using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.Service;

namespace PhoneCompany.API.Controllers
{
    [ApiController]
    [Route("services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetServices(
            [FromQuery] PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            var result = await _servicesService.GetServicesAsync(
                paginationParams,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{serviceId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetServiceById(
            [FromRoute] Guid serviceId,
            CancellationToken cancellationToken)
        {
            var result = await _servicesService.GetServiceByIdAsync(
                serviceId,
                cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateService(
            [FromBody] CreateServiceRequest request,
            CancellationToken cancellationToken)
        {
            await _servicesService.CreateServiceAsync(
                request,
                cancellationToken);

            return Ok();
        }

        [HttpPut("{serviceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateService(
            [FromRoute] Guid serviceId,
            [FromBody] UpdateServiceRequest request,
            CancellationToken cancellationToken)
        {
            await _servicesService.UpdateServiceAsync(
                serviceId,
                request,
                cancellationToken);

            return Ok();
        }

        [HttpDelete("{serviceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteService(
            [FromRoute] Guid serviceId,
            CancellationToken cancellationToken)
        {
            await _servicesService.DeleteServiceAsync(
                serviceId,
                cancellationToken);

            return Ok();
        }
    }
}