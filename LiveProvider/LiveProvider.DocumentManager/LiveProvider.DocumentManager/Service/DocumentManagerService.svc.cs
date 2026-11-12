using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using LiveProvider.Model;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using LiveProvider.Utility.Logger;
using LiveProvider.Utility.Utilities;
using DocumentManager.Business.BL.DocumentManager;
using DocumentManager.Business.BL.DocumentManager.Impl;
using LiveProvider.Utility.Constant.Errors;
using LiveProvider.Utility.CustomExceptions;
using System.IO;
using LiveProvider.Model.Response;
using LiveProvider.Utility.Constant.ProjectConstant;
using System.Configuration;
using LiveProvider.Model.TypeDTOs;



namespace LiveProvider.DocumentManager.Service
{
   
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DocumentManagerService" in code, svc and config file together.
   
    /// <summary>
   /// Author Muhammad Ismail
   /// Date 04 July 2011
   /// This is the maic class where we will provide the implementation of the methods expose through Our Service (RestFull)
   /// In this class we will dispatch request to buisness managers & on recieving reposne we will dispatch it to main handler
   /// In Case of exception or error we will handle the exception & generate the fault & send it to main handler :)
   /// </summary>
    public class DocumentManagerService : LiveProviderService,IDocumentManagerService
    {


        IDocumentManager documentManagerImpl = null;
        # region Test Methods 

        public String Ping()
        {
            LiveProviderLogger.Info("Ping call received");
            return "Document Manager Working";
        }

        public LiveProviderBaseDTO PingCustomObject()
        {
            return new LiveProviderBaseDTO { IsDeleted=true,IsDirty=false};

        }

        public LiveProviderBaseDTO UpdateCustomObject(LiveProviderBaseDTO obj)
        {
            obj.IsDirty = true;
            obj.IsDeleted = true;
            return obj;
        }

        #endregion Test Method



        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// THis Class will be serverd as main handler for document related operation :)
        /// </summary>
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
        /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// Method Will be used to load document Section Types
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
            string documentHostName = ConfigurationManager.AppSettings["DOCUMENT_HOST_NAME"].ToString();
            LiveProviderLogger.Info("ManagePatientDocuments call received");
            int Response = 0;
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

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 7/22/2011
        /// This method will save utility info and error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isError"></param>
        
        public void ManageLog(string message,string stackTrace, bool isError)
        {
            try
            {
                if (isError)
                {
                    LiveProviderLogger.Error(message);
                    LiveProviderLogger.Error(stackTrace);
                }
                else
                {
                    LiveProviderLogger.Info(message);
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp.Message);
                LiveProviderLogger.Error(exp.StackTrace);
                // Compose Genral Error Code For Exception
                String GeneralCode = ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX,
                                      ErrorCodes.SUB_SECTION_UTILITY_LOG, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                      ErrorCodes.GENERAL_ERROR_CODE_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX);

                HandleFault.HandleFautException(exp, exp.Message, GeneralCode);
            }
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 7/22/2011
        /// This method will update serverFilePath
        /// </summary>
        /// <param name="serverFilePath"></param>
        /// <param name="DocumentVirtualName"></param>
        /// <returns></returns>

        public bool UpdateServerFilePath(string serverFilePath, Guid DocumentVirtualName)
        {
            LiveProviderLogger.Info("UpdateServiceFilePath call received");
            bool Response = false;
            try
            {
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                Response = documentManagerImpl.UpdateServerFilePath(serverFilePath, DocumentVirtualName);
                LiveProviderLogger.Info("UpdateServiceFilePath Return Result ::" + Response);
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

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 22/8/2011
        /// This method will load patient document with respect to patientid and clinic branch id
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="patientID"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>

        public PaggingResponseDTO LoadPatientDocumentByClinicIdAndPatientID(int clinicBranchID, int patientID, int pageNo)
        {
            LiveProviderLogger.Info("LoadPatientDocumentByClinicId call received");
            PaggingResponseDTO response = null;
            try
            {
                int pageSze = DocumentManagerContants.DOCUMENT_PAGE_SIZE;
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                response = documentManagerImpl.LoadPatientDocumentByClinicIdAndPatientID(clinicBranchID, patientID, pageNo, pageSze);
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

        public PaggingResponseDTO SearchPatientDocumentByClinicId(int clinicBranchID, string searchText, int fieldID, int pageNo, int patientID)
        {
            LiveProviderLogger.Info("SearchPatientDocumentByClinicId call received");
            PaggingResponseDTO response = null;
            try
            {
                int pageSze = DocumentManagerContants.DOCUMENT_PAGE_SIZE;
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                response = documentManagerImpl.SearchPatientDocumentByClinicId(clinicBranchID, searchText, fieldID, pageNo, pageSze, patientID);
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
        /// Author Sahibzada Sami ud din
        /// Date 16 jan 2012
        /// This method will send email to user in case of total document uplaoded 
        /// sucessfully or not by the bulkupload.
        /// Note:: This method is temparay later on it will move to communication service.
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="feilds"></param>
        /// <returns></returns>
        public bool SendEmails(string[] recipient,string[] feilds, bool isSucess)
        { 
            bool response = false;
            try
            {
                if (documentManagerImpl == null)
                {
                    documentManagerImpl = new DocumentManagerImpl();
                }
                response = documentManagerImpl.SendEmails(recipient, feilds,isSucess);
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Info("Error Sending Email To User exp::" + exp.ToString());
            }
            return response;
        }
    }
}
