using System.Collections.Generic;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.DTOs;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces
{
    public interface ICommonParameterService
    {
        Task<List<CommonParameterDto>> GetChannelsServiceAsync(string userId);
        Task<List<CommonParameterDto>> GetZonesServiceAsync(string userId, decimal channelId);
        Task<List<CommonParameterDto>> GetDivisionsServiceAsync(string userId, decimal zoneId);
        Task<List<CommonParameterDto>> GetAreasServiceAsync(string userId, decimal divisionId);
        Task<List<CommonParameterDto>> GetTerritoriesServiceAsync(string userId, decimal areaId);
        Task<List<CommonParameterDto>> GetProductCategoriesServiceAsync();
        Task<List<CommonParameterDto>> GetProductDetailServiceAsync();
        Task<List<CommonParameterDto>> GetSalesChannelTypeServiceAsync(string userId);
        Task<List<CommonParameterDto>> GetQuantityTypeServiceAsync();
        Task<List<CommonParameterDto>> GetReportTypeServiceAsync();
        Task<List<CommonParameterDto>> GetTimeManagementServiceAsync();
        Task<List<CommonParameterDto>> GetChallanDistributorServiceAsync(DateTime? fromDate, DateTime? toDate,
            decimal? channelId, string userId);
        Task<List<CommonParameterDto>> GetChannelWiseDistributorServiceAsync(decimal? channelId, string userId);
    }
}