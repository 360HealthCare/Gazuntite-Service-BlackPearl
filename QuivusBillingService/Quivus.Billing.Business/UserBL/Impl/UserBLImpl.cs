using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusBilling.IntegratedServices.UserManagerService;
using QuivusBilling.ServiceResponse.User;
using Quivus.Utility.Logger;
using Quivus.Billing.Business.ReferenceFactory;
using Quivus.Utility;

namespace Quivus.Billing.Business.UserBL.Impl
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   01/05/2011
    /// </summary>
    public class UserBLImpl : IUserBL
    {
        private UserManagerServiceClient clientUser;

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/26/2012
        /// to get Patient info for super bill
        /// </summary>
        /// <param name="PatientIdList"></param>       
        /// <returns></returns>
        public List<SuperBillPatientInfoDTO> GetPatientBillingInfoByPatientId(List<int> PatientIdList)
        {
            List<SuperBillPatientInfoDTO> PatientInfoList = null;

            try
            {
               
                QuivusLogger.Info("Calling GetUserBasicInfoById");

                if (clientUser == null)
                {
                    clientUser = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserManagerService");

                QuivusLogger.Info("Dispatching GetPatientBillingInfoByPatientId Call TO UserManagerService");

                PatientInfoList = clientUser.GetPatientBillingInfoByPatientId(PatientIdList);

                QuivusLogger.Info("Response Recieved  PatientInfoList::" + PatientInfoList);
            }
            catch
            {
                throw;
            }
            return PatientInfoList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/06/2011
        /// to get Users basic info 
        /// </summary>
        /// <param name="UserIdList"></param>
        /// <returns></returns>
        public UserBasicInfoResponse GetUserBasicInfoById(List<int> UserIdList)
        {
            UserBasicInfoResponse response = null;
            List<UserBasicInfoDTO> userBasicInfoList = null;

            try
            {
                QuivusLogger.Info("Calling GetUserBasicInfoById");

                if (clientUser == null)
                {
                    clientUser = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Calling service :: UserManagerService");

                QuivusLogger.Info("Dispatching GetUserBasicInfoById Call TO UserManagerService");

                userBasicInfoList = clientUser.GetUserBasicInfoById(UserIdList);

                QuivusLogger.Info("Response Recieved  UserIdList::" + UserIdList);

                if (response == null)
                {
                    response = new UserBasicInfoResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (userBasicInfoList != null)
                {
                    response.UserBasicInfoList = userBasicInfoList;
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
        /// Date:   01/06/2011
        /// to get quivus ids against patients 
        /// </summary>
        /// <param name="PatientIdList"></param>
        /// <returns></returns>
        public PatientQuivusIdResponse GetQuivusIdsByPatientId(List<int> PatientIdList)
        {
            PatientQuivusIdResponse response = null;
            List<PatientQuivusIDDTO> patientQuivusIdList = null;

            try
            {
                QuivusLogger.Info("Calling GetQuivusIdsByPatientId");

                if (clientUser == null)
                {
                    clientUser = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                    //clientUser = new UserManagerServiceClient();
                }

                QuivusLogger.Info("Calling service :: UserManagerService");

                QuivusLogger.Info("Dispatching GetQuivusIdsByPatientId Call TO UserManagerService");

                patientQuivusIdList = clientUser.GetQuivusIdsByPatientId(PatientIdList);

                QuivusLogger.Info("Response Recieved  patientQuivusIdList::" + patientQuivusIdList);

                if (response == null)
                {
                    response = new PatientQuivusIdResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (patientQuivusIdList != null)
                {
                    response.PatientQuivusIdList = patientQuivusIdList;
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
    }
}
