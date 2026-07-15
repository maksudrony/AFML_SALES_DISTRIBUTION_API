using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport
{
    public interface IDayWiseDelRptRepository
    {
        Task<List<DayWiseDelRptMstDto>> GetMstFromDbAsync(DateTime fromDate, DateTime toDate, int fromTime,
        int toTime, int? channelId, int? distribId, string entryBy);

        Task<List<DayWiseDelRptDtlDto>> GetDtlFromDbAsync(long dcId);
    }
}
