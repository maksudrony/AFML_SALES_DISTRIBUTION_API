namespace AFML_SALES_DISTRIBUTION_API.Models
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public int? ProdCatId { get; set; }
        public string? ProdCode { get; set; }
        public string? ProdName { get; set; }

        public string? UnitCode { get; set; }
        public string? PettyUnit { get; set; }
        public string? CartonUnit { get; set; }

        public decimal? CostPrice { get; set; }
        public decimal? VatAmt { get; set; }
        public decimal? SupTax { get; set; }
        public decimal? SalesPrice { get; set; }
        public decimal? CommAmt { get; set; }

        public string? ActiveStatus { get; set; }
        public DateTime? EnterDate { get; set; }

        public string? UserId { get; set; }
        public decimal? CompId { get; set; }

        public decimal? CartonWeight { get; set; }
        public decimal? ProductWeight { get; set; }
        public decimal? DhCharge { get; set; }

        public string? ProdWeightType { get; set; }
        public string? CrtnWeightType { get; set; }

        public decimal? ProdTypeId { get; set; }
        public decimal? PettyWeight { get; set; }

        public string? Mdastatus { get; set; }

        public decimal? ProdIdMaster { get; set; }
        public string? TradeBonusStatus { get; set; }

        public decimal? TpPrice { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? VatPrice { get; set; }
        public decimal? InvPriceVat { get; set; }

        public string? VatProdName { get; set; }
        public string? SortProdName { get; set; }

        public decimal? ProdGrpId { get; set; }
        public decimal? ProdSlno { get; set; }
        public decimal? XlsSlno { get; set; }
        public string? SkuShortName { get; set; }
        public decimal? PackTypeId { get; set; }
        public string? ProdForcast { get; set; }
        public string? ProductHold { get; set; }
        public decimal? PrvSalesPrice { get; set; }
        public string? PrvActiveStatus { get; set; }
        public decimal? PrvXlsSlno { get; set; }
        public decimal? ProdUnit { get; set; }
        public decimal? BagMeasurement { get; set; }
        public decimal? NoOfQtyPerBag { get; set; }
        public decimal? NoOfQtyPerUom { get; set; }
        public decimal? WeightPerPack { get; set; }
        public string? DiscountAllow { get; set; }
        public string? Damage { get; set; }
        public string? SpecialOffer { get; set; }
        public decimal? Srsubsidiary { get; set; }
        public string? SupplierVheicle { get; set; }
        public string? CustomerVheicle { get; set; }

        public string? CompanyVheicle { get; set; }
        public string? SalesOfficeApplicable { get; set; }
        public decimal? ItemPackCategoryId { get; set; }
        public decimal? Vat { get; set; }
        public decimal? FgGroupId { get; set; }
        public decimal? ItemnSalesofficeGroup { get; set; }
        public decimal? CoaId { get; set; }
        public string? CoaName { get; set; }
        public string? IncentiveStatus { get; set; }
        public string? ProductManufacType { get; set; }
        public string? SalesChannel { get; set; }
        public decimal? ChannelId { get; set; }
        public decimal? ProductionProdId { get; set; }
        public decimal? SalesCatId { get; set; }
        public string? OthersProduct { get; set; }
        public string? SecondarySales { get; set; }
        public decimal? VehicleProdId { get; set; }
        public decimal? BudgetCatId { get; set; }
        public string? SqlInsert { get; set; }
        public decimal? WhId { get; set; }
        public decimal? LockDays { get; set; }
        public decimal? VehicleCatId { get; set; }
        public decimal? ProductionUnit { get; set; }
        public decimal? LitreToKg { get; set; }
        public decimal? ProductionConversion { get; set; }
    }
}
