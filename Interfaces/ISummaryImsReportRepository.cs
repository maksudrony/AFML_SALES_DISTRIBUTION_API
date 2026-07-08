using AFML_SALES_DISTRIBUTION_API.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces
{
    public interface ISummaryImsReportRepository
    {
        //Get Report Data
        Task<List<SummaryImsReportRow>> GetReportDataFromDbAsync(DateTime fromDate, DateTime toDate, 
            decimal? prodCatId, string entryBy, decimal channelId, decimal? zoneId, decimal? divisionId,
            decimal? areaId, decimal? territoryId);
    }
}