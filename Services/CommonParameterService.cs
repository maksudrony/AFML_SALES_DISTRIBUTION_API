using System.Collections.Generic;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.DTOs;
using AFML_SALES_DISTRIBUTION_API.Interfaces;

namespace AFML_SALES_DISTRIBUTION_API.Services
{
    public class CommonParameterService : ICommonParameterService
    {
        private readonly ICommonParameterRepository _repo;
        public CommonParameterService(ICommonParameterRepository repo) => 
            _repo = repo;

        public async Task<List<CommonParameterDto>> GetChannelsServiceAsync(string userId) => 
            await _repo.GetChannelsFromDbAsync(userId);
        public async Task<List<CommonParameterDto>> GetZonesServiceAsync(string userId, decimal channelId) => 
            await _repo.GetZonesFromDbAsync(userId, channelId);
        public async Task<List<CommonParameterDto>> GetDivisionsServiceAsync(string userId, decimal zoneId) => 
            await _repo.GetDivisionsFromDbAsync(userId, zoneId);
        public async Task<List<CommonParameterDto>> GetAreasServiceAsync(string userId, decimal divisionId) => 
            await _repo.GetAreasFromDbAsync(userId, divisionId);
        public async Task<List<CommonParameterDto>> GetTerritoriesServiceAsync(string userId, decimal areaId) => 
            await _repo.GetTerritoriesFromDbAsync(userId, areaId);
        public async Task<List<CommonParameterDto>> GetProductCategoriesServiceAsync() => 
            await _repo.GetProductCategoriesFromDbAsync();
        public async Task<List<CommonParameterDto>> GetProductDetailServiceAsync() =>
            await _repo.GetProductDetailFromDbAsync();
        public async Task<List<CommonParameterDto>> GetSalesChannelTypeServiceAsync(string userId)
        {
            return await _repo.GetSalesChannelTypeFromDbAsync(userId);
        }
        public async Task<List<CommonParameterDto>> GetQuantityTypeServiceAsync()
        {
            return await _repo.GetQuantityTypeFromDbAsync();
        }
        public async Task<List<CommonParameterDto>> GetReportTypeServiceAsync()
        {
            return await _repo.GetReportTypeFromDbAsync();
        }
        public async Task<List<CommonParameterDto>> GetTimeManagementServiceAsync() =>
             await _repo.GetTimeManagementFromDbAsync();

        public async Task<List<CommonParameterDto>> GetChallanDistributorServiceAsync(DateTime? fromDate, DateTime? toDate,
            decimal? channelId, string userId) =>
            await _repo.GetChallanDistributorFromDbAsync(fromDate, toDate, channelId, userId);

        public async Task<List<CommonParameterDto>> GetChannelWiseDistributorServiceAsync(decimal? channelId, string userId) =>
            await _repo.GetChannelWiseDistributorFromDbAsync(channelId, userId);


    }
}