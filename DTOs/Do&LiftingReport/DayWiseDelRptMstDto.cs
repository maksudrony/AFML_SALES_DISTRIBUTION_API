namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class DayWiseDelRptMstDto
    {
        public long? DcId { get; set; }
        public string DcNo { get; set; } = string.Empty;
        public string DcDate { get; set; } = string.Empty;
        public string ConfirmDate { get; set; } = string.Empty;

        public long? DoId { get; set; }
        public string DoNo { get; set; } = string.Empty;

        public int? ChannelId { get; set; }
        public string ChannelName { get; set; } = string.Empty;

        public int? ZoneId { get; set; }
        public string ZoneName { get; set; } = string.Empty;

        public int? DivisionId { get; set; }
        public int? AreaId { get; set; }

        public int? TerritoryId { get; set; }
        public string TerritoryName { get; set; } = string.Empty;

        public int? DistribId { get; set; }
        public string DistribCode { get; set; } = string.Empty;
        public string DistribName { get; set; } = string.Empty;

        public decimal? ChallanQty { get; set; }
    }
}
