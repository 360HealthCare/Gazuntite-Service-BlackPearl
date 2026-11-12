using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visit.DataAccess.DAO;
using Quivus.Utility.Logger;
using Quivus.Utility.Factory;
using QuivusModel.Billing.PendingBills;
using QuivusModel.Billing.Visit;
using QuivusModel.TypeDTOs.BillingTypeDTOs;
using Quivus.Model.QuickInput.Diagnosis;

namespace Visit.Business.BL.Billing.Impl
{
    /// <summary>
    /// Auhtor: Shahid Munir
    /// Date:   01/27/2012
    /// </summary>
    public class BillingManagerImpl: IBillingManager
    {

        private BillingDAO billingDAO;

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
            SuperBillHEDLevelDTO visitHEDData;

            try
            {
                QuivusLogger.Info("Calling LoadVisitHEDLevelCounts PARAM :: visitId " + visitId);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                visitHEDData = billingDAO.LoadVisitHEDLevelCounts(visitId);
            }
            catch (Exception exp)
            {
                throw exp;
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
                QuivusLogger.Info("Calling CountHPIforVisit visitId :: " + visitId);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                hpiCount = billingDAO.CountHPIforVisit(visitId);

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
                QuivusLogger.Info("Calling CountVisitChronicDiagnosis visitId :: " + visitId);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                diagCount = billingDAO.CountVisitChronicDiagnosis(visitId);

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
            bool result = false;

            try
            {
                QuivusLogger.Info("Calling ManageVisitDiagnosis diagnosisList :: " + diagnosisList);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                result = billingDAO.ManageVisitDiagnosis(diagnosisList);                

            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all place of service types
        /// </summary>
        /// <returns></returns>
        public List<PlaceOfServiceTypeDTO> LoadPlaceOfServiceTypeByName(string typrName)
        {            
            List<PlaceOfServiceTypeDTO> List = null;
            try
            {
                QuivusLogger.Info("Getting LoadPlaceOfServiceTypeByName");
                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = billingDAO.LoadPlaceOfServiceTypeByName(typrName);                
            }
            catch
            {
                throw;
            }
            return List;
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
            bool result = false;

            try
            {
                QuivusLogger.Info("Calling ManagePatientProcedureModifier InputList :: " + InputList);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                result = billingDAO.ManagePatientProcedureModifier(InputList);

            }
            catch
            {
                throw;
            }
            return result;
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
            List<PatientProcedureModifierDTO> list;

            try
            {
                QuivusLogger.Info("Calling LoadProcedureModifierDTOListByPatientProcedureId PARAM :: patientProcedureId " + patientProcedureId);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                list = billingDAO.LoadProcedureModifierDTOListByPatientProcedureId(patientProcedureId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
            List<ProcedureModifierTypeDTO> list;

            try
            {
                QuivusLogger.Info("Calling LoadProcedureModifierTypes PARAM :: ModifierCode " + ModifierCode);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                list = billingDAO.LoadProcedureModifierTypes(ModifierCode);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
            bool result = false;

            try
            {
                QuivusLogger.Info("Calling ManageVisitProcedure InputList :: " + InputList);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                result = billingDAO.ManageVisitProcedure(InputList);

                //for updated cpt.....
                //if (result)
                //{
                //    List<PatientProcedureModifierDTO> modifierList = new List<PatientProcedureModifierDTO>();
                //    foreach (QBProcedureDTO cpt in InputList)
                //    {
                //        PatientProcedureModifierDTO modiCode = null;
                //        if (cpt.ModifierTypeId1 != null && cpt.ModifierTypeId1 > 0)
                //        {
                //            modiCode = new PatientProcedureModifierDTO();
                //            modiCode.ProcedureModifierID = 0;
                //            modiCode.PatientProcedureID = cpt.PatientProcedureID;
                //        }
                //    }
                //}

            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get visit Data for Billing service 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public QBVisitDTO LoadVisitDataByVisitId(int visitId)
        {
            QBVisitDTO visitData;

            try
            {
                QuivusLogger.Info("Calling LoadVisitDataByVisitId PARAM :: visitId " + visitId);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                visitData = billingDAO.LoadVisitDataByVisitId(visitId);
            }
            catch (Exception exp)
            {
                throw exp;
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
        public List<QBProcedureDTO> LoadPatientProcedureByVisitId(int visitId)
        {
            List<QBProcedureDTO> list;

            try
            {
                QuivusLogger.Info("Calling LoadAllPendingBillsByProviderId PARAM :: visitId " + visitId);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                list = billingDAO.LoadProcedureDTOListByVisitId(visitId);

                foreach (QBProcedureDTO CPT in list)
                {                    
                    List<PatientProcedureModifierDTO> modifierList = null;
                    modifierList = LoadProcedureModifierDTOListByPatientProcedureId(CPT.PatientProcedureID);
                    int counter = 1;
                    foreach (PatientProcedureModifierDTO modi in modifierList)
                    {
                        if (counter == 1)
                        {
                            CPT.Modifier1 = modi.ProcedureModifierType.ModifierDescription.Split('-')[0].Trim();
                            CPT.ModifierTypeId1 = modi.ProcedureModifierType.ModifierTypeId;
                        }
                        else if (counter == 2)
                        {
                            CPT.Modifier2 = modi.ProcedureModifierType.ModifierDescription.Split('-')[0].Trim();
                            CPT.ModifierTypeId2 = modi.ProcedureModifierType.ModifierTypeId;
                        }
                        else if (counter == 3)
                        {
                            CPT.Modifier3 = modi.ProcedureModifierType.ModifierDescription.Split('-')[0].Trim();
                            CPT.ModifierTypeId3 = modi.ProcedureModifierType.ModifierTypeId;
                        }
                        else if (counter == 4)
                        {
                            CPT.Modifier4 = modi.ProcedureModifierType.ModifierDescription.Split('-')[0].Trim();
                            CPT.ModifierTypeId4 = modi.ProcedureModifierType.ModifierTypeId;
                        }
                        counter += 1;

                    }

                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling PatientDaignosisCreatedFromBilling PatientDiagnosisProcedureIds :: " + PatientDiagnosisProcedureIds + " UpdatedByUserID " + UpdatedByUserID);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                result = billingDAO.PatientDaignosisCreatedFromBilling(PatientDiagnosisProcedureIds, UpdatedByUserID);

            }
            catch
            {
                throw;
            }
            return result;
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
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling PatientDaignosisProceduresMarkAsIncludeInBilling PatientDiagnosisProcedureIds :: " + PatientDiagnosisProcedureIds + " UpdatedByUserID " + UpdatedByUserID);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                result = billingDAO.PatientDaignosisProceduresMarkAsIncludeInBilling(PatientDiagnosisProcedureIds, UpdatedByUserID, IsIncluded, CodeType);

            }
            catch
            {
                throw;
            }
            return result;
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
            List<PendingBillsDTO> list;

            try
            {
                QuivusLogger.Info("Calling LoadAllPendingBillsByProviderId PARAM :: providerId " + providerId);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                list = billingDAO.LoadAllPendingBillsByProviderId(providerId, clinincBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
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
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling VisitClaimCreated VisitId :: " + VisitId + " ClaimCreatedByUserID " + "OperationDate" + OperationDate);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                result = billingDAO.VisitClaimCreated(VisitId, ClaimCreatedByUserID, OperationDate, encounterTypeId,placeOfServiceTypeId);

            }
            catch
            {
                throw;
            }
            return result;
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
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling VisitClaimListCreated VisitIds :: " + VisitIds + " ClaimCreatedByUserID " + "OperationDate" + OperationDate);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                result = billingDAO.VisitClaimListCreated(VisitIds, ClaimCreatedByUserID, OperationDate);

            }
            catch
            {
                throw;
            }
            return result;
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
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling PatientProceduresMarkIncludedInBilling PatientProcedureIds :: " + PatientProcedureIds + " UpdatedByUserID " + UpdatedByUserID);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                result = billingDAO.PatientProceduresMarkIncludedInBilling(PatientProcedureIds, UpdatedByUserID, IsIncluded);

            }
            catch
            {
                throw;
            }
            return result;
        }
       */
    }
}
