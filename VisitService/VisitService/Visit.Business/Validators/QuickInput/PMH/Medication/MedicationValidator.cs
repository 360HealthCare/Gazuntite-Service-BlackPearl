using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.Medication;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.PMH.Medication
{
    public class MedicationValidator : QBaseValidator
    {
        internal static bool ValidateMedication(MedicationDTO obj, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
                    if (obj != null)
                    {
                        if (obj.PatientMedicationID > 0)
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
                            flag = ValidateKeyId(obj.PatientId, sb, flag, ErrorMessages.ROS_INVALID_PATIENT_ID_ERROR_MSG);

                    //    flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                    //    flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);

            }
            else
            {
                sb.Append("\n" + ErrorMessages.MEDICATION_LIST_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateMedication(List<MedicationDTO> MedicationList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;

            foreach (MedicationDTO obj in MedicationList)
            {
                if (obj != null)
                {
                    if (obj.PatientMedicationID > 0)
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
                    flag = ValidateKeyId(obj.PatientId, sb, flag, ErrorMessages.ROS_INVALID_PATIENT_ID_ERROR_MSG);

                    //    flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                    //    flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);

                }
                else
                {
                    sb.Append("\n" + ErrorMessages.MEDICATION_LIST_INVALID_ERROR_MSG);
                    flag = false;
                }
            }
            return flag;
        }
    }
}
