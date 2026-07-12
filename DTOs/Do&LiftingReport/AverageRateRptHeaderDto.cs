namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class AverageRateRptHeaderDto
    {
        public string ReportType { get; set; } = string.Empty;
        public string MonthlyDateRange { get; set; } = string.Empty;
        public string DailyDateRange { get; set; } = string.Empty;
    }
}
