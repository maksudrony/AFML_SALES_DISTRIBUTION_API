using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFML_SALES_DISTRIBUTION_API.Controllers.Do_LiftingReport
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DistribWisePendingRptController : ControllerBase
    {
        private readonly IDistribWisePendingRptService _reportService;

        public DistribWisePendingRptController(IDistribWisePendingRptService service)
        {
            _reportService = service;
        }

        [HttpGet("distrib-wise-pending-rpt")]

        public async Task<ActionResult> GetDistribWisePendingRptReport(DateTime? fromDate, DateTime? toDate,
            int? channelId, int? zoneId, int? divisionId, int? areaId, int? territoryId, int? productId,
            int? distribId, int orderTypeId)
        {
            try
            {
                var data = await _reportService.GetDistribWisePendingRptServiceAsync(fromDate, toDate,
                           channelId, zoneId, divisionId, areaId, territoryId, productId,
                           distribId, orderTypeId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                    //message = ex.Message
                });
            }
        }
    }
}
