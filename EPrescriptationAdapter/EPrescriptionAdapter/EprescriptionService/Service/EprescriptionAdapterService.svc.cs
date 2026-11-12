using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AdpaterManagers;
using AdpaterManagers.SSO;
using EprescriptionService.Common.DTOs;

namespace EprescriptionService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EprescriptionAdapterService" in code, svc and config file together.
    public class EprescriptionAdapterService : IEprescriptionAdapterService
    {
        #region IEPrescriptionAdapterService Members

        public string GetVersion()
        {
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return strVersion;
        }

        public string GetANSWebURL(string PracticeUserName)
        {
            try
            {
                ANSManager ansManger = new ANSManager();
                return ansManger.GetANSWebURL(PracticeUserName);


            }
            catch (Exception exception)
            {
                //TODO: Log the exception 
            }
            return "Success";

        }

        public string SSOLoginToEprescription(string rcopia_user_external_id, string Action, string StartUpScreen, string skip_auth, string MAC)
        {
            try
            {
                SingleSignonManger ssoManger = new SingleSignonManger();
                //TODO: Implement the SSO Call

                ANSManager ansManger = new ANSManager();
                string webURL = ansManger.GetANSWebURL("");
                return "Success";

            }
            catch (Exception exc)
            {

                //TODO: Log the exception
            }
            return null;
        }


        public string prepareLoginCallForEprescription(string rcopia_user_external_id)
        {
            string webURL = @"https://cert.drfirst.com/sso/portalServices";
            try
            {
                // SingleSignonManger ssoManger = new SingleSignonManger();
              //  ANSManager ansManger = new ANSManager();
               // webURL = ansManger.GetANSWebURL("");
                string practiceUserName = @"qu9898";

              //  webURL = webURL + @"?rcopia_portal_system_name=qvendor1&rcopia_user_id=qdoctor2&rcopia_user_external_id=ext_doc_123&action=login&service=rcopia&startup_screen=patient&rc_login_username=dfdoc&rc_practice_username=df18&system_name=vendordf18&vendor_id=vendordf18&external_patient_id=test1234&close_window=n&logout_url=https://cert.drfirst.com&allow_popup_screens=y&m&skip_auth=y&time=0408201108000";
                webURL =
                    @"https://cert.drfirst.com/sso/portalServices?rcopia_portal_system_name=qvendor1&rcopia_practice_user_name=qu9898&rcopia_user_external_id=ext_doc_123&rcopia_patient_system_name=qvendor1&rcopia_patient_external_id=test1234&service=rcopia&action=login&startup_screen=patient&close_window=n&allow_popup_screens=y&logout_url=https://cert.drfirst.com&time=080509000020&skip_auth=y";
            }
            catch (Exception exc)
            {

                //TODO: Log the exception
            }
            return webURL;
        }


        #endregion

        #region IEPrescriptionAdapterService Members


        public bool UploadPatient(Common.DTOs.PatientInfoDTO patientInfoDTO)
        {
            //TODO:
            // Add methode implementation 


           
            return false;
        }

        public bool UploadProblems(Common.DTOs.ProblemsInfoDTO ProblemInfoDTO)
        {
            //TODO:
            // Add methode implementation 
            return false;
        }

        public bool DownloadProblems(Common.DTOs.ProblemsInfoDTO ProblemInfoDTO)
        {
            //TODO:
            // Add methode implementation 
            return false;
        }

        public PatientAllergiesInfoDTO DownloadAllergies(string patientUserID, string rcopiaID, string clinicBranchID)
        {
            //TODO:
            // Add methode implementation 
            return null;
        }

        public PatientMedicationsInfoDTO DownloadMedications(string patientUserID, string rcopiaID, string clinicBranchID)
        {
            //TODO:
            // Add methode implementation 
            return null;
        }

        public Common.DTOs.ProblemsInfoDTO DownloadProblems(string patientUserID, string rcopiaID, string ClinicBranchID)
        {
            //TODO:
            // Add methode implementation 
            return null;
        }

        public Common.DTOs.PrescriptionInfoDTO DownloadPrescription(string patientUserID, string rcopiaID, string ClinicBranchID)
        {
            //TODO:
            // Add methode implementation 
            return null;
        }

        #endregion
    }
}
