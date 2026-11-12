using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using Quivus.Utility.Logger;
using System.ServiceModel;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.IntegratedServies.ReferenceFactory;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using Quivus.Utility.Fault;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using Quivus.Utility.MailTemplates;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AdderssBook;
using QuivusManager.Buisness.AddressBookBL;
using QuivusManager.Buisness.AddressBookBL.Impl;
using QuivusManager.Buisness.Utility;
using QuivusManager.Buisness.E_PrescriptionBL;
using QuivusManager.Buisness.E_PrescriptionBL.Impl;
using QuivusManager.IntegratedServies.EprescriptionService;
using QuivusManager.IntegratedServies.AddressBookService;
using QuivusManager.Buisness.ClinicBL;
//using QuivusModel.User.ClinicUser;
//using QuivusModel.Common;
//using QuivusModel.Association;


namespace QuivusManager.Buisness.ClinicBL.Impl
{
    public class ClinicBL : IClinicBL
    {

        UserManagerServiceClient UserServiceClient = null;
        
     
        /// <summary>
        /// Author Sami
        /// Date 15 Dec 2011      
        /// This method will return clinic, clinicbranch and station info for soap note
        /// </summary>        
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>       
        public ClinicDTO LoadClinicBranchAndStationInfo(int stationId)
        {
            ClinicDTO response = null;
            try
            {
                QuivusLogger.Info("LoadClinicBranchAndStationInfo PARAM :: stationId" + stationId);
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                response = UserServiceClient.LoadClinicBranchAndStationInfo(stationId);
            }
            catch
            {
                throw;
            }
            return response;
        }

    }
}
