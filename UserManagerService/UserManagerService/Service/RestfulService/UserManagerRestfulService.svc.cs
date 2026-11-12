using System;
using System.Collections.Generic;

//using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using Quivus.Model.Search;
using UserManager.Business.User.Patient;
using UserManager.Business.User.Patient.Impl;
using Quivus.Utility.CustomException;
using Quivus.Utility.Logger;
using System.Configuration;
using QuivusUtility.Utilities;
using System.Reflection;
using System.Data.SqlClient;

namespace UserManagerService.Service.RestfulService
{
    public class UserManagerRestfulService : IUserManagerRestfulService
    {
        public PatientSearchResultDTO SearchPatientbyClinicIdAndSearchString(string clinicBranch, string provider, string userType, string searchString, string demandPage)
        {
            int clinicBranchId = 0, providerId = 0, userTypeId = 0, demandPageNo = 0;
            if(!string.IsNullOrEmpty(clinicBranch))
                clinicBranchId = int.Parse(clinicBranch);
            if(!string.IsNullOrEmpty(provider))
                providerId = int.Parse(provider);
            if(!string.IsNullOrEmpty(userType))
                userTypeId = int.Parse(userType);
            if(!string.IsNullOrEmpty(demandPage))
                demandPageNo = int.Parse(demandPage);

            IPatientManager patientManagerImpl = null;
            PatientSearchResultDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientbyClinicIdAndSearchString(clinicBranchId, providerId, userTypeId, searchString, demandPageNo);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }
    }
}
