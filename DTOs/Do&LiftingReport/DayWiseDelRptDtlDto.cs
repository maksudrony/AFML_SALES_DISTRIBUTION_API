namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class DayWiseDelRptDtlDto
    {
        public long? DcId { get; set; }
        public int? ProductId { get; set; }

        public string ProdCode { get; set; } = string.Empty;
        public string ProdName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal? ChallanQty { get; set; }
    }
}
