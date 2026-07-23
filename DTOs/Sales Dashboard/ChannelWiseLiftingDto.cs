namespace AFML_SALES_DISTRIBUTION_API.DTOs.Sales_Dashboard
{
    public class ChannelWiseLiftingDto
    {
        public int ChannelId { get; set; }

        public string ChannelName { get; set; } = string.Empty;

        public decimal LiftingQty { get; set; }
    }
}
