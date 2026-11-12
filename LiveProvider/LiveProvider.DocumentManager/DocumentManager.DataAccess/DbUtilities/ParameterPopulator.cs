using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentManager.DataAccess.DbUtilities;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Utility.Constant.Errors;

namespace DocumentManager.DataAccess.DbUtilities
{
    /// <summary>
    /// Author:Sahibzada Sami ud din
    /// Date:05/07/2011
    /// Description: This class will add database parameters
    /// </summary>
    public class ParameterPopulator:BasePopulator
    {
        internal static bool PopulatePatientDocumenParameters(PatientDocumentDTO PatDocObj, bool IsUpdate, Database db, DbCommand command)
        {
            bool Response = false;
            try
            {
                if (PatDocObj.PatientID > 0 && !PatDocObj.IsDeleted)
                    db.AddInParameter(command, "@PatientID", DbType.Int32, PatDocObj.PatientID);
                if(!PatDocObj.IsDeleted)
                    SetDictationIfNotNull("@DocumentVirtualName", PatDocObj.DocumentVirtualName, db, command);
                if (!PatDocObj.IsDeleted)
                {
                    if (!string.IsNullOrEmpty(PatDocObj.DocumentName))
                    {
                        db.AddInParameter(command, "@DocumentName", DbType.String, PatDocObj.DocumentName);
                    }
                    if (!string.IsNullOrEmpty(PatDocObj.ServerFilePath))
                    {
                        db.AddInParameter(command, "@ServerFilePath", DbType.String, PatDocObj.ServerFilePath);
                    }
                   
                    if (!string.IsNullOrEmpty(PatDocObj.ClientFilePath))
                    {
                        db.AddInParameter(command, "@ClientFilePath", DbType.String, PatDocObj.ClientFilePath);
                    }
                    else
                    {
                        db.AddInParameter(command, "@ClientFilePath", DbType.String, DocumentManagerContants.TEMP_VALUE);
                    }
                    if (PatDocObj.DocumentTypeId != 0)
                    {
                        db.AddInParameter(command, "@DocumentTypeId", DbType.Int32, PatDocObj.DocumentTypeId);
                    }
                    else
                    {
                        db.AddInParameter(command, "@DocumentTypeId", DbType.Int32, 1);
                    }
                    
                    if (PatDocObj.SectionId > 0)
                    {
                        db.AddInParameter(command, "@SectionId", DbType.Int32, PatDocObj.SectionId);
                    }
                    if (PatDocObj.UploadedByUserId > 0)
                    {
                        db.AddInParameter(command, "@UploadedByUserId", DbType.Int32, PatDocObj.UploadedByUserId);
                    }
                    if (PatDocObj.UploadedByUserTypeId > 0)
                    {
                        db.AddInParameter(command, "@UploadedByUserTypeId", DbType.Int32, PatDocObj.UploadedByUserTypeId);
                    }
                    if (PatDocObj.UploadedByClinicBranchId > 0)
                    {
                        db.AddInParameter(command, "@UploadedClinicBranchId", DbType.Int32, PatDocObj.UploadedByClinicBranchId);
                    }
                    else
                        SetNullValue("@UploadedClinicBranchId", db, command);
                    if (!string.IsNullOrEmpty(PatDocObj.UploadedDate))
                    {
                        SetDateIfNotNull("@UploadedDate", DateTime.Parse(PatDocObj.UploadedDate), db, command);
                    }
                    else
                        db.AddInParameter(command, "@UploadedDate", DbType.Int32, DBNull.Value);
                    SetValueIfNotNull("@LastSeenByUserId", PatDocObj.UploadedByUserTypeId, db, command);
                    if (!string.IsNullOrEmpty(PatDocObj.LastSeenDate))//chapi by sami
                    {
                        SetDateIfNotNull("@LastSeenDate", DateTime.Parse(DateTime.Now.ToShortDateString()), db, command);
                    }
                    else
                    {
                        db.AddInParameter(command, "@LastSeenDate", DbType.DateTime, DBNull.Value);
                    }

                    db.AddInParameter(command, "@IsDocumentFileUploaded", DbType.Boolean, true);
                    if (!string.IsNullOrEmpty(PatDocObj.PatientFirstName))
                    {
                        db.AddInParameter(command, "@PatientFirstName", DbType.String, PatDocObj.PatientFirstName);
                    }
                    if (!string.IsNullOrEmpty(PatDocObj.PatientLastName))
                    {
                        db.AddInParameter(command, "@PatientLastName", DbType.String, PatDocObj.PatientLastName);
                    }
                    if (!string.IsNullOrEmpty(PatDocObj.PatientMiddleName))
                    {
                        db.AddInParameter(command, "@PatientMiddleName", DbType.String, PatDocObj.PatientMiddleName);
                    }
                    if(!string.IsNullOrEmpty(PatDocObj.UploadedByUserFirstName))
                    {
                        db.AddInParameter(command,"@UploadedByUserFirstName",DbType.String,PatDocObj.UploadedByUserFirstName);
                    }
                    if (!string.IsNullOrEmpty(PatDocObj.UploadedByUserLastName))
                    {
                        db.AddInParameter(command, "@UploadedByUserLastName", DbType.String, PatDocObj.UploadedByUserLastName);
                    }
                    if (!string.IsNullOrEmpty(PatDocObj.UploadedByUserMiddleName))
                    {
                        db.AddInParameter(command, "@UploadedByUserMiddleName", DbType.String, PatDocObj.UploadedByUserMiddleName);
                    }
                    if (!string.IsNullOrEmpty(PatDocObj.CommentNote))
                    {
                        db.AddInParameter(command, "@Note", DbType.String, PatDocObj.CommentNote);
                    }
                }
                if (IsUpdate)
                {
                    SetValueIfNotNull("@DocumentID", PatDocObj.DocumentId, db, command);
                    SetValueIfNotNull("@ModifiedBy", PatDocObj.ModifiedBy, db, command);
                }
                else if(!PatDocObj.IsDeleted)
                {
                    db.AddOutParameter(command, "@DocumentIDOut", DbType.Int32, 1);
                    if (PatDocObj.CreatedBy > 0)
                    {
                        db.AddInParameter(command, "@CreatedBy", DbType.Int32, PatDocObj.CreatedBy);
                        
                    }
                }
                if (PatDocObj.IsDeleted)
                {
                    SetValueIfNotNull("@DocumentID", PatDocObj.DocumentId, db, command);
                }
            }
            catch(Exception exp)
            {
                throw new ParameterPopulatorException(exp.Message, DocumentManagerContants.RECORD_NOT_INSERTED_ERROR_MSG, ErrorCodes.ComposeErrorCode
                    (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                    ErrorCodes.OPERATION_PARAMETER_POPULATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
            }

            return Response;
        }
        internal static bool PopulatePatientDocumenParameters(int clinicBranchID,int pageNo, int pageSize,  Database db, DbCommand command)
        {
            bool Response = false;
            try
            {
                if (clinicBranchID > 0)
                    db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
                if (pageNo > 0)
                    db.AddInParameter(command, "@PageNo", DbType.Int32, pageNo);
                if (pageSize > 0)
                    db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
            }
            catch (Exception exp)
            {
                throw new ParameterPopulatorException(exp.Message, DocumentManagerContants.RECORD_NOT_INSERTED_ERROR_MSG, ErrorCodes.ComposeErrorCode
                    (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                    ErrorCodes.OPERATION_PARAMETER_POPULATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
            }
            return Response;
        }

        internal static bool PopulatePatientDocumenParameters(int clinicBranchID, string searchText, int feildID, int pageNo, int pageSize, Database db, DbCommand command, bool isFilter, DateTime fromDate, DateTime toDate, string sectionName, bool isDateSearch,bool isSectoinFilter,int patientID)
        {
            bool Response = false;
            //DateTime dateSearch;
            try
            {
                if (clinicBranchID > 0)
                    db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
                if (!string.IsNullOrEmpty(searchText) && feildID != DocumentManagerContants.DATE_SEARCH_VALUE && feildID != 0)
                    db.AddInParameter(command, "@SearchText", DbType.String, searchText);
                if (!string.IsNullOrEmpty(searchText) && feildID == DocumentManagerContants.DATE_SEARCH_VALUE)
                {
                    //dateSearch = DateTime.Parse(searchText);
                    db.AddInParameter(command, "@DateSearch", DbType.DateTime, toDate);
                }
                if (feildID > 0 && feildID != DocumentManagerContants.DATE_SEARCH_VALUE)
                {
                    db.AddInParameter(command, "@FeildID", DbType.Int32, feildID);
                }
               
                if (pageNo > 0)
                    db.AddInParameter(command, "@PageNo", DbType.Int32, pageNo);
                if (pageSize > 0)
                    db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
                if (isFilter)
                {
                    db.AddInParameter(command, "@DateFrom", DbType.DateTime, fromDate);
                    db.AddInParameter(command, "@DateTo", DbType.DateTime, toDate);
                    db.AddInParameter(command, "@SectionName", DbType.String, sectionName);
                }
                if (isDateSearch)
                {
                    db.AddInParameter(command, "@DateFrom", DbType.DateTime, fromDate);
                    db.AddInParameter(command, "@DateTo", DbType.DateTime, toDate);
                }
                if (isSectoinFilter)
                {
                    db.AddInParameter(command, "@SectionName", DbType.String, sectionName);
                }
                db.AddInParameter(command, "@PatientID", DbType.Int32, patientID);
            }
            catch (Exception exp)
            {
                throw new ParameterPopulatorException(exp.Message, DocumentManagerContants.RECORD_NOT_INSERTED_ERROR_MSG, ErrorCodes.ComposeErrorCode
                    (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                    ErrorCodes.OPERATION_PARAMETER_POPULATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
            }
            return Response;
        }


        internal static bool PopulateServerFileUpdateParameters(string serverFilePath, Guid DocumentVirtualName, Database db, DbCommand command)
        {
            bool Response = false;
            try
            {
                if (!string.IsNullOrEmpty(serverFilePath))
                    db.AddInParameter(command, "@ServerFilePath", DbType.String, serverFilePath);
                if (!string.IsNullOrEmpty(DocumentVirtualName.ToString()))
                    db.AddInParameter(command, "@DocumentVirtualName", DbType.Guid, DocumentVirtualName);
            }
            catch (Exception exp)
            {
                throw new ParameterPopulatorException(exp.Message, DocumentManagerContants.RECORD_NOT_INSERTED_ERROR_MSG, ErrorCodes.ComposeErrorCode
                    (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                    ErrorCodes.OPERATION_PARAMETER_POPULATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
            }
            return Response;
        }

        internal static bool PopulatePatientDocumenParameters(int clinicBranchID, int patientID, int pageNo, int pageSize, Database db, DbCommand command)
        {
            bool Response = false;
            try
            {
                if (clinicBranchID > 0)
                    db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
                if (pageNo > 0)
                    db.AddInParameter(command, "@PageNo", DbType.Int32, pageNo);
                if (pageSize > 0)
                    db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
                if (patientID > 0)
                    db.AddInParameter(command, "@PatientID", DbType.Int32, patientID);
            }
            catch (Exception exp)
            {
                throw new ParameterPopulatorException(exp.Message, DocumentManagerContants.RECORD_NOT_INSERTED_ERROR_MSG, ErrorCodes.ComposeErrorCode
                    (ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX,
                    ErrorCodes.OPERATION_PARAMETER_POPULATOR_PREFIX, ErrorCodes.PARAMETER_NOT_POPULATE_PREFIX));
            }
            return Response;
        }
    }
}
