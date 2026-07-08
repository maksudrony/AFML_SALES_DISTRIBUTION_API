namespace AFML_SALES_DISTRIBUTION_API.DTOs
{
    public class LoginRequestDto
    {
        public decimal EmpEnroll { get; set; }
        public string EmpPwd { get; set; } = string.Empty;
    }
}
