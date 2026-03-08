using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.PhoneNumber;

namespace PhoneCompany.API.Controllers
{
    [ApiController]
    [Route("phone-numbers")]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly IPhoneNumbersService _phoneNumbersService;

        public PhoneNumbersController(IPhoneNumbersService phoneNumbersService)
        {
            _phoneNumbersService = phoneNumbersService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPhoneNumbers(
            [FromQuery] PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            var result = await _phoneNumbersService.GetPhoneNumbersAsync(
                paginationParams,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{phoneNumberId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPhoneNumberById(
            [FromRoute] Guid phoneNumberId,
            CancellationToken cancellationToken)
        {
            var result = await _phoneNumbersService.GetPhoneNumberByIdAsync(
                phoneNumberId,
                cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePhoneNumber(
            CancellationToken cancellationToken)
        {
            await _phoneNumbersService.CreatePhoneNumberAsync(
                cancellationToken);

            return Ok();
        }

        [HttpPut("{phoneNumberId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePhoneNumber(
            [FromRoute] Guid phoneNumberId,
            [FromBody] UpdatePhoneNumberRequest request,
            CancellationToken cancellationToken)
        {
            await _phoneNumbersService.UpdatePhoneNumberAsync(
                phoneNumberId,
                request,
                cancellationToken);

            return Ok();
        }

        [HttpDelete("{phoneNumberId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePhoneNumber(
            [FromRoute] Guid phoneNumberId,
            CancellationToken cancellationToken)
        {
            await _phoneNumbersService.DeletePhoneNumberAsync(
                phoneNumberId,
                cancellationToken);

            return Ok();
        }

        [HttpPost("{phoneNumberId}/tariff-plans/{tariffPlanId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddTariffPlanToNumber(
            Guid phoneNumberId,
            Guid tariffPlanId, 
            CancellationToken cancellationToken)
        {
            await _phoneNumbersService.AddTariffPlanToNumberAsync(
                phoneNumberId,
                tariffPlanId,
                cancellationToken);

            return Ok();
        }
    }
}
