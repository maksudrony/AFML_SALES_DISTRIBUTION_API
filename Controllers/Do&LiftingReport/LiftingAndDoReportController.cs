using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFML_SALES_DISTRIBUTION_API.Controllers.Do_LiftingReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiftingAndDoReportController : ControllerBase
    {
        private readonly ILiftingAndDoReportService _reportService;

        public LiftingAndDoReportController (ILiftingAndDoReportService service)
        {
            _reportService = service;
        }

        [HttpGet("lifting-and-do-report")]

        public async Task<ActionResult> GetLiftingAndDoReport (DateTime? fromDate, DateTime? toDate, DateTime? dayFromDate, DateTime? dayToDate,
            int? channelId, int? channelTypeId, int typeId, int reportTypeId, string entryBy)
        {
            try
            {
                var data = await _reportService.GetLiftingAndDoReportServiceAsync(fromDate, toDate, dayFromDate, dayToDate,
                           channelId, channelTypeId, typeId, reportTypeId, entryBy);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
    }
}
