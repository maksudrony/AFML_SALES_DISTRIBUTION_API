using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport
{
    public interface IDayWiseDelRptService
    {
        Task<List<DayWiseDelRptMstDto>> GetMstServiceAsync(DateTime fromDate, DateTime toDate, int fromTime,
        int toTime, int? channelId, int? distribId, string entryBy);

        Task<List<DayWiseDelRptDtlDto>> GetDtlServiceAsync(long dcId);
    }
}
