using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace AFML_SALES_DISTRIBUTION_API.Repositories.Do_LiftingReport
{
    public class AverageRateRptRepository : IAverageRateRptRepository
    {
        private readonly string _connectionString;

        public AverageRateRptRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }

        public async Task<AverageRateRptResponseDto> GetAverageRateRptFromDbAsync(
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
            const string storedProcedureName = "AFML_ERP.PRC_AVG_RATE_RPT_REACT";

            var reportRows = new List<AverageRateRptDto>();
            var reportHeader = new AverageRateRptHeaderDto();

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

                int monQtyIndex = reader.GetOrdinal("MON_QTY");
                int monValueIndex = reader.GetOrdinal("MON_VALUE");
                int monAvgRateIndex = reader.GetOrdinal("MON_AVG_RATE");

                int dayQtyIndex = reader.GetOrdinal("DAY_QTY");
                int dayValueIndex = reader.GetOrdinal("DAY_VALUE");
                int dayAvgRateIndex = reader.GetOrdinal("DAY_AVG_RATE");


                while (await reader.ReadAsync())
                {
                    var row = new AverageRateRptDto
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



                        MonQty = reader.IsDBNull(monQtyIndex)
                            ? null
                            : reader.GetDecimal(monQtyIndex),

                        MonValue = reader.IsDBNull(monValueIndex)
                            ? null
                            : reader.GetDecimal(monValueIndex),

                        MonAvgRate = reader.IsDBNull(monAvgRateIndex)
                            ? null
                            : reader.GetDecimal(monAvgRateIndex),



                        DayQty = reader.IsDBNull(dayQtyIndex)
                            ? null
                            : reader.GetDecimal(dayQtyIndex),

                        DayValue = reader.IsDBNull(dayValueIndex)
                            ? null
                            : reader.GetDecimal(dayValueIndex),

                        DayAvgRate = reader.IsDBNull(dayAvgRateIndex)
                            ? null
                            : reader.GetDecimal(dayAvgRateIndex)

                    };

                    reportRows.Add(row);
                }

                return new AverageRateRptResponseDto
                {
                    ReportHeader = reportHeader,
                    ReportRows = reportRows
                };

            }
            catch (OracleException ex)
            {
                throw new Exception("Unable to generate Average Rate Report.", ex);
                //throw new Exception($"Oracle database procedure '{storedProcedureName}' execution failed: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while generating the Average Rate Report", ex);
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
