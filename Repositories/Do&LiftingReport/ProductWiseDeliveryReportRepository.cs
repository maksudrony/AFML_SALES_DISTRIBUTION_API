using AFML_SALES_DISTRIBUTION_API.DTOs.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace AFML_SALES_DISTRIBUTION_API.Repositories.Do_LiftingReport
{
    public class ProductWiseDeliveryReportRepository : IProductWiseDeliveryReportRepository
    {
        private readonly string _connectionString;

        public ProductWiseDeliveryReportRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }

        public async Task<List<ProductWiseDeliveryReportDto>> GetProductWiseDeliveryReportFromDbAsync(
            DateTime? fromDate,
            DateTime? toDate,
            string? entryBy,
            int? productId)
        {
            const string storedProcedureName = "AFML_ERP.PRC_PROD_WISE_DELIVERY_REACT";

            var rows = new List<ProductWiseDeliveryReportDto>();

            await using var connection = new OracleConnection(_connectionString);
            await using var command = new OracleCommand(storedProcedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("P_FROM_DATE", OracleDbType.Date).Value = (object?)fromDate ?? DBNull.Value;
            command.Parameters.Add("P_TO_DATE", OracleDbType.Date).Value = (object?)toDate ?? DBNull.Value;
            command.Parameters.Add("P_ENTRY_BY", OracleDbType.Varchar2).Value = (object?)entryBy ?? DBNull.Value;
            command.Parameters.Add("P_PRODUCT_ID", OracleDbType.Decimal).Value = (object?)productId ?? DBNull.Value;

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
                int productIdIndex = reader.GetOrdinal("PRODUCT_ID");
                int prodCodeIndex = reader.GetOrdinal("PROD_CODE");
                int prodNameIndex = reader.GetOrdinal("PROD_NAME");

                int bagDelQtyIndex = reader.GetOrdinal("BAG_DEL_QTY");
                int delTonIndex = reader.GetOrdinal("DEL_TON");
                int deliveryValueIndex = reader.GetOrdinal("DELIVERY_VALUE");

                int ratePerBagIndex = reader.GetOrdinal("RATE_PER_BAG");
                int ratePerMtIndex = reader.GetOrdinal("RATE_PER_MT");

                int bagReturnQtyIndex = reader.GetOrdinal("BAG_RETURN_QTY");
                int totReturnValueIndex = reader.GetOrdinal("TOT_RETURN_VALUE");
                int returnQtyTonIndex = reader.GetOrdinal("RETURN_QTY_TON");

                int netDelQtyIndex = reader.GetOrdinal("NET_DEL_QTY");
                int netDelValueIndex = reader.GetOrdinal("NET_DEL_VALUE");

                while (await reader.ReadAsync())
                {
                    var row = new ProductWiseDeliveryReportDto
                    {
                        ProductId = reader.IsDBNull(productIdIndex) ? 0 : reader.GetInt32(productIdIndex),
                        ProdCode = reader.IsDBNull(prodCodeIndex) ? string.Empty : reader.GetString(prodCodeIndex),
                        ProdName = reader.IsDBNull(prodNameIndex) ? string.Empty : reader.GetString(prodNameIndex),

                        BagDelQty = reader.IsDBNull(bagDelQtyIndex) ? 0 : reader.GetDecimal(bagDelQtyIndex),
                        DelTon = reader.IsDBNull(delTonIndex) ? 0 : reader.GetDecimal(delTonIndex),
                        DeliveryValue = reader.IsDBNull(deliveryValueIndex) ? 0 : reader.GetDecimal(deliveryValueIndex),

                        RatePerBag = reader.IsDBNull(ratePerBagIndex) ? 0 : reader.GetDecimal(ratePerBagIndex),
                        RatePerMt = reader.IsDBNull(ratePerMtIndex) ? 0 : reader.GetDecimal(ratePerMtIndex),

                        BagReturnQty = reader.IsDBNull(bagReturnQtyIndex) ? 0 : reader.GetDecimal(bagReturnQtyIndex),
                        TotReturnValue = reader.IsDBNull(totReturnValueIndex) ? 0 : reader.GetDecimal(totReturnValueIndex),
                        ReturnQtyTon = reader.IsDBNull(returnQtyTonIndex) ? 0 : reader.GetDecimal(returnQtyTonIndex),

                        NetDelQty = reader.IsDBNull(netDelQtyIndex) ? 0 : reader.GetDecimal(netDelQtyIndex),
                        NetDelValue = reader.IsDBNull(netDelValueIndex) ? 0 : reader.GetDecimal(netDelValueIndex)
                    };

                    rows.Add(row);
                }
                return rows;
            }
            catch (OracleException ex)
            {
                throw new Exception($"Opps! '{storedProcedureName}' execution failed: {ex.Message}", ex);
            }
        }
    }
}
