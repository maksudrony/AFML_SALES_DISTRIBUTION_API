using System;

namespace AFML_SALES_DISTRIBUTION_API.Models
{
    public class SalesChannel
    {
        public int ChannelId { get; set; }
        public string? ChannelCode { get; set; }
        public string? ChannelName { get; set; }
        public string? CompId { get; set; }
        public DateTime? EnterDate { get; set; }
        public string? UserId { get; set; }
        public int? SlNo { get; set; }
        public string? ActiveStatus { get; set; }
    }
}