using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Quivus.Billing.Business.UserBL;
using Quivus.Billing.Business.UserBL.Impl;
using Quivus.Billing.Business.VisiBL;
using Quivus.Billing.Business.VisiBL.Impl;
using Quivus.Billing.DataAccess.DAO;
using Quivus.Billing.DataAccess1.DAO.EDI;
using Quivus.Utility.Factory;
using Quivus.Utility.Logger;
using QuivusBilling.IntegratedServices.UserManagerService;
using QuivusBilling.IntegratedServices.VisitService;
using QuivusBilling.ServiceResponse.User;
using QuivusBilling.ServiceResponse.Visit;
using QuivusModel.Billing.Claim;
using QuivusModel.Billing.EDIPaymentTransaction;
using QuivusModel.Billing.PendingBills;
using QuivusModel.Billing.Visit;
using Quivus.Billing.Business.SuperBill;
using Quivus.Billing.Business.SuperBill.Impl;
using QuivusModel.TypeDTOs.BillingTypeDTOs;

namespace Quivus.Billing.Business.Claim
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   12/16/2011
    /// </summary>
    public class BillingManagerImpl : IBillingManager
    {
       private BillingDAO _BillingDAO = null;
       private ClaimDAO _claimDAO = null;
       private EDIPaymentTransactionDAO _EDIPaymentTransactionDAO = null;
       private IVisitBL visitBLImpl = null;
       private IUserBL userBLImpl = null;

       private ISuperBill superBillBLImpl = null;


       /// <summary>
       /// Author:  Shahid Munir
       /// Date:    01/10/2011
       /// to save super bill
       /// </summary>
       /// <param name="superBillXml"></param>        
       /// <returns></returns>
       public bool SaveSuperBill(XmlDocument superBillXml)
       {
           bool UpdateResult = false;

           List<QuivusModel.Billing.PendingBills.PendingBillsDTO> sbDTOList = new List<QuivusModel.Billing.PendingBills.PendingBillsDTO>(); 
          QuivusModel.Billing.PendingBills.PendingBillsDTO sbDTO = new QuivusModel.Billing.PendingBills.PendingBillsDTO();          
          List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO> sbProcedureList = new List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO>();

           XmlDocument doc = new XmlDocument();
           XmlRootAttribute xRoot = new XmlRootAttribute();

           xRoot.ElementName = "PendingBills";
           xRoot.IsNullable = true;

           try
           {
               XmlSerializer serializer = new XmlSerializer(typeof(List<QuivusModel.Billing.PendingBills.PendingBillsDTO>), xRoot);
               string xml = superBillXml.InnerXml;

               sbDTOList = (List<QuivusModel.Billing.PendingBills.PendingBillsDTO>)serializer.Deserialize(new StringReader(xml));

               if (sbDTOList != null && sbDTOList.Count >  0)
               {
                   sbDTO = sbDTOList[0];
                   UpdateResult = CreateClaim(sbDTO);
               }

               

           }
           catch
           {

               throw;
           }
           return UpdateResult;
       }     

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all the encounter types
        /// </summary>
        /// <returns></returns>
       public EncounterTypeResultDTO LoadEncounterTypes()
       {
           EncounterTypeResultDTO response = new EncounterTypeResultDTO();
           List<QuivusBilling.IntegratedServices.VisitService.EncounterTypeDTO> vDTOList = null;

           try
           {
               QuivusLogger.Info("LoadPlaceOfServiceTypeByName Called ");

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               vDTOList = visitBLImpl.LoadEncounterTypes();

               foreach (QuivusBilling.IntegratedServices.VisitService.EncounterTypeDTO cmtDTO in vDTOList)
               {
                   QuivusModel.Billing.Visit.QBEncounterTypeDTO mDTO = new QuivusModel.Billing.Visit.QBEncounterTypeDTO();

                   mDTO.EncounterTypeID = cmtDTO.VisitEncounterTypeID;
                   mDTO.EncounterTypeName = cmtDTO.EncounterTypeName;

                   response.TOSTypeList.Add(mDTO);
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
       /// Date: 12/27/2011  
       /// to get all place of service types
       /// </summary>
       /// <returns></returns>
       public PlaceOfServiceResultDTO LoadPlaceOfServiceTypeByName(string typrName)
       {
           PlaceOfServiceResultDTO response = new PlaceOfServiceResultDTO();
           List<QuivusBilling.IntegratedServices.VisitService.PlaceOfServiceTypeDTO> vDTOList = null;

           try
           {
               QuivusLogger.Info("LoadPlaceOfServiceTypeByName Called PARAM typrName::" + typrName);

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               vDTOList = visitBLImpl.LoadPlaceOfServiceTypeByName(typrName);

               foreach (QuivusBilling.IntegratedServices.VisitService.PlaceOfServiceTypeDTO cmtDTO in vDTOList)
               {
                   QuivusModel.TypeDTOs.BillingTypeDTOs.PlaceOfServiceTypeDTO mDTO = new QuivusModel.TypeDTOs.BillingTypeDTOs.PlaceOfServiceTypeDTO();

                   mDTO.ServiceTypeID = cmtDTO.ServiceTypeID;
                   mDTO.ServiceTypeCode = cmtDTO.ServiceTypeCode;
                   mDTO.POSDescription = cmtDTO.POSDescription;

                   response.POSTypeList.Add(mDTO);
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
        /// to get ICD codes for super bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
       public ICDCodeResultDTO LoadICDCodeByName(string queryString)
       {
           ICDCodeResultDTO response = new ICDCodeResultDTO();
           List<QuivusBilling.IntegratedServices.VisitService.ICDCodeDTO> vDTOList = null;

           try
           {
               QuivusLogger.Info("LoadICDCodeByName Called PARAM queryString::" + queryString);

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               vDTOList = visitBLImpl.LoadICDCodeByName(queryString);

               foreach (QuivusBilling.IntegratedServices.VisitService.ICDCodeDTO cmtDTO in vDTOList)
               {
                   QuivusModel.Billing.Visit.QBICDCodeDTO mDTO = new QuivusModel.Billing.Visit.QBICDCodeDTO();

                   mDTO.ICDCodeID = cmtDTO.IcdCodeIdSeq;
                   mDTO.ICDCodeName = cmtDTO.IcdCodeName;
                   mDTO.ICDCodeDescription = cmtDTO.IcdCodeDescription;

                   response.ICDCodeList.Add(mDTO);
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
        /// to get procedure modifier types
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
       public ProcedureModifierTypeResultDTO LoadProcedureModifierTypes(String ModifierCode)
       {
           ProcedureModifierTypeResultDTO response = new ProcedureModifierTypeResultDTO();
           List<QuivusBilling.IntegratedServices.VisitService.ProcedureModifierTypeDTO> vDTOList = null;

           try
           {
               QuivusLogger.Info("LoadProcedureModifierTypes Called PARAM ModifierCode::" + ModifierCode);

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               vDTOList = visitBLImpl.LoadProcedureModifierTypes(ModifierCode);

               foreach (QuivusBilling.IntegratedServices.VisitService.ProcedureModifierTypeDTO cmtDTO in vDTOList)
               {
                   QuivusModel.Billing.Visit.ProcedureModifierTypeDTO mDTO = new QuivusModel.Billing.Visit.ProcedureModifierTypeDTO();

                   mDTO.ModifierTypeId = cmtDTO.ModifierTypeId;
                   mDTO.ModifierDescription = cmtDTO.ModifierDescription;                   

                   response.ProcedureModifierTypeList.Add(mDTO);
               }
           }
           catch
           {
               throw;
           }
           return response;
       }

       /// <summary>
       /// Author:  Shahid Munir
       /// Date:    01/10/2011
       /// to add Claim transactions to a claim
       /// </summary>
       /// <param name="claimTransactionIdList"></param>        
       /// <returns></returns>
       public bool ManagePendingBillProcedures(XmlDocument ProcedureXml)
       {
           bool UpdateResult = false;

           List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO> procedureList = new List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO>();
           List<QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO> vsisitProcedureList = new List<QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO>();

           XmlDocument doc = new XmlDocument();
           XmlRootAttribute xRoot = new XmlRootAttribute();

           xRoot.ElementName = "ProcedureInfo";
           xRoot.IsNullable = true;
          
           try
           {
           XmlSerializer serializer = new XmlSerializer(typeof(List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO>), xRoot);
           string xml = ProcedureXml.InnerXml;

           procedureList = (List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO>)serializer.Deserialize(new StringReader(xml));

           foreach (QuivusModel.Billing.PendingBills.SuperBillProcedureDTO bDTO in procedureList)
           {
               QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO vDTO = new QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO();
               CPTMedicalTypeDTO TypeDTO = new CPTMedicalTypeDTO();
               TypeDTO.CPTMedicalTypeID = bDTO.CPTMedicalTypeID;
               TypeDTO.ProcedureCode = bDTO.ProcedureCode;
               TypeDTO.ProcedureDescription = bDTO.ProcedureDescription;

               vDTO.CreatedBy = bDTO.CreatedBy;
               vDTO.CreatedAt = DateTime.Now;
               vDTO.ModifiedAt = DateTime.Now;
               vDTO.CPTMedicalType = TypeDTO;
               vDTO.Fee = bDTO.Fee;
               vDTO.Unit = bDTO.Unit;
               vDTO.VisitID = bDTO.VisitID;               
               vDTO.IncludeInBilling = bDTO.IncludeInBilling;
               vDTO.IsCreatedFromSuperBill = bDTO.IsCreatedFromSuperBill;
               vDTO.ModifiedBy = bDTO.ModifiedBy;
               vDTO.Modifier1 = bDTO.Modifier1;
               vDTO.Modifier2 = bDTO.Modifier2;
               vDTO.Modifier3 = bDTO.Modifier3;
               vDTO.Modifier4 = bDTO.Modifier4;
               vDTO.ModifierTypeId1 = bDTO.ModifierTypeId1;
               vDTO.ModifierTypeId2 = bDTO.ModifierTypeId2;
               vDTO.ModifierTypeId3 = bDTO.ModifierTypeId3;
               vDTO.ModifierTypeId4 = bDTO.ModifierTypeId4;
               vDTO.PatientID = bDTO.PatientID;
               vDTO.PatientProcedureID = bDTO.PatientProcedureID;
               vDTO.IsDeleted = bDTO.IsDeleted;

               vsisitProcedureList.Add(vDTO);
           }
           
          
               QuivusLogger.Info("ManagePendingBillProcedures Called");

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               UpdateResult = visitBLImpl.ManageVisitProcedure(vsisitProcedureList);
           }
           catch
           {

               throw;
           }
           return UpdateResult;
       }

       /// <summary>
       /// Author: Shahid Munir
       /// Date:   01/04/2011
       /// to get CPT Medical types
       /// </summary>
       /// <param name="CPTName"></param>
       /// <returns></returns>
       public CPTMedicalTypeResultDTO LoadCPTMedicalTypes(String CPTName)
       {
           CPTMedicalTypeResultDTO response = new CPTMedicalTypeResultDTO();
           List<QuivusBilling.IntegratedServices.VisitService.CPTMedicalTypeDTO> vDTOList = null;

           try
           {
               QuivusLogger.Info("LoadCPTMedicalTypes Called PARAM CPTName::" + CPTName);

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               vDTOList = visitBLImpl.LoadCPTMedicalTypes(CPTName);

               foreach (QuivusBilling.IntegratedServices.VisitService.CPTMedicalTypeDTO cmtDTO in vDTOList)
               {
                   QBCPTMedicalTypeDTO  mDTO= new QBCPTMedicalTypeDTO();

                   mDTO.CPTMedicalTypeID = cmtDTO.CPTMedicalTypeID;
                   mDTO.ProcedureCode = cmtDTO.ProcedureCode;
                   mDTO.ProcedureDescription = cmtDTO.ProcedureDescription;
                   
                   response.CPTMedicalTypeList.Add(mDTO);
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
       /// to get visit procedures for super ill
       /// </summary>
       /// <param name="visitId"></param>
       /// <returns></returns>
       public List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO> LoadProceuresByVisitId(int visitId)
       {
           List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO> procedureList = null;
           VisitProcedureResponse visitProcedureResponse = null;
           try
           {
               QuivusLogger.Info("LoadProceuresByVisitId Called PARAM claimId::" + visitId );

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               visitProcedureResponse = visitBLImpl.LoadVisitProceduresByVisitId(visitId);

               if (visitProcedureResponse != null)
               {
                   procedureList = new List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO>();

                   foreach (QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO cpt in visitProcedureResponse.VisitProcedureList)
                   {
                       QuivusModel.Billing.PendingBills.SuperBillProcedureDTO sbDTO = new QuivusModel.Billing.PendingBills.SuperBillProcedureDTO();

                       sbDTO.PatientID = cpt.PatientID;
                       sbDTO.PatientProcedureID = cpt.PatientProcedureID;
                       sbDTO.CPTMedicalTypeID = cpt.CPTMedicalType.CPTMedicalTypeID;
                       sbDTO.ProcedureCode = cpt.CPTMedicalType.ProcedureCode;
                       sbDTO.VisitDate = cpt.VisitDate.ToShortDateString();                       
                       sbDTO.ProcedureDescription = cpt.CPTMedicalType.ProcedureDescription;
                       sbDTO.VisitID = cpt.VisitID;
                       sbDTO.ModifiedBy = 0;
                       sbDTO.Fee = cpt.Fee;
                       sbDTO.IncludeInBilling = true;
                       sbDTO.IsCreatedFromSuperBill = false;
                       sbDTO.Modifier1 = cpt.Modifier1;
                       sbDTO.Modifier2 = cpt.Modifier2;
                       sbDTO.Modifier3 = cpt.Modifier3;
                       sbDTO.Modifier4 = cpt.Modifier4;                       
                       sbDTO.Unit = cpt.Unit;

                       procedureList.Add(sbDTO);

                   }
               }


           }
           catch
           {

               throw;
           }
           return procedureList;
       }

       /// <summary>
       /// Author: Shahid Munir
       /// Date:   01/04/2011
       /// to get visit procedures for super ill
       /// </summary>
       /// <param name="visitId"></param>
       /// <returns></returns>
       public List<QuivusModel.Billing.PendingBills.SuperBillDiagnosisDTO> LoadDiagnosisByVisitId(int visitId)
       {
           List<QuivusModel.Billing.PendingBills.SuperBillDiagnosisDTO> diagnosisList = null;
           List<PatientDiagnosisDTO> visitDiagnosisList = null;
           try
           {
               QuivusLogger.Info("LoadProceuresByVisitId Called PARAM claimId::" + visitId);

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               visitDiagnosisList = visitBLImpl.LoadVisitDiagnosis(visitId);

               if (visitDiagnosisList != null)
               {
                   diagnosisList = new List<QuivusModel.Billing.PendingBills.SuperBillDiagnosisDTO>();

                   foreach (PatientDiagnosisDTO DX in visitDiagnosisList)
                   {
                       QuivusModel.Billing.PendingBills.SuperBillDiagnosisDTO sbDTO = new QuivusModel.Billing.PendingBills.SuperBillDiagnosisDTO();

                       sbDTO.PatientDiagnosisID = DX.PatientDiagnosisID;
                       sbDTO.ICDCodeDescription = DX.ICDCode.IcdCodeDescription;
                       sbDTO.ICDCodeName = DX.ICDCode.IcdCodeName;
                       sbDTO.VisitDate = DX.VisitDate;
                       sbDTO.VisitID = DX.VisitID;
                       sbDTO.PatientID = DX.PatientID;
                       sbDTO.ICDCodeID = DX.ICDCode.IcdCodeIdSeq;
                       sbDTO.IncludeInBilling = true;
                       sbDTO.IsCreatedFromSuperBill = false;
                       diagnosisList.Add(sbDTO);

                   }
               }


           }
           catch
           {

               throw;
           }
           return diagnosisList;
       }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get signed visits data for which claims not created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
       public PendingBillsResultDTO LoadAllPendingBillsByProviderId(int providerId, int clinincBranchId)
       {
           PendingBillsResultDTO response = new PendingBillsResultDTO();
           List<QuivusBilling.IntegratedServices.VisitService.PendingBillsDTO> vDTOList = null;
           
           try
           {
               QuivusLogger.Info("LoadAllPendingBillsByProviderId Called PARAM providerId::" + providerId);

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               vDTOList = visitBLImpl.LoadAllPendingBillsByProviderId(providerId,clinincBranchId);

               PatientQuivusIdResponse pq = null;
               UserBasicInfoResponse ubi = null;
               string providerName = "";

               List<int> PatientIdList = new List<int>();

               foreach (QuivusBilling.IntegratedServices.VisitService.PendingBillsDTO pbDTO in vDTOList)
               {
                   PatientIdList.Add(pbDTO.PatientId);
               }

               List<QuivusBilling.IntegratedServices.UserManagerService.SuperBillPatientInfoDTO> PatientInfoList = null;

               List<QuivusModel.Billing.PendingBills.SuperBillProcedureDTO> ProcedureInfoList = null;
               List<QuivusModel.Billing.PendingBills.SuperBillDiagnosisDTO> DiagnosisInfoList = null;

               foreach (QuivusBilling.IntegratedServices.VisitService.PendingBillsDTO pbDTO in vDTOList)
               {
                   QuivusModel.Billing.PendingBills.PendingBillsDTO mDTO = new QuivusModel.Billing.PendingBills.PendingBillsDTO();

                   mDTO.VisitId = pbDTO.VisitId;
                   mDTO.PatientId = pbDTO.PatientId;
                   mDTO.VisitDate = pbDTO.VisitDate;
                   mDTO.ProviderId = pbDTO.ProviderId;
                   mDTO.BillPayment = pbDTO.BillPayment;
                   mDTO.EncounterTypeID = pbDTO.EncounterTypeID;
                   mDTO.ManagedByUserID = 0;
                  
                   if (superBillBLImpl == null)
                   {
                       superBillBLImpl = new SuperBillImpl();
                   }

                   QuivusLogger.Info("LoadENMCodeTypes Called ");

                   if (_BillingDAO == null)
                   {
                       _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                   }       
          


                   //for E/M Info
                   QuivusBilling.IntegratedServices.VisitService.SuperBillHEDLevelDTO visitHEDData = visitBLImpl.LoadVisitHEDLevelCounts(pbDTO.VisitId);
                   mDTO.HistoryLevelScale = superBillBLImpl.CalculateHistoryLevel(visitHEDData);
                   mDTO.ExaminationLevelScale = superBillBLImpl.CalculateExaminationLevel(visitHEDData);
                   mDTO.DecisionLevelScale = superBillBLImpl.CalculateDecisionComplexityLevel(mDTO.HistoryLevelScale, mDTO.ExaminationLevelScale, visitHEDData.IsNewEncounter);

                   String ENMSuggestedCode = superBillBLImpl.CalculateENMCode((visitHEDData.IsNewEncounter) ? 1 : 2, mDTO.HistoryLevelScale, mDTO.ExaminationLevelScale, mDTO.DecisionLevelScale);

                   List<QuivusModel.Billing.PendingBills.SuperBillENMServiceDTO> ENMServiceCodeList = new List<QuivusModel.Billing.PendingBills.SuperBillENMServiceDTO>();
                   QuivusModel.Billing.PendingBills.SuperBillENMServiceDTO enmDTO = new QuivusModel.Billing.PendingBills.SuperBillENMServiceDTO();
                   enmDTO.ClaimID = 0;
                   enmDTO.ENMCodeTypeID = 0;
                   enmDTO.ENMServiceID = 0;
                   enmDTO.Fee = 0;
                   enmDTO.EncounterType = (visitHEDData.IsNewEncounter) ? 1 : 2;
                   enmDTO.CodeType = 1;

                   if (ENMSuggestedCode != string.Empty)
                   {
                       List<ENMCodeTypeDTO> ENMCodeList = _BillingDAO.LoadENMCodeTypes();
                       
                       foreach (ENMCodeTypeDTO enm in ENMCodeList)
                       {
                           if (enm.ENMProcCode == ENMSuggestedCode)
                           {                               
                               enmDTO.ENMCodeTypeID = enm.ENMCodeTypeID;                              
                               enmDTO.Fee = enm.Price;                               
                               break;
                           }                           
                       }                       
                   }
                   ENMServiceCodeList.Add(enmDTO);
                   mDTO.ENMServiceInfo = ENMServiceCodeList;
                   
                   List<int> ilist = new List<int>();

                   if (userBLImpl == null)
                   {
                       userBLImpl = new UserBLImpl();
                   }

                   if (providerName == "")
                   {
                       ilist.Clear();
                       ilist.Add(pbDTO.ProviderId);
                       ubi = userBLImpl.GetUserBasicInfoById(ilist);
                       if (ubi != null && ubi.UserBasicInfoList.Count > 0)
                       {
                           providerName = ubi.UserBasicInfoList[0].LastName + ", " + ubi.UserBasicInfoList[0].FirstName;
                       }

                       PatientInfoList = userBLImpl.GetPatientBillingInfoByPatientId(PatientIdList);
                   }

                   if (PatientInfoList != null && PatientInfoList.Count> 0)
                   {
                       foreach (QuivusBilling.IntegratedServices.UserManagerService.SuperBillPatientInfoDTO umDTO in PatientInfoList)
                       {
                           if (umDTO.PatientId == pbDTO.PatientId)
                           {
                               mDTO.Patient = umDTO.Patient;
                               mDTO.QuivusId = umDTO.QuivusID;
                               mDTO.PatientUserId = umDTO.UserId;

                               QuivusModel.Billing.PendingBills.SuperBillPatientInfoDTO mpbDTO = new QuivusModel.Billing.PendingBills.SuperBillPatientInfoDTO();

                               mpbDTO.DateOfBirth = umDTO.DateOfBirth;
                               mpbDTO.Patient = umDTO.Patient;
                               mpbDTO.PatientId = umDTO.PatientId;
                               mpbDTO.SSN = umDTO.SSN;
                               mpbDTO.QuivusID = umDTO.QuivusID;
                               mpbDTO.Gender = umDTO.Gender;
                               mpbDTO.Phone = umDTO.Phone;
                               mpbDTO.PreferredLocation = umDTO.PreferredLocation;
                               mpbDTO.UserId = umDTO.UserId;
                              
                               if(umDTO.InsuranceInfo != null && umDTO.InsuranceInfo.Count > 0)
                               {
                                   List<QuivusModel.Billing.PendingBills.SuperBillPatientInsuranceDTO> insuranceDTOList = new List<QuivusModel.Billing.PendingBills.SuperBillPatientInsuranceDTO>();
                                   foreach(QuivusBilling.IntegratedServices.UserManagerService.SuperBillPatientInsuranceDTO umInsDTO in umDTO.InsuranceInfo)
                                   {
                                       QuivusModel.Billing.PendingBills.SuperBillPatientInsuranceDTO insuranceDTO = new QuivusModel.Billing.PendingBills.SuperBillPatientInsuranceDTO();

                                       if (umInsDTO.InsuranceTypeName == "Primary")
                                           mDTO.PriInsOrganizationID = umInsDTO.InsOrganizationID;
                                       else if (umInsDTO.InsuranceTypeName == "Secondary")
                                           mDTO.SecInsOrganizationID = umInsDTO.InsOrganizationID;
                                       else if (umInsDTO.InsuranceTypeName == "Tertiary")
                                           mDTO.TerInsOrganizationID = umInsDTO.InsOrganizationID;
                                       insuranceDTO.InsuranceName = umInsDTO.InsuranceName;
                                       insuranceDTO.InsurancePlan = umInsDTO.InsurancePlan;
                                       insuranceDTO.InsuranceTypeName = umInsDTO.InsuranceTypeName;
                                       insuranceDTO.IsEligible = umInsDTO.IsEligible;
                                       insuranceDTO.PolicyNumber = umInsDTO.PolicyNumber;
                                       insuranceDTO.ExpiryDate = umInsDTO.ExpiryDate;
                                       insuranceDTO.Copay = umInsDTO.Copay;

                                       insuranceDTOList.Add(insuranceDTO);
                                   }

                                   mpbDTO.InsuranceInfo = insuranceDTOList;
                                  
                               }

                               mDTO.PatientInfo = mpbDTO;

                               break;
                           }
                       }
                   }
                   mDTO.Provider = providerName;
                  
                       
                   ProcedureInfoList = LoadProceuresByVisitId(pbDTO.VisitId);
                  

                   if (ProcedureInfoList != null && ProcedureInfoList.Count > 0)
                   {
                       mDTO.ProcedureInfo = ProcedureInfoList;
                   }

                   DiagnosisInfoList = LoadDiagnosisByVisitId(pbDTO.VisitId);

                   if (DiagnosisInfoList != null && DiagnosisInfoList.Count > 0)
                   {
                       mDTO.DiagnosisInfo = DiagnosisInfoList;
                   }

                   if (_claimDAO == null)
                   {
                       _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                   }

                   List<QuivusModel.Billing.PendingBills.SuperBillWorkRelatedServicesDTO> WRSList = _claimDAO.LoadWRSByClaimId(0);

                   if (WRSList != null && WRSList.Count > 0)
                   {
                       mDTO.WRSInfo = WRSList;
                   }

                   response.PendingBillsList.Add(mDTO);
               }

               
           }
           catch
           {

               throw;
           }
           return response;
       }

       public bool CreateClaim(int VisitId, int managedByUserId, int encounterTypeId, int placeOfServiceTypeId)
       {
           return false;
       }

       /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to create Claim from visit id 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>      
       public bool CreateClaim(QuivusModel.Billing.PendingBills.PendingBillsDTO pendingBill)
       {
           bool Result = false;
           VisitResponse visitResponse = null;
           ClaimDTO claim = null;
           int ClaimId = 0;
           VisitProcedureResponse visitTransactionResponse = null;
           List<ClaimTransactionDTO> claimTransactionList = null;
           //VisitInsuranceResponse visitInsuranceResponse = null; //to be implemented
           //List<ClaimInsuranceDTO> claimInsuranceList = null;
           //List<ClaimBillingStatusDTO> claimBillingStatusList = null;  

           List<QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO> vsisitProcedureList = new List<QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO>();
                      
           try
           {
               int managedByUserId = pendingBill.ManagedByUserID;

               bool CPTResult = false;
               decimal CPTsAmount = 0;
               if (pendingBill.ProcedureInfo.Count > 0)
               {
                   foreach (QuivusModel.Billing.PendingBills.SuperBillProcedureDTO bDTO in pendingBill.ProcedureInfo)
                   {
                       QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO vDTO = new QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO();
                       CPTMedicalTypeDTO TypeDTO = new CPTMedicalTypeDTO();
                       TypeDTO.CPTMedicalTypeID = bDTO.CPTMedicalTypeID;
                       TypeDTO.ProcedureCode = bDTO.ProcedureCode;
                       TypeDTO.ProcedureDescription = bDTO.ProcedureDescription;

                       vDTO.CreatedBy = bDTO.CreatedBy;
                       vDTO.CreatedAt = DateTime.Now;
                       vDTO.ModifiedAt = DateTime.Now;
                       vDTO.CPTMedicalType = TypeDTO;
                       vDTO.PatientID = pendingBill.ProviderId;
                       vDTO.Fee = bDTO.Fee;
                       vDTO.Unit = bDTO.Unit;
                       vDTO.VisitID = pendingBill.VisitId;
                       vDTO.IncludeInBilling = bDTO.IncludeInBilling;
                       vDTO.IsCreatedFromSuperBill = bDTO.IsCreatedFromSuperBill;
                       vDTO.ModifiedBy = bDTO.ModifiedBy;
                       vDTO.Modifier1 = bDTO.Modifier1;
                       vDTO.Modifier2 = bDTO.Modifier2;
                       vDTO.Modifier3 = bDTO.Modifier3;
                       vDTO.Modifier4 = bDTO.Modifier4;
                       vDTO.ModifierTypeId1 = bDTO.ModifierTypeId1;
                       vDTO.ModifierTypeId2 = bDTO.ModifierTypeId2;
                       vDTO.ModifierTypeId3 = bDTO.ModifierTypeId3;
                       vDTO.ModifierTypeId4 = bDTO.ModifierTypeId4;
                       vDTO.PatientID = bDTO.PatientID;
                       vDTO.PatientProcedureID = bDTO.PatientProcedureID;
                       vDTO.IsDeleted = bDTO.IsDeleted;

                       vsisitProcedureList.Add(vDTO);

                       CPTsAmount += (bDTO.Fee * bDTO.Unit);
                   }

                   if (visitBLImpl == null)
                   {
                       visitBLImpl = new VisitBLImpl();
                   }

                   CPTResult = visitBLImpl.ManageVisitProcedure(vsisitProcedureList);
               }
                
                bool ICDResult = false;
                if (pendingBill.DiagnosisInfo.Count > 0)
                {

                    List<QuivusBilling.IntegratedServices.VisitService.PatientDiagnosisDTO> dxList = new List<QuivusBilling.IntegratedServices.VisitService.PatientDiagnosisDTO>();

                    foreach (QuivusModel.Billing.PendingBills.SuperBillDiagnosisDTO dxDTO in pendingBill.DiagnosisInfo)
                    {
                        QuivusBilling.IntegratedServices.VisitService.PatientDiagnosisDTO vDiagnosis = new QuivusBilling.IntegratedServices.VisitService.PatientDiagnosisDTO();
                        QuivusBilling.IntegratedServices.VisitService.ICDCodeDTO ICDCode = new QuivusBilling.IntegratedServices.VisitService.ICDCodeDTO();

                        ICDCode.IcdCodeIdSeq = dxDTO.ICDCodeID;
                        ICDCode.IcdCodeDescription = dxDTO.ICDCodeDescription;

                        vDiagnosis.PatientDiagnosisID = dxDTO.PatientDiagnosisID;
                        vDiagnosis.ModifiedBy = dxDTO.ModifiedBy;
                        vDiagnosis.CreatedBy = dxDTO.CreatedBy;
                        vDiagnosis.ICDCode = ICDCode;
                        vDiagnosis.VisitID = pendingBill.VisitId;
                        vDiagnosis.PatientID = pendingBill.ProviderId;
                        vDiagnosis.IsDeleted = false;
                        vDiagnosis.IsActive = true;

                        dxList.Add(vDiagnosis);
                    }

                    if (visitBLImpl == null)
                    {
                        visitBLImpl = new VisitBLImpl();
                    }

                    ICDResult = visitBLImpl.ManageVisitDiagnosis(dxList);
                    
                }
               QuivusLogger.Info("CreateClaim Called PARAM pendingBill::" + pendingBill);

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl() ;
               }

               visitResponse = visitBLImpl.LoadVisitDataByVisitId(pendingBill.VisitId);

               //if (visitResponse.VisitData.IsClaimCreated == true)
               //{
               //    //message: claim already created
               //    return false;
               //}              

               claim = new ClaimDTO();
               claim.ClaimID = 0;
               claim.PatientID = visitResponse.VisitData.PatientId;
               claim.VisitID = visitResponse.VisitData.VisitId;
               claim.ProviderID = visitResponse.VisitData.SignedByUserId;
               claim.CreatedBy = managedByUserId;
               claim.TotalCharges = CPTsAmount;

               //if (userBLImpl == null)
               //{
               //    userBLImpl = new UserBLImpl();
               //}

               //List<int> PatientIdList = new List<int>();
               //PatientIdList.Add(visitResponse.VisitData.PatientId);

               //PatientQuivusIdResponse pq = userBLImpl.GetQuivusIdsByPatientId(PatientIdList);

               //claim.QuivusID = pq.PatientQuivusIdList[0].QuivusId;
               //claim.PatientUserID = pq.PatientQuivusIdList[0].PatientUserId; 

               claim.QuivusID = pendingBill.QuivusId;
               claim.PatientUserID = pendingBill.PatientUserId; 

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

               ClaimId = _claimDAO.InsertClaim(claim);

               Result = (ClaimId > 0);

               #region Management of inner DTOs

               if (Result)
               {
                   #region saving work related services

                   bool WRSResult = false;
                   if (pendingBill.WRSInfo.Count > 0)
                   {
                       WRSResult = _claimDAO.InsertClaimWRS(pendingBill.WRSInfo, ClaimId);
                   }
                   

                   #endregion

                   #region saving ENM Codes

                    bool ENMResult = false;
                    if (pendingBill.ENMServiceInfo.Count > 0)
                    {
                        ENMResult = _claimDAO.InsertClaimENMCode(pendingBill.ENMServiceInfo, ClaimId);
                    }
                   #endregion

                   #region saving CMS Questions

                   pendingBill.HCFA.ClaimID = ClaimId;

                   bool CMSResult = _claimDAO.InsertClaimCMSQuestions(pendingBill.HCFA);

                   #endregion

                   ClaimBillingStatusDTO billingStatusDTO = new ClaimBillingStatusDTO();

                   billingStatusDTO.ClaimID = ClaimId;
                   billingStatusDTO.BillingStatusTypeID = pendingBill.BillingStatusTypeID;


                   #region Claim Transactions

                   visitTransactionResponse = visitBLImpl.LoadVisitProceduresByVisitId(pendingBill.VisitId);
                   List<int> PatientProcedureIDList = new List<int>();

                   if (visitTransactionResponse != null)
                   {
                       claimTransactionList = new List<ClaimTransactionDTO>();

                      
                       foreach (QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO cpt in visitTransactionResponse.VisitProcedureList)
                       {
                           if (cpt.IncludeInBilling == true)
                           {
                               ClaimTransactionDTO ct = new ClaimTransactionDTO();
                               ct.ClaimID = ClaimId;
                               ct.ID = 0;
                               ct.ProcedureID = cpt.PatientProcedureID;
                               ct.ProcedureCode = cpt.CPTMedicalType.ProcedureCode;
                               ct.DateFrom = Convert.ToDateTime(visitResponse.VisitData.VisitDate.ToShortDateString());
                               ct.DateTo = Convert.ToDateTime(visitResponse.VisitData.VisitDate.ToShortDateString());
                               ct.CreatedBy = managedByUserId;
                               ct.Amount = cpt.Fee;
                               claimTransactionList.Add(ct);

                               PatientProcedureIDList.Add(cpt.PatientProcedureID);
                           }
                       }
                   }

                   if (claimTransactionList != null && claimTransactionList.Count > 0)
                   {
                       if (claimTransactionList.Count == 1)
                       {
                           _claimDAO.InsertClaimTransactions(claimTransactionList[0]);                           
                       }
                       else
                       {
                           _claimDAO.ManageClaimTransactions(claimTransactionList);
                       }

                       visitBLImpl.PatientProceduresMarkIncludedInBilling(PatientProcedureIDList, managedByUserId,true);
                   }

                   #endregion                   

                   #region Claim Insurances

                   //VisitInsuranceResponse = visitBLImpl.LoadVisitInsurancesByVisitId(VisitId);

                   //if (VisitInsuranceResponse != null)
                   //{
                   //    claimInsuranceList = new List<ClaimTransactionDTO>();
                   //    foreach (InsuranceDTO ins in VisitInsuranceResponse.VisitInsuranceList)
                   //    {
                   //        ClaimInsuranceDTO ci = new ClaimInsuranceDTO();
                   //        ci.ClaimID = ClaimId;
                   //        ci.ClaimInsuranceID = 0;                           
                   //        ci.CreatedBy = managedByUserId;
                   //        claimInsuranceList.Add(ci);
                   //    }
                   //}

                   //if (claimInsuranceList != null && claimInsuranceList.Count > 0)
                   //{
                   //    if (claimInsuranceList.Count == 1)
                   //    {
                   //        _claimDAO.InsertClaimInsurance(claimInsuranceList[0]);
                   //    }
                   //    else
                   //    {
                   //        _claimDAO.ManageClaimInsurance(claimInsuranceList);
                   //    }

                   //}

                   ////handling claim billing status according to visit insurance response

                   #endregion

                   bool VisitMarked = visitBLImpl.MarkVisitaAsClaimCreated(pendingBill.VisitId, managedByUserId, DateTime.Now, pendingBill.EncounterTypeID, pendingBill.ServiceTypeID);

               }
               #endregion
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
       /// to create Claims from visit id list 
       /// </summary>
       /// <param name="VisitIdList"></param>
       /// <returns></returns>
       public bool CreateClaim(List<int> VisitIdList, int managedByUserId)
       {
           bool Result = false;
           VisitResponse visitResponse = null;
           List<ClaimDTO> claims = null;
          
           List<ClaimTransactionDTO> claimTransactionList = null;
           //List<ClaimBillingStatusDTO> claimBillingStatusList = null;
           //VisitInsuranceResponse claimInsuranceList = null; //to be implemented

           try
           {
               QuivusLogger.Info("CreateClaim Called PARAM claimIdList::" + VisitIdList);

               if (visitBLImpl == null)
               {
                   visitBLImpl = new VisitBLImpl();
               }

               //visitResponse = visitBLImpl.LoadVisitDataByVisitId(VisitId);

               //claimTransactionList = visitBLImpl.LoadVisitProcedureByVisitId(VisitId)

               // foreach visit
               //claim.ClaimID = 0;
               //claim.PatientID = visitResponse.VisitData.PatientId;
               //claim.VisitID = visitResponse.VisitData.VisitId;
               //claim.ProviderID = visitResponse.VisitData.SignedByUserId;

               //if (claimTransactionList != null && claimTransactionList.Count > 0)
               //{
               //    claim.Transaction = claimTransactionList;
               //}

               //if (claimInsuranceList != null && claimInsuranceList.Count > 0)
               //{
               //    claim.Insurance = claimInsuranceList;
               //}

               //if (claimBillingStatusList != null && claimBillingStatusList.Count > 0)
               //{
               //    claim.BillingStatus = claimBillingStatusList;
               //}

               if (_claimDAO == null)
               {
                   _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
               }

               Result = _claimDAO.ManageClaim(claims);

           }
           catch
           {

               throw;
           }

           return Result;
       }
                

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to get already created claims of Provider
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public List<ClaimSearchDTO> LoadClaimsByProviderId(int providerId)
        {
            List<ClaimSearchDTO> LoadClaimsList = null;
            try
            {
                QuivusLogger.Info("LoadClaimsByProviderId Called PARAM providerId::" + providerId);

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                LoadClaimsList = _claimDAO.LoadClaimsByProviderId(providerId);

                if (userBLImpl == null)
                {
                    userBLImpl = new UserBLImpl();
                }
                
                for (int i = 0; i < LoadClaimsList.Count; i++)
                {
                    List<int> lst = new List<int>();
                    lst.Add(LoadClaimsList[i].ProviderId);
                    UserBasicInfoResponse user= userBLImpl.GetUserBasicInfoById(lst);
                    if (user != null && user.UserBasicInfoList.Count > 0)
                    {
                        LoadClaimsList[i].ProviderName = user.UserBasicInfoList[0].LastName + ", " + user.UserBasicInfoList[0].FirstName;
                    }
                    lst.Clear();
                    lst.Add(LoadClaimsList[i].PatientUserId);
                    user = userBLImpl.GetUserBasicInfoById(lst);
                    if (user != null && user.UserBasicInfoList.Count > 0)
                    {
                        LoadClaimsList[i].PatientName = user.UserBasicInfoList[0].LastName + ", " + user.UserBasicInfoList[0].FirstName;
                    }
                    

                    //LoadClaimsList[i].ClaimTransactionList = _claimDAO.LoadClaimTransactionsByClaimId(LoadClaimsList[i].ClaimID);
                }

                //for (int i = 0; i < LoadClaimsList.Count; i++)
                //{
                //    List<int> lst = new List<int>();
                //    lst.Add(LoadClaimsList[i].PatientUserId);
                //    UserBasicInfoResponse user = userBLImpl.GetUserBasicInfoById(lst);
                //    LoadClaimsList[i].PatientName = user.UserBasicInfoList[0].LastName + ", " + user.UserBasicInfoList[0].FirstName;
                //}

            }
            catch 
            {
                
                throw;
            }
            return LoadClaimsList;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim data 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public ClaimDTO LoadClaimByClaimId(int claimId)
        {
            ClaimDTO claimDTO = null;
            try
            {
                QuivusLogger.Info("LoadClaimByClaimId Called PARAM claimId::" + claimId);

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                claimDTO = _claimDAO.LoadClaimByClaimId(claimId);
            }
            catch
            {

                throw;
            }
            return claimDTO;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim transaction data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public List<ClaimTransactionDTO> LoadClaimMissingTransactionsByVisitId(int visitId, int claimId, int managedByUserId)
        {            
            List<ClaimTransactionDTO> claimTransactionList = null;
            VisitProcedureResponse visitTransactionResponse = null;
            try
            {
                QuivusLogger.Info("LoadClaimMissingTransactionsByVisitId Called PARAM claimId::" + visitId);

                if (visitBLImpl == null)
                {
                    visitBLImpl = new VisitBLImpl();
                }
                
                visitTransactionResponse = visitBLImpl.LoadVisitProceduresByVisitId(visitId);

                if (visitTransactionResponse != null)
                {
                    claimTransactionList = new List<ClaimTransactionDTO>();

                    foreach (QuivusBilling.IntegratedServices.VisitService.QBProcedureDTO cpt in visitTransactionResponse.VisitProcedureList)
                    {
                        if (cpt.IncludeInBilling == false)
                        {
                            ClaimTransactionDTO ct = new ClaimTransactionDTO();
                            ct.ClaimID = claimId;
                            ct.ID = 0;
                            ct.ProcedureID = cpt.PatientProcedureID;
                            ct.ProcedureCode = cpt.CPTMedicalType.ProcedureCode;
                            ct.DateFrom = cpt.VisitDate;
                            ct.DateTo = cpt.VisitDate;
                            ct.CreatedBy = managedByUserId;
                            claimTransactionList.Add(ct);

                        }
                    }
                }


            }
            catch
            {

                throw;
            }
            return claimTransactionList;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim insurance data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public List<ClaimInsuranceDTO> LoadClaimInsurancesByClaimId(int claimId)
        {
            List<ClaimInsuranceDTO> ClaimsInsuranceList = null;
            try
            {
                QuivusLogger.Info("LoadClaimInsurancesByClaimId Called PARAM claimId::" + claimId);

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                ClaimsInsuranceList = _claimDAO.LoadClaimInsurancesByClaimId(claimId);
            }
            catch
            {

                throw;
            }
            return ClaimsInsuranceList;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim transaction data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public List<ClaimTransactionDTO> LoadClaimTransactionsByClaimId(int claimId)
        {
            List<ClaimTransactionDTO> ClaimsTransactionList = null;
            try
            {
                QuivusLogger.Info("LoadClaimTransactionsByClaimId Called PARAM claimId::" + claimId);

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                ClaimsTransactionList = _claimDAO.LoadClaimTransactionsByClaimId(claimId);
            }
            catch
            {

                throw;
            }
            return ClaimsTransactionList;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim transaction data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        /*public List<ClaimTransactionDTO> LoadClaimMissingTransactionsByVisitId(int visitId, int claimId, int managedByUserId)
        {
            VisitResponse visitResponse = null;
            List<ClaimTransactionDTO> claimTransactionList = null;
            VisitProcedureResponse visitTransactionResponse = null;
            try
            {
                QuivusLogger.Info("LoadClaimMissingTransactionsByVisitId Called PARAM claimId::" + visitId);

                if (visitBLImpl == null)
                {
                    visitBLImpl = new VisitBLImpl();
                }

                visitResponse = visitBLImpl.LoadVisitDataByVisitId(visitId);
                visitTransactionResponse = visitBLImpl.LoadVisitProceduresByVisitId(visitId);                

                if (visitTransactionResponse != null)
                {
                    claimTransactionList = new List<ClaimTransactionDTO>();

                    foreach (QBProcedureDTO cpt in visitTransactionResponse.VisitProcedureList)
                    {
                        if (cpt.IncludeInBilling == true && cpt.IsIncludedInBilling == false)
                        {
                            ClaimTransactionDTO ct = new ClaimTransactionDTO();
                            ct.ClaimID = claimId;
                            ct.ID = 0;
                            ct.ProcedureID = cpt.PatientProcedureID;
                            ct.ProcedureCode = cpt.CPTMedicalType.ProcedureCode;
                            ct.DateFrom = Convert.ToDateTime(visitResponse.VisitData.VisitDate.ToShortDateString());
                            ct.DateTo = Convert.ToDateTime(visitResponse.VisitData.VisitDate.ToShortDateString());
                            ct.CreatedBy = managedByUserId;                         
                            claimTransactionList.Add(ct);
                            
                        }
                    }
                }
                
                
            }
            catch
            {

                throw;
            }
            return claimTransactionList;
        }*/

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/20/2011
        /// to manage claim data 
        /// </summary>
        /// <param name="claimList"></param>
        /// <returns></returns>
        public bool ManageClaim(List<ClaimDTO> claimList)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("ManageClaim Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.ManageClaim(claimList);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert claim data 
        /// </summary>
        /// <param name="claimDTO"></param>
        /// <returns></returns>
        public int InsertClaim(ClaimDTO claimDTO)
        {
            int Result;
            try
            {
                QuivusLogger.Info("InsertClaim Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                Result = _claimDAO.InsertClaim(claimDTO);
            }
            catch 
            {
                
                throw;
            }

            return Result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/26/2011
        /// to manage claim billing status data 
        /// </summary>
        /// <param name="claimBillingStatusList"></param>
        /// <returns></returns>
        public bool ManageClaimBillingStatus(List<ClaimBillingStatusDTO> claimBillingStatusList)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("ManageClaimBillingStatus Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.ManageClaimBillingStatus(claimBillingStatusList);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/22/2011
        /// to insert claim's billing status data 
        /// </summary>
        /// <param name="claimBillingStatusInsertDTO"></param>
        /// <returns></returns>
        public int InsertClaimBillingStatus(ClaimBillingStatusDTO claimBillingStatusDTO)
        {
            int Result;
            try
            {
                QuivusLogger.Info("InsertClaimBillingStatus Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                Result = _claimDAO.InsertClaimBillingStatus(claimBillingStatusDTO);
            }
            catch
            {

                throw;
            }

            return Result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/26/2011
        /// to manage claim insurance data 
        /// </summary>
        /// <param name="claimInsuranceList"></param>
        /// <returns></returns>        
        public bool ManageClaimInsurance(List<ClaimInsuranceDTO> claimInsuranceList)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("ManageClaimInsurance Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.ManageClaimInsurance(claimInsuranceList);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/22/2011
        /// to insert claim's insurance data 
        /// </summary>
        /// <param name="claimInsuranceInsertDTO"></param>
        /// <returns></returns>
        public int InsertClaimInsurance(ClaimInsuranceDTO claimInsuranceDTO)
        {
            int Result;
            try
            {
                QuivusLogger.Info("InsertClaimInsurance Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                Result = _claimDAO.InsertClaimInsurance(claimInsuranceDTO);
            }
            catch
            {

                throw;
            }

            return Result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/26/2011
        /// to manage claim transaction data 
        /// </summary>
        /// <param name="claimTransactionList"></param>
        /// <returns></returns>
        public bool ManageClaimTransaction(List<ClaimTransactionDTO> claimTransactionList)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("ManageClaimTransaction Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.ManageClaimTransactions(claimTransactionList);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to split Claim transactions to new claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public bool SplitClaimTransactions(List<int> claimTransactionIdList, int claimId, int managedByUserId)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("SplitClaimTransactions Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.SplitClaimTransactions(claimTransactionIdList, claimId, managedByUserId);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to remove Claim transactions from claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>        
        /// <returns></returns>
        public bool RemoveClaimTransactions(string claimTransactionIdList, int managedByUserId)
        {
            bool UpdateResult = false;
            List<ClaimTransactionDTO> ClaimTransactionList;

            List<int> TransactionIdList = new List<int>();
            List<int> PatientProcedureIdList = new List<int>();

            if (claimTransactionIdList.Contains(","))
            {
                String[] arrclaimTransactionIdList = claimTransactionIdList.Split(',');

                foreach (string item in arrclaimTransactionIdList)
                {
                    TransactionIdList.Add(int.Parse(item.Split('^')[0]));
                    PatientProcedureIdList.Add(int.Parse(item.Split('^')[1]));
                }
            }
            else
            {
                TransactionIdList.Add(int.Parse(claimTransactionIdList.Split('^')[0]));
                PatientProcedureIdList.Add(int.Parse(claimTransactionIdList.Split('^')[1]));
            }

            try
            {
                QuivusLogger.Info("RemoveClaimTransactions Called");
                ClaimTransactionList = new List<ClaimTransactionDTO>();

                foreach (int TransactionId in TransactionIdList)
                {
                    ClaimTransactionDTO ct = new ClaimTransactionDTO();

                    ct.ID = TransactionId;
                    ct.IsDeleted = true;
                    ct.ModifiedBy = managedByUserId;
                    ct.DateFrom = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    ct.DateTo = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    ClaimTransactionList.Add(ct);
                    
                }

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.ManageClaimTransactions(ClaimTransactionList);
                if (UpdateResult == true)
                {
                    if (visitBLImpl == null)
                    {
                        visitBLImpl = new VisitBLImpl();
                    }

                    UpdateResult = visitBLImpl.PatientProceduresMarkIncludedInBilling(PatientProcedureIdList, managedByUserId, false);
                }
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to add Claim transactions to a claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>        
        /// <returns></returns>
        public bool AddClaimTransactions(XmlDocument TransactionXml , int managedByUserId)
        {
            bool UpdateResult = false;

            List<ClaimTransactionDTO> ClaimTransactionList = new List<ClaimTransactionDTO>();

            XmlDocument doc = new XmlDocument();
            XmlRootAttribute xRoot = new XmlRootAttribute();
            
            xRoot.ElementName = "Transactions";            
            xRoot.IsNullable = true;            

            XmlSerializer serializer = new XmlSerializer(typeof(List<ClaimTransactionDTO>),xRoot);
            string xml = TransactionXml.InnerXml;

            ClaimTransactionList = (List<ClaimTransactionDTO>)serializer.Deserialize(new StringReader(xml));
                 
            List<int> PatientProcedureIdList = new List<int>();                        

            try
            {
                QuivusLogger.Info("AddClaimTransactions Called");                

                foreach (ClaimTransactionDTO TransactionDTO in ClaimTransactionList)
                {
                    PatientProcedureIdList.Add(TransactionDTO.ProcedureID);
                }

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.ManageClaimTransactions(ClaimTransactionList);
                if (UpdateResult == true)
                {
                    if (visitBLImpl == null)
                    {
                        visitBLImpl = new VisitBLImpl();
                    }

                    UpdateResult = visitBLImpl.PatientProceduresMarkIncludedInBilling(PatientProcedureIdList, managedByUserId, true);
                }
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/22/2011
        /// to insert claim's transactions data 
        /// </summary>
        /// <param name="claimTransactionInsertDTO"></param>
        /// <returns></returns>
        public int InsertClaimTransaction(ClaimTransactionDTO claimTransactionDTO)
        {
            int Result;
            try
            {
                QuivusLogger.Info("InsertClaimTransactions Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                Result = _claimDAO.InsertClaimTransactions(claimTransactionDTO);
            }
            catch
            {

                throw;
            }

            return Result;
        }

        
        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim submission batch data 
        /// </summary>
        /// <param name="claimSubmissionBatchDTO"></param>
        /// <returns></returns>
        public int InsertClaimSubmissionBatch(ClaimSubmissionBatchDTO claimSubmissionBatchDTO)
        {
            int Result;
            try
            {
                QuivusLogger.Info("InsertClaimSubmissionBatch Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                Result = _claimDAO.InsertClaimSubmissionBatch(claimSubmissionBatchDTO);
            }
            catch
            {

                throw;
            }

            return Result;
        }

         /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim submission batches  
        /// </summary>
        /// <param name="claimSubmissionBatchList"></param>
        /// <returns></returns>
        public bool ManageClaimSubmissionBatch(List<ClaimSubmissionBatchDTO> claimSubmissionBatchList)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("ManageClaimSubmissionBatch Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.ManageClaimSubmissionBatch(claimSubmissionBatchList);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim payments data 
        /// </summary>
        /// <param name="claimPaymentDTO"></param>
        /// <returns></returns>
        public int InsertClaimPayment(ClaimPaymentsDTO claimPaymentDTO)
        {
            int Result;
            try
            {
                QuivusLogger.Info("InsertClaimPayment Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                Result = _claimDAO.InsertClaimPayment(claimPaymentDTO);
            }
            catch
            {

                throw;
            }

            return Result;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim payments  
        /// </summary>
        /// <param name="claimPaymentsList"></param>
        /// <returns></returns>
        public bool ManageClaimPayments(List<ClaimPaymentsDTO> claimPaymentsList)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("ManageClaimPayments Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.ManageClaimPayments(claimPaymentsList);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim payments data 
        /// </summary>
        /// <param name="claimAdjustmentDTO"></param>
        /// <returns></returns>
        public int InsertClaimAdjustment(ClaimAdjustmentDTO claimAdjustmentDTO)
        {
            int Result;
            try
            {
                QuivusLogger.Info("InsertClaimAdjustment Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                Result = _claimDAO.InsertClaimAdjustment(claimAdjustmentDTO);
            }
            catch
            {

                throw;
            }

            return Result;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim adjustments  
        /// </summary>
        /// <param name="claimAdjustmentList"></param>
        /// <returns></returns>
        public bool ManageClaimAdjustments(List<ClaimAdjustmentDTO> claimAdjustmentList)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("ManageClaimAdjustments Called");

                if (_claimDAO == null)
                {
                    _claimDAO = (ClaimDAO)DAOFactory.GetDataAccessObject(typeof(ClaimDAO));
                }

                UpdateResult = _claimDAO.ManageClaimAdjustments(claimAdjustmentList);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        #region EDI Payment Transaction Methods

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/28/2011
        /// to manage Payment adjustment mapping data 
        /// </summary>
        /// <param name="adjustmentMappingList"></param>
        /// <returns></returns>
        public bool ManageAdjustmentMapping(List<AdjustmentMappingDTO> adjustmentMappingList)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("ManageClaimBillingStatus Called");

                if (_EDIPaymentTransactionDAO == null)
                {
                    _EDIPaymentTransactionDAO = (EDIPaymentTransactionDAO)DAOFactory.GetDataAccessObject(typeof(EDIPaymentTransactionDAO));
                }

                UpdateResult = _EDIPaymentTransactionDAO.ManageAdjustmentMapping(adjustmentMappingList);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert claim data 
        /// </summary>
        /// <param name="adjustmentMappingDTO"></param>
        /// <returns></returns>
        public int InsertAdjustmentMapping(AdjustmentMappingDTO adjustmentMappingDTO)
        {
            int Result;
            try
            {
                QuivusLogger.Info("InsertClaimTransactions Called");

                if (_EDIPaymentTransactionDAO == null)
                {
                    _EDIPaymentTransactionDAO = (EDIPaymentTransactionDAO)DAOFactory.GetDataAccessObject(typeof(EDIPaymentTransactionDAO));
                }

                Result = _EDIPaymentTransactionDAO.InsertAdjustmentMapping(adjustmentMappingDTO);

                
            }
            catch
            {

                throw;
            }

            return Result;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeDTO"></param>
        /// <returns></returns>
        public int InsertAdjustmentReasonCode(AdjustmentReasonCodeDTO adjustmentReasonCodeDTO)
        {
            int Result;
            try
            {
                QuivusLogger.Info("InsertClaimTransactions Called");

                if (_EDIPaymentTransactionDAO == null)
                {
                    _EDIPaymentTransactionDAO = (EDIPaymentTransactionDAO)DAOFactory.GetDataAccessObject(typeof(EDIPaymentTransactionDAO));
                }

                Result = _EDIPaymentTransactionDAO.InsertAdjustmentReasonCode(adjustmentReasonCodeDTO);


            }
            catch
            {

                throw;
            }

            return Result;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/28/2011
        /// to Insert/Update/Delete Adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeList"></param>
        /// <returns></returns>
        public bool ManageAdjustmentReasonCodes(List<AdjustmentReasonCodeDTO> adjustmentReasonCodeList)
        {
            bool UpdateResult = false;

            try
            {
                QuivusLogger.Info("ManageClaimBillingStatus Called");

                if (_EDIPaymentTransactionDAO == null)
                {
                    _EDIPaymentTransactionDAO = (EDIPaymentTransactionDAO)DAOFactory.GetDataAccessObject(typeof(EDIPaymentTransactionDAO));
                }

                UpdateResult = _EDIPaymentTransactionDAO.ManageAdjustmentReasonCodes(adjustmentReasonCodeList);
            }
            catch
            {

                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/28/2011
        /// to load all Adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeList"></param>
        /// <returns></returns>
        public List<AdjustmentReasonCodeDTO> LoadAdjustmentReasonCodes()
        {
            List<AdjustmentReasonCodeDTO> AdjustmentReasonCodeList = null;
            try
            {
                QuivusLogger.Info("LoadAdjustmentReasonCodes Called");

                if (_EDIPaymentTransactionDAO == null)
                {
                    _EDIPaymentTransactionDAO = (EDIPaymentTransactionDAO)DAOFactory.GetDataAccessObject(typeof(EDIPaymentTransactionDAO));
                }

                AdjustmentReasonCodeList = _EDIPaymentTransactionDAO.LoadAdjustmentReasonCodes();
            }
            catch
            {

                throw;
            }
            return AdjustmentReasonCodeList;
        }

        #endregion
    }
}
