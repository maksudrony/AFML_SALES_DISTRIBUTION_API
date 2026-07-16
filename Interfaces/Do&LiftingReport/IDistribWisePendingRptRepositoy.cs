using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport
{
    public interface IDistribWisePendingRptRepository
    {
        Task<List<DistribWisePendingRptDto>> GetDistribWisePendingRptFromDbAsync(DateTime? fromDate, DateTime? toDate,
            int? channelId, int? zoneId, int? divisionId, int? areaId, int? territoryId, int? productId,
            int? distribId, int orderTypeId, string entryBy);
    }
}
