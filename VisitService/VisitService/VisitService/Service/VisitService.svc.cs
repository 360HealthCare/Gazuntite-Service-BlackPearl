using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


using Quivus.Utility.Logger;
using Visit.Business.Common;


using Quivus.Model.QuickInput;
using Quivus.Model.TypeDTOs;
using Quivus.Model.Visit;
using Quivus.Model.PatientBackground;
using Quivus.Utility.CustomException;
using QuivusUtility.Utilities;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.MedicalHistory;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Visit.Business.BL.Encounter;
using Visit.Business.BL.Encounter.Impl;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.FamilyHistory;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Vital;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.HPI;
using Quivus.Model.QuickInput.MainQuickInput;
using Visit.Business.BL.QuickInput.Impl;
using Visit.Business.BL.ViewBackground.Impl;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Followup;
using QuivusModel.User.Patient;


namespace VisitService.Service
{
    public partial class VisitService : QuivusBaseService, IVisitService
    {

        VisitStaticDataManager objVisitSDManager = null;
        IVisitManager  VisitImpl = null;
        //IQuickInputManager QuickInputManager = null;
        //IViewBackgroundManager ViewBackgroungManager = null;

        #region Default Implementation of GetServiceVersion
        public override string GetServiceVersion()
        {
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return strVersion;
        }

       
        #endregion

      
        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>
        public List<ImmunizationTypeDTO> LoadImmunizationTypes()
        {
            List<ImmunizationTypeDTO> response = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                response = objVisitSDManager.LoadImmunizationTypes();
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
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>      
        public List<string> LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                response = VisitImpl.LoadImmunizationLotNumberByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
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
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization dose
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>    
        public List<string> LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                response = VisitImpl.LoadImmunizationDoseByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
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
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>     
        public List<string> LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if ( VisitImpl == null)
                {
                     VisitImpl = new VisitManagerImpl();
                }

                response =  VisitImpl.LoadImmunizationRefusalReasonByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
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
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        public List<string> LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if ( VisitImpl == null)
                {
                     VisitImpl = new VisitManagerImpl();
                }

                response =  VisitImpl.LoadImmunizationSiteByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
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
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        public List<string> LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if ( VisitImpl == null)
                {
                     VisitImpl = new VisitManagerImpl();
                }

                response =  VisitImpl.LoadImmunizationSeriesByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
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
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>      
        public List<string> LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {   
                if ( VisitImpl == null)
                {
                     VisitImpl = new VisitManagerImpl();
                }

                response =  VisitImpl.LoadImmunizationManufacturerByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
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
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public List<string> LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> response = null;
            try
            {
                if ( VisitImpl == null)
                {
                     VisitImpl = new VisitManagerImpl();
                }

                response =  VisitImpl.LoadImmunizationRoutByClinicId(searchCriteria, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
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
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>     
        public List<ImmunizationDTO> LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<ImmunizationDTO> ImmunizationList = null;
            try
            {  
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                ImmunizationList = VisitImpl.LoadPatientImmunizationsByPatientId(patientId, providerUserId, userTypeId, clinicBranchID,  stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ImmunizationList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Disease Types 
        /// </summary>
        /// <returns></returns>
        public List<DiseaseTypeDTO> LoadDiseaseTypes(int ClinicBranchID, int FamilyHistoryTypeID)
        {
            List<DiseaseTypeDTO> DiseaseList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                DiseaseList = objVisitSDManager.LoadDiseaseTypes(ClinicBranchID,FamilyHistoryTypeID);

                QuivusLogger.Info("LoadMedicalHistoryTypes Return List ::" + DiseaseList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return DiseaseList; 
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 14 March 2011
        /// This Method Will be used to load Vital Types 
        /// </summary>
        /// <returns></returns>        
        public List<VitalTypeDTO> LoadVitalTypes()
        {
            List<VitalTypeDTO> vitalTypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                vitalTypeList = objVisitSDManager.LoadVitalTypes();

                QuivusLogger.Info("LoadVitalTypes Return List ::" + vitalTypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return vitalTypeList; 
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to HPI detail types 
        /// </summary>
        /// <returns></returns>        
        public List<HPITypeDTO> LoadHPIDetailTypes()
        {
            List<HPITypeDTO> hpiTypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                hpiTypeList = objVisitSDManager.LoadHPIDetailTypes();

                QuivusLogger.Info("LoadHPIDetailTypes Return List ::" + hpiTypeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return hpiTypeList; 
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Mar 2011
        /// get hpi details for auto complete suggestion box, by type id and clinic branch
        /// </summary>
        /// <param name="hpiTypeId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>       
        public List<string> LoadHPIDetailAutoCompleteByTypeIdAndClinicId(int hpiTypeId, int clinicBranchId, string searchCriteria)
        {
            List<string> response = null;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                response = VisitImpl.LoadHPIDetailAutoCompleteByTypeIdAndClinicId(hpiTypeId, clinicBranchId, searchCriteria);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
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
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>

        public bool CoSignVisit(int VisitId, int ProviderId, int ProviderUserId, int ProviderTypeId, DateTime OperationDate)
        {
            bool result = false;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                result = VisitImpl.CoSignVisit(VisitId, ProviderId, ProviderUserId, ProviderTypeId, OperationDate);
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
            return result;
        }


        /// <summary>
        /// Author Ismail 
        /// Date 4 may 2011
        /// This Method Will be used to load module types Types 
        /// </summary>
        /// <returns></returns>
        public List<ModuleTypeDTO> LoadVisitModuleTypes()
        {
            List<ModuleTypeDTO> TypeList = null;
            try
            {
                if (objVisitSDManager == null)
                {
                    objVisitSDManager = new VisitStaticDataManager();
                }

                TypeList = objVisitSDManager.LoadModuleTypes();

                QuivusLogger.Info("LoadHPIDetailTypes Return List ::" + TypeList);
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
            return TypeList; 
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 06 May 2011
        /// This Method Will be used to import visit By template
        /// </summary>
        /// <returns></returns>
        public QuickInputDTO ImportVisitByTemplate(int TemplateId, int ClinicBranchId)
        {
            QuickInputDTO QIObject = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                QIObject = ViewBackgroundImpl.ImportVisitByTemplate(TemplateId, ClinicBranchId);

                QuivusLogger.Info("ImportVisitByTemplate Return QIObject ::" + QIObject);
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
            return QIObject; 
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 08/04/2011
        /// </summary>
        /// <param name="hpiTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>

        public List<FollowupDataControlTypeDTO> LoadFollowupDataControlType()
        {
            List<FollowupDataControlTypeDTO> response = null;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                response = VisitImpl.LoadFollowupDataControlType();
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
        /// Author Sahibzada Sami ud Din
        /// Date 08/04/2011
        /// </summary>
        /// <param name="followupDataControlTypeID"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public List<ServiceTypeDTO> LoadPatientFollowupAutoCompleteByTypeIdAndClinicId(int followupDataControlTypeID, int clinicBranchId)
        {
            List<ServiceTypeDTO> response = null;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                response = VisitImpl.LoadPatientFollowupAutoCompleteByTypeIdAndClinicId(followupDataControlTypeID, clinicBranchId);
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
        /// Author Ismail
        /// Date 08/25/2011
        /// </summary>
       
        /// <returns></returns>
        public List<PatientLastSeenDTO> LoadPatientLastSeenByPatientId(int PatientId)
        {
            List<PatientLastSeenDTO> response = null;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                response = VisitImpl.LoadPatientLastSeenByPatientId(PatientId);
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
   
    
    }
}
