using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.Services.Do_LiftingReport
{
    public class LiftingAndDoReportService : ILiftingAndDoReportService
    {
        private readonly ILiftingAndDoReportRepository _repo;

        public LiftingAndDoReportService (ILiftingAndDoReportRepository repo)
        {
            _repo = repo;
        }
        public async Task<LiftingAndDoReportResponseDto> GetLiftingAndDoReportServiceAsync (DateTime? fromDate, DateTime? toDate, DateTime? dayFromDate, DateTime? dayToDate,
            int? channelId, int? channelTypeId, int typeId, int reportTypeId, string entryBy)
        {
            if (!fromDate.HasValue || !toDate.HasValue)
            {
                throw new ArgumentException ("Opss! You must Select the From Date And To date!");
            }
            else if (!dayFromDate.HasValue || !dayToDate.HasValue)
            {
                throw new ArgumentException("Opss! You must Select the Day From Date And Day To date!");
            }
            else if (typeId == 0 || reportTypeId == 0 || string.IsNullOrWhiteSpace(entryBy))
            {
                throw new ArgumentException("Opss! Required parameters cannot be null or empty.");
            }

            return await _repo.GetLiftingAndDoReportFromDbAsync(fromDate, toDate, dayFromDate, dayToDate,
                           channelId, channelTypeId, typeId, reportTypeId, entryBy);
        }
    }
}
