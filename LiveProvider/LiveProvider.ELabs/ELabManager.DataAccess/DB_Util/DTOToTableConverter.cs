using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LiveProvider.Utility.Logger;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.StoreProcedureConstant;
using LiveProvider.Utility.Constant.Errors;

namespace ELabManager.DataAccess.DB_Util
{
    /// <summary>
    /// Author Sahibzada Sami uddin
    /// Date 17 Oct 2011
    /// convert the module notes to TVP
    /// </summary>
    public class DTOToTableConverter
    {
        internal static DataTable StationIDsListConverter(List<int> stationIDsList)
        {
            DataTable stationIDsTable = null;
            try
            {
                stationIDsTable = new DataTable();
                stationIDsTable.Columns.Add("StationId", typeof(System.Int32));
                stationIDsTable.AcceptChanges();
                foreach (int stationID in stationIDsList)
                {
                    DataRow drStationID = stationIDsTable.NewRow();
                    drStationID["StationId"] = stationID;
                    stationIDsTable.Rows.Add(drStationID);
                }
                stationIDsTable.AcceptChanges();
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw new TableConversionException(ELabSPConstant.ELAB_CONVERSION_ERROR_MSG + ".StationIDsListConverter", ErrorCodes.STATION_IDS_CONVERTER);
            }
            return stationIDsTable;
        }


        internal static DataTable OrderNumbersListConverter(List<string> orderNumbersList)
        {
            DataTable orderNumbersTable = null;
            try
            {
                orderNumbersTable = new DataTable();
                orderNumbersTable.Columns.Add("OrderNumber", typeof(System.String));
                orderNumbersTable.AcceptChanges();
                foreach (string orderNumber in orderNumbersList)
                {
                    DataRow drOrderNumber = orderNumbersTable.NewRow();
                    drOrderNumber["OrderNumber"] = orderNumber;
                    orderNumbersTable.Rows.Add(drOrderNumber);
                }
                orderNumbersTable.AcceptChanges();
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw new TableConversionException(ELabSPConstant.ELAB_CONVERSION_ERROR_MSG + ".StationIDsListConverter", ErrorCodes.STATION_IDS_CONVERTER);
            }
            return orderNumbersTable;
        }
    }
}
