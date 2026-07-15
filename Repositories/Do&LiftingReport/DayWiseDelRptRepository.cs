using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace AFML_SALES_DISTRIBUTION_API.Repositories.Do_LiftingReport;

public class DayWiseDelRptRepository : IDayWiseDelRptRepository
{
    private readonly string _connectionString;

    public DayWiseDelRptRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
    }

    public async Task<List<DayWiseDelRptMstDto>> GetMstFromDbAsync(
        DateTime fromDate,
        DateTime toDate,
        int fromTime,
        int toTime,
        int? channelId,
        int? distribId,
        string entryBy)
    {
        const string storedProcedureName = "AFML_ERP.PRC_DAY_WISE_DEL_RPT_MST_REACT";

        var reportRows = new List<DayWiseDelRptMstDto>();

        await using var connection = new OracleConnection(_connectionString);
        await using var command = new OracleCommand(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("P_FROM_DATE", OracleDbType.Date).Value = fromDate;
        command.Parameters.Add("P_TO_DATE", OracleDbType.Date).Value = toDate;
        command.Parameters.Add("P_FROM_TIME", OracleDbType.Decimal).Value = fromTime;
        command.Parameters.Add("P_TO_TIME", OracleDbType.Decimal).Value = toTime;
        command.Parameters.Add("P_CHANNEL_ID", OracleDbType.Decimal).Value = (object?)channelId ?? DBNull.Value;
        command.Parameters.Add("P_DISTRIB_ID", OracleDbType.Decimal).Value = (object?)distribId ?? DBNull.Value;
        command.Parameters.Add("P_ENTRY_BY", OracleDbType.Varchar2).Value = entryBy;

        var resultCursor = new OracleParameter("P_MST_RESULT", OracleDbType.RefCursor)
        {
            Direction = ParameterDirection.Output
        };

        command.Parameters.Add(resultCursor);

        try
        {
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            using var reader = ((OracleRefCursor)resultCursor.Value).GetDataReader();

            int dcIdIndex = reader.GetOrdinal("DC_ID");
            int dcNoIndex = reader.GetOrdinal("DC_NO");
            int dcDateIndex = reader.GetOrdinal("DC_DATE");
            int confirmDateIndex = reader.GetOrdinal("CONFIRM_DATE");
            int doIdIndex = reader.GetOrdinal("DO_ID");
            int doNoIndex = reader.GetOrdinal("DO_NO");
            int channelIdIndex = reader.GetOrdinal("CHANNEL_ID");
            int channelNameIndex = reader.GetOrdinal("CHANNEL_NAME");
            int zoneIdIndex = reader.GetOrdinal("ZONE_ID");
            int zoneNameIndex = reader.GetOrdinal("ZONE_NAME");
            int divisionIdIndex = reader.GetOrdinal("DIVISION_ID");
            int areaIdIndex = reader.GetOrdinal("AREA_ID");
            int territoryIdIndex = reader.GetOrdinal("TERRITORY_ID");
            int territoryNameIndex = reader.GetOrdinal("TERRITORY_NAME");
            int distribIdIndex = reader.GetOrdinal("DISTRIB_ID");
            int distribCodeIndex = reader.GetOrdinal("DISTRIB_CODE");
            int distribNameIndex = reader.GetOrdinal("DISTRIB_NAME");
            int challanQtyIndex = reader.GetOrdinal("CHALLAN_QTY");

            while (await reader.ReadAsync())
            {
                var row = new DayWiseDelRptMstDto
                {
                    DcId = reader.IsDBNull(dcIdIndex)
                        ? null
                        : reader.GetInt64(dcIdIndex),

                    DcNo = reader.IsDBNull(dcNoIndex)
                        ? string.Empty
                        : reader.GetString(dcNoIndex),

                    DcDate = reader.IsDBNull(dcDateIndex)
                        ? string.Empty
                        : reader.GetString(dcDateIndex),

                    ConfirmDate = reader.IsDBNull(confirmDateIndex)
                        ? string.Empty
                        : reader.GetString(confirmDateIndex),

                    DoId = reader.IsDBNull(doIdIndex)
                        ? null
                        : reader.GetInt64(doIdIndex),

                    DoNo = reader.IsDBNull(doNoIndex)
                        ? string.Empty
                        : reader.GetString(doNoIndex),

                    ChannelId = reader.IsDBNull(channelIdIndex)
                        ? null
                        : reader.GetInt32(channelIdIndex),

                    ChannelName = reader.IsDBNull(channelNameIndex)
                        ? string.Empty
                        : reader.GetString(channelNameIndex),

                    ZoneId = reader.IsDBNull(zoneIdIndex)
                        ? null
                        : reader.GetInt32(zoneIdIndex),

                    ZoneName = reader.IsDBNull(zoneNameIndex)
                        ? string.Empty
                        : reader.GetString(zoneNameIndex),

                    DivisionId = reader.IsDBNull(divisionIdIndex)
                        ? null
                        : reader.GetInt32(divisionIdIndex),

                    AreaId = reader.IsDBNull(areaIdIndex)
                        ? null
                        : reader.GetInt32(areaIdIndex),

                    TerritoryId = reader.IsDBNull(territoryIdIndex)
                        ? null
                        : reader.GetInt32(territoryIdIndex),

                    TerritoryName = reader.IsDBNull(territoryNameIndex)
                        ? string.Empty
                        : reader.GetString(territoryNameIndex),

                    DistribId = reader.IsDBNull(distribIdIndex)
                        ? null
                        : reader.GetInt32(distribIdIndex),

                    DistribCode = reader.IsDBNull(distribCodeIndex)
                        ? string.Empty
                        : reader.GetString(distribCodeIndex),

                    DistribName = reader.IsDBNull(distribNameIndex)
                        ? string.Empty
                        : reader.GetString(distribNameIndex),

                    ChallanQty = reader.IsDBNull(challanQtyIndex)
                        ? null
                        : reader.GetDecimal(challanQtyIndex)
                };

                reportRows.Add(row);
            }

            return reportRows;
        }
        catch (OracleException ex)
        {
            throw new Exception("Unable to generate Day Wise Delivery Detail Report.", ex);
            //throw new Exception (ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //throw new Exception("An unexpected error occurred while generating the Day Wise Delivery Detail Report.", ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<List<DayWiseDelRptDtlDto>> GetDtlFromDbAsync(long dcId)
    {
        const string storedProcedureName = "AFML_ERP.PRC_DAY_WISE_DEL_RPT_DTL_REACT";

        var reportRows = new List<DayWiseDelRptDtlDto>();

        await using var connection = new OracleConnection(_connectionString);
        await using var command = new OracleCommand(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("P_DC_ID", OracleDbType.Int64).Value = dcId;

        var resultCursor = new OracleParameter("P_DTL_RESULT", OracleDbType.RefCursor)
        {
            Direction = ParameterDirection.Output
        };

        command.Parameters.Add(resultCursor);

        try
        {
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            using var reader = ((OracleRefCursor)resultCursor.Value).GetDataReader();

            int dcIdIndex = reader.GetOrdinal("DC_ID");
            int productIdIndex = reader.GetOrdinal("PRODUCT_ID");
            int prodCodeIndex = reader.GetOrdinal("PROD_CODE");
            int prodNameIndex = reader.GetOrdinal("PROD_NAME");
            int unitNameIndex = reader.GetOrdinal("UNIT_NAME");
            int challanQtyIndex = reader.GetOrdinal("CHALLAN_QTY");
            int productPriceIndex = reader.GetOrdinal("PRODUCT_PRICE");
            int challanValueIndex = reader.GetOrdinal("CHALLAN_VALUE");

            while (await reader.ReadAsync())
            {
                var row = new DayWiseDelRptDtlDto
                {
                    DcId = reader.IsDBNull(dcIdIndex)
                        ? null
                        : reader.GetInt64(dcIdIndex),

                    ProductId = reader.IsDBNull(productIdIndex)
                        ? null
                        : reader.GetInt32(productIdIndex),

                    ProdCode = reader.IsDBNull(prodCodeIndex)
                        ? string.Empty
                        : reader.GetString(prodCodeIndex),

                    ProdName = reader.IsDBNull(prodNameIndex)
                        ? string.Empty
                        : reader.GetString(prodNameIndex),

                    UnitName = reader.IsDBNull(unitNameIndex)
                        ? string.Empty
                        : reader.GetString(unitNameIndex),

                    ChallanQty = reader.IsDBNull(challanQtyIndex)
                        ? null
                        : reader.GetDecimal(challanQtyIndex),

                    ProductPrice = reader.IsDBNull(productPriceIndex)
                        ? null
                        : reader.GetDecimal(productPriceIndex),

                    ChallanValue = reader.IsDBNull(challanValueIndex)
                        ? null
                        : reader.GetDecimal(challanValueIndex)
                };

                reportRows.Add(row);
            }

            return reportRows;
        }
        catch (OracleException ex)
        {
            throw new Exception("Unable to generate Day Wise Delivery Detail Report.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while generating the Day Wise Delivery Detail Report.", ex);
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