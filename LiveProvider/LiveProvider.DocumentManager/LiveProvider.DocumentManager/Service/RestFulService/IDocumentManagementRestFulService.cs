using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using LiveProvider.Model.TypeDTOs;
using LiveProvider.Model.Response;
using LiveProvider.Model.TypeDTOs.DocumentManager;

namespace LiveProvider.DocumentManager.Service
{
    [ServiceContract]
    public interface IDocumentManagementRestFulService
    {
       
        [OperationContract]
        [WebInvoke(UriTemplate = "UploadDocument/{patientId}/{sectionId}/{documentVirtualName}/{fileName}/{practice}/{station}")]
        void UploadDocument(Stream FileContents, string patientId, string sectionId, string documentVirtualName, string fileName, string practice, string station);

        [OperationContract]
        [WebGet(UriTemplate = "LoadDocumentTypes")]
        List<DocumentTypeDTO> LoadDocumentTypes();

        [OperationContract]
        [WebGet(UriTemplate = "LoadDocumentSectionTypes")]
        List<DocumentSectionTypeDTO> LoadDocumentSectionTypes();

        [OperationContract]
        [WebInvoke(UriTemplate = "ManagePatientDocuments", RequestFormat = WebMessageFormat.Json, Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        int ManagePatientDocuments(PatientDocumentDTO PatientDocumentObject);

        [OperationContract]
        [WebGet(UriTemplate = "LoadPatientDocumentByClinicId/{clinicBranchID}/{pageNo}")]
        PaggingResponseDTO LoadPatientDocumentByClinicId(string clinicBranchID, string pageNo);

        [OperationContract]
        [WebGet(UriTemplate = "LoadAllProviderByClinicBranchId/{clinicBranchID}")]
        List<LProviderDTO> LoadAllProviderByClinicBranchId(string clinicBranchID);

        [OperationContract]
        [WebGet(UriTemplate = "SearchPatientbyClinicIdAndSearchString/{clinicBranchId}/{providerId}/{userTypeId}/{searchString}/{demandPageNo}")]
        LPatientSearchResultDTO SearchPatientbyClinicIdAndSearchString(string clinicBranchId, string providerId, string userTypeId, string searchString, string demandPageNo);

        [OperationContract]
        [WebGet(UriTemplate="LoadSearchField")]
        List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> LoadSearchField();

        [OperationContract]
        [WebGet(UriTemplate = "SearchPatientDocumentByClinicId/{clinicBranchID}/{searchText}/{fieldName}/{pageNo}")]
        PaggingResponseDTO SearchPatientDocumentByClinicId(string clinicBranchID, string searchText, string fieldName, string pageNo);

        [OperationContract]
        [WebGet(UriTemplate = "CreateServerPath/{path}")]
        bool CreateServerPath(string path);

        
    }
}
