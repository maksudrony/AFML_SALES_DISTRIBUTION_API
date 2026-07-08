using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.DTOs;
using AFML_SALES_DISTRIBUTION_API.Interfaces;

namespace AFML_SALES_DISTRIBUTION_API.Services
{
    public class SummaryImsReportService : ISummaryImsReportService
    {
        private readonly ISummaryImsReportRepository _repo;
        public SummaryImsReportService(ISummaryImsReportRepository repo) => 
            _repo = repo;

        public async Task<List<SummaryImsReportRow>> GetSummaryImsReportServiceAsync(string fromDate, string toDate,
            decimal? prodCatId, string entryBy, decimal channelId, decimal? zoneId, decimal? divisionId,
            decimal? areaId, decimal? territoryId)
        {
            if (string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate))
                throw new ArgumentException("Opps! Dates cannot be null or empty.");

            if (string.IsNullOrEmpty(entryBy))
                throw new ArgumentException("Opps! Entry By or User Enroll can not be Null!");

            if (channelId <= 0)
                throw new ArgumentException("Opps! Channel Id can not be null and You must provide right channel ID!");

            DateTime start = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return await _repo.GetReportDataFromDbAsync(start, end, prodCatId, entryBy, channelId, zoneId,
                divisionId, areaId, territoryId);
        }
    }
}