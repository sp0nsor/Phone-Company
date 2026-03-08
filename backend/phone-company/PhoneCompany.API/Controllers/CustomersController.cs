using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.Customer;

namespace PhoneCompany.API.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCustomers(
            [FromQuery] PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            var result = await _customersService.GetCustomersAsync(
                paginationParams,
                cancellationToken);

            return Ok(result);
        }


        [HttpGet("{customerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCustomerById(
            [FromRoute] Guid customerId, 
            CancellationToken cancellationToken)
        {
            var result = await _customersService.GetCustomerByIdAsync(
                customerId,
                cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{customerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomer(
            [FromRoute] Guid customerId,
            CancellationToken cancellationToken)
        {
            await _customersService.DeleteCustomerAsync(
                customerId,
                cancellationToken);

            return Ok();
        }

        [HttpPut("{customerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCustmer(
            [FromRoute] Guid customerId,
            [FromBody] UpdateCustomerRequest updateCustomerRequest,
            CancellationToken cancellationToken)
        {
            await _customersService.UpdateCustomerAsync(
                customerId,
                updateCustomerRequest,
                cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomer(
            [FromBody] CreateCustomerRequest createCustomerRequest,
            CancellationToken cancellationToken)
        {
            await _customersService.CreateCustomerAsync(
                createCustomerRequest, 
                cancellationToken);

            return Ok();
        }
    }
}
