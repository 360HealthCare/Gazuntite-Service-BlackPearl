using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using System.IO;
using LiveProvider.Model.Response;
using LiveProvider.Model.TypeDTOs;

namespace DocumentManager.Business.BL.DocumentManager
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 july 2011
    /// Simple Interface conatins document related method definations 
    /// </summary>
    public interface IDocumentManager
    {
        List<DocumentTypeDTO> LoadDocumentTypes();

        List<DocumentSectionTypeDTO> LoadDocumentSectionTypes();

        int ManagePatientDocuments(PatientDocumentDTO PatientDocumentObject, string documentHostName);

        void UploadDocument(Stream FileContents, string patientId, string sectionId, string documentVirtualName,string fileName, string practice, string station, string documentRootDirectory);

        PaggingResponseDTO LoadPatientDocumentByClinicId(int clinicBranchID, int pageNo, int pageSize);

        bool UpdateServerFilePath(string serverFilePath, Guid DocumentVirtualName);

        List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> LoadSearchField();

        PaggingResponseDTO SearchPatientDocumentByClinicId(int clinicBranchID, string searchText, int fieldId, int pageNo, int pageSize, int patientID);

        PaggingResponseDTO LoadPatientDocumentByClinicIdAndPatientID(int clinicBranchID, int patientID, int pageNo, int pageSize);

        LPatientSearchResultDTO SearchPatientbyClinicIdAndSearchString(string clinicBranchId, string providerId, string userTypeId, string searchString, string demandPageNo);

        List<LProviderDTO> LoadAllProviderByClinicBranchId(string clinicBranchID);

        bool CreateServerPath(string path, string ftpAddress, string ftpUser, string ftpPassword);

        bool SendEmails(string[] recipients, string[] feilds, bool isSucess);
    }
}
