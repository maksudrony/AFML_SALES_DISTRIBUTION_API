using AFML_SALES_DISTRIBUTION_API.DTOs;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces
{
    public interface IChannelWiseDistribService
    {
        Task<ChannelWiseDistribResponseDto> GetServiceAsync(int? channelId, string userId, string? search, 
            int page, int pageSize);
    }
}
