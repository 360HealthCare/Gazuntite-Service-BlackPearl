using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.ServiceResponse.ServiceCustomResponse.E_PRescription;
using QuivusManager.IntegratedServies.EprescriptionService;

namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    
    public partial interface IQuivusManagerService
    {
        [OperationContract]
        ANSWebURLResponse GetANSUrl(string PracticeName, UserSessionDTO userSession);
         [OperationContract]
        LoginCallResponse PrepareLoginCallForEprescription(string RCOPIAUserExternalID,int PatientId, UserSessionDTO userSession);

        [OperationContract]
         UrlResponse GetUrlForEditAllergies(String rcopiaIDForUser, String rcopiaIDForPatient, String practiceName,
 String practiceUserName, UserSessionDTO userSession);

         [OperationContract]
         UrlResponse GetUrlForEditPrescription(String rcopiaIDForUser, String rcopiaIDForPatient, String practiceName,
         String practiceUserName, UserSessionDTO userSession);

         [OperationContract]
         UrlResponse GetUrlForEditMedication(String rcopiaIDForUser, String rcopiaIDForPatient, String practiceName,
         String practiceUserName, UserSessionDTO userSession);

          [OperationContract]
         String UploadPatient(PatientInfoDTO patientInfoDTO);

          [OperationContract]
          ServiceResponse SyncEPrescription(int patientID, string rcopiaID, int clinicBranchID,int VisitId, UserSessionDTO userSession);

          [OperationContract]
          NotificationResponse GetNotificationsForProvider(string ExternalId, UserSessionDTO userSession);
    }
}
