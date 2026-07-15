using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Microsoft.AspNetCore.Mvc;

namespace AFML_SALES_DISTRIBUTION_API.Controllers.Do_LiftingReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayWiseDelRptController : ControllerBase
    {
        private readonly IDayWiseDelRptService _reportService;

        public DayWiseDelRptController(IDayWiseDelRptService service)
        {
            _reportService = service;
        }

        [HttpGet("day-wise-del-rpt-mst")]
        public async Task<ActionResult> GetMstReport(DateTime fromDate, DateTime toDate, int fromTime,
            int toTime, int? channelId, int? distribId, string entryBy)
        {
            try
            {
                var data = await _reportService.GetMstServiceAsync(fromDate, toDate, fromTime, toTime,
                    channelId, distribId, entryBy);

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

        [HttpGet("day-wise-del-rpt-dtl")]
        public async Task<ActionResult> GetDtlReport(long dcId)
        {
            try
            {
                var data = await _reportService.GetDtlServiceAsync(dcId);

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