using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.Interfaces;
using AFML_SALES_DISTRIBUTION_API.DTOs;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace AFML_SALES_DISTRIBUTION_API.Repositories
{
    // Local processing interface model mapping strongly typed container row framework context
    internal class FlatMenuNode
    {
        public int Level { get; set; }
        public decimal MainList { get; set; }
        public decimal? SubList { get; set; } // Native Nullable struct type handles structural `.Value` property or hash check safely
        public string Label { get; set; } = string.Empty;
        public string? RawPath { get; set; }
        public string? Icon { get; set; }
    }

    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }

        public async Task<(int code, string name, string enroll)> LoginAsync(decimal empEnroll, string password)
        {
            await using var connection = new OracleConnection(_connectionString);
            await using var command = new OracleCommand("AFML_ERP.PRC_AUTH_MAKSUD_VAL", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OracleParameter("P_USERNAME", OracleDbType.Decimal) { Value = empEnroll });
            command.Parameters.Add(new OracleParameter("P_PASSWORD", OracleDbType.Varchar2) { Value = password.Trim() });

            var resultParam = new OracleParameter
            {
                ParameterName = "P_RESULT",
                OracleDbType = OracleDbType.Decimal,
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(resultParam);

            var empNameParam = new OracleParameter
            {
                ParameterName = "P_EMP_NAME",
                OracleDbType = OracleDbType.Varchar2,
                Size = 200,
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(empNameParam);

            var empEnrollParam = new OracleParameter
            {
                ParameterName = "P_EMP_ENROLL",
                OracleDbType = OracleDbType.Varchar2,
                Size = 50,
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(empEnrollParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            int statusOutcomeCode = 0;
            string employeeNameValue = "Employee";
            string employeeEnrollValue = "0"; 

            if (resultParam.Value != DBNull.Value)
            {
                statusOutcomeCode = Convert.ToInt32(resultParam.Value.ToString());
            }

            if (empNameParam.Value != DBNull.Value)
            {
                employeeNameValue = empNameParam.Value.ToString()!;
            }

            if (empEnrollParam.Value != DBNull.Value)
            {
                employeeEnrollValue = empEnrollParam.Value.ToString()!;
            }

            return (statusOutcomeCode, employeeNameValue, employeeEnrollValue);
        }

        public async Task<List<MenuDto>> GetUserMenuTreeAsync(decimal empEnroll)
        {
            var roots = new List<MenuDto>();
            await using var connection = new OracleConnection(_connectionString);
            await using var command = new OracleCommand("AFML_ERP.PRC_DYNAMIC_APEX_MENU", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OracleParameter("P_USER_ID", OracleDbType.Varchar2) { Value = empEnroll.ToString() });

            var refCursorParam = new OracleParameter
            {
                ParameterName = "P_RESULT",
                OracleDbType = OracleDbType.RefCursor,
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(refCursorParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            // Using strong explicit list collection layer execution contexts
            var flatList = new List<FlatMenuNode>();
            using var reader = ((Oracle.ManagedDataAccess.Types.OracleRefCursor)refCursorParam.Value).GetDataReader();

            while (await reader.ReadAsync())
            {
                flatList.Add(new FlatMenuNode
                {
                    Level = reader.GetInt32(0),
                    MainList = reader.GetDecimal(1),
                    SubList = reader.IsDBNull(2) ? null : reader.GetDecimal(2),
                    Label = reader.GetString(3),
                    RawPath = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Icon = reader.IsDBNull(5) ? null : reader.GetString(5)
                });
            }

            var lookup = new Dictionary<decimal, MenuDto>();

            // Pass 1: Parse string routing rules format
            foreach (var item in flatList)
            {
                string? cleanRouterPath = null;
                if (item.RawPath != null)
                {
                    cleanRouterPath = item.RawPath.ToLower().Replace(" ", "");
                }

                lookup[item.MainList] = new MenuDto
                {
                    Label = item.Label,
                    Icon = item.Icon,
                    Path = cleanRouterPath
                };
            }

            // Pass 2: Reconstruct perfect multi-nested child configurations models structures safely
            foreach (var item in flatList)
            {
                var currentDto = lookup[item.MainList];

                // Safe lookup verification checks utilizing strict strongly typed configuration layer logic
                if (item.SubList == null || !lookup.ContainsKey(item.SubList.Value))
                {
                    roots.Add(currentDto);
                }
                else
                {
                    lookup[item.SubList.Value].Children.Add(currentDto);
                }
            }

            return roots;
        }
    }
}

