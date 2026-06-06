using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.DTOs;
using AFML_SALES_DISTRIBUTION_API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AFML_SALES_DISTRIBUTION_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository authRepo, IConfiguration config)
        {
            _authRepo = authRepo;
            _config = config;
        }

        public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto request)
        {
            var result = await _authRepo.LoginAsync(request.EmpEnroll, request.EmpPwd);

            if (result.code != 1)
            {
                return new LoginResponseDto
                {
                    StatusCode = 0,
                    Message = "Opps! invalid password! Please use correct Password!",
                    EmpName = "Employee"
                };
            }

            var token = CreateToken(request.EmpEnroll, result.name);

            return new LoginResponseDto
            {
                StatusCode = 1,
                Message = "Authentication Success.",
                Token = token,
                EmpName = result.name
            };
        }

        private string CreateToken(decimal empEnroll, string empName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, empEnroll.ToString()),
                new Claim(ClaimTypes.Name, empName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}