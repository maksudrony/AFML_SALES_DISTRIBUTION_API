using System;

namespace AFML_SALES_DISTRIBUTION_API.Models
{
    public class AreaInfo
    {
        public int? DivisionId { get; set; }
        public int AreaId { get; set; }
        public string? AreaCode { get; set; }
        public string? AreaName { get; set; }
        public string? CompId { get; set; }
        public DateTime? EnterDate { get; set; }
        public string? UserId { get; set; }
        public string? Status { get; set; }
        public decimal? SlNo { get; set; }
        public string? ActiveStatus { get; set; }
    }
}