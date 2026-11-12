using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.Procedure;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Assessment
{
    public class ProcedureValidator : QBaseValidator
    {
        internal static bool ValidateProcedure(List<ProcedureDTO> InputList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (InputList != null)
            {
                foreach (ProcedureDTO obj in InputList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientProcedureID > 0)
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

                        if (obj.CPTMedicalType != null)
                        {
                            flag = ValidateKeyId(obj.CPTMedicalType.CPTMedicalTypeID, sb, flag, ErrorMessages.CPT_MEDICAL_TYPE_ID_ERROR_MSG);
                        }
                        else
                        {
                            sb.Append("\n" + ErrorMessages.CPT_MEDICAL_TYPE_ERROR_MSG);
                            flag = false;
                        }
                        //    flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.DIANOSIS_INVALID_PATIENT_ID_ERROR_MSG);

                        //flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                        //flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.PROCEDURE_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateVBProcedure(List<ProcedureDTO> InputList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (InputList != null)
            {
                foreach (ProcedureDTO obj in InputList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientProcedureID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                                flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.PROCEDURE_INVALID_CREATEDBY_ERROR_MSG);
                                flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.PROCEDURE_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                            flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.PROCEDURE_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                        if (obj.CPTMedicalType != null)
                        {
                            flag = ValidateKeyId(obj.CPTMedicalType.CPTMedicalTypeID, sb, flag, ErrorMessages.CPT_MEDICAL_TYPE_ID_ERROR_MSG);
                        }
                        else
                        {
                            sb.Append("\n" + ErrorMessages.CPT_MEDICAL_TYPE_ERROR_MSG);
                            flag = false;
                        }
                        flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.PROCEDURE_INVALID_PATIENT_ID_ERROR_MSG);

                        //flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                        //flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.PROCEDURE_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
