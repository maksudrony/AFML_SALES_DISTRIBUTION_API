using AFML_SALES_DISTRIBUTION_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFML_SALES_DISTRIBUTION_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChallanWiseDistribController : ControllerBase
    {
        private readonly IChallanWiseDistribService _service;

        public ChallanWiseDistribController(IChallanWiseDistribService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetChallanWiseDistrib(
            DateTime? fromDate,
            DateTime? toDate,
            int? channelId,
            string userId,
            string? search,
            int page = 1,
            int pageSize = 50)
        {
            try
            {
                return Ok(await _service.GetServiceAsync(fromDate, toDate, channelId, userId, search, page, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
