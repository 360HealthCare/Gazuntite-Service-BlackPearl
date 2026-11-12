using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.FollowUp;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Assessment
{
     public class FollowupInstructionValidator : QBaseValidator
    {

         internal static bool ValidateFollowupInstruction(List<PatientFollowupInstructionDTO> InputList, ref StringBuilder sb)
         {
             bool flag = true;

             bool IsUpdate = false;
             if (InputList != null)
             {
                 foreach (PatientFollowupInstructionDTO obj in InputList)
                 {
                     if (obj != null)
                     {
                         if (obj.FollowupInstructionID > 0)
                             IsUpdate = true;

                         if (!IsUpdate)
                         {
                             //   flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.ROS_INVALID_CREATEDBY_ERROR_MSG);
                             //    flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.ROS_INVALID_CREATEDAT_ERROR_MSG);
                         }
                         if (IsUpdate)
                         {
                             //    flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.ROS_INVALID_MODIFIEDBY_ERROR_MSG);
                         }

                         //   flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.ROS_INVALID_VISIT_ID_ERROR_MSG);
                         //    flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.ROS_INVALID_PATIENT_ID_ERROR_MSG);

                         ////if (obj.objServiceTypeDTO != null)
                         ////{
                         ////    flag = ValidateKeyId(obj.objServiceTypeDTO.FollowupServiceTypeId, sb, flag, ErrorMessages.INVALID_FOLLOW_UP_SERVICE_TYPE_ID_ERROR_MSG);
                         ////}
                         ////else
                         ////{
                         ////    sb.Append("\n" + ErrorMessages.INVALID_FOLLOW_UP_SERVICE_TYPE_ERROR_MSG);
                         ////    flag = false;
                         ////}



                     }
                 }
             }
             else
             {
                 sb.Append("\n" + ErrorMessages.ROS_LIST_INVALID_ERROR_MSG);
                 flag = false;
             }
             return flag;
         }


         internal static bool ValidateVBFollowupInstruction(List<PatientFollowupInstructionDTO> InputList, ref StringBuilder sb)
         {
             bool flag = true;

             bool IsUpdate = false;
             if (InputList != null)
             {
                 foreach (PatientFollowupInstructionDTO obj in InputList)
                 {
                     if (obj != null)
                     {
                         if (obj.FollowupInstructionID > 0)
                             IsUpdate = true;

                         if (!IsUpdate)
                         {
                             flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.FOLLOW_UP_INSTRUCTION_INVALID_CREATEDBY_ERROR_MSG);
                             flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.FOLLOW_UP_INSTRUCTION_INVALID_CREATEDAT_ERROR_MSG);
                         }
                         if (IsUpdate && obj.IsDirty)
                         {
                             flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.FOLLOW_UP_INSTRUCTION_INVALID_MODIFIEDBY_ERROR_MSG);
                         }

                         flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.FOLLOW_UP_INSTRUCTION_INVALID_VISIT_ID_ERROR_MSG);
                         flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.FOLLOW_UP_INSTRUCTION_INVALID_PATIENT_ID_ERROR_MSG);

                         
                     }
                 }
             }
             else
             {
                 sb.Append("\n" + ErrorMessages.FOLLOW_UP_INSTRUCTION__LIST_INVALID_ERROR_MSG);
                 flag = false;
             }
             return flag;
         }
    }
}
