using AFML_SALES_DISTRIBUTION_API.DTOs;
using AFML_SALES_DISTRIBUTION_API.Interfaces;

namespace AFML_SALES_DISTRIBUTION_API.Services
{
    public class ChallanWiseDistribService : IChallanWiseDistribService
    {
        private readonly IChallanWiseDistribRepository _repo;

        public ChallanWiseDistribService(IChallanWiseDistribRepository repo) =>
            _repo = repo;

        public async Task<ChallanWiseDistribResponseDto> GetServiceAsync(DateTime? fromDate, DateTime? toDate, 
            int? channelId, string userId, string? search, int page, int pageSize) =>
            await _repo.GetDbAsync(fromDate, toDate, channelId, userId, search, page, pageSize);
    }
}
