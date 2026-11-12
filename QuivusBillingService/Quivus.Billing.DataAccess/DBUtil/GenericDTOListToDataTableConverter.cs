using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using System.Reflection;

namespace Quivus.Billing.DataAccess.DBUtil
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date: 12/29/2011
    /// Generic class to convert the DTO list to datatable   
    /// </summary>
    public class GenericDTOListToDataTableConverter
    {
        
        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/29/2011
        /// convert the DTO list to datatable to be used as TVP  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ConvertDTOListToDataTable<T>(IList<T> data)        
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
                                   
            foreach (PropertyDescriptor prop in properties)
            {                
                if (prop.Name.ToUpper() != "CREATEDAT" && prop.Name.ToUpper() != "MODIFIEDAT")
                {
                    table.Columns.Add(
                        prop.Name,
                        (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            ? Nullable.GetUnderlyingType(prop.PropertyType)
                            : prop.PropertyType
                    );
                }
            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.Name.ToUpper() != "CREATEDAT" && prop.Name.ToUpper() != "MODIFIEDAT")
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/29/2011
        /// convert the DTO list to datatable to be used as TVP  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ConvertTransactionDTOListToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            bool isNameChanged = false;
            string sNewName = "";

            foreach (PropertyDescriptor prop in properties)
            {
                switch (prop.Name.ToUpper())
                {
                    case "ID":
                        isNameChanged = true;
                        sNewName = "ClaimTransactionID";
                        break;
                    case "PROCEDUREID":
                        isNameChanged = true;
                        sNewName = "PatientProcedureID";
                        break;
                    case "DATEFROM":
                        isNameChanged = true;
                        sNewName = "ServiceDateFrom";
                        break;
                    case "DATETO":
                        isNameChanged = true;
                        sNewName = "ServiceDateTo";
                        break;
                    case "INS1RESP":
                        isNameChanged = true;
                        sNewName = "Insurance1Response";
                        break;
                    case "INS2RESP":
                        isNameChanged = true;
                        sNewName = "Insurance2Response";
                        break;
                    case "INS3RESP":
                        isNameChanged = true;
                        sNewName = "Insurance3Response";
                        break;
                    default: isNameChanged = false;
                        break;
                }
                if (prop.Name.ToUpper() != "CREATEDAT" && prop.Name.ToUpper() != "MODIFIEDAT")
                {
                    table.Columns.Add(
                        ((isNameChanged) ? sNewName : prop.Name),
                        (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            ? Nullable.GetUnderlyingType(prop.PropertyType)
                            : prop.PropertyType
                    );
                }
            }

            isNameChanged = false;            
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    switch (prop.Name.ToUpper())
                    {
                        case "ID":
                            isNameChanged = true;
                            sNewName = "ClaimTransactionID";
                            break;
                        case "PROCEDUREID":
                            isNameChanged = true;
                            sNewName = "PatientProcedureID";
                            break;
                        case "DATEFROM":
                            isNameChanged = true;
                            sNewName = "ServiceDateFrom";
                            break;
                        case "DATETO":
                            isNameChanged = true;
                            sNewName = "ServiceDateTo";
                            break;
                        case "INS1RESP":
                            isNameChanged = true;
                            sNewName = "Insurance1Response";
                            break;
                        case "INS2RESP":
                            isNameChanged = true;
                            sNewName = "Insurance2Response";
                            break;
                        case "INS3RESP":
                            isNameChanged = true;
                            sNewName = "Insurance3Response";
                            break;
                        default: isNameChanged = false;
                            break;
                    }
                    if (prop.Name.ToUpper() != "CREATEDAT" && prop.Name.ToUpper() != "MODIFIEDAT")
                        row[((isNameChanged) ? sNewName : prop.Name)] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
        
    }
}
