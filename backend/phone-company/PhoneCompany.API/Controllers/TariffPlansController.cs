using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests;
using PhoneCompany.Application.Requests.TariffPlan;

namespace PhoneCompany.API.Controllers
{
    [ApiController]
    [Route("tariff-plans")]
    public class TariffPlansController : ControllerBase
    {
        private readonly ITariffPlansService _tariffPlansService;

        public TariffPlansController(ITariffPlansService tariffPlansService)
        {
            _tariffPlansService = tariffPlansService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTariffPlans(
            [FromQuery] PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            var result = await _tariffPlansService.GetTariffPlansAsync(
                paginationParams,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{tariffPlanId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetTariffPlanById(
            [FromRoute] Guid tariffPlanId,
            CancellationToken cancellationToken)
        {
            var result = await _tariffPlansService.GetTariffPlanByIdAsync(
                tariffPlanId,
                cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTariffPlan(
            [FromBody] CreateTariffPlanRequest request,
            CancellationToken cancellationToken)
        {
            await _tariffPlansService.CreateTariffPlanAsync(
                request,
                cancellationToken);

            return Ok();
        }

        [HttpPut("{tariffPlanId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTariffPlan(
            [FromRoute] Guid tariffPlanId,
            [FromBody] UpdateTariffPlanRequest request,
            CancellationToken cancellationToken)
        {
            await _tariffPlansService.UpdateTariffPlanAsync(
                tariffPlanId,
                request,
                cancellationToken);

            return Ok();
        }

        [HttpDelete("{tariffPlanId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTariffPlan(
            [FromRoute] Guid tariffPlanId,
            CancellationToken cancellationToken)
        {
            await _tariffPlansService.DeleteTariffPlanAsync(
                tariffPlanId,
                cancellationToken);

            return Ok();
        }

        [HttpPost("{tariffPlanId}/services/{serviceId}")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddServiceToTariffPlan(
            Guid tariffPlanId,
            Guid serviceId,
            CancellationToken cancellationToken)
        {
            await _tariffPlansService.AddServiceToTariffPlanAsync(
                tariffPlanId,
                serviceId,
                cancellationToken);

            return Ok();
        }
    }
}