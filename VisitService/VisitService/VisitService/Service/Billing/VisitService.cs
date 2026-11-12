using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Visit.Business.BL.Billing;
using Quivus.Utility.Logger;
using QuivusUtility.Utilities;
using Quivus.Utility.CustomException;
using Visit.Business.BL.Billing.Impl;
using QuivusModel.Billing.PendingBills;
using QuivusModel.Billing.Visit;
using QuivusModel.TypeDTOs.BillingTypeDTOs;
using Quivus.Model.QuickInput.Diagnosis;

namespace VisitService.Service
{

    /// <summary>
    /// Author: Shahid Munir
    /// Date: 02/03/2012
    /// Billing Service related Mehtods
    /// </summary>
    public partial class VisitService
    {
        
        IBillingManager billingImpl = null;


        #region E/M calculation for Super bill

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get History Examination and Decision Level counts
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public SuperBillHEDLevelDTO LoadVisitHEDLevelCounts(int visitId)
        {
            SuperBillHEDLevelDTO visitHEDData = new SuperBillHEDLevelDTO();
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                visitHEDData = billingImpl.LoadVisitHEDLevelCounts(visitId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return visitHEDData;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get count of patient HPI against visit for Super Bill E/M calculation
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public int CountHPIforVisit(int visitId)
        {
            int hpiCount = 0;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                hpiCount = billingImpl.CountHPIforVisit(visitId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return hpiCount;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get count of choronic diagnosis against visit for Super Bill E/M calculation
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public int CountVisitChronicDiagnosis(int visitId)
        {
            int diagCount = 0;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                diagCount = billingImpl.CountVisitChronicDiagnosis(visitId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return diagCount;
        }

        #endregion

        /// <summary>
        /// Auhtor: Shahid Munir
        /// Date:   01/27/2012
        /// to manage diagnosis updated created from Super Bill 
        /// </summary>
        /// <param name="diagnosisList"></param>
        /// <returns></returns>
        public bool ManageVisitDiagnosis(List<PatientDiagnosisDTO> diagnosisList)
        {
            bool Status = false;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                Status = billingImpl.ManageVisitDiagnosis(diagnosisList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all place of service types
        /// </summary>
        /// <returns></returns>
        public List<PlaceOfServiceTypeDTO> LoadPlaceOfServiceTypeByName(string typrName)
        {
            List<PlaceOfServiceTypeDTO> POSList = null;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                POSList = billingImpl.LoadPlaceOfServiceTypeByName(typrName);

                QuivusLogger.Info("LoadPlaceOfServiceTypeByName Return typrName ::" + typrName);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return POSList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to manage procedure modifiers from Super Bill
        /// </summary>
        /// <param name="InputList"></param>        
        /// <returns></returns>
        public bool ManagePatientProcedureModifier(List<PatientProcedureModifierDTO> InputList)
        {
            bool Status = false;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                Status = billingImpl.ManagePatientProcedureModifier(InputList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get procedure modifiers for Super Bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="IsTemplateVisit"></param>
        /// <returns></returns>
        public List<PatientProcedureModifierDTO> LoadProcedureModifierDTOListByPatientProcedureId(int patientProcedureId)
        {
            List<PatientProcedureModifierDTO> procedureModifierList = null;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                procedureModifierList = billingImpl.LoadProcedureModifierDTOListByPatientProcedureId(patientProcedureId);

                QuivusLogger.Info("LoadProcedureModifierTypes Return ModifierCode ::" + patientProcedureId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return procedureModifierList;
        }

        /// <summary>
        /// Auhtor: Shahid Munir
        /// Date:   01/27/2012
        /// to get Procedure modifier types
        /// </summary>
        /// <param name="ModifierCode"></param>
        /// <returns></returns>
        public List<ProcedureModifierTypeDTO> LoadProcedureModifierTypes(String ModifierCode)
        {
            List<ProcedureModifierTypeDTO> procedureList = null;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                procedureList = billingImpl.LoadProcedureModifierTypes(ModifierCode);

                QuivusLogger.Info("LoadProcedureModifierTypes Return ModifierCode ::" + ModifierCode);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return procedureList;
        }
        
        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to manage visit procedures from Super Bill
        /// </summary>
        /// <param name="InputList"></param>        
        /// <returns></returns>
        public bool ManageVisitProcedure(List<QBProcedureDTO> InputList)
        {
            bool Status = false;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                Status = billingImpl.ManageVisitProcedure(InputList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get visit Data for Billing service 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public QBVisitDTO LoadVisitDTOByVisitId(int visitId)
        {
            QBVisitDTO visitData = null;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                visitData = billingImpl.LoadVisitDataByVisitId(visitId);

                QuivusLogger.Info("LoadVisitDTOByVisitId Return visitData ::" + visitData);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return visitData;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/27/2012
        /// to get visit procedures for Billing service
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public List<QBProcedureDTO> LoadProceduresByVisitId(int visitId)
        {
            List<QBProcedureDTO> procedureList = null;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                procedureList = billingImpl.LoadPatientProcedureByVisitId(visitId);

                QuivusLogger.Info("LoadPatientProcedureByVisitId Return ProcedureList ::" + procedureList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return procedureList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark Diagnosis as created from super bill
        /// </summary>d
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        public bool PatientDaignosisCreatedFromBilling(List<int> PatientDiagnosisProcedureIds, int UpdatedByUserID)
        {
            bool Status = false;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                Status = billingImpl.PatientDaignosisCreatedFromBilling(PatientDiagnosisProcedureIds, UpdatedByUserID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark Diagnosis or procedures as include in billing
        /// </summary>
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        public bool PatientDaignosisProceduresMarkAsIncludeInBilling(List<int> PatientDiagnosisProcedureIds, int UpdatedByUserID, bool IsIncluded, string CodeType)
        {
            bool Status = false;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                Status = billingImpl.PatientDaignosisProceduresMarkAsIncludeInBilling(PatientDiagnosisProcedureIds, UpdatedByUserID, IsIncluded, CodeType);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to load all visit that for which claim not created for billing
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public List<PendingBillsDTO> LoadAllPendingBillsByProviderId(int providerId, int clinincBranchId)
        {
            List<PendingBillsDTO> response = null;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                response = billingImpl.LoadAllPendingBillsByProviderId(providerId, clinincBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark visit that its claim created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="ClaimCreatedByUserID"></param>
        /// <param name="OperationDate"></param>
        /// <returns></returns>
        public bool VisitClaimCreated(int VisitId, int ClaimCreatedByUserID, DateTime OperationDate, int encounterTypeId, int placeOfServiceTypeId)
        {
            bool Status = false;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                Status = billingImpl.VisitClaimCreated(VisitId, ClaimCreatedByUserID, OperationDate,encounterTypeId,placeOfServiceTypeId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark visit that its claim created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="ClaimCreatedByUserID"></param>
        /// <param name="OperationDate"></param>
        /// <returns></returns>
        public bool VisitClaimListCreated(List<int> VisitIds, int ClaimCreatedByUserID, DateTime OperationDate)
        {
            bool Status = false;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                Status = billingImpl.VisitClaimListCreated(VisitIds, ClaimCreatedByUserID, OperationDate);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }

        /*
        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark procedures as included in billing
        /// </summary>
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        public bool PatientProceduresMarkIncludedInBilling(List<int> PatientProcedureIds, int UpdatedByUserID, bool IsIncluded)
        {
            bool Status = false;
            try
            {
                if (billingImpl == null)
                {
                    billingImpl = new BillingManagerImpl();
                }

                Status = billingImpl.PatientProceduresMarkIncludedInBilling(PatientProcedureIds, UpdatedByUserID, IsIncluded);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Status;
        }
        */
    }
}