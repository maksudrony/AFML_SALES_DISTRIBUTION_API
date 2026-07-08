namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class LiftingAndDoReportDto
    {
        public int? ChannelType { get; set; }
        public int? ChannelId { get; set; }
        public string ChannelName { get; set; } = string.Empty;
        public int? ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal? MonthlyConsumer { get; set; }
        public decimal? MonthlyBulk {  get; set; }
        public decimal? MonthlyCorporate { get; set; }
        public decimal? MonthlyCommodityTrading { get; set; }
        public decimal? MonthlyTotal { get; set; }
        public decimal? DailyConsumer { get; set; }
        public decimal? DailyBulk { get; set; }
        public decimal? DailyCorporate { get; set; }
        public decimal? DailyCommodityTrading { get; set; }
        public decimal? DailyTotal { get; set; }

    }
}
