using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.TypeDTOs.BillingTypeDTOs;
using Quivus.Utility.Logger;
using Quivus.Billing.DataAccess;
using Quivus.Utility.Factory;
using Quivus.Billing.DataAccess.DAO;
using QuivusModel.Billing.PendingBills;

namespace Quivus.Billing.Business.Common
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date: 12/27/2011
    /// List all the static data related to Billing Service
    /// </summary>
    public class BillingStaticDataManager
    {
        private BillingDAO _BillingDAO;


        /// <summary>
        /// Author: Shahid Munir
        /// Date: 03/06/2012  
        /// to get all the E/M Code types
        /// </summary>
        /// <returns></returns>
        public FrequencyCodeTypeResultDTO LoadFrequencyCodeTypes()
        {
            FrequencyCodeTypeResultDTO response = new FrequencyCodeTypeResultDTO();

            try
            {
                QuivusLogger.Info("LoadFrequencyCodeTypes Called ");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                response.FrequencyCodeTypeList = _BillingDAO.GetAllClaimFrequencyCodeTypes();
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 03/06/2012  
        /// to get all the E/M Code types
        /// </summary>
        /// <returns></returns>
        public ENMCodeTypeResultDTO LoadENMCodeTypes()
        {
            ENMCodeTypeResultDTO response = new ENMCodeTypeResultDTO();

            try
            {
                QuivusLogger.Info("LoadENMCodeTypes Called ");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                response.ENMCodeTypeList = _BillingDAO.LoadENMCodeTypes();
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
        /// to get all payment soyrce types
        /// </summary>
        /// <returns></returns>
        public List<PaymentSourceTypeDTO> GetAllPaymentSourceTypes()
        {
            List<PaymentSourceTypeDTO> List = null;
            try
            {
                QuivusLogger.Info("Getting PaymentSourceTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllPaymentSourceTypes();
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
            try
            {
                QuivusLogger.Info("Getting PaymentTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllPaymentTypes();
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
            try
            {
                QuivusLogger.Info("Getting AcceptAssignmentTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllAcceptAssignmentTypes();
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
            try
            {
                QuivusLogger.Info("Getting AdjustmentGroupCodeTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllAdjustmentGroupCodeTypes();
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
            try
            {
                QuivusLogger.Info("Getting BillingModeTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllBillingModeTypes();
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
            try
            {
                QuivusLogger.Info("Getting BillingStatusTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllBillingStatusTypes();
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
            try
            {
                QuivusLogger.Info("Getting ClaimFrequencyCodeTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllClaimFrequencyCodeTypes();
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
            try
            {
                QuivusLogger.Info("Getting ClaimStatusCategoryTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllClaimStatusCategoryTypes();
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
            try
            {
                QuivusLogger.Info("Getting ClaimStatusCodeTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllClaimStatusCodeTypes();
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
            try
            {
                QuivusLogger.Info("Getting CMSSecondaryTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllCMSSecondaryTypes();
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
            try
            {
                QuivusLogger.Info("Getting CommunicationTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllCommunicationTypes();
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
            try
            {
                QuivusLogger.Info("Getting DelayReasonCodeTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllDelayReasonCodeTypes();
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
            try
            {
                QuivusLogger.Info("Getting EDIReceiverTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllEDIReceiverTypes();
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
            try
            {
                QuivusLogger.Info("Getting EnrollmentTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllEnrollmentTypes();
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
            try
            {
                QuivusLogger.Info("Getting HospiceEmployedTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllHospiceEmployedTypes();
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
            try
            {
                QuivusLogger.Info("Getting MediaTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllMediaTypes();
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
            try
            {
                QuivusLogger.Info("Getting PaymentClaimAmountQualifierTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllPaymentClaimAmountQualifierTypes();
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
            try
            {
                QuivusLogger.Info("Getting PaymentLevelTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllPaymentLevelTypes();
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
            try
            {
                QuivusLogger.Info("Getting PaymentMethodCodeTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllPaymentMethodCodeTypes();
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
            try
            {
                QuivusLogger.Info("Getting PaymentProcedureAmountQualifierTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllPaymentProcedureAmountQualifierTypes();
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
            try
            {
                QuivusLogger.Info("Getting PaymentProcdureQualifierTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllPaymentProcdureQualifierTypes();
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
            try
            {
                QuivusLogger.Info("Getting ProviderAdjustmentTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllProviderAdjustmentTypes();
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
            try
            {
                QuivusLogger.Info("Getting StatementTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllStatementTypes();
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
            try
            {
                QuivusLogger.Info("Getting TransactionHandlingCodeTypes");

                if (_BillingDAO == null)
                {
                    _BillingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }

                List = _BillingDAO.GetAllTransactionHandlingCodeTypes();
            }
            catch
            {
                throw;
            }
            return List;
        }
    }
}
