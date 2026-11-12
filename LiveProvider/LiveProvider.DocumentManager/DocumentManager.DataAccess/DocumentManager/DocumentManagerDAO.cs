using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using System.Data;
using LiveProvider.Utility.Constant.StoreProcedureConstant;
using LiveProvider.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using DocumentManager.DataAccess.DbUtilities.DocumentManager;
using DocumentManager.DataAccess.DbUtilities;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Utility.Constant.Errors;
using LiveProvider.Model.Response;
using LiveProvider.Model.TypeDTOs;

namespace DocumentManager.DataAccess.DocumentManager
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 July 2011
    /// This class will be serverd as Database manager for document related operation :)
    /// </summary>
    public class DocumentManagerDAO : BaseDAO
    {

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// This Method Will be used to load the document types from DB 
        /// </summary>
        /// <returns></returns>
        public List<DocumentTypeDTO> LoadDocumentTypes()
        {
            List<DocumentTypeDTO> documentList = null;
            IDataReader objIReader = null;
            string sProc = DocumentManagerSPConstant.DOCUMENT_TYPE_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                documentList = new List<DocumentTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        documentList = ReaderToDTOConverter.DocumentTypeConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return documentList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// Method Will be used to load document Section Types
        /// </summary>
        /// <returns></returns>
        public List<DocumentSectionTypeDTO> LoadDocumentSectionTypes()
        {
            List<DocumentSectionTypeDTO> documentList = null;
            IDataReader objIReader = null;
            string sProc = DocumentManagerSPConstant.DOCUMENT_SECTION_TYPE_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                documentList = new List<DocumentSectionTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        documentList = ReaderToDTOConverter.DocumentSectionTypeConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return documentList;
        }
         /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 05/07/2011
        /// Method will be to update and insert document information.
        /// </summary>
        /// <param name="PatientDocumentObject"></param>
        /// <returns></returns>
        public int ManagePatientDocuments(PatientDocumentDTO PatientDocumentObject, bool IsUpdate)
        {
            int Response=0;
            string sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_UPDATE_SP;
                }
                if (PatientDocumentObject.IsDeleted)
                {
                    sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_DELETE_SP;
                }
                LiveProviderLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);
                ParameterPopulator.PopulatePatientDocumenParameters(PatientDocumentObject, IsUpdate, db, command);
                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(DocumentManagerContants.RECORD_NOT_INSERTED_ERROR_MSG, ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX,
                        ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_SAVE_PREFIX+
                        ErrorCodes.SEPRATOR+ErrorCodes.OPERATION_UPDATE_PREFIX,ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                {
                    if (!IsUpdate && !PatientDocumentObject.IsDeleted)
                        int.TryParse(db.GetParameterValue(command, "DocumentIDOut").ToString(), out Response);
                    else
                        Response = PatientDocumentObject.DocumentId;
                }
            }
            catch
            {
                throw;
            }
            return Response;
        }

        /// <summary>
        /// Author:Sahibzada Sami ud Din
        /// Date:07/13/2011
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public PaggingResponseDTO LoadPatientDocumentByClinicId(int clinicBranchID, int pageNo, int pageSize)
        {
            LiveProviderLogger.Info("LoadPatientDocumentByClinicId call recived");
            PaggingResponseDTO response = null;
            string sProc = string.Empty;
            IDataReader objIReader = null;
            try
            {
                sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_LOAD_BY_CLINIC_ID;
                LiveProviderLogger.Info("Calling SP:"+sProc);
                Database db = DatabaseFactory.CreateDatabase(); 
                DbCommand command = db.GetStoredProcCommand(sProc);
                ParameterPopulator.PopulatePatientDocumenParameters(clinicBranchID, pageNo, pageSize, db, command);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        response = ReaderToDTOConverter.PatientDocumentConverter(objIReader, this.LoadDocumentSectionTypes());
                    }
                }

            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 7/22/2011
        /// This method will update server file path
        /// </summary>
        /// <param name="serverFilePath"></param>
        /// <param name="DocumentVirtualName"></param>
        /// <returns></returns>
        public bool UpdateServerFilePath(string serverFilePath, Guid DocumentVirtualName)
        {
            string sProc = DocumentManagerSPConstant.DOCUMENT_SERVERFILE_UPDATE_SP;
            bool response = false;
            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);
                ParameterPopulator.PopulateServerFileUpdateParameters(serverFilePath,DocumentVirtualName, db, command);
                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(DocumentManagerContants.RECORD_NOT_INSERTED_ERROR_MSG, ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX,
                        ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_UPDATE_PREFIX, ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                    response = true;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author:Sahibzada Sami uddin
        /// Date:19/08/2011
        /// This Method will search patient documents by field name and search text
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public PaggingResponseDTO SearchPatientDocumentByClinicId(int clinicBranchID, string searchText, int feildID, int pageNo, int pageSize, bool isFilter, DateTime fromDate, DateTime toDate, string sectionName,bool isDateSearch,bool isSectoinFilter, int patientID)
        {
            List<DocumentSectionTypeDTO> documentList = this.LoadDocumentSectionTypes();
            LiveProviderLogger.Info("SearchPatientDocumentByClinicId call recived");
            PaggingResponseDTO response = null;
            string sProc = string.Empty;
            IDataReader objIReader = null;
            try
            {
                if (isDateSearch)
                {
                    sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_DATE_FILTER;
                }
                else if(isSectoinFilter)
                {
                    sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_SECTION_FILTER;
                }
                else if (isFilter)
                {
                    sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_FILTER;
                }
                else if (feildID == DocumentManagerContants.DATE_SEARCH_VALUE)
                {
                    sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_Date_SEARCH;
                }
                else
                {
                    sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_SEARCH;
                }
                LiveProviderLogger.Info("Calling SP:" + sProc);
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);
                ParameterPopulator.PopulatePatientDocumenParameters(clinicBranchID, searchText, feildID, pageNo, pageSize, db, command, isFilter, fromDate, toDate, sectionName, isDateSearch, isSectoinFilter,patientID);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        response = ReaderToDTOConverter.PatientDocumentConverter(objIReader, documentList);
                    }
                }

            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author:Sahibzada Sami uddin
        /// Date:20/8/2011
        /// This method will load searching fields
        /// </summary>
        /// <returns></returns>

        public List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> LoadSearchField()
        {
            LiveProviderLogger.Info("LoadSearchField");
            List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> searchList = null;
            string sProc = string.Empty;
            IDataReader objIReader = null;
            try
            {
                sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_LOAD_SEARCH_FIELD;
                LiveProviderLogger.Info("Calling SP:" + sProc);
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        searchList = ReaderToDTOConverter.SearchFieldConverter(objIReader);
                    }
                }

            }
            catch
            {
                throw;
            }
            return searchList;
        }

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 22/8/2011
        /// This method will load patients with respect to clinicid and patientid
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="patientID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public PaggingResponseDTO LoadPatientDocumentByClinicIdAndPatientID(int clinicBranchID, int patientID, int pageNo, int pageSize)
        {
            LiveProviderLogger.Info("LoadPatientDocumentByClinicIdAndPatientID call recived");
            PaggingResponseDTO response = null;
            string sProc = string.Empty;
            IDataReader objIReader = null;
            try
            {
                sProc = DocumentManagerSPConstant.DOCUMENT_PATIENT_LOAD_BY_CLINICID_AND_PATIENTID_SP;
                LiveProviderLogger.Info("Calling SP:"+sProc);
                Database db = DatabaseFactory.CreateDatabase(); 
                DbCommand command = db.GetStoredProcCommand(sProc);
                ParameterPopulator.PopulatePatientDocumenParameters(clinicBranchID, patientID, pageNo, pageSize, db, command);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                      //  response = ReaderToDTOConverter.PatientDocumentConverter(objIReader, this.LoadDocumentSectionTypes());
                  
                        response = ReaderToDTOConverter.PatientDocumentConverter(objIReader, this.LoadDocumentSectionTypes());
                    }
                }

            }
            catch
            {
                throw;
            }
            return response;
        }

    }
}
