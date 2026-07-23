using AFML_SALES_DISTRIBUTION_API.DTOs;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces
{
    public interface IChallanWiseDistribRepository
    {
        Task<ChallanWiseDistribResponseDto> GetDbAsync(DateTime? fromDate, DateTime? toDate, int? channelId, 
            string userId, string? search, int page, int pageSize);
    }
}
