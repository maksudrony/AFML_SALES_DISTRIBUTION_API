using System.Collections.Generic;

namespace AFML_SALES_DISTRIBUTION_API.DTOs
{
    public class MenuDto
    {
        public string Label { get; set; } = string.Empty;
        public string? Path { get; set; }
        public string? Icon { get; set; }
        public List<MenuDto> Children { get; set; } = new List<MenuDto>();
    }

    public class LoginResponseDto
    {
        public int StatusCode { get; set; } // Returns 1 for Success, 0 for Failure
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string EmpName { get; set; } = string.Empty;
        public string EmpEnroll { get; set; } = string.Empty;

        public List<MenuDto> MenuTree { get; set; } = new List<MenuDto>();  // For Dynamic menu
    }
}