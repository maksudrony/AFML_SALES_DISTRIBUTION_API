namespace AFML_SALES_DISTRIBUTION_API.DTOs
{
    public class ChallanWiseDistribResponseDto
    {
        public List<ChallanWiseDistribDto> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public bool HasMore { get; set; }
    }
}
