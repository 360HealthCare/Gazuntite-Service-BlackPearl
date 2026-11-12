using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.Clinic;
using Quivus.Utility.Logger;
using UserManager.DataAccess.Clinic.ClinicBranch;
using Quivus.Utility.Factory;
using Quivus.Utility.CustomException;
using Quivus.Utility;

namespace UserManager.Business.Clinic.Impl
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 15 Dec 2011
    /// This class provides method for clinic,clinic branch and station
    public class ClinicManager : IClinicManager
    {

        ClinicBranchDAO clinicBranchDAO=null;

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
                QuivusLogger.Info("LoadClinicBranchInfo PARAM clinicBranchId :: " + stationId);

                if (stationId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.STATION_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                
                if (clinicBranchDAO == null)
                {
                    clinicBranchDAO = (ClinicBranchDAO)DAOFactory.GetDataAccessObject(typeof(ClinicBranchDAO));
                }
                response = clinicBranchDAO.LoadClinicBranchAndStationInfo(stationId);
            }
            catch
            {
                throw;
            }
            return response;
        }



        public List<int> LoadAllClinicBranch()
        {
            List<int> ids = null;
            try
            {

                if (clinicBranchDAO == null)
                {
                    clinicBranchDAO = (ClinicBranchDAO)DAOFactory.GetDataAccessObject(typeof(ClinicBranchDAO));
                }

                ids = clinicBranchDAO.LoadAllClinicBranch();
            }
            catch
            {
                throw;
            }

            return ids;

        }

        public List<int> LoadClinicBranchUsers(int userTypeId, int clinicBranchId)
        {
            List<int> ids = null;
            try
            {

                if (clinicBranchDAO == null)
                {
                    clinicBranchDAO = (ClinicBranchDAO)DAOFactory.GetDataAccessObject(typeof(ClinicBranchDAO));
                }

                ids = clinicBranchDAO.LoadClinicBranchUserIDs(userTypeId, clinicBranchId);
            }
            catch
            {
                throw;
            }

            return ids;

        }
    }
}
