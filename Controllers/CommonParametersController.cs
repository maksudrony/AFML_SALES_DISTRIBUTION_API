using AFML_SALES_DISTRIBUTION_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AFML_SALES_DISTRIBUTION_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommonParametersController : ControllerBase
    {
        private readonly ICommonParameterService _paramService;
        public CommonParametersController(ICommonParameterService paramService) => _paramService = paramService;

        [HttpGet("channels")]
        public async Task<IActionResult> GetChannels(string userId)
        {
            try 
            {
                return Ok(await _paramService.GetChannelsServiceAsync(userId)); 
            }
            catch (Exception ex) 
            { 
                return BadRequest(new 
                { 
                    error = ex.Message 
                }); 
            }
        }

        [HttpGet("zones")]
        public async Task<IActionResult> GetZones(string userId, decimal channelId)
        {
            try 
            { 
                return Ok(await _paramService.GetZonesServiceAsync(userId, channelId)); 
            }
            catch (Exception ex) 
            { 
                return BadRequest(new 
                { 
                    error = ex.Message 
                }); 
            }
        }

        [HttpGet("divisions")]
        public async Task<IActionResult> GetDivisions(string userId, decimal zoneId)
        {
            try 
            { 
                return Ok(await _paramService.GetDivisionsServiceAsync(userId, zoneId)); 
            }
            catch (Exception ex) 
            { 
                return BadRequest(new 
                { 
                    error = ex.Message 
                }); 
            }
        }

        [HttpGet("areas")]
        public async Task<IActionResult> GetAreas(string userId, decimal divisionId)
        {
            try 
            { 
                return Ok(await _paramService.GetAreasServiceAsync(userId, divisionId)); 
            }
            catch (Exception ex) 
            { 
                return BadRequest(new 
                { 
                    error = ex.Message 
                }); 
            }
        }

        [HttpGet("territories")]
        public async Task<IActionResult> GetTerritories(string userId, decimal areaId)
        {
            try 
            { 
                return Ok(await _paramService.GetTerritoriesServiceAsync(userId, areaId)); 
            }
            catch (Exception ex) 
            { 
                return BadRequest(new 
                { 
                    error = ex.Message 
                }); 
            }
        }

        [HttpGet("product-categories")]
        public async Task<IActionResult> GetProductCategories()
        {
            try 
            { 
                return Ok(await _paramService.GetProductCategoriesServiceAsync()); 
            }
            catch (Exception ex) 
            { 
                return BadRequest(new 
                { 
                    error = ex.Message 
                }); 
            }
        }

        [HttpGet("product-detail")]
        public async Task<IActionResult> GetProductDetail()
        {
            try
            {
                return Ok(await _paramService.GetProductDetailServiceAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new 
                { 
                    error = ex.Message 
                });
            }
        }

        [HttpGet("sales-channel-type")]
        public async Task<IActionResult> GetSalesChannelType(string userId)
        {
            try
            {
                return Ok(await _paramService.GetSalesChannelTypeServiceAsync(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(new 
                { 
                    error = ex.Message 
                });
            }
        }

        [HttpGet("quantity-type")]
        public async Task<IActionResult> GetQuantityType()
        {
            try
            {
                return Ok(await _paramService.GetQuantityTypeServiceAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new 
                { 
                    error = ex.Message 
                });
            }
        }

        [HttpGet("report-type")]
        public async Task<IActionResult> GetReportType()
        {
            try
            {
                return Ok(await _paramService.GetReportTypeServiceAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new 
                { 
                    error = ex.Message 
                });
            }
        }

        [HttpGet("time-management")]
        public async Task<IActionResult> GetTimeManagement()
        {
            try
            {
                return Ok(await _paramService.GetTimeManagementServiceAsync());
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