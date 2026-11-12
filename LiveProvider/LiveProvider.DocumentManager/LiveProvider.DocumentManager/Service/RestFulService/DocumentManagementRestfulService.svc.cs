using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.IO;
using LiveProvider.Utility.FileOperator;
using LiveProvider.Utility.Logger;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using DocumentManager.Business.BL.DocumentManager;
using DocumentManager.Business.BL.DocumentManager.Impl;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Utilities;
using LiveProvider.Utility.Constant.Errors;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Model.Response;
using System.Configuration;
using LiveProvider.Model.TypeDTOs;

namespace LiveProvider.DocumentManager.Service.RestFulService
{
    public class DocumentManagementRestfulService :  IDocumentManagementRestFulService
    {
        IDocumentManager documentManagerImpl = null;


        public void UploadDocument(Stream FileContents, string patientId, string sectionId, string documentVirtualName, string fileName, string practice, string station)
        {
            string documentRootDirectory = ConfigurationManager.AppSettings["DOCUMENT_MANAGEMENT_DIRECTORY"].ToString();
            LiveProviderLogger.Info("UploadDocument call received at service level");
            try
            {
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                documentManagerImpl.UploadDocument(FileContents, patientId, sectionId, documentVirtualName, fileName, practice, station, documentRootDirectory);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                       ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
        }

        public List<DocumentTypeDTO> LoadDocumentTypes()
        {
            LiveProviderLogger.Info("LoadDocumentTypes call received");

            List<DocumentTypeDTO> response = null;
            try
            {
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                response = documentManagerImpl.LoadDocumentTypes();
            }
            catch (DataReaderToDTOConversionException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, String.Empty, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                       ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return response;
        }

        /// <summary>
        /// Author:Sahibzada Sami ud din
        /// </summary>
        /// <returns></returns>

        public List<DocumentSectionTypeDTO> LoadDocumentSectionTypes()
        {
            LiveProviderLogger.Info("LoadDocumentSectionTypes call received");

            List<DocumentSectionTypeDTO> response = null;
            try
            {
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                response = documentManagerImpl.LoadDocumentSectionTypes();
            }
            catch (DataReaderToDTOConversionException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, String.Empty, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                       ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return response;
        }

        /// <summary>
        /// Author:Sahibzada Sami ud Din
        /// Date:06/07/2011
        /// Description:This method will manage patient documents.
        /// </summary>
        /// <param name="PatientDocumentObject"></param>
        /// <returns></returns>
        
        public int ManagePatientDocuments(PatientDocumentDTO PatientDocumentObject)
        {
            LiveProviderLogger.Info("ManagePatientDocuments call received");
            int Response = 0;
            string documentHostName = ConfigurationManager.AppSettings["DOCUMENT_HOST_NAME"].ToString();
            try
            {
  
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                LiveProviderLogger.Info("calling documentManagerImpl.ManagePatientDocuments(PatientDocumentObject");
                Response = documentManagerImpl.ManagePatientDocuments(PatientDocumentObject, documentHostName);
                LiveProviderLogger.Info("PatientDocumentObject Return Result ::" + Response);
            }
            catch (ParameterPopulatorException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.ErrorCode);
            }
            catch (RecordNotInsertException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.Code);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX,
                                      ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }

            return Response;
        }
        public PaggingResponseDTO LoadPatientDocumentByClinicId(string clinicBranchID, string pageNo)
        {
            LiveProviderLogger.Info("LoadPatientDocumentByClinicId call received");
            PaggingResponseDTO response = null;
            try
            {
                int clinicId = int.Parse(clinicBranchID);
                int pageNum = int.Parse(pageNo);
                int pageSze = DocumentManagerContants.DOCUMENT_PAGE_SIZE;
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                response = documentManagerImpl.LoadPatientDocumentByClinicId(clinicId, pageNum, pageSze);
            }
            catch (ParameterPopulatorException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.ErrorCode);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX,
                                      ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return response;
        }


        public List<LProviderDTO> LoadAllProviderByClinicBranchId(string clinicBranchID)
        {
            List<LProviderDTO> providerList = null;
            try
            {
               if (documentManagerImpl == null)
               {
                    documentManagerImpl = new DocumentManagerImpl();
               }
                    providerList = documentManagerImpl.LoadAllProviderByClinicBranchId(clinicBranchID);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX,
                                      ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_CALL_USER_MANAGER_SERVICE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return providerList;
        }


        public LPatientSearchResultDTO SearchPatientbyClinicIdAndSearchString(string clinicBranchId, string providerId, string userTypeId, string searchString, string demandPageNo)
        {
            LPatientSearchResultDTO searchResult = null;  
            try
            {
                LiveProviderLogger.Info("Calling LoadPatientbyClinicIdAndSearchString PARAM:: clinicId " + clinicBranchId + " providerId " + providerId + " userTypeId " + userTypeId + " searchString " + searchString + " demandPageNo " + demandPageNo);
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                searchResult = documentManagerImpl.SearchPatientbyClinicIdAndSearchString(clinicBranchId, providerId, userTypeId, searchString, demandPageNo);
            }
            catch
            {
                throw;
            }
            return searchResult;
        }


        /// <summary>
        /// Author Sahibzada Sami Uddin
        /// Date 20/8/2011
        /// This method will get all search fields
        /// </summary>
        /// <returns></returns>

        public List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> LoadSearchField()
        {
             LiveProviderLogger.Info("LoadDocumentTypes call received");

             List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> response = null;
            try
            {
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                response = documentManagerImpl.LoadSearchField();
            }
            catch (DataReaderToDTOConversionException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, String.Empty, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);

                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                       ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                       ErrorCodes.GENERAL_ERROR_CODE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 20/08/2011
        /// This method will search patient documents
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="searchText"></param>
        /// <param name="fieldName"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>

        public PaggingResponseDTO SearchPatientDocumentByClinicId(string clinicBranchID, string searchText, string fieldId, string pageNo)
        {
            LiveProviderLogger.Info("SearchPatientDocumentByClinicId call received");
            PaggingResponseDTO response = null;
            try
            {
                int clinicId = int.Parse(clinicBranchID);
                int pageNum = int.Parse(pageNo);
                int intFieldId = int.Parse(fieldId);
                int pageSze = DocumentManagerContants.DOCUMENT_PAGE_SIZE;
                int patientID = 0;
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                response = documentManagerImpl.SearchPatientDocumentByClinicId(clinicId, searchText, intFieldId, pageNum, pageSze, patientID);
            }
            catch (ParameterPopulatorException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.Message, exp.ErrorCode);
            }
            catch (InvalidArgumentException exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX,
                                      ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return response;
        }

        /// <summary>
        /// Author:Sahibzada Sami uddin
        /// Date:9/10/2011
        /// Description This method will create document file directory at server
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ftpAddress"></param>
        /// <param name="ftpUser"></param>
        /// <param name="ftpPassword"></param>
        /// <returns></returns>
        
        public bool CreateServerPath(string path)
        {
            LiveProviderLogger.Info("Calling Create Server File Path");
            bool response = false;
            string ftpAddress = string.Empty;
            string ftpUser = string.Empty;
            string ftpPassword = string.Empty;
            try
            {
                ftpAddress = ConfigurationManager.AppSettings["FtpHost"].ToString();
                ftpUser = ConfigurationManager.AppSettings["FtpUserName"].ToString();
                ftpPassword = ConfigurationManager.AppSettings["FtpPassword"].ToString();
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                response = documentManagerImpl.CreateServerPath(path, ftpAddress, ftpUser, ftpPassword);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX,
                                      ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
            return response;
        }
    }
}
