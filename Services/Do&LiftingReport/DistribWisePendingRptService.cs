
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.Services.Do_LiftingReport
{
    public class DistribWisePendingRptService : IDistribWisePendingRptService
    {
        private readonly IDistribWisePendingRptRepository _repo;

        public DistribWisePendingRptService(IDistribWisePendingRptRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<DistribWisePendingRptDto>> GetDistribWisePendingRptServiceAsync(DateTime? fromDate, DateTime? toDate,
            int? channelId, int? zoneId, int? divisionId, int? areaId, int? territory, int? productId,
            int? distribId, int orderTypeId, string entryBy)
        {
            if (orderTypeId == 0 || string.IsNullOrWhiteSpace(entryBy))
            {
                throw new ArgumentException("Opss! Please choose Order Type.");
            }

            return await _repo.GetDistribWisePendingRptFromDbAsync(fromDate, toDate,
                channelId, zoneId, divisionId, areaId, territory, productId, distribId, orderTypeId, entryBy);
        }
    }
}
