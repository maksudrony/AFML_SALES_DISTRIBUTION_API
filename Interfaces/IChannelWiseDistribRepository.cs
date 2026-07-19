using AFML_SALES_DISTRIBUTION_API.DTOs;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces
{
    public interface IChannelWiseDistribRepository
    {
        Task<ChannelWiseDistribResponseDto> GetDbAsync(int? channelId, string userId, string? search, 
            int page, int pageSize);
    }
}
