namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class LiftingAndDoReportHeaderDto
    {
        public string ReportType { get; set; } = string.Empty;
        public string MonthlyDateRange { get; set; } = string.Empty;
        public string DailyDateRange { get; set; } = string.Empty;
    }
}
