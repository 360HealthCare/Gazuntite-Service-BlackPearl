using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using System.Reflection;
using System.Data;

namespace Quivus.Billing.DataAccess.DBUtil
{
    public class GenericDataReaderToDTOConverter
    {
        public static void ConvertToTransactionDTOList<T>(List<T> destObjectList, IDataReader objIReader, Type destType)
        {
            String emptyString = string.Empty;

            PropertyInfo[] propertyInfos;
            propertyInfos = destType.GetProperties(); // Load Properties Of Class 

            try
            {
                while (objIReader.Read())
                {
                    T destObject = (T)Activator.CreateInstance(destType);

                    foreach (PropertyInfo currentProperty in propertyInfos)
                    {
                        if (currentProperty.PropertyType.IsClass && !currentProperty.PropertyType.IsInstanceOfType(emptyString))
                        {
                            //if (targetObj != null)
                            //{
                            //    object sourceSub = currentProperty.GetValue(sourceObject, null);
                            //    object destSub = targetObj.GetValue(destObject, null);
                            //    CloneObject(sourceSub, ref destSub);
                            //}
                        }
                        else
                        {
                            object readData = null;

                            try
                            {
                                bool isNameChanged = false;
                                string sNewName = "";
                                switch (currentProperty.Name.ToUpper())
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
                                readData = objIReader[((isNameChanged) ? sNewName : currentProperty.Name)];
                            }
                            catch (Exception exp)
                            {
                                QuivusLogger.Error(exp + ". Just log this exception as this property not found in DB.");
                            }

                            if (readData != DBNull.Value && readData != null)
                            {
                                currentProperty.SetValue(destObject, readData, null);
                            }
                        }
                    }

                    destObjectList.Add(destObject);
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new DTOConversionException(ProjectConstant.GENERIC_DTO_CONVERSION_ERROR_MSG + ".ConvertToDTOList", ProjectConstant.ERROR_CODE.GenericDTOConversion.ToString());
            }
        }
    }
}
