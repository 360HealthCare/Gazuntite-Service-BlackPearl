using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.EprescriptionService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.E_PRescription;
using QuivusManager.IntegratedServies.UserManagerService;

namespace QuivusManager.Buisness.E_PrescriptionBL
{
    public interface IEPrescriptionBL
    {

        ANSWebURLResponse GetANSUrl(String practiceName);

        String GetANSWebURL(String PracticeUserName);

        String SSOLoginToEprescription(String rcopia_user_external_id, String Action, String StartUpScreen,
        String skip_auth, String MAC);

        String UploadPatient(PatientInfoDTO patientInfoDTO);

        //Boolean UploadProblems(ProblemsInfoDTO ProblemInfoDTO);

     //   PatientAllergiesInfoDTO DownloadAllergies(String patientUserID, String rcopiaID, String clinicBranchID);

     //   PatientMedicationsInfoDTO DownloadMedications(String patientUserID, String rcopiaID, String clinicBranchID);

        //ProblemsInfoDTO DownloadProblems(String patientUserID, String rcopiaID, String ClinicBranchID);

        //PrescriptionInfoDTO DownloadPrescription(String patientUserID, String rcopiaID, String ClinicBranchID);

        LoginCallResponse prepareLoginCallForEprescription(string rcopia_user_external_id,int PatientId);




        UrlResponse GetUrlForEditAllergies(string rcopiaIDForUser, string rcopiaIDForPatient, string practiceName, string practiceUserName);

        UrlResponse GetUrlForEditPrescription(string rcopiaIDForUser, string rcopiaIDForPatient, string practiceName, string practiceUserName);

        UrlResponse GetUrlForEditMedication(string rcopiaIDForUser, string rcopiaIDForPatient, string practiceName, string practiceUserName);

        ServiceResponse.ServiceCustomResponse.ServiceResponse SyncEPrescription(int patientUserID, string rcopiaID, int clinicBranchID,int VisitId);
        List<PatientInfoDTO> SynchronizePaitents(List<ExternalPatientInfoDTO> externalPatientList);

        NotificationResponse GetNotificationsForProvider(string ExternalId);
    }
}
