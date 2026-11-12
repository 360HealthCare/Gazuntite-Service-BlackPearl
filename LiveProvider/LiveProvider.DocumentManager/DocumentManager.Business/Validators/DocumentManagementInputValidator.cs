using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using LiveProvider.Utility.Constant.Errors;
using System.IO;
using System.Text.RegularExpressions;

namespace DocumentManager.Business.Validators
{
    public class DocumentManagementInputValidator : DocumentManagementBaseValidator
    {
        internal static bool ValidatePatientDocumentDTO(PatientDocumentDTO PatientDocumentObject, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;
            if (PatientDocumentObject != null)
            {
                if (!PatientDocumentObject.IsDeleted)
                {
                    flag = ValidateKeyId(PatientDocumentObject.PatientID, sb, flag, ErrorMessages.INVALID_PATIENTID_ERROR_MSG);
                    flag = ValidateStringInfo(PatientDocumentObject.DocumentName, sb, flag, ErrorMessages.INVALID_DOCUMENTNAME_ERROR_MSG);
                    flag = ValidateKeyId(PatientDocumentObject.SectionId, sb, flag, ErrorMessages.INVALID_SESSIONID_ERROR_MSG);
                    flag = ValidateKeyId(PatientDocumentObject.UploadedByUserId, sb, flag, ErrorMessages.INVALID_UPLOADEDBYUSERID_ERROR_MSG);
                    flag = ValidateKeyId(PatientDocumentObject.UploadedByUserTypeId, sb, flag, ErrorMessages.INVALID_UPLOADEDBYUSERTYPEID_ERROR_MSG);
                    flag = ValidateStringInfo(PatientDocumentObject.PatientFirstName, sb, flag, ErrorMessages.INVALID_PATIENT_FIRST_NAME_MSG);
                    flag = ValidateStringInfo(PatientDocumentObject.UploadedByUserFirstName, sb, flag, ErrorMessages.INVALID_PATIENT_UPLOADED_BY_USER_FIRST_NAME_MSG);
                }
                if (IsUpdate || PatientDocumentObject.IsDeleted)
                {
                    flag = ValidateKeyId(PatientDocumentObject.DocumentId, sb, flag, ErrorMessages.INVALID_DOCUMENTID_ERROR_MSG);
                }
                else
                {
                    flag = ValidateKeyId(PatientDocumentObject.CreatedBy, sb, flag, ErrorMessages.INVALID_CREATEDBY_ERROR_MSG);
                }
            }
            else
            {
                flag = false;
                sb.Append("\n" + ErrorMessages.PATIENTDOCUMENT_OBJECT_NULL_MSG);

            }
            return flag;
        }

        internal static bool ValidatePatientDocumentDTO(int clinicBranchID, int pageNo, int pageSize, ref StringBuilder sb)
        {
            bool flag = true;
            flag = ValidateKeyId(clinicBranchID, sb, flag, ErrorMessages.INVALID_CLIENTFILEPATH_ERROR_MSG);
            flag = ValidateKeyId(pageNo, sb, flag, ErrorMessages.INVALID_PAGENO_ERROR_MSG);
            flag = ValidateKeyId(pageSize, sb, flag, ErrorMessages.INVALID_PAGESIZE_ERROR_MSG);
            return flag;
        }

        internal static bool ValidateSearchingPara(string searchText, int clinicBranchID, int pageNo, int pageSize, ref StringBuilder sb)
        {
            bool flag = true;
            flag = ValidateKeyId(clinicBranchID, sb, flag, ErrorMessages.INVALID_CLIENTFILEPATH_ERROR_MSG);
            flag = ValidateKeyId(pageNo, sb, flag, ErrorMessages.INVALID_PAGENO_ERROR_MSG);
            flag = ValidateKeyId(pageSize, sb, flag, ErrorMessages.INVALID_PAGESIZE_ERROR_MSG);
            flag = ValidateStringInfo(searchText, sb, flag, ErrorMessages.INVALID_SEARCHTEXT_ERROR_MSG);

            return flag;
        }

        internal static bool ValidateUploadDocumentPara(Stream fileContents, string patientId, string sectionId, string documentVirtualName, ref StringBuilder sb)
        {
            bool flag = true;
            flag = ValidateStringInfo(patientId, sb, flag, ErrorMessages.INVALID_PATIENTID_ERROR_MSG);
            flag = ValidateStringInfo(sectionId, sb, flag, ErrorMessages.INVALID_SESSIONID_ERROR_MSG);
            flag = ValidateStringInfo(documentVirtualName, sb, flag, ErrorMessages.INVALID_DOCUMENT_VIRTUAL_PATH_ERROR_MSG);
            if (fileContents == null)
            {
                flag = false;
                sb.Append(ErrorMessages.INVALID_FILECONTENT_ERROR_MSG);
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        internal static bool ValidateUploadDocumentPara(string patientId, string sectionId, string documentVirtualName, ref StringBuilder sb)
        {
            bool flag = true;
            flag = ValidateStringInfo(patientId, sb, flag, ErrorMessages.INVALID_PATIENTID_ERROR_MSG);
            flag = ValidateStringInfo(sectionId, sb, flag, ErrorMessages.INVALID_SESSIONID_ERROR_MSG);
            flag = ValidateStringInfo(documentVirtualName, sb, flag, ErrorMessages.INVALID_DOCUMENT_VIRTUAL_PATH_ERROR_MSG);
            return flag;
        }

        internal static bool ValidateServerFilePathPara(string serverFilePath, Guid DocumentVirtualName,ref StringBuilder sb)
        {
            bool flag = true;
            if(string.IsNullOrEmpty(DocumentVirtualName.ToString()))
            {
                sb.Append(ErrorMessages.INVALID_DOCUMENT_VIRTUAL_PATH_ERROR_MSG);
                flag=false;
            }
            flag = ValidateStringInfo(serverFilePath, sb, flag, ErrorMessages.INVALID_SERVERFILEPATH_ERROR_MSG);
           
            return flag;
        }

        public static bool IsValidSearchDate(string value, out DateTime date)
        {
            bool flag = false;
            int yearToAdd;
            int yearToSearch;

            date = new DateTime();

            try
            {
                if (Regex.IsMatch(value, @"^[0-9]{8}$")) // MMDDYYYY
                {
                    date = new DateTime(int.Parse(value.Substring(4, 4)), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)));
                    flag = true;
                }
                else if (Regex.IsMatch(value, @"^[0-9]{6}$")) // MMDDYY
                {
                    flag = true;

                    yearToSearch = int.Parse(value.Substring(4, 2));

                    if (DateTime.Now.Year >= (2000 + yearToSearch))
                        yearToAdd = 2000;
                    else
                        yearToAdd = 1900;

                    date = new DateTime(yearToAdd + yearToSearch, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)));

                }
                else if (Regex.IsMatch(value, @"^[0-9]{2}-[0-9]{2}-[0-9]{2}$") || Regex.IsMatch(value, @"^[0-9]{2}/[0-9]{2}/[0-9]{2}$")) // MM-DD-YY, MM/DD/YY
                {
                    flag = true;

                    yearToSearch = int.Parse(value.Substring(6, 2));

                    if (DateTime.Now.Year >= (2000 + yearToSearch))
                        yearToAdd = 2000;
                    else
                        yearToAdd = 1900;

                    date = new DateTime(yearToAdd + yearToSearch, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(3, 2)));
                }
                else if (Regex.IsMatch(value, @"^[0-9]{2}/[0-9]{2}/[0-9]{4}$") || Regex.IsMatch(value, @"^[0-9]{2}-[0-9]{2}-[0-9]{4}$")) // MM/DD/YYYY, MM-DD-YYYY   
                {
                    flag = true;
                    date = new DateTime(int.Parse(value.Substring(6, 4)), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(3, 2)));
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
    }
}
