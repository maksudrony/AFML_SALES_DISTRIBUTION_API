using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AFML_SALES_DISTRIBUTION_API.DTOs;
using AFML_SALES_DISTRIBUTION_API.Interfaces;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace AFML_SALES_DISTRIBUTION_API.Repositories
{
    public class SummaryImsReportRepository : ISummaryImsReportRepository
    {
        private readonly string _connectionString;

        public SummaryImsReportRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }

        public async Task<List<SummaryImsReportRow>> GetReportDataFromDbAsync(DateTime fromDate, DateTime toDate,
            decimal? prodCatId, string entryBy, decimal channelId, decimal? zoneId, decimal? divisionId,
            decimal? areaId, decimal? territoryId)
        {
            const string storedProcedureName = "AFML_ERP.PRC_SUMMARY_IMS_REPORT_REACT";
            var rows = new List<SummaryImsReportRow>();

            await using var connection = new OracleConnection(_connectionString);
            await using var command = new OracleCommand(storedProcedureName, connection) { CommandType = CommandType.StoredProcedure };

            command.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Date) { Value = fromDate });
            command.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Date) { Value = toDate });
            command.Parameters.Add(new OracleParameter("P_PROD_CAT_ID", OracleDbType.Decimal) { Value = (object?)prodCatId ?? DBNull.Value });
            command.Parameters.Add(new OracleParameter("P_ENTRY_BY", OracleDbType.Varchar2) { Value = entryBy });
            command.Parameters.Add(new OracleParameter("P_CHANNEL_ID", OracleDbType.Decimal) { Value = channelId });
            command.Parameters.Add(new OracleParameter("P_ZONE_ID", OracleDbType.Decimal) { Value = (object?)zoneId ?? DBNull.Value });
            command.Parameters.Add(new OracleParameter("P_DIVISION_ID", OracleDbType.Decimal) { Value = (object?)divisionId ?? DBNull.Value });
            command.Parameters.Add(new OracleParameter("P_AREA_ID", OracleDbType.Decimal) { Value = (object?)areaId ?? DBNull.Value });
            command.Parameters.Add(new OracleParameter("P_TERRITORY_ID", OracleDbType.Decimal) { Value = (object?)territoryId ?? DBNull.Value });

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
                    var row = new SummaryImsReportRow
                    {
                        ChannelName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        ZoneName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        DivisionName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        AreaName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                        TerritoryName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                        DistribName = reader.IsDBNull(11) ? string.Empty : reader.GetString(11),
                        SoEnrol = reader.IsDBNull(12) ? string.Empty : reader.GetValue(12).ToString()!,
                        EmpName = reader.IsDBNull(13) ? string.Empty : reader.GetString(13),
                        JoiningDate = reader.IsDBNull(14) ? string.Empty : reader.GetOracleValue(14).ToString()!
                    };

                    int fieldCount = reader.FieldCount;
                    row.GrandTotal = reader.IsDBNull(fieldCount - 1) ? 0 : Convert.ToDecimal(reader.GetValue(fieldCount - 1));

                    for (int i = 15; i < fieldCount - 1; i++)
                    {
                        string columnName = reader.GetName(i);
                        decimal dayValue = reader.IsDBNull(i) ? 0 : Convert.ToDecimal(reader.GetValue(i));
                        row.DaysData[columnName] = dayValue;
                    }
                    rows.Add(row);
                }
                return rows;
            }
            catch (OracleException ex)
            {
                // 🚀 Ekhon absolute explicit procedure name throw korbe
                throw new Exception("Unable to generate Summary IMS Report Please Check Procedure.", ex);
                //throw new Exception($"Oracle database procedure '{storedProcedureName}' execution failed: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while generating the Day Wise Delivery Detail Report.", ex);
            }
        }
    }
}