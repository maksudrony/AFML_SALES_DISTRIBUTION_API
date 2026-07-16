using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AFML_SALES_DISTRIBUTION_API.Controllers.Do_LiftingReport
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductWiseDeliveryReportController : ControllerBase
    {
        private readonly IProductWiseDeliveryReportService _reportService;

        public ProductWiseDeliveryReportController(IProductWiseDeliveryReportService service)
        {
            _reportService = service;
        }

        [HttpGet("product-wise-delivery-report")]
        public async Task<IActionResult> GetProductWiseDeliveryReport (
            DateTime? fromDate,
            DateTime? todate, 
            string? entryBy, 
            int? productId
            )
        {
            //if (!fromDate.HasValue || !todate.HasValue)
            //{
            //    return BadRequest(new { message = "Opps! Required parameters missing! fromDate, toDate  are strictly mandatory!" });
            //}
            try
            {
                var data = await _reportService.GetIProductWiseDeliveryReportServiceAsync(fromDate, todate,
                    entryBy, productId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new {
                    error = ex.Message
                });

            }
        }


    }
}
