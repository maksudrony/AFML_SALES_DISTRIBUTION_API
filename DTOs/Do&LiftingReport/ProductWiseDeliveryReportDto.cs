namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class ProductWiseDeliveryReportDto
    {
        public int ProductId { get; set; }
        public string ProdCode { get; set; } = string.Empty;
        public string ProdName { get; set; } = string.Empty;

        public decimal? BagDelQty { get; set; }
        public decimal? DelTon { get; set; }
        public decimal? DeliveryValue { get; set; }

        public decimal? RatePerBag { get; set; }
        public decimal? RatePerMt { get; set; }

        public decimal? BagReturnQty { get; set; }
        public decimal? TotReturnValue { get; set; }
        public decimal? ReturnQtyTon { get; set; }

        public decimal NetDelQty { get; set; }
        public decimal NetDelValue { get; set; }
    }
}
