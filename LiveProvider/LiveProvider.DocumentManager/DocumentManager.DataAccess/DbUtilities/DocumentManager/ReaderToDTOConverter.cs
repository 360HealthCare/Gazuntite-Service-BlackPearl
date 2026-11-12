using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using LiveProvider.Utility.Logger;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.Errors;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Model.Response;
using LiveProvider.Model.TypeDTOs;

namespace DocumentManager.DataAccess.DbUtilities.DocumentManager
{
    /// <summary>
    /// Author Muhammad Ismail,Sahibzada Sami uddin
    /// Date 04 july 2011
    /// This class will be served as DB utlity class responsible for extracting the informtion from data reder & populate the coreesponding DTO 
    /// </summary>
    public class ReaderToDTOConverter
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 July 2011
        /// This Method will be used to extract information from data reader & Populate in document type DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<DocumentTypeDTO> DocumentTypeConverter(IDataReader objIReader)
        {
            List<DocumentTypeDTO> documentList = null;
            try
            {
                if (objIReader != null)
                {
                    documentList = new List<DocumentTypeDTO>();
                    {
                        while (objIReader.Read())
                        {
                            DocumentTypeDTO documentObj = new DocumentTypeDTO();

                            if (objIReader["DocumentTypeID"] != DBNull.Value)
                            {
                                documentObj.DocumentTypeId = Int32.Parse(objIReader["DocumentTypeID"].ToString());
                            }
                            if (objIReader["DocumentTypeName"] != DBNull.Value)
                            {
                                documentObj.DocumentTypeName = objIReader["DocumentTypeName"].ToString();
                            }

                            documentList.Add(documentObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LiveProviderLogger.Error(ex);
                throw new DataReaderToDTOConversionException(DocumentManagerErrorConstant.DOCUMENT_TYPE_DTO_CONVERSION_ERROR_MSG ,ex.Message,
                     ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                   ErrorCodes.SECTION_DOCUMENT_MANAGMENT_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                   ErrorCodes.DTO_CONVERSION_ERROR_PREFIX));
            }
            return documentList;
        }

        internal static List<DocumentSectionTypeDTO> DocumentSectionTypeConverter(IDataReader objIReader)
        {
            List<DocumentSectionTypeDTO> documentList = null;
            List<DocumentSectionTypeDTO> tempDocumentList = null;
            try
            {
                if (objIReader != null)
                {
                    documentList = new List<DocumentSectionTypeDTO>();
                    tempDocumentList = new List<DocumentSectionTypeDTO>();
                    while (objIReader.Read())
                    {

                        DocumentSectionTypeDTO documentObj = new DocumentSectionTypeDTO();

                        if (objIReader["SectionId"] != DBNull.Value)
                        {
                            documentObj.SectionId = Int32.Parse(objIReader["SectionId"].ToString());
                        }
                        if (objIReader["SectionName"] != DBNull.Value)
                        {
                            documentObj.SectionName = objIReader["SectionName"].ToString();
                        }
                        if (objIReader["ParentId"] != DBNull.Value)
                        {
                            documentObj.ParentId = Int32.Parse(objIReader["ParentId"].ToString());
                        }
                        if (objIReader["OrderNo"] != DBNull.Value)
                        {
                            documentObj.OrderNo = Int32.Parse(objIReader["OrderNo"].ToString());
                        }
                        if (objIReader["ParentId"] == DBNull.Value)
                        {
                            documentList.Add(documentObj);
                        }
                        else
                        {
                            tempDocumentList.Add(documentObj);
                        }
                    }
                }
                if (documentList != null && documentList.Count > 0)
                {
                    foreach (DocumentSectionTypeDTO subSecObj in tempDocumentList)
                    {
                        foreach (DocumentSectionTypeDTO secObj in documentList)
                        {
                            if (subSecObj.ParentId == secObj.SectionId)
                            {
                                if (secObj.ChildSectionType == null)
                                {
                                    secObj.ChildSectionType = new List<DocumentSectionTypeDTO>();
                                }
                                secObj.ChildSectionType.Add(subSecObj);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LiveProviderLogger.Error(ex);
                throw new DataReaderToDTOConversionException(DocumentManagerErrorConstant.DOCUMENT_TYPE_DTO_CONVERSION_ERROR_MSG, ex.Message,
                     ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                   ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                   ErrorCodes.DTO_CONVERSION_ERROR_PREFIX));
            }
            return documentList;
        }
        internal static PaggingResponseDTO PatientDocumentConverter(IDataReader objIDataReader, List<DocumentSectionTypeDTO> sectionList)
        {
            PaggingResponseDTO response = null; 
            List<PatientDocumentDTO> patientDocumentsList = null;
            List<PatientDocumentDTO> tempList=null;
            try
            {
                if (objIDataReader != null)
                {
                    response = new PaggingResponseDTO();
                    patientDocumentsList = new List<PatientDocumentDTO>();
                    tempList = new List<PatientDocumentDTO>();
                    if (objIDataReader.Read())
                    {
                        response.TotalRecord = int.Parse(objIDataReader["TotalRecords"].ToString());
                        response.FirstRecord = int.Parse(objIDataReader["FirstRecord"].ToString());
                        response.LastRecord = int.Parse(objIDataReader["LastRecord"].ToString());
                    }
                    foreach (DocumentSectionTypeDTO objSec in sectionList)
                    {
                        PatientDocumentDTO objPatientDoc = new PatientDocumentDTO();
                        objPatientDoc.SectionId = objSec.SectionId;
                        objPatientDoc.SectionName = objSec.SectionName;
                        objPatientDoc.DocumentName = "";
                        objPatientDoc.StrUploadedDate = "";
                        objPatientDoc.PatientFullName = "";
                        objPatientDoc.UploadedByUserFullName = "";
                        objPatientDoc.CommentNote = "";

                        patientDocumentsList.Add(objPatientDoc);
                    }
                    objIDataReader.NextResult();
                    while (objIDataReader.Read())
                    {
                        PatientDocumentDTO document = new PatientDocumentDTO();
                        if (objIDataReader["DocumentID"] != DBNull.Value)
                        {
                            document.DocumentId = int.Parse(objIDataReader["DocumentID"].ToString());
                        }

                        if (objIDataReader["PatientID"] != DBNull.Value)
                        {
                            document.PatientID = int.Parse(objIDataReader["PatientID"].ToString());
                        }
                        if (objIDataReader["DocumentTypeID"] != DBNull.Value)
                        {
                            document.DocumentTypeId = int.Parse(objIDataReader["DocumentTypeID"].ToString());
                        }
                        if (objIDataReader["DocumentTypeName"] != DBNull.Value)
                        {
                            document.DocumentTypeName = objIDataReader["DocumentTypeName"].ToString();
                        }
                        else
                        {
                            document.DocumentTypeName = "";
                        }
                        if (objIDataReader["SectionId"] != DBNull.Value)
                        {
                            document.SectionId = int.Parse(objIDataReader["SectionId"].ToString());
                        }
                        if (objIDataReader["SectionName"] != DBNull.Value)
                        {
                            document.SectionName = objIDataReader["SectionName"].ToString();
                        }
                        else
                        {
                            document.SectionName = "";
                        }
                        if (objIDataReader["ParentId"] != DBNull.Value)
                        {
                            document.SectionParentId = int.Parse(objIDataReader["ParentId"].ToString());
                        }
                        if (objIDataReader["DocumentVirtualName"] != DBNull.Value)
                        {
                            document.DocumentVirtualName = Guid.Parse(objIDataReader["DocumentVirtualName"].ToString());
                        }
                        if (objIDataReader["DocumentName"] != DBNull.Value)
                        {
                            document.DocumentName = objIDataReader["DocumentName"].ToString();
                        }
                        else
                        {
                            document.DocumentName = "";
                        }
                        if (objIDataReader["ServerFilePath"] != DBNull.Value)
                        {
                            document.ServerFilePath = objIDataReader["ServerFilePath"].ToString();
                        }
                        if (objIDataReader["ClientFilePath"] != DBNull.Value)
                        {
                            document.ClientFilePath = objIDataReader["ClientFilePath"].ToString();
                        }
                        if (objIDataReader["UploadedByUserId"] != DBNull.Value)
                        {
                            document.UploadedByUserId = int.Parse(objIDataReader["UploadedByUserId"].ToString());
                        }
                        if (objIDataReader["UploadedDate"] != DBNull.Value)
                        {
                            document.UploadedDate = objIDataReader["UploadedDate"].ToString();
                            DateTime date = DateTime.Parse(document.UploadedDate);
                            document.StrUploadedDate = date.ToShortDateString();
                        }
                        else
                        {
                            document.StrUploadedDate = "";
                        }
                        if (objIDataReader["Note"] != DBNull.Value)
                        {
                            document.CommentNote = objIDataReader["Note"].ToString();
                        }
                        else
                        {
                            document.CommentNote = "";
                        }
                        if (objIDataReader["PatientFirstName"] != DBNull.Value)
                        {
                            document.PatientFirstName = objIDataReader["PatientFirstName"].ToString();
                        }
                        else
                        {
                            document.PatientFirstName = "";
                        }
                        if (objIDataReader["PatientLastName"] != DBNull.Value)
                        {
                            document.PatientLastName = objIDataReader["PatientLastName"].ToString();
                        }
                        else
                        {
                            document.PatientLastName = "";
                        }
                        if (objIDataReader["PatientMiddleName"] != DBNull.Value)
                        {
                            document.PatientMiddleName = objIDataReader["PatientMiddleName"].ToString();
                        }
                        else
                        {
                            document.PatientMiddleName = "";
                        }
                        if (objIDataReader["UploadedByUserFirstName"] != DBNull.Value)
                        {
                            document.UploadedByUserFirstName = objIDataReader["UploadedByUserFirstName"].ToString();
                        }
                        else
                        {
                            document.UploadedByUserFirstName = "";
                        }
                        if (objIDataReader["UploadedByUserLastName"] != DBNull.Value)
                        {
                            document.UploadedByUserLastName = objIDataReader["UploadedByUserLastName"].ToString();
                        }
                        else
                        {
                            document.UploadedByUserLastName = "";
                        }
                        if (objIDataReader["UploadedByUserMiddleName"] != DBNull.Value)
                        {
                            document.UploadedByUserMiddleName = objIDataReader["UploadedByUserMiddleName"].ToString();
                        }
                        else
                        {
                            document.UploadedByUserMiddleName = "";
                        }
                        if (!string.IsNullOrEmpty(document.UploadedByUserFirstName))
                        {
                            document.UploadedByUserFullName = document.UploadedByUserFirstName;
                        }
                        if (!string.IsNullOrEmpty(document.UploadedByUserLastName))
                        {
                            document.UploadedByUserFullName = document.UploadedByUserFullName + DocumentManagerContants.NAME_SPACE + document.UploadedByUserLastName;
                        }
                        else
                            document.UploadedByUserFullName = "";
                        if(!string.IsNullOrEmpty(document.UploadedByUserMiddleName))
                        {
                            document.UploadedByUserFullName = document.UploadedByUserFullName + DocumentManagerContants.NAME_SPACE + document.UploadedByUserMiddleName;
                        }
                        if (!string.IsNullOrEmpty(document.PatientFirstName))
                        {
                            document.PatientFullName = document.PatientFirstName;
                        }
                        if (!string.IsNullOrEmpty(document.PatientMiddleName))
                        {
                            document.PatientFullName = document.PatientFullName + DocumentManagerContants.NAME_SPACE + document.PatientMiddleName;
                        }
                        if (!string.IsNullOrEmpty(document.PatientLastName))
                        {
                            document.PatientFullName = document.PatientFullName + DocumentManagerContants.NAME_SPACE + document.PatientLastName;
                        }
                        

                        // Added By Ismail August 29 2011 this check is provided to skip main category uploader name ....
                        if (string.IsNullOrEmpty(document.UploadedByUserFullName))
                        {
                            document.UploadedByUserFullName = string.Empty;
                        }

                        if (string.IsNullOrEmpty(document.CommentNote))
                        {
                            document.CommentNote = string.Empty;
                        }
                        // End Added By Ismail :)

                        foreach (PatientDocumentDTO objPatient in patientDocumentsList)
                        {
                            if (document.SectionParentId == objPatient.SectionId || document.SectionId == objPatient.SectionId)
                            {
                                if (objPatient.ChildDocument == null)
                                {
                                    objPatient.ChildDocument = new List<PatientDocumentDTO>();
                                }
                                objPatient.ChildDocument.Add(document);
                                break;
                            }
                        }
                        
                    }
                    patientDocumentsList.RemoveAll(x => x.ChildDocument == null);
                    response.PatientDocumentsList = patientDocumentsList;
                }
            }
            catch (Exception ex)
            {
                throw new DataReaderToDTOConversionException(DocumentManagerErrorConstant.DOCUMENT_TYPE_DTO_CONVERSION_ERROR_MSG, ex.Message,
                    ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                    ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX, ErrorCodes.DTO_CONVERSION_ERROR_PREFIX));
            }
            return response;
        }

        /// <summary>
        /// Author: Sahibzada Sami uddin
        /// Date:19/08/2011
        /// </summary>
        /// <param name="objIReader"></param>
        /// <returns></returns>

        internal static List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> SearchFieldConverter(IDataReader objIReader)
        {
            List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO> searchFieldList = null;
            try
            {
                if (objIReader != null)
                {
                    searchFieldList = new List<LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO>();
                    while (objIReader.Read())
                    {
                        LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO searchFeildObj = new LiveProvider.Model.TypeDTOs.DocumentManager.SearchFieldTypeDTO();
                        searchFeildObj.FieldID = int.Parse(objIReader["FieldID"].ToString());
                        searchFeildObj.FieldName = objIReader["FieldName"].ToString();
                        searchFieldList.Add(searchFeildObj);
                    }
                }
            }
             catch (Exception ex)
            {
                throw new DataReaderToDTOConversionException(DocumentManagerErrorConstant.DOCUMENT_TYPE_DTO_CONVERSION_ERROR_MSG, ex.Message,
                    ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                    ErrorCodes.SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX, ErrorCodes.DTO_CONVERSION_ERROR_PREFIX));
            }
            return searchFieldList;
        }
    }
}
