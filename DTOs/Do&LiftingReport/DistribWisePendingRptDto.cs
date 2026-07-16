namespace AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport
{
    public class DistribWisePendingRptDto
    {
        public int? ChannelId { get; set; }
        public string? ChannelName { get; set; }

        public string? DivisionName { get; set; }
        public string? TerritoryName { get; set; }

        public string? DistribCode { get; set; }
        public string? DistribName { get; set; }

        public long? DoId { get; set; }
        public string? DoNo { get; set; }
        public string? DoDate { get; set; }

        public string? PoNo { get; set; }
        public string? DeliveryPoint { get; set; }

        public int? ProductId { get; set; }
        public string? ProdName { get; set; }

        public decimal? ProductPrice { get; set; }

        public decimal? DoQtyBag { get; set; }
        public decimal? DoQtyTon { get; set; }

        public decimal? PendingQtyBag { get; set; }
        public decimal? PendingQtyTon { get; set; }
    }
}
