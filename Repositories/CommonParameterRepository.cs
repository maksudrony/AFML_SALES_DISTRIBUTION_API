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
    public class CommonParameterRepository : ICommonParameterRepository
    {
        private readonly string _connectionString;

        public CommonParameterRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection")!;
        }

        private async Task<List<CommonParameterDto>> ExecuteParamSPAsync(string spName, List<OracleParameter> parameters)
        {
            var list = new List<CommonParameterDto>();
            await using var connection = new OracleConnection(_connectionString);
            await using var command = new OracleCommand(spName, connection) { CommandType = CommandType.StoredProcedure };

            foreach (var param in parameters) command.Parameters.Add(param);

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
                    list.Add(new CommonParameterDto
                    {
                        Name = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        Id = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1))
                    });
                }
                return list;
            }
            catch (OracleException ex)
            {
                throw new Exception($"Oracle database procedure '{spName}' execution failed: {ex.Message}", ex);
            }
        }

        public async Task<List<CommonParameterDto>> GetChannelsFromDbAsync(string userId)
        {
            var p = new List<OracleParameter> { 
                new("P_USER_ID", OracleDbType.Varchar2) { Value = userId } 
            };
            return await ExecuteParamSPAsync("AFML_ERP.PRC_SALES_CHANNEL", p);
        }

        public async Task<List<CommonParameterDto>> GetZonesFromDbAsync(string userId, decimal channelId)
        {
            var p = new List<OracleParameter> {
                new("P_USER_ID", OracleDbType.Varchar2) { Value = userId },
                new("P_CHANNEL_ID", OracleDbType.Decimal) { Value = channelId }
            };
            return await ExecuteParamSPAsync("AFML_ERP.PRC_ZONE_INFO", p);
        }

        public async Task<List<CommonParameterDto>> GetDivisionsFromDbAsync(string userId, decimal zoneId)
        {
            var p = new List<OracleParameter> {
                new("P_USER_ID", OracleDbType.Varchar2) { Value = userId },
                new("P_ZONE_ID", OracleDbType.Decimal) { Value = zoneId }
            };
            return await ExecuteParamSPAsync("AFML_ERP.PRC_DIVISION_INFO", p);
        }

        public async Task<List<CommonParameterDto>> GetAreasFromDbAsync(string userId, decimal divisionId)
        {
            var p = new List<OracleParameter> {
                new("P_USER_ID", OracleDbType.Varchar2) { Value = userId },
                new("P_DIVISION_ID", OracleDbType.Decimal) { Value = divisionId }
            };
            return await ExecuteParamSPAsync("AFML_ERP.PRC_AREA_INFO", p);
        }

        public async Task<List<CommonParameterDto>> GetTerritoriesFromDbAsync(string userId, decimal areaId)
        {
            var p = new List<OracleParameter> {
                new("P_USER_ID", OracleDbType.Varchar2) { Value = userId },
                new("P_AREA_ID", OracleDbType.Decimal) { Value = areaId }
            };
            return await ExecuteParamSPAsync("AFML_ERP.PRC_TERRITORY_INFO", p);
        }

        public async Task<List<CommonParameterDto>> GetProductCategoriesFromDbAsync()
        {
            return await ExecuteParamSPAsync("AFML_ERP.PRC_PRODUCT_CATEGORY_IMS", []);
        }

        public async Task<List<CommonParameterDto>> GetProductDetailFromDbAsync()
        {
            //return await ExecuteParamSPAsync("AFML_ERP.PRC_PRODUCT_DETAIL", new List<OracleParameter>());
            //new List<OracleParameter>() eta baad diye ekhon [] eta modern way likhe , mane empty list parameter hisebe jabe
            
            return await ExecuteParamSPAsync("AFML_ERP.PRC_PRODUCT_DETAIL", []);
        }

        public async Task<List<CommonParameterDto>> GetSalesChannelTypeFromDbAsync(string userId)
        {
            var p = new List<OracleParameter> {
                new("P_USER_ID", OracleDbType.Varchar2) { Value = userId }
            };
            return await ExecuteParamSPAsync("AFML_ERP.PRC_SALES_CHANNEL_TYPE", p);
        }

        public async Task<List<CommonParameterDto>> GetQuantityTypeFromDbAsync()
        {
            return await ExecuteParamSPAsync("AFML_ERP.PRC_QUANTITY_TYPE", []);
        }

        public async Task<List<CommonParameterDto>> GetReportTypeFromDbAsync()
        {
            return await ExecuteParamSPAsync("AFML_ERP.PRC_REPORT_TYPE", []);
        }

        public async Task<List<CommonParameterDto>> GetTimeManagementFromDbAsync()
        {
            return await ExecuteParamSPAsync("AFML_ERP.PRC_TIME_MANAGEMENT", []);
        }

        public async Task<List<CommonParameterDto>> GetChallanDistributorFromDbAsync(DateTime? fromDate, DateTime? toDate,
            decimal? channelId, string userId)
        {
            var p = new List<OracleParameter> {
                new("P_FROM_DATE", OracleDbType.Date) { Value = fromDate },
                new("P_TO_DATE", OracleDbType.Date) { Value = toDate },
                new("P_CHANNEL_ID", OracleDbType.Decimal) { Value = channelId },
                new("P_USER_ID", OracleDbType.Varchar2) { Value = userId }
            };
            return await ExecuteParamSPAsync("AFML_ERP.PRC_CHALLAN_DISTRIBUTOR", p);
        }

    }
}