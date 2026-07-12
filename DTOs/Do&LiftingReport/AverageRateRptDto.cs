namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class AverageRateRptDto
    {
        public int? ChannelType { get; set; }
        public int? ChannelId { get; set; }
        public string ChannelName { get; set; } = string.Empty;
        public int? ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal? MonQty { get; set; }
        public decimal? MonValue { get; set; }
        public decimal? MonAvgRate { get; set; }
        public decimal? DayQty { get; set; }
        public decimal? DayValue { get; set; }
        public decimal? DayAvgRate { get; set; }
    }
}
