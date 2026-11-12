using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusBilling.ServiceResponse.Visit;
using QuivusBilling.IntegratedServices.VisitService;

namespace Quivus.Billing.Business.VisiBL
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   01/04/2011
    /// </summary>
    public interface IVisitBL
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
        /// to get all the encounter types
        /// </summary>
        /// <returns></returns>
        List<EncounterTypeDTO> LoadEncounterTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all place of service types
        /// </summary>
        /// <returns></returns>
        List<PlaceOfServiceTypeDTO> LoadPlaceOfServiceTypeByName(string typrName);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get ICD codes for super bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        List<ICDCodeDTO> LoadICDCodeByName(string queryString);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get procedure modifier types
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        List<ProcedureModifierTypeDTO> LoadProcedureModifierTypes(String CPTName);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get modifiers of a patient procedure
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        List<PatientProcedureModifierDTO> LoadProcedureModifierDTOListByPatientProcedureId(int patientProcedureId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to manage procedure modifier from Super Bill
        /// </summary>
        /// <param name="InputList"></param>        
        /// <returns></returns>
        bool ManagePatientProcedureModifier(List<PatientProcedureModifierDTO> InputList);

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
        /// Date:   01/04/2011
        /// to get signed visits data for which claims not created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        List<CPTMedicalTypeDTO> LoadCPTMedicalTypes(String CPTName);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get signed visits data for which claims not created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        List<PendingBillsDTO> LoadAllPendingBillsByProviderId(int providerId, int clinincBranchId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get visit diagnosis
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        List<PatientDiagnosisDTO> LoadVisitDiagnosis(int visitId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get visit diagnosis
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        string LoadVisitDiagnosisByVisitId(int visitId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get visit data 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        VisitResponse LoadVisitDataByVisitId(int VisitId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get visit procedures
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        VisitProcedureResponse LoadVisitProceduresByVisitId(int visitId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/09/2011
        /// to mark visit as claim created
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="ClaimCreatedByUserID"></param>
        /// <param name="OperationDate"></param>
        /// <returns></returns>
        bool MarkVisitaAsClaimCreated(int VisitId, int ClaimCreatedByUserID, DateTime OperationDate, int encounterTypeId, int placeOfServiceTypeId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/09/2011
        /// to mark patient procedures as included in billing
        /// </summary>
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        bool PatientProceduresMarkIncludedInBilling(List<int> PatientProcedureIds, int UpdatedByUserID, bool IsIncluded);
    }
}
