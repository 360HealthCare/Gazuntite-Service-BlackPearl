using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EprescriptionService.Common.DTOs;

namespace EprescriptionService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEprescriptionAdapterService" in both code and config file together.
    [ServiceContract]
    public interface IEprescriptionAdapterService
    {
        [OperationContract]
        string GetVersion();

        [OperationContract]
        String GetANSWebURL(String PracticeUserName);


        [OperationContract]
        String SSOLoginToEprescription(String rcopia_user_external_id, String Action, String StartUpScreen,
                                       String skip_auth, String MAC);


        [OperationContract]
        Boolean UploadPatient(PatientInfoDTO patientInfoDTO);


        [OperationContract]
        Boolean UploadProblems(ProblemsInfoDTO ProblemInfoDTO);






        [OperationContract]
        PatientAllergiesInfoDTO DownloadAllergies(String patientUserID, String rcopiaID, String clinicBranchID);


        [OperationContract]
        PatientMedicationsInfoDTO DownloadMedications(String patientUserID, String rcopiaID, String clinicBranchID);


        [OperationContract]
        ProblemsInfoDTO DownloadProblems(String patientUserID, String rcopiaID, String ClinicBranchID);


        [OperationContract]
        PrescriptionInfoDTO DownloadPrescription(String patientUserID, String rcopiaID, String ClinicBranchID);


        [OperationContract]
        string prepareLoginCallForEprescription(string rcopia_user_external_id);
    }
}
