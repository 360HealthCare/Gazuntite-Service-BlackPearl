using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Quivus.Utility;


namespace QuivusUtility.Utilities
{
    public class QuickInputTempUtility
    {
        public bool SaveTempQI(byte[] byteArray, string fileName)
        {
            bool response = false;
            try
            {
                if (!Directory.Exists(VisitConstant.VISIT_QI_TEMP_FILE_DIRECTORY))
                {
                    Directory.CreateDirectory(VisitConstant.VISIT_QI_TEMP_FILE_DIRECTORY);
                }
               
                string filePath = VisitConstant.VISIT_QI_TEMP_FILE_DIRECTORY + VisitConstant.DIRECTORY_SAPERATOR + fileName;
                FileStream tempQIFile = new FileStream(filePath, FileMode.Create);
                tempQIFile.Write(byteArray, 0, byteArray.Length);
                tempQIFile.Close();
                byteArray = null;
                response = true;
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool DeleteTempQI(string fileName)
        {
            bool response = false;
            try
            {
                string filePath = VisitConstant.VISIT_QI_TEMP_FILE_DIRECTORY + VisitConstant.DIRECTORY_SAPERATOR + fileName;
                File.Delete(filePath);
                response = true;
            }
            catch
            {
                //TODO: uncomment the following line when deploying finally 
                //throw;
            }
            return response;
        }

        public bool IsTempQIExists(string fileName)
        {
            bool response = false;
            try
            {
                string filePath = VisitConstant.VISIT_QI_TEMP_FILE_DIRECTORY + VisitConstant.DIRECTORY_SAPERATOR + fileName;
                response = File.Exists(filePath);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public byte[] LoadTempQI(string fileName)
        {
            byte[] byteArray = null;
            try
            {
                string filePath = VisitConstant.VISIT_QI_TEMP_FILE_DIRECTORY + VisitConstant.DIRECTORY_SAPERATOR + fileName;
                FileStream tempQIFile = new FileStream(filePath, FileMode.Open);
                BinaryReader tempQIBR = new BinaryReader(tempQIFile);
                long totalBytes = new System.IO.FileInfo(filePath).Length;
                byteArray = tempQIBR.ReadBytes((Int32)totalBytes);
                tempQIFile.Close();
                tempQIFile.Dispose();
                tempQIFile = null;
                tempQIBR.Close();
                
            }
            catch
            {
                throw;
            }
            return byteArray;
        }

    }
}
