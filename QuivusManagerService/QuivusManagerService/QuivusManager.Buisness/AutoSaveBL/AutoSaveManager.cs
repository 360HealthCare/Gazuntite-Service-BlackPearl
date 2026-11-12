using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.VisitService;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using QuivusUtility.Utilities;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;

namespace QuivusManager.Buisness.AutoSaveBL
{
    public class AutoSaveManager
    {
        QuickInputTempUtility tempUtilityObj = null;

        public bool SaveTempQI(QuickInputDTO quickInputDTO, string fileName)
        {
            bool response = false;
            try
            {
                MemoryStream tempQIMS = new MemoryStream();
                BinaryFormatter tempQIBF = new BinaryFormatter();
                tempQIBF.Serialize(tempQIMS, quickInputDTO);
                byte[] byteArray = tempQIMS.ToArray();
                if (tempUtilityObj == null)
                {
                    tempUtilityObj = new QuickInputTempUtility(); 
                }
                response = tempUtilityObj.SaveTempQI(byteArray, fileName);
                tempQIMS.Close();
                tempQIMS.Dispose();
                tempQIMS = null;
            }
            catch
            {
                throw;
            }
            return response;
        }

        public QuickInputResponse LoadTempQI(string fileName)
        {
            QuickInputResponse tempQIResponse = new QuickInputResponse();
            try
            {
                if (tempUtilityObj == null)
                {
                    tempUtilityObj = new QuickInputTempUtility(); 
                }
                byte[] byteArray = tempUtilityObj.LoadTempQI(fileName);
                MemoryStream tempQIMS = new MemoryStream(byteArray);
                BinaryFormatter tempQIBF = new BinaryFormatter();
                tempQIMS.Position = 0;
                QuickInputDTO quickInputDTO = new QuickInputDTO();
                quickInputDTO = (QuickInputDTO)tempQIBF.Deserialize(tempQIMS);
                tempQIResponse.ResponseData = quickInputDTO;
            }
            catch
            {
                throw;
            }
            return tempQIResponse;
        }
    }
}
