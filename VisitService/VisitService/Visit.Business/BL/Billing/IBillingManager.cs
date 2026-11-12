using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Billing.PendingBills;
using QuivusModel.Billing.Visit;
using QuivusModel.TypeDTOs.BillingTypeDTOs;
using Quivus.Model.QuickInput.Diagnosis;

namespace Visit.Business.BL.Billing
{
    /// <summary>
    /// Auhtor: Shahid Munir
    /// Date:   01/27/2012
    /// Billing Service related Mehtods
    /// </summary>
    public interface IBillingManager
    {
        #region E/M calculation for Super bill

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get History Examination and Decision Level counts
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        SuperBillHEDLevelDTO LoadVisitHEDLevelCounts(int visitId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get count of patient HPI against visit for Super Bill E/M calculation
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        int CountHPIforVisit(int visitId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get count of choronic diagnosis against visit for Super Bill E/M calculation
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        int CountVisitChronicDiagnosis(int visitId);

        #endregion

        /// <summary>
        /// /// Auhtor: Shahid Munir
        /// Date:   01/27/2012
        /// to manage diagnosis updated created from Super Bill 
        /// </summary>
        /// <param name="diagnosisList"></param>
        /// <returns></returns>
        bool ManageVisitDiagnosis(List<PatientDiagnosisDTO> diagnosisList);

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all place of service types
        /// </summary>
        /// <returns></returns>
        List<PlaceOfServiceTypeDTO> LoadPlaceOfServiceTypeByName(string typrName);

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to manage procedure modifiers from Super Bill
        /// </summary>
        /// <param name="InputList"></param>        
        /// <returns></returns>
        bool ManagePatientProcedureModifier(List<PatientProcedureModifierDTO> InputList);
       
        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get procedure modifiers for Super Bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="IsTemplateVisit"></param>
        /// <returns></returns>
        List<PatientProcedureModifierDTO> LoadProcedureModifierDTOListByPatientProcedureId(int patientProcedureId);

        /// <summary>
        /// Auhtor: Shahid Munir
        /// Date:   01/27/2012
        /// to get Procedure modifier types
        /// </summary>
        /// <param name="ModifierCode"></param>
        /// <returns></returns>
        List<ProcedureModifierTypeDTO> LoadProcedureModifierTypes(String ModifierCode);
        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to manage visit procedures from Super Bill
        /// </summary>
        /// <param name="InputList"></param>        
        /// <returns></returns>
        bool ManageVisitProcedure(List<QBProcedureDTO> InputList);

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get visit Data for Billing service 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        QBVisitDTO LoadVisitDataByVisitId(int visitId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/27/2012
        /// to get visit procedures for Billing service
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        List<QBProcedureDTO> LoadPatientProcedureByVisitId(int visitId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark Diagnosis  as created from super bill
        /// </summary>
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        bool PatientDaignosisCreatedFromBilling(List<int> PatientDiagnosisProcedureIds, int UpdatedByUserID);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark Diagnosis or procedures as include in billing
        /// </summary>
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        bool PatientDaignosisProceduresMarkAsIncludeInBilling(List<int> PatientDiagnosisProcedureIds, int UpdatedByUserID, bool IsIncluded, string CodeType);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to load all visit that for which claim not created for billing
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        List<PendingBillsDTO> LoadAllPendingBillsByProviderId(int providerId, int clinincBranchId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark visit that its claim created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="ClaimCreatedByUserID"></param>
        /// <param name="OperationDate"></param>
        /// <returns></returns>
        bool VisitClaimCreated(int VisitId, int ClaimCreatedByUserID, DateTime OperationDate, int encounterTypeId, int placeOfServiceTypeId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark visit that its claim created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="ClaimCreatedByUserID"></param>
        /// <param name="OperationDate"></param>
        /// <returns></returns>
        bool VisitClaimListCreated(List<int> VisitIds, int ClaimCreatedByUserID, DateTime OperationDate);

        /*/// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark procedures as included in billing
        /// </summary>
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        bool PatientProceduresMarkIncludedInBilling(List<int> PatientProcedureIds, int UpdatedByUserID, bool IsIncluded);
        */
        
    }
}
