using System.Threading.Tasks;

namespace AFML_SALES_DISTRIBUTION_API.Interfaces
{
    public interface IAuthRepository
    {
        // Choto name pattern rule integration completed
        Task<(int code, string name)> LoginAsync(decimal empEnroll, string password);
    }
}