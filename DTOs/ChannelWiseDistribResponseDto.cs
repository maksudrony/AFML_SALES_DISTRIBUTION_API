namespace AFML_SALES_DISTRIBUTION_API.DTOs
{
    public class ChannelWiseDistribResponseDto
    {
        public List<ChannelWiseDistribDto> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public bool HasMore { get; set; }
    }
}
