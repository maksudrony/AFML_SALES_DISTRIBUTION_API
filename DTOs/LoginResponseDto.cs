namespace AFML_SALES_DISTRIBUTION_API.DTOs
{
    public class LoginResponseDto
    {
        public int StatusCode { get; set; } // Returns 1 for Success, 0 for Failure
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string EmpName { get; set; } = string.Empty;
    }
}