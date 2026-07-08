using System;

namespace AFML_SALES_DISTRIBUTION_API.Models
{
    public class DivisionInfo
    {
        public int DivisionId { get; set; }
        public string? DivisionCode { get; set; }
        public string? DivisionName { get; set; }
        public string? CompId { get; set; }
        public DateTime? EnterDate { get; set; }
        public string? UserId { get; set; }
        public int? ZoneId { get; set; }
        public string? Status { get; set; }
        public decimal? SlNo { get; set; }
        public string? ActiveStatus { get; set; }
    }
}