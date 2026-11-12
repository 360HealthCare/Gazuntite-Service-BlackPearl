using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QuivusModel.Billing.Claim;
using Quivus.Billing.Business.Claim;
using Quivus.Utility.CustomException;
using Quivus.Utility.Logger;
using QuivusUtility.Utilities;
using Quivus.Billing.Business.Common;
using QuivusModel.TypeDTOs.BillingTypeDTOs;
using QuivusModel.Billing.EDIPaymentTransaction;
using QuivusBilling.ServiceResponse.Visit;
using Quivus.Billing.Business.VisiBL;
using Quivus.Billing.Business.VisiBL.Impl;
using System.Xml;
using QuivusModel.Billing.PendingBills;

namespace QuivusBillingService
{
    
    public class BillingService : IBillingService
    {
        IBillingManager billingManagerImpl = null;

        BillingStaticDataManager objBillingSDManager = null;

        IVisitBL visitBLImpl = null;

        /// <summary>
       /// Author:  Shahid Munir
       /// Date:    01/10/2011
       /// to save super bill
       /// </summary>
       /// <param name="superBillXml"></param>        
       /// <returns></returns>
        public bool SaveSuperBill(XmlDocument superBillXml)
        {
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.SaveSuperBill(superBillXml);
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
            return Result;
        }

        /// <summary>
       /// Author: Shahid Munir
       /// Date: 12/27/2011  
       /// to get all the E/M Code types
       /// </summary>
       /// <returns></returns>
        public ENMCodeTypeResultDTO LoadENMCodeTypes()
        {
            ENMCodeTypeResultDTO response = null;

            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }               

                response = objBillingSDManager.LoadENMCodeTypes();
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
        /// Date: 12/27/2011  
        /// to get all the encounter types
        /// </summary>
        /// <returns></returns>
        public EncounterTypeResultDTO LoadEncounterTypes()
        {
            EncounterTypeResultDTO response = null;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                response = billingManagerImpl.LoadEncounterTypes();
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
        /// Date: 12/27/2011  
        /// to get all place of service types
        /// </summary>
        /// <returns></returns>
        public PlaceOfServiceResultDTO LoadPlaceOfServiceTypeByName(string typrName)
        {
            PlaceOfServiceResultDTO response = null;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                response = billingManagerImpl.LoadPlaceOfServiceTypeByName(typrName);
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
        /// Date:   01/04/2011
        /// to get ICD codes for super bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public ICDCodeResultDTO LoadICDCodeByName(string queryString)
        {
            ICDCodeResultDTO response = null;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                response = billingManagerImpl.LoadICDCodeByName(queryString);
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
        /// Date:   01/04/2011
        /// to get procedure modifier types
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public ProcedureModifierTypeResultDTO LoadProcedureModifierTypes(String ModifierCode)
        {
            ProcedureModifierTypeResultDTO response = null;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                response = billingManagerImpl.LoadProcedureModifierTypes(ModifierCode);
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
       /// Author:  Shahid Munir
       /// Date:    01/10/2011
       /// to add Claim transactions to a claim
       /// </summary>
       /// <param name="claimTransactionIdList"></param>        
       /// <returns></returns>
        public bool ManagePendingBillProcedures(XmlDocument ProcedureXml)
        {
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManagePendingBillProcedures(ProcedureXml);
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
            return Result;
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
            CPTMedicalTypeResultDTO response = null;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                response = billingManagerImpl.LoadCPTMedicalTypes(CPTName);
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
        /// Date:   01/04/2011
        /// to get signed visits data for which claims not created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public QuivusModel.Billing.PendingBills.PendingBillsResultDTO LoadAllPendingBillsByProviderId(string providerId, string clinincBranchId)
        {
            QuivusModel.Billing.PendingBills.PendingBillsResultDTO response = null;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                response = billingManagerImpl.LoadAllPendingBillsByProviderId(int.Parse(providerId),int.Parse(clinincBranchId));
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
        /// Date:   01/04/2011
        /// to create Claim from visit id 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public bool CreateClaim(string VisitId, string managedByUserId)
        {
            bool Result = false;
            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                //Result = billingManagerImpl.CreateClaim(int.Parse(VisitId), int.Parse(managedByUserId));
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

            return Result;
        }

       // /// <summary>
       ///// Author: Shahid Munir
       ///// Date:   01/04/2011
       ///// to create Claims from visit id list 
       ///// </summary>
       ///// <param name="VisitIdList"></param>
       ///// <returns></returns>
        //public bool CreateClaim(List<string> VisitIdList, string managedByUserId)
        //{
        //    List<int> VisitList= new List<int>();

        //    foreach (string item in VisitIdList)
        //    {
        //        VisitList.Add(int.Parse(item));
        //    }

        //    bool Result = false;
        //    try
        //    {
        //        if (billingManagerImpl == null)
        //        {
        //            billingManagerImpl = new BillingManagerImpl();
        //        }

        //        Result = billingManagerImpl.CreateClaim(VisitList, int.Parse(managedByUserId));
        //    }
        //    catch (InvalidArgumentException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp, exp.ErrorDetail);
        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }

        //    return Result;
        //}

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to get already created claims of Provider
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns>list of providers's created claims</returns>
        public ClaimSearchResultDTO LoadClaimsByProviderId(string providerId)
        {
            ClaimSearchResultDTO response = new ClaimSearchResultDTO();
            
            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl =new BillingManagerImpl();
                }

                response.ClaimSearchList = billingManagerImpl.LoadClaimsByProviderId(int.Parse(providerId));
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
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim data 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public ClaimDTO LoadClaimByClaimId(string claimId)
        {
            ClaimDTO ClaimDTO = null;
            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                ClaimDTO = billingManagerImpl.LoadClaimByClaimId(int.Parse(claimId));
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

            return ClaimDTO;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim insurance data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public List<ClaimInsuranceDTO> LoadClaimInsurancesByClaimId(string claimId)
        {
            List<ClaimInsuranceDTO> ClaimsInsuranceList = null;
            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                ClaimsInsuranceList = billingManagerImpl.LoadClaimInsurancesByClaimId(int.Parse(claimId));
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

            return ClaimsInsuranceList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/20/2011
        /// to manage claim data 
        /// </summary>
        /// <param name="claimList"></param>
        /// <returns></returns>
        public bool ManageClaim(List<ClaimDTO> claimList)
        {
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl =new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManageClaim(claimList);
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
           
            return Result;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert claim information
        /// </summary>
        /// <param name="claimDTO"></param>
        /// <returns>Current ClaimID</returns>
        public int InsertClaim(ClaimDTO claimInsertDTO)
        {
            int Result = 0;
           
            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl =new BillingManagerImpl();
                }

                Result = billingManagerImpl.InsertClaim(claimInsertDTO);
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
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManageClaimInsurance(claimInsuranceList);
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
            return Result;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/26/2011
        /// to insert claim insurance information
        /// </summary>
        /// <param name="claimInsuranceDTO"></param>
        /// <returns>Current ClaimInsuranceID</returns>       
        public int InsertClaimInsurance(ClaimInsuranceDTO claimInsuranceDTO)
        {
            int Result = 0;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl =new BillingManagerImpl();
                }

                Result = billingManagerImpl.InsertClaimInsurance(claimInsuranceDTO);
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
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl =new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManageClaimBillingStatus(claimBillingStatusList);
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
            return Result;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/26/2011
        /// to insert claim billing status information
        /// </summary>
        /// <param name="claimBillingStatusDTO"></param>
        /// <returns>Current ClaimBillingStatusID</returns>       
        public int InsertClaimBillingStatus(ClaimBillingStatusDTO claimBillingStatusDTO)
        {
            int Result = 0;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl =new BillingManagerImpl();
                }

                Result = billingManagerImpl.InsertClaimBillingStatus(claimBillingStatusDTO);
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
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl =new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManageClaimTransaction(claimTransactionList);
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
            return Result;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to split Claim transactions to new claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public bool SplitClaimTransactions(string claimTransactionIdList, string claimId, string managedByUserId)
        {
            bool Result = false;

            List<int> TransactionIdList = new List<int>();

            if (claimTransactionIdList.Contains(","))
            {
                string[] tList = claimTransactionIdList.Split(',');

                foreach (string item in tList)
                {
                    TransactionIdList.Add(int.Parse(item));
                }
            }
            else
            {
                TransactionIdList.Add(int.Parse(claimTransactionIdList.Trim()));
            }
            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.SplitClaimTransactions(TransactionIdList, int.Parse(claimId), int.Parse(managedByUserId));
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
            return Result;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to remove Claim transactions to from claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>        
        /// <returns></returns>
        public bool RemoveClaimTransactions(string claimTransactionIdList, string managedByUserId)
        {
            bool Result = false;
                        
            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.RemoveClaimTransactions(claimTransactionIdList, int.Parse(managedByUserId));
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
            return Result;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to add Claim transactions to a claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>        
        /// <returns></returns>
        public bool AddClaimTransactions(XmlDocument TransactionXml, string managedByUserId)
        {
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.AddClaimTransactions(TransactionXml, int.Parse(managedByUserId));
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
            return Result;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/26/2011
        /// to insert claim transaction information
        /// </summary>
        /// <param name="claimTransactionDTO"></param>
        /// <returns>Current ClaimTransactionID</returns>        
        public int InsertClaimTransaction(ClaimTransactionDTO claimTransactionDTO)
        {
            int Result = 0;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl =new BillingManagerImpl();
                }

                Result = billingManagerImpl.InsertClaimTransaction(claimTransactionDTO);
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
            return Result;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/28/2011
        /// to get claim's transactions by claim id
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public ClaimTransactionSearchResultDTO LoadClaimTransactionsByClaimId(string claimId)
        {
            ClaimTransactionSearchResultDTO response = new ClaimTransactionSearchResultDTO();

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                response.ClaimTransactionSearchList = billingManagerImpl.LoadClaimTransactionsByClaimId(int.Parse(claimId));
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
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim transaction data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public ClaimTransactionSearchResultDTO LoadClaimMissingTransactionsByVisitId(string visitId, string claimId, string managedByUserId)
        {
            ClaimTransactionSearchResultDTO response = new ClaimTransactionSearchResultDTO();

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                response.ClaimTransactionSearchList = billingManagerImpl.LoadClaimMissingTransactionsByVisitId(int.Parse(visitId), int.Parse(claimId), int.Parse(managedByUserId));
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
        /// Date:   01/04/2011
        /// to get visit diagnosis
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public string LoadVisitDiagnosisByVisitId(string visitId)
        {
            string response = null;
            try
            {
                QuivusLogger.Info("In QuivusBillingService LoadVisitDiagnosisByVisitId Called");
                QuivusLogger.Info("LoadPatientDiagnosisByVisitId PARAM VisitId::" + visitId);


                if (visitBLImpl == null)
                    {
                        visitBLImpl = new VisitBLImpl();
                    }
                response = visitBLImpl.LoadVisitDiagnosisByVisitId(int.Parse(visitId));
                
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
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim submission batch data 
        /// </summary>
        /// <param name="claimSubmissionBatchDTO"></param>
        /// <returns></returns>
        public int InsertClaimSubmissionBatch(ClaimSubmissionBatchDTO claimSubmissionBatchDTO)
        {
            int Result = 0;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.InsertClaimSubmissionBatch(claimSubmissionBatchDTO);
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
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManageClaimSubmissionBatch(claimSubmissionBatchList);
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
            return Result;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim payments data 
        /// </summary>
        /// <param name="claimPaymentDTO"></param>
        /// <returns></returns>
        public int InsertClaimPayment(ClaimPaymentsDTO InsertClaimPayment)
        {
            int Result = 0;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.InsertClaimPayment(InsertClaimPayment);
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
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManageClaimPayments(claimPaymentsList);
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
            return Result;
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
            int Result = 0;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.InsertClaimAdjustment(claimAdjustmentDTO);
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
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManageClaimAdjustments(claimAdjustmentList);
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
            return Result;
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
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManageAdjustmentMapping(adjustmentMappingList);
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
            return Result;
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
            int Result = 0;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.InsertAdjustmentMapping(adjustmentMappingDTO);
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
            int Result = 0;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.InsertAdjustmentReasonCode(adjustmentReasonCodeDTO);
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
            bool Result = false;

            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                Result = billingManagerImpl.ManageAdjustmentReasonCodes(adjustmentReasonCodeList);
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
            return Result;
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
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }

                AdjustmentReasonCodeList = billingManagerImpl.LoadAdjustmentReasonCodes();
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
            return AdjustmentReasonCodeList;
        }

        #endregion

        #region Population of Type DTOs

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment soyrce types
        /// </summary>
        /// <returns></returns>
        public List<PaymentSourceTypeDTO> GetAllPaymentSourceTypes()
        {
            List<PaymentSourceTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllPaymentSourceTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment types
        /// </summary>
        /// <returns></returns>
        public List<PaymentTypeDTO> GetAllPaymentTypes()
        {
            List<PaymentTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllPaymentTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all accept assigmnet types
        /// </summary>
        /// <returns></returns>
        public List<AcceptAssignmentTypeDTO> GetAllAcceptAssignmentTypes()
        {
            List<AcceptAssignmentTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllAcceptAssignmentTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all adjustment group code types
        /// </summary>
        /// <returns></returns>
        public List<AdjustmentGroupCodeTypeDTO> GetAllAdjustmentGroupCodeTypes()
        {
            List<AdjustmentGroupCodeTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllAdjustmentGroupCodeTypes();
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

            return List;
        }

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all billing mode types
        /// </summary>
        /// <returns></returns>
        public List<BillingModeTypeDTO> GetAllBillingModeTypes()
        {
            List<BillingModeTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllBillingModeTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all billing status types
        /// </summary>
        /// <returns></returns>
        public List<BillingStatusTypeDTO> GetAllBillingStatusTypes()
        {
            List<BillingStatusTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllBillingStatusTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all claim frequency code types
        /// </summary>
        /// <returns></returns>
        public FrequencyCodeTypeResultDTO LoadFrequencyCodeTypes()
        {
            FrequencyCodeTypeResultDTO response = null;

            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                response = objBillingSDManager.LoadFrequencyCodeTypes();
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
        /// Date: 12/27/2011  
        /// to get all claim status category types
        /// </summary>
        /// <returns></returns>
        public List<ClaimStatusCategoryTypeDTO> GetAllClaimStatusCategoryTypes()
        {
            List<ClaimStatusCategoryTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllClaimStatusCategoryTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all claim status code types
        /// </summary>
        /// <returns></returns>
        public List<ClaimStatusCodeTypeDTO> GetAllClaimStatusCodeTypes()
        {
            List<ClaimStatusCodeTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllClaimStatusCodeTypes();
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

            return List;
        }

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all CMS secondary types
        /// </summary>
        /// <returns></returns>
        public List<CMSSecondaryTypeDTO> GetAllCMSSecondaryTypes()
        {
            List<CMSSecondaryTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllCMSSecondaryTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all communication types
        /// </summary>
        /// <returns></returns>
        public List<CommunicationTypeDTO> GetAllCommunicationTypes()
        {
            List<CommunicationTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllCommunicationTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all delay reason code types
        /// </summary>
        /// <returns></returns>
        public List<DelayReasonCodeTypeDTO> GetAllDelayReasonCodeTypes()
        {
            List<DelayReasonCodeTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllDelayReasonCodeTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all EDI receiver types
        /// </summary>
        /// <returns></returns>
        public List<EDIReceiverTypeDTO> GetAllEDIReceiverTypes()
        {
            List<EDIReceiverTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllEDIReceiverTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all enrollment types
        /// </summary>
        /// <returns></returns>
        public List<EnrollmentTypeDTO> GetAllEnrollmentTypes()
        {
            List<EnrollmentTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllEnrollmentTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all hospice employed types
        /// </summary>
        /// <returns></returns>
        public List<HospiceEmployedTypeDTO> GetAllHospiceEmployedTypes()
        {
            List<HospiceEmployedTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllHospiceEmployedTypes();
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

            return List;
        }

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all media types (paper or electronic)
        /// </summary>
        /// <returns></returns>
        public List<MediaTypeDTO> GetAllMediaTypes()
        {
            List<MediaTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllMediaTypes();
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

            return List;
        }

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all claim amount qualifier types
        /// </summary>
        /// <returns></returns>
        public List<PaymentClaimAmountQualifierTypeDTO> GetAllPaymentClaimAmountQualifierTypes()
        {
            List<PaymentClaimAmountQualifierTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllPaymentClaimAmountQualifierTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment level types (procedure or claim)
        /// </summary>
        /// <returns></returns>
        public List<PaymentLevelTypeDTO> GetAllPaymentLevelTypes()
        {
            List<PaymentLevelTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllPaymentLevelTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment method code types
        /// </summary>
        /// <returns></returns>
        public List<PaymentMethodCodeTypeDTO> GetAllPaymentMethodCodeTypes()
        {
            List<PaymentMethodCodeTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllPaymentMethodCodeTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment peocedure amount qualifier types
        /// </summary>
        /// <returns></returns>
        public List<PaymentProcedureAmountQualifierTypeDTO> GetAllPaymentProcedureAmountQualifierTypes()
        {
            List<PaymentProcedureAmountQualifierTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllPaymentProcedureAmountQualifierTypes();
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

            return List;
        }

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment procedure qualifier types
        /// </summary>
        /// <returns></returns>
        public List<PaymentProcdureQualifierTypeDTO> GetAllPaymentProcdureQualifierTypes()
        {
            List<PaymentProcdureQualifierTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllPaymentProcdureQualifierTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all provider adjustment types
        /// </summary>
        /// <returns></returns>
        public List<ProviderAdjustmentTypeDTO> GetAllProviderAdjustmentTypes()
        {
            List<ProviderAdjustmentTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllProviderAdjustmentTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all statement types
        /// </summary>
        /// <returns></returns>
        public List<StatementTypeDTO> GetAllStatementTypes()
        {
            List<StatementTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllStatementTypes();
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

            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all accept assigmnet types
        /// </summary>
        /// <returns></returns>
        public List<TransactionHandlingCodeTypeDTO> GetAllTransactionHandlingCodeTypes()
        {
            List<TransactionHandlingCodeTypeDTO> List = null;
            try
            {
                if (objBillingSDManager == null)
                {
                    objBillingSDManager = new BillingStaticDataManager();
                }

                List = objBillingSDManager.GetAllTransactionHandlingCodeTypes();
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

            return List;
        }

        #endregion
    }
}
