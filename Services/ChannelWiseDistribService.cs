using System;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.DTOs;
using AFML_SALES_DISTRIBUTION_API.Interfaces;

namespace AFML_SALES_DISTRIBUTION_API.Services
{
    public class ChannelWiseDistribService : IChannelWiseDistribService
    {
        private readonly IChannelWiseDistribRepository _repo;

        public ChannelWiseDistribService(IChannelWiseDistribRepository repo) => 
            _repo = repo;

        public async Task<ChannelWiseDistribResponseDto> GetServiceAsync(int? channelId, string userId, 
            string? search, int page, int pageSize) =>
            await _repo.GetDbAsync(channelId, userId, search, page, pageSize);
    }
}