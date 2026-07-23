using AFML_SALES_DISTRIBUTION_API.DTOs;
using AFML_SALES_DISTRIBUTION_API.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace AFML_SALES_DISTRIBUTION_API.Repositories
{
    public class ChallanWiseDistribRepository : IChallanWiseDistribRepository
    {
        private readonly string _connectionString;

        public ChallanWiseDistribRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }

        public async Task<ChallanWiseDistribResponseDto> GetDbAsync(
            DateTime? fromDate, 
            DateTime? toDate, 
            int? channelId, 
            string userId, 
            string? search, 
            int page, 
            int pageSize)
        {
            var response = new ChallanWiseDistribResponseDto();
            const string spName = "AFML_ERP.PRC_CHALLAN_DISTRIBUTOR_REACT";

            await using var connection = new OracleConnection(_connectionString);
            await using var command = new OracleCommand(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("P_FROM_DATE", OracleDbType.Date).Value = (object?)fromDate ?? DBNull.Value;
            command.Parameters.Add("P_TO_DATE", OracleDbType.Date).Value = (object?)toDate ?? DBNull.Value;
            command.Parameters.Add("P_CHANNEL_ID", OracleDbType.Int32).Value = channelId.HasValue ? (object)channelId.Value : DBNull.Value;
            command.Parameters.Add("P_USER_ID", OracleDbType.Varchar2).Value = string.IsNullOrWhiteSpace(userId) ? DBNull.Value : userId;
            command.Parameters.Add("P_SEARCH", OracleDbType.Varchar2).Value = string.IsNullOrWhiteSpace(search) ? DBNull.Value : search.Trim();
            command.Parameters.Add("P_PAGE", OracleDbType.Int32).Value = page;
            command.Parameters.Add("P_PAGE_SIZE", OracleDbType.Int32).Value = pageSize;

            // total count
            var totalCountParam = new OracleParameter
            {
                ParameterName = "P_TOTAL_COUNT",
                OracleDbType = OracleDbType.Int32,
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(totalCountParam);

            // refcursor er result
            var refCursorParam = new OracleParameter
            {
                ParameterName = "P_RESULT",
                OracleDbType = OracleDbType.RefCursor,
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(refCursorParam);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                using var reader = ((Oracle.ManagedDataAccess.Types.OracleRefCursor)refCursorParam.Value).GetDataReader();
                while (await reader.ReadAsync())
                {
                    response.Items.Add(new ChallanWiseDistribDto
                    {
                        Name = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        Id = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1))
                    });
                }

                if (totalCountParam.Value != null && totalCountParam.Value != DBNull.Value)
                {
                    response.TotalCount = Convert.ToInt32(totalCountParam.Value.ToString());
                }

                response.HasMore = (page * pageSize) < response.TotalCount;

                return response;
            }
            catch (OracleException ex)
            {
                throw new Exception("Unable to generate Challan Wise Distributor Parameter.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while generating the Challan Wise Distributor Parameter", ex);
            }
        }
    }
}
