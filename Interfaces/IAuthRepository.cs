using AFML_SALES_DISTRIBUTION_API.DTOs;
using System.Threading.Tasks;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces
{
    public interface IAuthRepository
    {
        //Return Type
        Task<(int code, string name, string enroll)> LoginAsync(decimal empEnroll, string password);

        //Dynamic Menu Processing
        Task<List<MenuDto>> GetUserMenuTreeAsync(decimal empEnroll);
    }
}