using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.TypeDTOs.BillingTypeDTOs;
using System.Data;
using QuivusUtility.ProjectConstants;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Quivus.Utility;
using Quivus.Utility.CustomException;

namespace Quivus.Billing.DataAccess.DAO
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date: 12/27/2011   
    /// </summary>
    public class BillingDAO
    {
        /// <summary>
       /// Author: Shahid Munir
       /// Date: 12/27/2011  
       /// to get all the E/M Code types
       /// </summary>
       /// <returns></returns>
        public List<ENMCodeTypeDTO> LoadENMCodeTypes()
        {
            List<ENMCodeTypeDTO> List = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.ENM_CODE_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<ENMCodeTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(ENMCodeTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
            }
            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment source types
        /// </summary>
        /// <returns></returns>
        public List<PaymentSourceTypeDTO> GetAllPaymentSourceTypes()
        {
            List<PaymentSourceTypeDTO> List = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.PAYMENT_SOURCE_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PaymentSourceTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(PaymentSourceTypeDTO));
                    }
                }
            }
            catch
            {
                throw;            
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.PAYMENT_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PaymentTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(PaymentTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.ACCEPT_ASSIGNMENT_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<AcceptAssignmentTypeDTO>();
               
                Database db = DatabaseFactory.CreateDatabase(); 
                DbCommand command = db.GetStoredProcCommand(sProc); 

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(AcceptAssignmentTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.ADJUSTMENT_GROUP_CODE_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<AdjustmentGroupCodeTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(AdjustmentGroupCodeTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.BILLING_MODE_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<BillingModeTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(BillingModeTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.BILLING_STATUS_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<BillingStatusTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(BillingStatusTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
            }
            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all claim frequency code types
        /// </summary>
        /// <returns></returns>
        public List<ClaimFrequencyCodeTypeDTO> GetAllClaimFrequencyCodeTypes()
        {
            List<ClaimFrequencyCodeTypeDTO> List = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.CLAIM_FREQUENCY_CODE_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<ClaimFrequencyCodeTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(ClaimFrequencyCodeTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
            }
            return List;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.CLAIM_STATUS_CATEGORY_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<ClaimStatusCategoryTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(ClaimStatusCategoryTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.CLAIM_STATUS_CODE_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<ClaimStatusCodeTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(ClaimStatusCodeTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.CMS_SECONDARY_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<CMSSecondaryTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(CMSSecondaryTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.COMMUNICATION_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<CommunicationTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(CommunicationTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.DELAY_REASON_CODE_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<DelayReasonCodeTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(DelayReasonCodeTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.EDI_RECEIVER_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<EDIReceiverTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(EDIReceiverTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.ENROLLMENT_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<EnrollmentTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(EnrollmentTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.HOSPICE_EMPLOYED_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<HospiceEmployedTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(HospiceEmployedTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.MEDIA_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<MediaTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(MediaTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.PAYMENT_CLAIM_AMOUNT_QUALIFIER_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PaymentClaimAmountQualifierTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(PaymentClaimAmountQualifierTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.PAYMENT_LEVEL_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PaymentLevelTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(PaymentLevelTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.PAYMENT_METHOD_CODE_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PaymentMethodCodeTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(PaymentMethodCodeTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.PAYMENT_PROCEDURE_AMOUNT_QUALIFIER_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PaymentProcedureAmountQualifierTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(PaymentProcedureAmountQualifierTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.PAYMENT_PROCDURE_QUALIFIER_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PaymentProcdureQualifierTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(PaymentProcdureQualifierTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.PROVIDER_ADJUSTMENT_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<ProviderAdjustmentTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(ProviderAdjustmentTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.STATEMENT_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<StatementTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(StatementTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
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
            IDataReader objIReader = null;
            string sProc = BillingConstant.TRANSACTION_HANDLING_CODE_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<TransactionHandlingCodeTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(TransactionHandlingCodeTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
            }
            return List;
        }
    }
}
