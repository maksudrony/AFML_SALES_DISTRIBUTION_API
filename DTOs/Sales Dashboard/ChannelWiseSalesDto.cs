namespace AFML_SALES_DISTRIBUTION_API.DTOs.Sales_Dashboard
{
    public class ChannelWiseSalesDto
    {
        public int ChannelId { get; set; }

        public string ChannelName { get; set; } = string.Empty;

        public decimal SalesQty { get; set; }
    }
}
