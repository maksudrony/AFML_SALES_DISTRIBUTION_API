using System;
using System.Data;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.Interfaces;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace AFML_SALES_DISTRIBUTION_API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }

        public async Task<(int code, string name)> LoginAsync(decimal empEnroll, string password)
        {
            await using var connection = new OracleConnection(_connectionString);
            await using var command = new OracleCommand("AFML_ERP.PRC_AUTH_MAKSUD_VAL", connection);

            command.CommandType = CommandType.StoredProcedure;

            // IN Parameters
            command.Parameters.Add(new OracleParameter("P_USERNAME", OracleDbType.Decimal) { Value = empEnroll });
            command.Parameters.Add(new OracleParameter("P_PASSWORD", OracleDbType.Varchar2) { Value = password.Trim() });

            // OUT Parameter 
            var resultParam = new OracleParameter
            {
                ParameterName = "P_RESULT",
                OracleDbType = OracleDbType.Decimal,
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(resultParam);

            // OUT Parameter
            var empNameParam = new OracleParameter
            {
                ParameterName = "P_EMP_NAME",
                OracleDbType = OracleDbType.Varchar2,
                Size = 200,
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(empNameParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            int statusOutcomeCode = 0;
            string employeeNameValue = "Employee";

            if (resultParam.Value != DBNull.Value)
            {
                statusOutcomeCode = Convert.ToInt32(resultParam.Value.ToString());
            }

            if (empNameParam.Value != DBNull.Value)
            {
                employeeNameValue = empNameParam.Value.ToString()!;
            }

            return (statusOutcomeCode, employeeNameValue);
        }
    }
}