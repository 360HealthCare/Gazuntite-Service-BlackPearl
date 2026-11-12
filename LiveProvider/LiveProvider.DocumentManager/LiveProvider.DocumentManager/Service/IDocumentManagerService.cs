using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using LiveProvider.Model;
using LiveProvider.Model.TypeDTOs;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using System.IO;
using LiveProvider.Utility.Fault;
using LiveProvider.Model.Response;


namespace LiveProvider.DocumentManager.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDocumentManagerService" in both code and config file together.
   /// <summary>
   /// Author Muhammad Ismail
   /// Date 04 july 2011
   /// This is our main interface where we define our document related methods & define thier access method 
   /// </summary>
    [ServiceContract]
    public interface IDocumentManagerService
    {
        # region Test Methods
        [OperationContract]
        [WebGet(UriTemplate = "/Ping")]
        String Ping();

        [OperationContract]
        [WebGet(UriTemplate = "/CustomCall")]
        LiveProviderBaseDTO PingCustomObject();

        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateCustomCall", Method = "POST")]
        LiveProviderBaseDTO UpdateCustomObject(LiveProviderBaseDTO obj);
        #endregion Test Method


        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<DocumentTypeDTO> LoadDocumentTypes();


        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<DocumentSectionTypeDTO> LoadDocumentSectionTypes();

        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        int ManagePatientDocuments(PatientDocumentDTO PatientDocumentObject);

        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        void ManageLog(string message, string stackTrace, bool isError);

        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        bool UpdateServerFilePath(string serverFilePath,Guid DocumentVirtualName);

        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        PaggingResponseDTO LoadPatientDocumentByClinicIdAndPatientID(int clinicBranchID, int patientID, int pageNo);

        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> LoadSearchField();

        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        PaggingResponseDTO SearchPatientDocumentByClinicId(int clinicBranchID, string searchText, int fieldID, int pageNo, int patientID);

        [OperationContract]
        [FaultContract(typeof(LiveProviderFault))]
        bool SendEmails(string[] recipients, string[] feilds, bool isSucess);
    }
}
