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

        public LiftingAndDoReportRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }

        public async Task<LiftingAndDoReportResponseDto> GetLiftingAndDoReportFromDbAsync(
            DateTime? fromDate,
            DateTime? toDate,
            DateTime? dayFromDate,
            DateTime? dayToDate,
            int? channelId,
            int? channelTypeId,
            int typeId,
            int reportTypeId,
            string entryBy)
        {
            const string storedProcedureName = "AFML_ERP.PRC_LIFTING_AND_DO_RPT_REACT";

            var reportRows = new List<LiftingAndDoReportDto>();
            var reportHeader = new LiftingAndDoReportHeaderDto();

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

            var resultCursor = new OracleParameter("P_RESULT", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(resultCursor);

            var headerCursor = new OracleParameter("P_HEADER_RESULT", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(headerCursor);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                using (var headerReader =
                    ((OracleRefCursor)headerCursor.Value).GetDataReader())
                {
                    if (await headerReader.ReadAsync())
                    {
                        reportHeader.ReportType = headerReader["REPORT_TYPE_NAME"]?.ToString() ?? string.Empty;
                        reportHeader.MonthlyDateRange = headerReader["MONTHLY_DATE_RANGE"]?.ToString() ?? string.Empty;
                        reportHeader.DailyDateRange = headerReader["DAILY_DATE_RANGE"]?.ToString() ?? string.Empty;
                    }
                }

                using var reader = ((OracleRefCursor)resultCursor.Value).GetDataReader();

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
                        ChannelType = reader.IsDBNull(channelTypeIndex)
                            ? null
                            : reader.GetInt32(channelTypeIndex),

                        ChannelId = reader.IsDBNull(channelIdIndex)
                            ? null
                            : reader.GetInt32(channelIdIndex),

                        ChannelName = reader.IsDBNull(channelNameIndex)
                            ? string.Empty
                            : reader.GetString(channelNameIndex),

                        ProductId = reader.IsDBNull(productIdIndex)
                            ? null
                            : reader.GetInt32(productIdIndex),

                        ProductName = reader.IsDBNull(productNameIndex)
                            ? string.Empty
                            : reader.GetString(productNameIndex),

                        MonthlyConsumer = reader.IsDBNull(monthlyConsumerIndex)
                            ? null
                            : reader.GetDecimal(monthlyConsumerIndex),

                        MonthlyBulk = reader.IsDBNull(monthlyBulkIndex)
                            ? null
                            : reader.GetDecimal(monthlyBulkIndex),

                        MonthlyCorporate = reader.IsDBNull(monthlyCorporateIndex)
                            ? null
                            : reader.GetDecimal(monthlyCorporateIndex),

                        MonthlyCommodityTrading = reader.IsDBNull(monthlyCommodityTradingIndex)
                            ? null
                            : reader.GetDecimal(monthlyCommodityTradingIndex),

                        MonthlyTotal = reader.IsDBNull(monthlyTotalIndex)
                            ? null
                            : reader.GetDecimal(monthlyTotalIndex),

                        DailyConsumer = reader.IsDBNull(dailyConsumerIndex)
                            ? null
                            : reader.GetDecimal(dailyConsumerIndex),

                        DailyBulk = reader.IsDBNull(dailyBulkIndex)
                            ? null
                            : reader.GetDecimal(dailyBulkIndex),

                        DailyCorporate = reader.IsDBNull(dailyCorporateIndex)
                            ? null
                            : reader.GetDecimal(dailyCorporateIndex),

                        DailyCommodityTrading = reader.IsDBNull(dailyCommodityTradingIndex)
                            ? null
                            : reader.GetDecimal(dailyCommodityTradingIndex),

                        DailyTotal = reader.IsDBNull(dailyTotalIndex)
                            ? null
                            : reader.GetDecimal(dailyTotalIndex)
                    };

                    reportRows.Add(row);
                }

                return new LiftingAndDoReportResponseDto
                {
                    ReportHeader = reportHeader,
                    ReportRows = reportRows
                };

            }
            catch (OracleException ex)
            {
                throw new Exception("Unable to generate Lifting and DO Report.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while generating the Lifting and DO Report",ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }
    }
}