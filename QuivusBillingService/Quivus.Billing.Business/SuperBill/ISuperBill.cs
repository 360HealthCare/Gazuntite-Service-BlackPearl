using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusBilling.IntegratedServices.VisitService;

namespace Quivus.Billing.Business.SuperBill
{
    /// <summary>
    /// Author: Shahid Munir
    /// </summary>
    public interface ISuperBill
    {
        /// <summary>
        /// Author: Shahid Munir
        /// Date:   02/21/2012
        /// to get history level scale for super bill E/M Service charges info
        /// </summary>
        /// <param name="visitHEDData"></param>
        /// <returns></returns>
        int CalculateHistoryLevel(SuperBillHEDLevelDTO visitHEDData);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   02/21/2012
        /// to get examination level scale for super bill E/M Service charges info 
        /// </summary>
        /// <param name="visitHEDData"></param>
        /// <returns></returns>
        int CalculateExaminationLevel(SuperBillHEDLevelDTO visitHEDData);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   02/21/2012
        /// to get decision level scale for super bill E/M Service charges info
        /// </summary>        
        /// <returns></returns>
        int CalculateDecisionComplexityLevel(int historyLevel, int examinationLevel, bool isNewEncounter);

        String CalculateENMCode(int patientVisitType, int historyLevel, int examinationLevel, int decisionLevel);
    }
}
