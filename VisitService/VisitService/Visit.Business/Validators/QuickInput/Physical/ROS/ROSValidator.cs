using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.ROS;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Physical.ROS
{
    public class ROSValidator : QBaseValidator
    {
        internal static bool ValidateROS(List<ROSDTO> ROSList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (ROSList != null)
            {
                foreach (ROSDTO obj in ROSList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientROSID > 0)
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

                    /*  Comented By Ismail Validation Needs To be Verified Acc To New Changes i.e Level 3 Category
                      flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                        flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);
                        flag = ValidateKeyId(obj.ROSSubLevel3CategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_LEVEL_CATEGORY_ID_ERROR_MSG);
                     */

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

        internal static bool ValidateVBROS(List<ROSDTO> ROSList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;

            if (ROSList != null)
            {
                foreach (ROSDTO obj in ROSList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientROSID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                               flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.ROS_INVALID_CREATEDBY_ERROR_MSG);
                                flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.ROS_INVALID_CREATEDAT_ERROR_MSG);
                        }
                      //  if (IsUpdate && obj.IsDirty)
                        if (IsUpdate)
                        {
                            //    flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.ROS_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                        //   flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.ROS_INVALID_VISIT_ID_ERROR_MSG);
                        //    flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.ROS_INVALID_PATIENT_ID_ERROR_MSG);

                        /*  Comented By Ismail Validation Needs To be Verified Acc To New Changes i.e Level 3 Category
                          * flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                             flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);
                             flag = ValidateKeyId(obj.ROSSubLevel3CategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_LEVEL_CATEGORY_ID_ERROR_MSG);
                             */
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
    }
}
