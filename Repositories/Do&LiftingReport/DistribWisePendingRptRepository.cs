using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace AFML_SALES_DISTRIBUTION_API.Repositories.Do_LiftingReport
{
    public class DistribWisePendingRptRepository : IDistribWisePendingRptRepository
    {
        private readonly string _connectionString;

        public DistribWisePendingRptRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }

        public async Task<List<DistribWisePendingRptDto>> GetDistribWisePendingRptFromDbAsync(
            DateTime? fromDate,
            DateTime? toDate,
            int? channelId, 
            int? zoneId, 
            int? divisionId, 
            int? areaId, 
            int? territoryId, 
            int? productId,
            int? distribId, 
            int orderTypeId)
        {
            const string storedProcedureName = "AFML_ERP.PRC_DISTRIB_WISE_PENDING_RPT_REACT";

            var reportRows = new List<DistribWisePendingRptDto>();

            await using var connection = new OracleConnection(_connectionString);
            await using var command = new OracleCommand(storedProcedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("P_FROM_DATE", OracleDbType.Date).Value = (object?)fromDate ?? DBNull.Value;
            command.Parameters.Add("P_TO_DATE", OracleDbType.Date).Value = (object?)toDate ?? DBNull.Value;
            command.Parameters.Add("P_CHANNEL_ID", OracleDbType.Decimal).Value = (object?)channelId ?? DBNull.Value;
            command.Parameters.Add("P_ZONE_ID", OracleDbType.Decimal).Value = (object?)zoneId ?? DBNull.Value;
            command.Parameters.Add("P_DIVISION_ID", OracleDbType.Decimal).Value = (object?)divisionId ?? DBNull.Value;
            command.Parameters.Add("P_AREA_ID", OracleDbType.Decimal).Value = (object?)areaId ?? DBNull.Value;
            command.Parameters.Add("P_TERRITORY_ID", OracleDbType.Decimal).Value = (object?)territoryId ?? DBNull.Value;
            command.Parameters.Add("P_PRODUCT_ID", OracleDbType.Decimal).Value = (object?)productId ?? DBNull.Value;
            command.Parameters.Add("P_DISTRIB_ID", OracleDbType.Decimal).Value = (object?)distribId ?? DBNull.Value;
            command.Parameters.Add("ORDER_TYPE_ID", OracleDbType.Decimal).Value = orderTypeId;

            var resultCursor = new OracleParameter("P_RESULT", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(resultCursor);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                using var reader = ((OracleRefCursor)resultCursor.Value).GetDataReader();

                int channelIdIndex = reader.GetOrdinal("CHANNEL_ID");
                int channelNameIndex = reader.GetOrdinal("CHANNEL_NAME");

                int divisionNameIndex = reader.GetOrdinal("DIVISION_NAME");
                int territoryNameIndex = reader.GetOrdinal("TERRITORY_NAME");

                int distribCodeIndex = reader.GetOrdinal("DISTRIB_CODE");
                int distribNameIndex = reader.GetOrdinal("DISTRIB_NAME");

                int doIdIndex = reader.GetOrdinal("DO_ID");
                int doNoIndex = reader.GetOrdinal("DO_NO");
                int doDateIndex = reader.GetOrdinal("DO_DATE");

                int poNoIndex = reader.GetOrdinal("PO_NO");
                int deliveryPointIndex = reader.GetOrdinal("DELIVERY_POINT");

                int productIdIndex = reader.GetOrdinal("PRODUCT_ID");
                int productNameIndex = reader.GetOrdinal("PROD_NAME");

                int productPriceIndex = reader.GetOrdinal("PRODUCT_PRICE");

                int doQtyBagIndex = reader.GetOrdinal("DO_QTY_BAG");
                int doQtyTonIndex = reader.GetOrdinal("DO_QTY_TON");

                int pendingQtyBagIndex = reader.GetOrdinal("PENDING_QTY_BAG");
                int pendingQtyTonIndex = reader.GetOrdinal("PENDING_QTY_TON");


                while (await reader.ReadAsync())
                {
                    var row = new DistribWisePendingRptDto
                    {
                        ChannelId = reader.IsDBNull(channelIdIndex)
                        ? null
                        : reader.GetInt32(channelIdIndex),

                        ChannelName = reader.IsDBNull(channelNameIndex)
                        ? string.Empty
                        : reader.GetString(channelNameIndex),

                        DivisionName = reader.IsDBNull(divisionNameIndex)
                        ? string.Empty
                        : reader.GetString(divisionNameIndex),

                        TerritoryName = reader.IsDBNull(territoryNameIndex)
                        ? string.Empty
                        : reader.GetString(territoryNameIndex),

                        DistribCode = reader.IsDBNull(distribCodeIndex)
                        ? string.Empty
                        : reader.GetString(distribCodeIndex),

                        DistribName = reader.IsDBNull(distribNameIndex)
                        ? string.Empty
                        : reader.GetString(distribNameIndex),

                        DoId = reader.IsDBNull(doIdIndex)
                        ? null
                        : reader.GetInt64(doIdIndex),

                        DoNo = reader.IsDBNull(doNoIndex)
                        ? string.Empty
                        : reader.GetString(doNoIndex),

                        DoDate = reader.IsDBNull(doDateIndex)
                        ? string.Empty
                        : reader.GetString(doDateIndex),

                        PoNo = reader.IsDBNull(poNoIndex)
                        ? string.Empty
                        : reader.GetString(poNoIndex),

                        DeliveryPoint = reader.IsDBNull(deliveryPointIndex)
                        ? string.Empty
                        : reader.GetString(deliveryPointIndex),

                        ProductId = reader.IsDBNull(productIdIndex)
                        ? null
                        : reader.GetInt32(productIdIndex),

                        ProdName = reader.IsDBNull(productNameIndex)
                        ? string.Empty
                        : reader.GetString(productNameIndex),

                        ProductPrice = reader.IsDBNull(productPriceIndex)
                        ? null
                        : reader.GetDecimal(productPriceIndex),

                        DoQtyBag = reader.IsDBNull(doQtyBagIndex)
                        ? null
                        : reader.GetDecimal(doQtyBagIndex),

                        DoQtyTon = reader.IsDBNull(doQtyTonIndex)
                        ? null
                        : reader.GetDecimal(doQtyTonIndex),

                        PendingQtyBag = reader.IsDBNull(pendingQtyBagIndex)
                        ? null
                        : reader.GetDecimal(pendingQtyBagIndex),

                        PendingQtyTon = reader.IsDBNull(pendingQtyTonIndex)
                        ? null
                        : reader.GetDecimal(pendingQtyTonIndex),

                    };

                    reportRows.Add(row);
                }

                return reportRows;

            }
            catch (OracleException ex)
            {
                throw new Exception("Unable to generate Distributor Wise Pending Report Please Check Procedure.", ex);
                //throw new Exception($"Oracle database procedure '{storedProcedureName}' execution failed: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while generating the Distributor Wise Pending Report", ex);
            }
        }
    }
}
