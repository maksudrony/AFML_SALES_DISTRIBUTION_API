using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport
{
    public interface IProductWiseDeliveryReportRepository
    {
        Task<List<ProductWiseDeliveryReportDto>> GetProductWiseDeliveryReportFromDbAsync(DateTime? fromDate,
            DateTime? todate, string? entryBy, int? productId);
    }
}
