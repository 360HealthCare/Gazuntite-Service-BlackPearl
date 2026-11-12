using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Billing.PendingBills;
using Quivus.Utility.Logger;
using UserManager.DataAccess.Billing;
using Quivus.Utility.Factory;
using QuivusModel.Billing.User;
using UserManagerService.DataAccess.Patient;
using Quivus.Model.User.Patient;

namespace UserManager.Business.Billing.Impl
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   01/31/2012
    /// Billing Service related Mehtods
    /// </summary>
    public class BillingManagerImpl : IBillingManager
    {
        BillingDAO billingDAO = null;
        PatientDAO patientDAO = null;

        //Billing Service related Mehtods

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
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling GetPatientBillingInfoByPatientId PARAM:: PatientIdList " + PatientIdList);


                if (billingDAO == null)
                {
                    billingDAO = (BillingDAO)DAOFactory.GetDataAccessObject(typeof(BillingDAO));
                }
                PatientInfoList = billingDAO.GetPatientBillingInfoByPatientId(PatientIdList);

                if (PatientInfoList.Count > 0 && patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }
                foreach (SuperBillPatientInfoDTO ptDTO in PatientInfoList)
                {
                    List<PatientInsuranceDTO> piList = patientDAO.LoadPatientInsuranceInfoByPatientIdForCompleteProfile(ptDTO.PatientId);
                    List<SuperBillPatientInsuranceDTO> sbInsList = new List<SuperBillPatientInsuranceDTO>();
                    foreach (PatientInsuranceDTO umInsDTO in piList)
                    {
                        SuperBillPatientInsuranceDTO insuranceDTO = new SuperBillPatientInsuranceDTO();

                        insuranceDTO.InsOrganizationID = umInsDTO.InsuranceOrganization.InsuranceOrganizationID;
                        insuranceDTO.InsuranceName = umInsDTO.InsuranceOrganization.InsuranceOrganizationName;
                        insuranceDTO.InsurancePlan = umInsDTO.InsurancePlan;
                        insuranceDTO.InsuranceTypeName = umInsDTO.InsuranceType.InsuranceTypeName;
                        insuranceDTO.IsEligible = umInsDTO.IsEligible;
                        insuranceDTO.PolicyNumber = umInsDTO.PolicyNumber;
                        insuranceDTO.ExpiryDate = umInsDTO.EndDate;
                        insuranceDTO.Copay = umInsDTO.Copay;

                        sbInsList.Add(insuranceDTO);
                    }

                    if (sbInsList.Count > 0)
                    {
                        ptDTO.InsuranceInfo = sbInsList;
                    }
                }
            }
            catch
            {
                throw;
            }
            return PatientInfoList;
        }               
    }
}
