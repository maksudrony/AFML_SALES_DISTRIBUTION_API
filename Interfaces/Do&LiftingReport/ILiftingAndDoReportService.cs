using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport
{
    public interface ILiftingAndDoReportService
    {
        Task <LiftingAndDoReportResponseDto> GetLiftingAndDoReportServiceAsync (DateTime? fromDate, DateTime? toDate, DateTime? dayFromDate, DateTime? dayToDate,
            int? channelId, int? channelTypeId, int typeId, int reportTypeId, string entryBy);
    }
}
