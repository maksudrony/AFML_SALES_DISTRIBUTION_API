using System.Collections.Generic;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.DTOs;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces
{
    public interface ICommonParameterRepository
    {
        Task<List<CommonParameterDto>> GetChannelsFromDbAsync(string userId);
        Task<List<CommonParameterDto>> GetZonesFromDbAsync(string userId, decimal channelId);
        Task<List<CommonParameterDto>> GetDivisionsFromDbAsync(string userId, decimal zoneId);
        Task<List<CommonParameterDto>> GetAreasFromDbAsync(string userId, decimal divisionId);
        Task<List<CommonParameterDto>> GetTerritoriesFromDbAsync(string userId, decimal areaId);
        Task<List<CommonParameterDto>> GetProductCategoriesFromDbAsync();
        Task<List<CommonParameterDto>> GetProductDetailFromDbAsync();
        Task<List<CommonParameterDto>> GetSalesChannelTypeFromDbAsync(string userId);
        Task<List<CommonParameterDto>> GetQuantityTypeFromDbAsync();
        Task<List<CommonParameterDto>> GetReportTypeFromDbAsync();
        Task<List<CommonParameterDto>> GetTimeManagementFromDbAsync();
        Task<List<CommonParameterDto>> GetChallanDistributorFromDbAsync(DateTime? fromDate, DateTime? toDate,
            decimal? channelId, string userId);
        Task<List<CommonParameterDto>> GetChannelWiseDistributorFromDbAsync(decimal? channelId, string userId);
    }
}