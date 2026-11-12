using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusUtility.ProjectConstants;
using Quivus.Billing.Business.VisiBL;
using Quivus.Utility.Logger;
using Quivus.Billing.Business.VisiBL.Impl;
using QuivusBilling.IntegratedServices.VisitService;
using QuivusModel.Billing.PendingBills;

namespace Quivus.Billing.Business.SuperBill.Impl
{
    public class SuperBillImpl: ISuperBill
    {
        private IVisitBL visitBLImpl = null;

        #region Calculate History,Examination,Decision Level

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   02/21/2012
        /// to get history level scale for super bill E/M Service charges info
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public int CalculateHistoryLevel(QuivusBilling.IntegratedServices.VisitService.SuperBillHEDLevelDTO visitHEDData )
        {
            int result = 0;
            int[,] cd = new int[4, 8] { { 1, -1, -1, -1, -1, -1, -1, -1 }, { 2, 3, -1, -1, -1, -1, -1, -1 }, { 1, -1, -1, -1, -1, -1, -1, -1 }, { 4, 5, 6, 7, 8, 9, 10, 11 } };
            int[,] ros = new int[4, 8] { { 0, -1, -1, -1, -1, -1, -1, -1 }, { 1, -1, -1, -1, -1, -1, -1, -1 }, { 2, 3, 4, 5, 6, 7, 8, 9 }, { 10, 11, 12, 13, 14, 15, 16, 17 } };
            int[,] pfsh = new int[4, 8] { { 1, -1, -1, -1, -1, -1, -1, -1 }, { 0, -1, -1, -1, -1, -1, -1, -1 }, { 1, -1, -1, -1, -1, -1, -1, -1 }, { 2, 3, -1, -1, -1, -1, -1, -1 } };
            int CDCount = 0;
            int HPICount = 0;
            int ROSCount = 0;
            int PFSHCount = 0;
            result = (int)BillingConstant.SuperBillHistoryLevelScales.NA;             

            try
            {
               
                CDCount = visitHEDData.ChronicDiagnosis;
                HPICount = visitHEDData.HPI;
                ROSCount = visitHEDData.ROS;
                PFSHCount = visitHEDData.MedicalHistory + visitHEDData.FamilyHistory + visitHEDData.SocialHistory;

                //CDCount = countNumberOfChronicDiagnosisForVisit(visitId);
                //HPICount = countNumberOfHPIForVisit(visitId);
                //ROSCount = countNumberOfROSForVisit(visitId);
                //PFSHCount = countNumberOfPFSHForVisit(visitId);

                //Calculate For Problem Focused
                int commonCDHPI = CDCount + HPICount;
                result = (int)BillingConstant.SuperBillHistoryLevelScales.NA;

                if (commonCDHPI == 1 && ROSCount == 0 && PFSHCount == 0)
                {
                    return result;
                }
                int cdIndex = 0;
                int rosIndex = 0;
                int pfshIndex = 0;
                if (commonCDHPI != 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (commonCDHPI == cd[i, j])
                            {
                                cdIndex = i;
                            }
                            if (commonCDHPI > cd[3, 7])
                            {
                                cdIndex = 3;
                            }
                        }
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (ROSCount == ros[i, j])
                            {
                                rosIndex = i;
                            }
                            if (ROSCount > ros[3, 7])
                            {
                                rosIndex = 3;
                            }
                        }
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (PFSHCount == pfsh[i, j])
                            {
                                pfshIndex = i;
                            }
                            //if (PFSHCount > pfsh[3, 7])
                            //{
                            //    pfshIndex = 3;
                            //}
                        }
                    }


                    result = selectMinimumValueOfThree(cdIndex + 1, rosIndex + 1, pfshIndex + 1);
                    result = result + 1;
                }
                else
                {
                    // Console.WriteLine("Else");
                    result = (int)BillingConstant.SuperBillHistoryLevelScales.NA;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }



            return result;
        }
        public int CalculateExaminationLevel(QuivusBilling.IntegratedServices.VisitService.SuperBillHEDLevelDTO visitHEDData)
        {
            int result = (int)BillingConstant.SuperBillExaminationLevelScales.NA;
            try
            {
                int countForPhysical = visitHEDData.PhysicalTypes;
                if (countForPhysical > 0 && countForPhysical < 6)
                {
                    result = (int)BillingConstant.SuperBillExaminationLevelScales.Focused;
                }
                else if (countForPhysical > 5 && countForPhysical < 12)
                {
                    result = (int)BillingConstant.SuperBillExaminationLevelScales.Expanded;
                }
                else if (countForPhysical > 11 && countForPhysical < 18)
                {
                    result = (int)BillingConstant.SuperBillExaminationLevelScales.Detailed;
                }
                else if (countForPhysical > 17)
                {
                    result = (int)BillingConstant.SuperBillExaminationLevelScales.Comprehensive;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return result;
        }
        public int CalculateDecisionComplexityLevel(int historyLevel, int examinationLevel,  bool isNewEncounter)
        {
            int result = (int)BillingConstant.SuperBillDecisionLevelScales.StraightForward;
            if (isNewEncounter)
            {
                if (historyLevel == (int)BillingConstant.SuperBillHistoryLevelScales.Expanded && examinationLevel == (int)BillingConstant.SuperBillExaminationLevelScales.Expanded)
                {
                    result = (int)BillingConstant.SuperBillDecisionLevelScales.Low;
                }
                else if (historyLevel == (int)BillingConstant.SuperBillHistoryLevelScales.Detailed && examinationLevel == (int)BillingConstant.SuperBillExaminationLevelScales.Detailed)
                {
                    result = (int)BillingConstant.SuperBillDecisionLevelScales.Moderate;
                }

                else if (historyLevel == (int)BillingConstant.SuperBillHistoryLevelScales.Comprehensive && examinationLevel == (int)BillingConstant.SuperBillExaminationLevelScales.Comprehensive)
                {
                    result = (int)BillingConstant.SuperBillDecisionLevelScales.High;
                }
            }

            return result;
        }
       
        #endregion

        #region CalculateENMCode


        public String CalculateENMCode(int patientVisitType, int historyLevel, int examinationLevel, int decisionLevel)
        {
            String suggestedCode = "";
            
            string[] enmCodesNewEncounter = new string[] { "99201", "99202", "99203", "99204", "99205" };
            string[] enmCodesEstablishedEncounter = new string[] { "99211", "99212", "99213", "99214", "99215" };
            try
            {
               
                if (historyLevel == (int)BillingConstant.SuperBillHistoryLevelScales.NA)
                {
                    return "";
                }
                else
                {
                    if (patientVisitType == (int)BillingConstant.EncounterType.New)
                    {
                        if (examinationLevel == (int)BillingConstant.SuperBillExaminationLevelScales.NA ||
                           decisionLevel == (int)BillingConstant.SuperBillDecisionLevelScales.NA)
                        {

                            return "";
                        }

                        int minVal = selectMinimumValueOfThree(historyLevel, examinationLevel, decisionLevel);
                        suggestedCode = enmCodesNewEncounter[minVal - 1];


                    }
                    else//Patient has established Visit
                    {
                        if (examinationLevel == (int)BillingConstant.SuperBillExaminationLevelScales.NA &&
                          decisionLevel == (int)BillingConstant.SuperBillDecisionLevelScales.NA)
                        {

                            return "";
                        }
                        int result = selectAtleaseTwoMatchingElseMiddleOfThree(historyLevel, examinationLevel, decisionLevel);
                        suggestedCode = enmCodesEstablishedEncounter[result - 1];
                    }
                }
                
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return suggestedCode;
        }
        
        private int selectMinimumValueOfThree(int a, int b, int c)
        {
            int result = 0;


            if (a < b)
            {
                result = a;
            }
            else
            {
                result = b;
            }
            if (c < result)
            {
                result = c;
            }

            return result;
        }
        private int selectMaxValueOfThree(int a, int b, int c)
        {
            int result = 0;


            if (a > b)
            {
                result = a;
            }
            else
            {
                result = b;
            }
            if (c > result)
            {
                result = c;
            }

            return result;
        }
        private int selectAtleaseTwoMatchingElseMiddleOfThree(int a, int b, int c)
        {
            int result = 0;


            if (a == b || a == c)
            {
                result = a;
            }
            else if (b == c)
            {
                result = b;
            }
            else
            {
                result = selectMinimumValueOfThree(a, b, c);
            }
            return result;
        }
        #endregion
    }
}
