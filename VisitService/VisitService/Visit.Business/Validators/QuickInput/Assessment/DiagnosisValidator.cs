using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.Diagnosis;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Assessment
{
    /// Author Mansoor 
    /// Date 21 Mar 2011 
    public class DiagnosisValidator : QBaseValidator
    {
        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used validate Patient HPI data
        /// </summary>
        /// <returns></returns>
        public static bool ValidateDiagnosis(List<PatientDiagnosisDTO> diagnosisList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (diagnosisList != null)
            {
                foreach (PatientDiagnosisDTO obj in diagnosisList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientDiagnosisID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                        //    flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.DIANOSIS_INVALID_CREATEDBY_ERROR_MSG);
                        //    flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.DIANOSIS_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                        //    flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.DIANOSIS_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                        if (obj.ICDCode == null)
                        {
                            sb.Append("\n" + ErrorMessages.INVALID_ICD_CODE_OBJECT_ERROR_MSG);
                            flag = false;
                        }
                        else
                        {

                            flag = ValidateKeyId(obj.ICDCode.IcdCodeIdSeq, sb, flag, ErrorMessages.INVALID_ICD_CODE_ID_ERROR_MSG);
                        }
                    //    flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.DIANOSIS_INVALID_PATIENT_ID_ERROR_MSG);

                        //flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                        //flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.DIAGNOSIS_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateVBDiagnosis(List<PatientDiagnosisDTO> diagnosisList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (diagnosisList != null)
            {
                foreach (PatientDiagnosisDTO obj in diagnosisList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientDiagnosisID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                                flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.DIANOSIS_INVALID_CREATEDBY_ERROR_MSG);
                                flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.DIANOSIS_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                               flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.DIANOSIS_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                        if (obj.ICDCode == null)
                        {
                            sb.Append("\n" + ErrorMessages.INVALID_ICD_CODE_OBJECT_ERROR_MSG);
                            flag = false;
                        }
                        else
                        {

                            flag = ValidateKeyId(obj.ICDCode.IcdCodeIdSeq, sb, flag, ErrorMessages.INVALID_ICD_CODE_ID_ERROR_MSG);
                        }
                            flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.DIANOSIS_INVALID_PATIENT_ID_ERROR_MSG);

                           // flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.DIANOSIS_INVALID_VISIT_ID_ERROR_MSG);
                        //flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.DIAGNOSIS_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateSyncDiagnosis(List<PatientDiagnosisDTO> diagnosisList, ref StringBuilder sb)
        {
            return true;
           // throw new NotImplementedException();
        }
    }
}
