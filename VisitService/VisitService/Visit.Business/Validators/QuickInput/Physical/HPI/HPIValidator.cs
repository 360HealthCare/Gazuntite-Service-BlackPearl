using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.HPI;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Physical.HPI
{
  
    /// Author Mansoor 
    /// Date 21 Mar 2011      
    public class HPIValidator : QBaseValidator
    {
        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used validate Patient HPI data
        /// </summary>
        /// <returns></returns>
        public static bool ValidateHPI(List<HPI_CCDTO> hpiList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (hpiList != null)
            {
                foreach (HPI_CCDTO obj in hpiList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientHPIID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                          //  flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.HPI_INVALID_CREATEDBY_ERROR_MSG);
                         //   flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.HPI_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                         //   flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.HPI_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                     //   flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.HPI_INVALID_VISIT_ID_ERROR_MSG);
                     //   flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.HPI_INVALID_PATIENT_ID_ERROR_MSG);

                        //flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                        //flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.HPI_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateVBHPI(List<HPI_CCDTO> hpiList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (hpiList != null)
            {
                foreach (HPI_CCDTO obj in hpiList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientHPIID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                              flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.HPI_INVALID_CREATEDBY_ERROR_MSG);
                               flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.HPI_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                               flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.HPI_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                           flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.HPI_INVALID_VISIT_ID_ERROR_MSG);
                           flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.HPI_INVALID_PATIENT_ID_ERROR_MSG);

                        //flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                        //flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.HPI_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
