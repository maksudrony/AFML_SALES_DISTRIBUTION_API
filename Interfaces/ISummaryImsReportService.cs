using AFML_SALES_DISTRIBUTION_API.DTOs;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces
{
    public interface ISummaryImsReportService
    {
        //get report data
        Task<List<SummaryImsReportRow>> GetSummaryImsReportServiceAsync(string fromDate, string toDate, 
            decimal? prodCatId, string entryBy, decimal channelId, decimal? zoneId, decimal? divisionId,
            decimal? areaId, decimal? territoryId);
    }
}