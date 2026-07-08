using System;

namespace AFML_SALES_DISTRIBUTION_API.Models
{
    public class TerritoryInfo
    {
        public int? AreaId { get; set; }
        public int TerritoryId { get; set; }
        public string? TerritoryCode { get; set; }
        public string? TerritoryName { get; set; }
        public string? CompId { get; set; }
        public DateTime? EnterDate { get; set; }
        public string? UserId { get; set; }
        public string? Status { get; set; }
        public string? ActiveStatus { get; set; }
    }
}