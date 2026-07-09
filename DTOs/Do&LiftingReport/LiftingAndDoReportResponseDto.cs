namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class LiftingAndDoReportResponseDto
    {
        public LiftingAndDoReportHeaderDto ReportHeader { get; set; } = new();

        public List<LiftingAndDoReportDto> ReportRows { get; set; } = new();
    }
}
