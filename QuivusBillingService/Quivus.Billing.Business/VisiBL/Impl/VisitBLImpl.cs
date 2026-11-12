using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusBilling.ServiceResponse.Visit;
using QuivusBilling.IntegratedServices.VisitService;
using Quivus.Utility;
using Quivus.Billing.Business.ReferenceFactory;
using Quivus.Utility.Logger;

namespace Quivus.Billing.Business.VisiBL.Impl
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   01/04/2011
    /// </summary>
    public class VisitBLImpl : IVisitBL
    {
        private VisitServiceClient clientVisit;

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
                QuivusLogger.Info("Calling LoadVisitHEDLevelCounts");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadVisitHEDLevelCounts Call TO VisitService");

                visitHEDData = clientVisit.LoadVisitHEDLevelCounts(visitId);

            }
            catch
            {
                throw;
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
                QuivusLogger.Info("Calling CountHPIforVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching CountHPIforVisit Call TO VisitService");

                hpiCount = clientVisit.CountHPIforVisit(visitId);

            }
            catch
            {
                throw;
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
                QuivusLogger.Info("Calling CountVisitChronicDiagnosis");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching CountVisitChronicDiagnosis Call TO VisitService");

                diagCount = clientVisit.CountVisitChronicDiagnosis(visitId);

            }
            catch
            {
                throw;
            }
            return diagCount;
        }

        #endregion

        /// <summary>
        /// /// Auhtor: Shahid Munir
        /// Date:   01/27/2012
        /// to manage diagnosis updated created from Super Bill 
        /// </summary>
        /// <param name="diagnosisList"></param>
        /// <returns></returns>
        public bool ManageVisitDiagnosis(List<PatientDiagnosisDTO> diagnosisList)
        {
            bool Result = false;

            try
            {
                QuivusLogger.Info("Calling ManageVisitDiagnosis");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManageVisitDiagnosis Call TO VisitService");

                Result = clientVisit.ManageVisitDiagnosis(diagnosisList);

            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all the encounter types
        /// </summary>
        /// <returns></returns>
        public List<EncounterTypeDTO> LoadEncounterTypes()
        {
            List<EncounterTypeDTO> TOSList = null;

            try
            {
                QuivusLogger.Info("Calling LoadEncounterTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadEncounterTypes Call TO VisitService");

                TOSList = clientVisit.LoadEncounterTypes();

                QuivusLogger.Info("Response Recieved ::" + TOSList);

            }
            catch
            {
                throw;
            }
            return TOSList;
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
                QuivusLogger.Info("Calling LoadICDCodeByName");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPlaceOfServiceTypeByName Call TO VisitService");

                POSList = clientVisit.LoadPlaceOfServiceTypeByName(typrName);

                QuivusLogger.Info("Response Recieved ::" + POSList);

            }
            catch
            {
                throw;
            }
            return POSList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get ICD codes for super bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public List<ICDCodeDTO> LoadICDCodeByName(string queryString)
        {
            List<ICDCodeDTO> ICDCodeList = null;

            try
            {
                QuivusLogger.Info("Calling LoadICDCodeByName");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadICDCodeByName Call TO VisitService");

                ICDCodeList = clientVisit.LoadICDCodeByName(queryString);

                QuivusLogger.Info("Response Recieved ::" + ICDCodeList);

            }
            catch
            {
                throw;
            }
            return ICDCodeList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get procedure modifier types
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public List<ProcedureModifierTypeDTO> LoadProcedureModifierTypes(String ModifierCode)
        {
            List<ProcedureModifierTypeDTO> cptModifierTypeList = null;

            try
            {
                QuivusLogger.Info("Calling LoadProcedureModifierTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadProcedureModifierTypes Call TO VisitService");

                cptModifierTypeList = clientVisit.LoadProcedureModifierTypes(ModifierCode);

                QuivusLogger.Info("Response Recieved ::" + cptModifierTypeList);

            }
            catch
            {
                throw;
            }
            return cptModifierTypeList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get modifiers of a patient procedure
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public List<PatientProcedureModifierDTO> LoadProcedureModifierDTOListByPatientProcedureId(int patientProcedureId)
        {
            List<PatientProcedureModifierDTO> cptModifierList = null;

            try
            {
                QuivusLogger.Info("Calling LoadProcedureModifierDTOListByPatientProcedureId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadAllPendingBillsByProviderId Call TO VisitService");

                cptModifierList = clientVisit.LoadProcedureModifierDTOListByPatientProcedureId(patientProcedureId);

                QuivusLogger.Info("Response Recieved ::" + cptModifierList);

            }
            catch
            {
                throw;
            }
            return cptModifierList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to manage procedure modifier from Super Bill
        /// </summary>
        /// <param name="InputList"></param>        
        /// <returns></returns>
        public bool ManagePatientProcedureModifier(List<PatientProcedureModifierDTO> InputList)
        {
            bool Result = false;

            try
            {
                QuivusLogger.Info("Calling ManagePatientProcedureModifier");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientProcedureModifier Call TO VisitService");

                Result = clientVisit.ManagePatientProcedureModifier(InputList);

            }
            catch
            {
                throw;
            }
            return Result;
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
            bool Result = false;

            try
            {
                QuivusLogger.Info("Calling ManageVisitProcedure");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManageVisitProcedure Call TO VisitService");

                Result = clientVisit.ManageVisitProcedure(InputList);

            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get signed visits data for which claims not created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public List<CPTMedicalTypeDTO> LoadCPTMedicalTypes(String CPTName)
        {
            List<CPTMedicalTypeDTO> cptMedicalTypeList = null;

            try
            {
                QuivusLogger.Info("Calling LoadCPTMedicalTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadCPTMedicalTypes Call TO VisitService");

                cptMedicalTypeList = clientVisit.LoadCPTMedicalTypes(CPTName);

                QuivusLogger.Info("Response Recieved ::" + cptMedicalTypeList);

            }
            catch
            {
                throw;
            }
            return cptMedicalTypeList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get signed visits data for which claims not created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public List<PendingBillsDTO> LoadAllPendingBillsByProviderId(int providerId, int clinincBranchId)
        {
            List<PendingBillsDTO> pendingBillsData = null;

            try
            {
                QuivusLogger.Info("Calling LoadAllPendingBillsByProviderId");

                if (clientVisit == null)
                {                    
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadAllPendingBillsByProviderId Call TO VisitService");

                pendingBillsData = clientVisit.LoadAllPendingBillsByProviderId(providerId,clinincBranchId);

                QuivusLogger.Info("Response Recieved ::" + pendingBillsData);
               
            }
            catch
            {
                throw;
            }
            return pendingBillsData;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get visit diagnosis
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public List<PatientDiagnosisDTO> LoadVisitDiagnosis(int visitId)
        {
            List<PatientDiagnosisDTO> visitDiagnosisList = null;
            try
            {
                QuivusLogger.Info("Calling LoadVisitDiagnosis");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadVisitDiagnosis Call TO VisitService");

                visitDiagnosisList = clientVisit.LoadPatientDiagnosisByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  VisitDiagnosisList::" + visitDiagnosisList);
                
            }
            catch
            {
                throw;
            }
            return visitDiagnosisList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get visit diagnosis
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public string LoadVisitDiagnosisByVisitId(int visitId)
        {
            string diagnoses = "";            
            List<PatientDiagnosisDTO> visitDiagnosisList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientDiagnosisByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientDiagnosisByVisitId Call TO VisitService");

                visitDiagnosisList = clientVisit.LoadPatientDiagnosisByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  VisitDiagnosisList::" + visitDiagnosisList);               

                if (visitDiagnosisList != null)
                {
                    for (int i = 0; i < visitDiagnosisList.Count; i++)
                    {
                        diagnoses += i + 1 + " : " + visitDiagnosisList[i].ICDCode.IcdCodeName.Replace(".","") + "\t";
                    }
                    
                }                
            }
            catch
            {
                throw;
            }
            return diagnoses;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get visit data 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public VisitResponse LoadVisitDataByVisitId(int VisitId)
        {
            VisitResponse response = null;
            QBVisitDTO visitData = null; 
            
            try
            {
                QuivusLogger.Info("Calling LoadVisitDataByVisitId");

                if (clientVisit == null)
                {
                    //clientVisit = new VisitServiceClient();
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadVisitDataByVisitId Call TO VisitService");

                visitData = clientVisit.LoadVisitDTOByVisitId(VisitId);

                QuivusLogger.Info("Response Recieved ::" + visitData);

                if (response == null)
                {
                    response = new VisitResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (visitData != null)
                {
                    response.VisitData = visitData;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get visit procedures
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public VisitProcedureResponse LoadVisitProceduresByVisitId(int visitId)
        {
            VisitProcedureResponse response = null;
            List<QBProcedureDTO> visitProcedureList = null;
            try
            {
                QuivusLogger.Info("Calling LoadVisitProceduresByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadVisitProceduresByVisitId Call TO VisitService");

                visitProcedureList = clientVisit.LoadProceduresByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  VisitProcedureList::" + visitProcedureList);

                if (response == null)
                {
                    response = new VisitProcedureResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (visitProcedureList != null)
                {
                    response.VisitProcedureList = visitProcedureList;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/09/2011
        /// to mark visit as claim created
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="ClaimCreatedByUserID"></param>
        /// <param name="OperationDate"></param>
        /// <returns></returns>
        public bool MarkVisitaAsClaimCreated(int VisitId, int ClaimCreatedByUserID, DateTime OperationDate, int encounterTypeId, int placeOfServiceTypeId)
        {
            bool Result = false;
            
            try
            {
                QuivusLogger.Info("Calling MarkVisitaAsClaimCreated");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching MarkVisitaAsClaimCreated Call TO VisitService");

                Result = clientVisit.VisitClaimCreated(VisitId, ClaimCreatedByUserID, DateTime.Now,encounterTypeId,placeOfServiceTypeId);
               
            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/09/2011
        /// to mark visit as claim created
        /// </summary>
        /// <param name="VisitIds"></param>
        /// <param name="ClaimCreatedByUserID"></param>
        /// <param name="OperationDate"></param>
        /// <returns></returns>
        public bool MarkVisitAsClaimListCreated(List<int> VisitIds, int ClaimCreatedByUserID, DateTime OperationDate)
        {
            bool Result = false;

            try
            {
                QuivusLogger.Info("Calling MarkVisitaAsClaimCreated");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching MarkVisitaAsClaimCreated Call TO VisitService");

                Result = clientVisit.VisitClaimListCreated(VisitIds, ClaimCreatedByUserID, DateTime.Now);

            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/09/2011
        /// to mark patient procedures as included in billing
        /// </summary>
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        public bool PatientProceduresMarkIncludedInBilling(List<int> PatientProcedureIds, int UpdatedByUserID, bool IsIncluded)
        {
            bool Result = false;

            try
            {
                QuivusLogger.Info("Calling PatientProceduresMarkIncludedInBilling");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching PatientProceduresMarkIncludedInBilling Call TO VisitService");

                Result = clientVisit.PatientDaignosisProceduresMarkAsIncludeInBilling(PatientProcedureIds, UpdatedByUserID, IsIncluded,"P");

            }
            catch
            {
                throw;
            }
            return Result;
        }
    }
}
