namespace AFML_SALES_DISTRIBUTION_API.Models
{
    public class RemoteAccUser
    {
        public int EmpEnroll { get; set; }
        public string? EmpName { get; set; }
        public string? UserName { get; set; }
        public string? WebUser { get; set; }
        public string? AppUser { get; set; }
        public string? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? RoleId { get; set; }
        public string? EmpPwd { get; set; }
        public string? DeviceAddress { get; set; }
        public string? ContractNo { get; set; }
        public string? Status { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? MacAddress { get; set; }
        public string? DeptDescLong { get; set; }
        public string? DesigDescription { get; set; }
    }
}
