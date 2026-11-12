using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;

namespace Quivus.Utility
{
    public class GenericDataReaderToDTOConverter
    {
        public static void ConvertToDTO<T>(ref T destObject, IDataReader objReader)
        {
            String emptyString = string.Empty;

            PropertyInfo[] propertyInfos;
            propertyInfos = destObject.GetType().GetProperties();

            try
            {
                foreach (PropertyInfo currentProperty in propertyInfos)
                {
                    if (currentProperty.PropertyType.IsClass && !currentProperty.PropertyType.IsInstanceOfType(emptyString))
                    {
                        //if (targetObj != null)
                        //{
                        //    object sourceSub = currentProperty.GetValue(sourceObject, null);
                        //    object destSub = targetObj.GetValue(destObject, null);
                        //    ConvertToDTO(sourceSub, ref destSub);
                        //}
                    }
                    else
                    {
                        object readData = null;

                        try
                        {
                            readData = objReader[currentProperty.Name];
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
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new DTOConversionException(ProjectConstant.GENERIC_DTO_CONVERSION_ERROR_MSG + ".ConvertToDTO", ProjectConstant.ERROR_CODE.GenericDTOConversion.ToString());
            }
        }


        public static void ConvertToDTOList<T>(List<T> destObjectList, IDataReader objIReader, Type destType)
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
                                readData = objIReader[currentProperty.Name];
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
