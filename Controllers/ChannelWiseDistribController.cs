using System;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFML_SALES_DISTRIBUTION_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChannelWiseDistribController : ControllerBase
    {
        private readonly IChannelWiseDistribService _service;

        public ChannelWiseDistribController(IChannelWiseDistribService service) => _service = service;

        [HttpGet("channel-wise-distrib")]
        public async Task<IActionResult> Get(
            [FromQuery] int? channelId,
            [FromQuery] string userId,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
        {
            try
            {
                return Ok(await _service.GetServiceAsync(channelId, userId, search, page, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}