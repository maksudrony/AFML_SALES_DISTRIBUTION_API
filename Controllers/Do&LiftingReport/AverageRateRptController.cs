using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFML_SALES_DISTRIBUTION_API.Controllers.Do_LiftingReport
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AverageRateRptController : ControllerBase
    {
        private readonly IAverageRateRptService _reportService;

        public AverageRateRptController(IAverageRateRptService service)
        {
            _reportService = service;
        }

        [HttpGet("avg-rate-rpt")]

        public async Task<ActionResult> GetAverageRateRptReport(DateTime? fromDate, DateTime? toDate, DateTime? dayFromDate, DateTime? dayToDate,
            int? channelId, int? channelTypeId, int typeId, int reportTypeId, string entryBy)
        {
            try
            {
                var data = await _reportService.GetAverageRateRptServiceAsync(fromDate, toDate, dayFromDate, dayToDate,
                           channelId, channelTypeId, typeId, reportTypeId, entryBy);
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
