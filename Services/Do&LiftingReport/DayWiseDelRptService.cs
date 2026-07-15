using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.Services.Do_LiftingReport
{
    public class DayWiseDelRptService : IDayWiseDelRptService
    {
        private readonly IDayWiseDelRptRepository _repository;

        public DayWiseDelRptService(IDayWiseDelRptRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DayWiseDelRptMstDto>> GetMstServiceAsync(DateTime fromDate, DateTime toDate,
            int fromTime, int toTime, int? channelId, int? distribId, string entryBy)
        {
            return await _repository.GetMstFromDbAsync(fromDate, toDate, fromTime, toTime, channelId,
                distribId, entryBy);
        }

        public async Task<List<DayWiseDelRptDtlDto>> GetDtlServiceAsync(long dcId)
        {
            return await _repository.GetDtlFromDbAsync(dcId);
        }
    }
}