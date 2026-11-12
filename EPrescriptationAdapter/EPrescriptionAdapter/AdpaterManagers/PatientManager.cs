using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using EprescriptionService.Common.DTOs;
using EprescriptionService.Utility;

namespace AdpaterManagers
{
    public class PatientManager
    {

        private XMLNode PrepareVendorInfoNode(); 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientInfoDTO"></param>
        public void UploadPatient(PatientInfoDTO patientInfoDTO)
        {

            var xmlUploadPatientRequest = new XmlDocument();
                
            XmlTextReader messageTextReader;
          
                xmlUploadPatientRequest = new XmlTextReader(HttpContext.Current.Server.MapPath("~/") + SystemConstants.UploadPatientMessagePath);
                xmlUploadPatientRequest.Read();
                xmlUploadPatientRequest.Load(messageTextReader);
               

            #region filling patient dto



            #endregion
            

        }
    }
}
