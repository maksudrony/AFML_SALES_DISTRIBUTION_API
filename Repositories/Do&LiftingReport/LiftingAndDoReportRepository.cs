using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace AFML_SALES_DISTRIBUTION_API.Repositories.Do_LiftingReport
{
    public class LiftingAndDoReportRepository : ILiftingAndDoReportRepository
    {
        private readonly string _connectionString;
        public LiftingAndDoReportRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }
        public async Task<List<LiftingAndDoReportDto>> GetLiftingAndDoReportFromDbAsync (DateTime? fromDate, DateTime? toDate, DateTime? dayFromDate, DateTime? dayToDate,
            int? channelId, int? channelTypeId, int typeId, int reportTypeId, string entryBy)
        {
            const string storedProcedureName = "AFML_ERP.PRC_LIFTING_AND_DO_RPT_REACT";

            var rows = new List<LiftingAndDoReportDto>();

            await using var connection = new OracleConnection(_connectionString);
            await using var command = new OracleCommand(storedProcedureName, connection) 
            {
                CommandType = CommandType.StoredProcedure 
            };


            command.Parameters.Add("P_FROM_DATE", OracleDbType.Date).Value = (object?)fromDate ?? DBNull.Value;
            command.Parameters.Add("P_TO_DATE", OracleDbType.Date).Value = (object?)toDate ?? DBNull.Value;
            command.Parameters.Add("P_DAY_FROM_DATE", OracleDbType.Date).Value = (object?)dayFromDate ?? DBNull.Value;
            command.Parameters.Add("P_DAY_TO_DATE", OracleDbType.Date).Value = (object?)dayToDate ?? DBNull.Value;
            command.Parameters.Add("P_CHANNEL_ID", OracleDbType.Decimal).Value = (object?)channelId ?? DBNull.Value;
            command.Parameters.Add("P_CHANNEL_TYPE", OracleDbType.Decimal).Value = (object?)channelTypeId ?? DBNull.Value;
            command.Parameters.Add("P_TYPE_ID", OracleDbType.Decimal).Value = typeId;
            command.Parameters.Add("P_REPORT_TYPE_ID", OracleDbType.Decimal).Value = reportTypeId;
            command.Parameters.Add("P_ENTRY_BY", OracleDbType.Varchar2).Value = entryBy;

            var refCursorParam = new OracleParameter("P_RESULT", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(refCursorParam);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                using var reader = ((OracleRefCursor)refCursorParam.Value).GetDataReader();

                // Cache column indexes (only once)
                int channelTypeIndex = reader.GetOrdinal("CHANNEL_TYPE");
                int channelIdIndex = reader.GetOrdinal("CHANNEL_ID");
                int channelNameIndex = reader.GetOrdinal("CHANNEL_NAME");
                int productIdIndex = reader.GetOrdinal("PRODUCT_ID");
                int productNameIndex = reader.GetOrdinal("PROD_NAME");

                int monthlyConsumerIndex = reader.GetOrdinal("MONTHLY_CONSUMER");
                int monthlyBulkIndex = reader.GetOrdinal("MONTHLY_BULK");
                int monthlyCorporateIndex = reader.GetOrdinal("MONTHLY_CORPORATE");
                int monthlyCommodityTradingIndex = reader.GetOrdinal("MONTHLY_COMMODITY_TRADING");
                int monthlyTotalIndex = reader.GetOrdinal("MONTHLY_TOTAL");

                int dailyConsumerIndex = reader.GetOrdinal("DAILY_CONSUMER");
                int dailyBulkIndex = reader.GetOrdinal("DAILY_BULK");
                int dailyCorporateIndex = reader.GetOrdinal("DAILY_CORPORATE");
                int dailyCommodityTradingIndex = reader.GetOrdinal("DAILY_COMMODITY_TRADING");
                int dailyTotalIndex = reader.GetOrdinal("DAILY_TOTAL");

                while (await reader.ReadAsync())
                {
                    var row = new LiftingAndDoReportDto
                    {
                        ChannelType = reader.IsDBNull(channelTypeIndex) ? 0 : reader.GetInt32(channelTypeIndex),
                        ChannelId = reader.IsDBNull(channelIdIndex) ? 0 : reader.GetInt32(channelIdIndex),
                        ChannelName = reader.IsDBNull(channelNameIndex) ? string.Empty : reader.GetString(channelNameIndex),
                        ProductId = reader.IsDBNull(productIdIndex) ? 0 : reader.GetInt32(productIdIndex),
                        ProductName = reader.IsDBNull(productNameIndex) ? string.Empty : reader.GetString(productNameIndex),

                        MonthlyConsumer = reader.IsDBNull(monthlyConsumerIndex) ? 0 : reader.GetDecimal(monthlyConsumerIndex),
                        MonthlyBulk = reader.IsDBNull(monthlyBulkIndex) ? 0 : reader.GetDecimal(monthlyBulkIndex),
                        MonthlyCorporate = reader.IsDBNull(monthlyCorporateIndex) ? 0 : reader.GetDecimal(monthlyCorporateIndex),
                        MonthlyCommodityTrading = reader.IsDBNull(monthlyCommodityTradingIndex) ? 0 : reader.GetDecimal(monthlyCommodityTradingIndex),
                        MonthlyTotal = reader.IsDBNull(monthlyTotalIndex) ? 0 : reader.GetDecimal(monthlyTotalIndex),

                        DailyConsumer = reader.IsDBNull(dailyConsumerIndex) ? 0 : reader.GetDecimal(dailyConsumerIndex),
                        DailyBulk = reader.IsDBNull(dailyBulkIndex) ? 0 : reader.GetDecimal(dailyBulkIndex),
                        DailyCorporate = reader.IsDBNull(dailyCorporateIndex) ? 0 : reader.GetDecimal(dailyCorporateIndex),
                        DailyCommodityTrading = reader.IsDBNull(dailyCommodityTradingIndex) ? 0 : reader.GetDecimal(dailyCommodityTradingIndex),
                        DailyTotal = reader.IsDBNull(dailyTotalIndex) ? 0 : reader.GetDecimal(dailyTotalIndex)
                    };
                    rows.Add(row);
                }
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception($"Opps! '{storedProcedureName}' execution failed: {ex.Message}", ex);
            }
        }
    }
}
