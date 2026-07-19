using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport
{
    public interface IDistribWisePendingRptService
    {
        Task<List<DistribWisePendingRptDto>> GetDistribWisePendingRptServiceAsync(DateTime? fromDate, DateTime? toDate,
            int? channelId, int? zoneId, int? divisionId, int? areaId, int? territoryId, int? productId,
            int? distribId, int orderTypeId);
    }
}
