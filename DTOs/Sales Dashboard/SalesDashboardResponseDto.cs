namespace AFML_SALES_DISTRIBUTION_API.DTOs.Sales_Dashboard
{
    public class SalesDashboardResponseDto
    {
        public SalesDashboardSummaryDto Summary { get; set; } = new();

        public List<ChannelWiseLiftingDto> ChannelWiseLifting { get; set; } = new();

        public List<ChannelWiseSalesDto> ChannelWiseSales { get; set; } = new();
    }
}
