using System;

namespace AFML_SALES_DISTRIBUTION_API.Models
{
    public class ChannelWiseUser
    {
        public int ChannelId { get; set; }
        public string? EmpId { get; set; }
        public string? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}