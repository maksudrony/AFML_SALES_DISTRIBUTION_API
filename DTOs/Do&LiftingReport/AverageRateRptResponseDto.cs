using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class AverageRateRptResponseDto
    {
        public AverageRateRptHeaderDto ReportHeader { get; set; } = new();

        public List<AverageRateRptDto> ReportRows { get; set; } = new();
    }
}