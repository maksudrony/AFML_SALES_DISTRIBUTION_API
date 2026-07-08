using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using System.Globalization;

namespace AFML_SALES_DISTRIBUTION_API.Services.Do_LiftingReport
{
    public class ProductWiseDeliveryReportService : IProductWiseDeliveryReportService
    {
        private readonly IProductWiseDeliveryReportRepository _repo;

        public ProductWiseDeliveryReportService (IProductWiseDeliveryReportRepository repo) =>
            _repo = repo;

        public async Task<List<ProductWiseDeliveryReportDto>> GetIProductWiseDeliveryReportServiceAsync(DateTime? fromDate,
            DateTime? toDate, string? entryBy, int? productId)
        {
            if (!fromDate.HasValue || !toDate.HasValue)
            {
                throw new ArgumentException("Opss! You must Select the From Date And To date!");
            }

            return await _repo.GetProductWiseDeliveryReportFromDbAsync (fromDate, toDate, entryBy, productId);
        }
    }
}
