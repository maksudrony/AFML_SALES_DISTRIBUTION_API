using System;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AFML_SALES_DISTRIBUTION_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummaryImsReportController : ControllerBase
    {
        private readonly ISummaryImsReportService _reportService;
        public SummaryImsReportController(ISummaryImsReportService service) 
            => _reportService = service;

        [HttpGet("summary-ims-report")]
        public async Task<IActionResult> GetSummaryImsReport(
            string fromDate,
            string toDate,
            decimal? prodCatId,
            string entryBy,
            decimal? channelId,
            decimal? zoneId,
            decimal? divisionId,
            decimal? areaId,
            decimal? territoryId
            )
        {
            if (string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate) ||string.IsNullOrEmpty(entryBy) || !channelId.HasValue)
            {
                return BadRequest(new { message = "Opps! Required parameters missing! fromDate, toDate, entryBy, and channelId are strictly mandatory!" });
            }

            try
            {
                var data = await _reportService.GetSummaryImsReportServiceAsync(fromDate, toDate, prodCatId,
                    entryBy, channelId.Value, zoneId, divisionId, areaId, territoryId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new 
                {
                    message = ex.Message 
                });
            }
        }
    }
}