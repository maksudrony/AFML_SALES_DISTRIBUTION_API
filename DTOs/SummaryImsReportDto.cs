using System.Collections.Generic;

namespace AFML_SALES_DISTRIBUTION_API.DTOs
{
    // Main Report Columns
    public class SummaryImsReportRow
    {
        public string ChannelName { get; set; } = string.Empty;
        public string ZoneName { get; set; } = string.Empty;
        public string DivisionName { get; set; } = string.Empty;
        public string AreaName { get; set; } = string.Empty;
        public string TerritoryName { get; set; } = string.Empty;
        public string DistribName { get; set; } = string.Empty;
        public string SoEnrol { get; set; } = string.Empty;
        public string EmpName { get; set; } = string.Empty;
        public string JoiningDate { get; set; } = string.Empty;
        public Dictionary<string, decimal> DaysData { get; set; } = new Dictionary<string, decimal>();
        public decimal GrandTotal { get; set; }
    }
}