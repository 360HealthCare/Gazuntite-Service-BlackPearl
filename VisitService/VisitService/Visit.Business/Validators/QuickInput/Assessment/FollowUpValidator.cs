using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.FollowUp;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Assessment
{
    public class FollowUpValidator : QBaseValidator
    {
        internal static bool ValidateFollowUp(PatientFollowUpDTO followUpDTO, ref StringBuilder sb)
        {
            bool flag = true;
            bool IsUpdate = false;

            if (followUpDTO != null)
            {
                if (followUpDTO.PatientFollowUpID > 0)
                    IsUpdate = true;

                if (!IsUpdate)
                {
                 //   flag = ValidateKeyId(followUpDTO.CreatedBy, sb, flag, ErrorMessages.FOLLOWUP_INVALID_CREATEDBY_ERROR_MSG);
                //    flag = ValidateDate(followUpDTO.CreatedAt, sb, flag, ErrorMessages.FOLLOWUP_INVALID_CREATEDAT_ERROR_MSG);
                }

                if (IsUpdate)
                {
                //    flag = ValidateKeyId(followUpDTO.ModifiedBy, sb, flag, ErrorMessages.FOLLOWUP_INVALID_MODIFIEDBY_ERROR_MSG);
                }

                //flag = ValidateDate(followUpDTO.FollowUpDate, sb, flag, ErrorMessages.FOLLOWUP_DATE_ERROR_MSG);
           
                //flag = ValidateKeyId(followUpDTO.LabTestID, sb, flag, ErrorMessages.FOLLOWUP_INVALID_LAB_TEST_ID_ERROR_MSG);
                //flag = ValidateKeyId(followUpDTO.DrugID, sb, flag, ErrorMessages.FOLLOWUP_INVALID_DRUG_ID_ERROR_MSG);

                if (followUpDTO.FollowUpMethodType == null )
                {
                    sb.Append("\n" + ErrorMessages.FOLLOWUP_METHOD_TYPE_ERROR_MSG);
                    flag = false;
                }
                else  //else is added by sami for bug fixing. Error Msg : "Object reference not set to an instance of an object.,"
                    flag = ValidateKeyId(followUpDTO.FollowUpMethodType.FollowUpMethodTypeID, sb, flag, ErrorMessages.FOLLOWUP_METHOD_TYPE_ID_ERROR_MSG);

           //     flag = ValidateKeyId(followUpDTO.PatientID, sb, flag, ErrorMessages.FOLLOWUP_INVALID_PATIENT_ID_ERROR_MSG);

                

            }
            else
            {
                sb.Append("\n" + ErrorMessages.ROS_LIST_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }


        internal static bool ValidateVBFollowUp(PatientFollowUpDTO followUpDTO, ref StringBuilder sb)
        {
            bool flag = true;
            bool IsUpdate = false;

            if (followUpDTO != null)
            {
                if (followUpDTO.PatientFollowUpID > 0)
                    IsUpdate = true;

                if (!IsUpdate)
                {
                       flag = ValidateKeyId(followUpDTO.CreatedBy, sb, flag, ErrorMessages.FOLLOWUP_INVALID_CREATEDBY_ERROR_MSG);
                        flag = ValidateDate(followUpDTO.CreatedAt, sb, flag, ErrorMessages.FOLLOWUP_INVALID_CREATEDAT_ERROR_MSG);
                }

                //if (IsUpdate && followUpDTO.IsDirty)
                if (IsUpdate)
                {
                        flag = ValidateKeyId(followUpDTO.ModifiedBy, sb, flag, ErrorMessages.FOLLOWUP_INVALID_MODIFIEDBY_ERROR_MSG);
                }

                //flag = ValidateDate(followUpDTO.FollowUpDate, sb, flag, ErrorMessages.FOLLOWUP_DATE_ERROR_MSG);

                //flag = ValidateKeyId(followUpDTO.LabTestID, sb, flag, ErrorMessages.FOLLOWUP_INVALID_LAB_TEST_ID_ERROR_MSG);
                //flag = ValidateKeyId(followUpDTO.DrugID, sb, flag, ErrorMessages.FOLLOWUP_INVALID_DRUG_ID_ERROR_MSG);

                if (followUpDTO.FollowUpMethodType == null)
                {
                    sb.Append("\n" + ErrorMessages.FOLLOWUP_METHOD_TYPE_ERROR_MSG);
                    flag = false;
                }
                else
                flag = ValidateKeyId(followUpDTO.FollowUpMethodType.FollowUpMethodTypeID, sb, flag, ErrorMessages.FOLLOWUP_METHOD_TYPE_ID_ERROR_MSG);

                     flag = ValidateKeyId(followUpDTO.PatientID, sb, flag, ErrorMessages.FOLLOWUP_INVALID_PATIENT_ID_ERROR_MSG);
                     flag = ValidateKeyId(followUpDTO.VisitID, sb, flag, ErrorMessages.FOLLOWUP_INVALID_VISIT_ID_ERROR_MSG);




            }
            else
            {
                sb.Append("\n" + ErrorMessages.ROS_LIST_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
