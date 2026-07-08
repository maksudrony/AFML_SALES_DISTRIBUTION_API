using System;

namespace AFML_SALES_DISTRIBUTION_API.Models
{
    public class ZoneInfo
    {
        public int ZoneId { get; set; }
        public string? ZoneCode { get; set; }
        public string? ZoneName { get; set; }
        public string? CompId { get; set; }
        public DateTime? EnterDate { get; set; }
        public string? UserId { get; set; }
        public decimal? SlNo { get; set; }
        public string? ActiveStatus { get; set; }
        public int? ChannelId { get; set; }
        public string? RcCode { get; set; }
        public int? ReportChannelId { get; set; }
    }
}