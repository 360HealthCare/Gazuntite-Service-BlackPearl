using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;

using QuivusManager.Buisness.VisitBL;

using QuivusManager.Buisness.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using Quivus.Utility.Fault;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using Quivus.Utility.CustomException;
using QuivusManager.Buisness.ApplicationSession;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;
using QuivusManager.Buisness.StaticDataBL.Impl;
using QuivusManager.Buisness.VisitBL.QuickInput;
using QuivusManager.Buisness.VisitBL.ViewBackgorund;
using QuivusManager.Buisness.VisitBL.ViewBackgorund.Impl;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;
using QuivusManagerService.CustomeControls.General.SoapNote;
using QuivusManagerService.CustomeControls.General;
using QuivusManagerService.CustomeControls;
using System.Web.UI;
using QuivusManager.IntegratedServies.AddressBookService;
using System.IO;
using System.Web.Services;
using System.Web.UI.HtmlControls;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {

        IViewBackgroundBL viewBackImpl = null;
        IQuickInputBL quickInputImpl = null;
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>        
        public EncounterTypeResponse LoadEncounterTypes(UserSessionDTO userSession)
        {
            EncounterTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadEncounterTypes Called");
           //     QuivusLogger.Info("LoadUserDefaultQuickLinksbyLocation PARAM UserId::" + UserId + "ClinicBranchId::" + ClinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadEncounterTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new EncounterTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new EncounterTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }           
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the template types
        /// </summary>
        /// <returns></returns>      
        public SoapTemplateResponse LoadSoapTemplateByClinicBranchId(int clinicBranchId, UserSessionDTO userSession)
        {
            SoapTemplateResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadSoapTemplateByClinicBranchId Called");
                QuivusLogger.Info("LoadSoapTemplateByClinicBranchId PARAM ClinicBranchId::" + clinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadSoapTemplateByClinicBranchId(clinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SoapTemplateResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SoapTemplateResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Medical History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        public MedicalHistoryTypeResponse LoadMedicalHistoryTypes(UserSessionDTO userSession)
        {
            MedicalHistoryTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadMedicalHistoryTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadMedicalHistoryTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new MedicalHistoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new MedicalHistoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Family History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>

        public FamilyHistoryTypeResponse LoadFamilyHistoryTypes(UserSessionDTO userSession)
        {
            FamilyHistoryTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadFamilyHistoryTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadFamilyHistoryTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new FamilyHistoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new FamilyHistoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>     
        public ImmunizationTypesResponse LoadImmunizationTypes(UserSessionDTO userSession)
        {
            ImmunizationTypesResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationTypes Called");
                //   QuivusLogger.Info("LoadPatientVisitsByPatientIdAndClinicId PARAM patientId::" + patientId + "clinicBranchId::" + clinicBranchId + "showAll::" + showAll);

                //TEMP
                //throw new InvalidSessionException(ProjectConstant.EXPIRED_USER_SESSION_ID, (int)ProjectConstant.ERROR_CODE.InvalidSession);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadImmunizationTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationTypesResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImmunizationTypesResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Main Category
        /// </summary>
        /// <returns></returns>

        public ROSMainCategoryTypeResponse LoadROSMainCategoryTypes(bool IsMale, UserSessionDTO userSession)
        {
            ROSMainCategoryTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadROSMainCategoryTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadROSMainCategoryTypes(IsMale);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ROSMainCategoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ROSMainCategoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Sub Category
        /// </summary>
        /// <returns></returns>
        public ROSSubCategoryTypeResponse LoadROSSubCategoryTypes(UserSessionDTO userSession)
        {
            ROSSubCategoryTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadROSSubCategoryTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadROSSubCategoryTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ROSSubCategoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ROSSubCategoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        //Commented by Mansoor
        ///// <summary>
        ///// Author Muhammad Ismail 
        ///// Date 21 March 2011
        ///// This Method Will be used to load HPI Data Control Types
        ///// </summary>
        ///// <returns></returns>
        //public HPIDataControlTypeResponse LoadHPIDataControlTypes(UserSessionDTO userSession)
        //{
        //    HPIDataControlTypeResponse response = null;
        //    try
        //    {
        //        QuivusLogger.Info("In QuivusManagerService LoadHPIDataControlTypes Called");

        //        if (ApplicationSessionHandler.IsValidUserSession(userSession))
        //        {
        //            if (StaticDataBL == null)
        //            {
        //                StaticDataBL = new StaticDataBLImpl();
        //            }
        //            response = StaticDataBL.LoadHPIDataControlTypes();
        //        }
        //    }
        //    catch (InvalidSessionException exp)
        //    {
        //        QuivusLogger.Error(exp);
        //        if (response == null)
        //            response = new HPIDataControlTypeResponse();

        //        ResponseErrorHandler.HandleException(response, exp, exp.Code);
        //    }
        //    catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
        //    {
        //        QuivusLogger.Error(exp);

        //        if (response == null)
        //            response = new HPIDataControlTypeResponse();

        //        ResponseErrorHandler.HandleException(response, exp);
        //    }
        //    return response;
        //}


        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load HPI detail types 
        /// </summary>
        /// <returns></returns>   
        public HPIDetailTypeResponse LoadHPIDetailTypes(UserSessionDTO userSession)
        {
            HPIDetailTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadHPIDetailTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadHPIDetailTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new HPIDetailTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new HPIDetailTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Addication types 
        /// </summary>
        /// <returns></returns>

        public SocialHistoryAddictionTypeResponse LoadSocialHistoryAddictionTypes(UserSessionDTO userSession)
        {
            SocialHistoryAddictionTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadSocialHistoryAddictionTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadSocialHistoryAddictionTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryAddictionTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SocialHistoryAddictionTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>

        public SocialMaritalStatusTypeResponse LoadMaritalStatusTypes(UserSessionDTO userSession)
        {
            SocialMaritalStatusTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadMaritalStatusTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadMaritalStatusTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialMaritalStatusTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SocialMaritalStatusTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        public SocialHistoryUsageAmountTypeResponse LoadSocialHistoryUsageAmountTypes(UserSessionDTO userSession)
        {
            SocialHistoryUsageAmountTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadSocialHistoryUsageAmountTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadSocialHistoryUsageAmountTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryUsageAmountTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SocialHistoryUsageAmountTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social Marital Status types 
        /// </summary>
        /// <returns></returns>
        public SocialHistoryMediumTypeResponse LoadSocialHistoryMediumTypes(UserSessionDTO userSession)
        {
            SocialHistoryMediumTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadSocialHistoryMediumTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadSocialHistoryMediumTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryMediumTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SocialHistoryMediumTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// This Method Will be used to load Plan types 
        /// </summary>
        /// <returns></returns>       
        public PlanTypeResponse LoadAllRecommendationPlanTypes(UserSessionDTO userSession)
        {
            PlanTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadAllRecommendationPlanTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadAllRecommendationPlanTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PlanTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PlanTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Duration types 
        /// </summary>
        /// <returns></returns>       
        public DurationTypeResponse LoadAllRecommendationDurationTypes(UserSessionDTO userSession)
        {
            DurationTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadAllRecommendationDurationTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadAllRecommendationDurationTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new DurationTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new DurationTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Method types 
        /// </summary>
        /// <returns></returns>        
        public MethodTypeResponse LoadAllRecommendationMethodTypes(UserSessionDTO userSession)
        {
            MethodTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadAllRecommendationMethodTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadAllRecommendationMethodTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new MethodTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new MethodTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }



        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Physical Main Category
        /// </summary>
        /// <returns></returns>
        public PhysicalMainCategoryTypeResponse LoadPhysicalMainCategoryTypes(bool IsMale, UserSessionDTO userSession)
        {
            PhysicalMainCategoryTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPhysicalMainCategoryTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadPhysicalMainCategoryTypes(IsMale);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PhysicalMainCategoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PhysicalMainCategoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Physical Sub Category
        /// </summary>
        /// <returns></returns>
        /// 

           public PhysicalSubCategoryTypeResponse LoadPhysicalSubCategoryTypes(UserSessionDTO userSession)
        {
            PhysicalSubCategoryTypeResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPhysicalSubCategoryTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    response = StaticDataBL.LoadPhysicalSubCategoryTypes();
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PhysicalSubCategoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PhysicalSubCategoryTypeResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

           /// <summary>
           /// Author Muhammad Ismail 
           /// Date 22 March 2011
           /// This Method Will be used to load lab LIONIC Test Types
           /// /// </summary>
           /// <returns></returns>
           public LIONICLabTestTypeResponse LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName,UserSessionDTO userSession)
           {
               LIONICLabTestTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadLIONICLabTestType Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadLIONICLabTestType(LabtestTypeID, LabTestCodeOrName);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new LIONICLabTestTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new LIONICLabTestTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Mansoor 
           /// Date 25 March 2011
           /// This Method Will be used to load Follow Up Method
           /// </summary>
           /// <returns></returns>           
           public FollowUpMethodTypesResponse LoadFollowUpMethodType(UserSessionDTO userSession)
           {
               FollowUpMethodTypesResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadFollowUpMethodType Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadFollowUpMethodType();
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new FollowUpMethodTypesResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new FollowUpMethodTypesResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Ismail
           /// Date 06 April 2011
           /// Method Load In House Lab Types By Clinic branch Id
           /// </summary>
           /// <param name="ClinicBranchId"></param>
           /// <returns></returns>
           public InHouseLabTestTypeResponse LoadInHouseLabTestTypeByClinicBranchId(int ClinicBranchId,UserSessionDTO userSession)
           {
               InHouseLabTestTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadInHouseLabTestTypeByClinicBranchId Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadInHouseLabTestTypeByClinicBranchId(ClinicBranchId);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new InHouseLabTestTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new InHouseLabTestTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Ismail 
           /// Date 06 april 2011
           /// Method Load CPT Medical Types
           /// </summary>
           /// <returns></returns>
           public CPTMedicalTypeResponse LoadCPTMedicalTypes(String CPTName,UserSessionDTO userSession)
           {
               CPTMedicalTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadCPTMedicalTypes Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadCPTMedicalTypes(CPTName);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new CPTMedicalTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new CPTMedicalTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Muhammad Ismail 
           /// Date 07 April 2011
           /// This Method Will be used to load ROS  Categories :)
           /// </summary>
           /// <returns></returns>
           public PatientROSResponse LoadROSCategoryTypes(bool IsMale, UserSessionDTO userSession)
           {
               PatientROSResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadROSMainCategoryTypes Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadROSCategoryTypes(IsMale);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new PatientROSResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new PatientROSResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Muhammad Ismail 
           /// Date 07 April 2011
           /// This Method Will be used to load ROS  Categories :)
           /// </summary>
           /// <returns></returns>
           public PatientPhysicalResponse LoadPhysicalCategoryTypes(bool IsMale, UserSessionDTO userSession)
           {
               PatientPhysicalResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadROSMainCategoryTypes Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadPhysicalCategoryTypes(IsMale);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new PatientPhysicalResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new PatientPhysicalResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }


           /// <summary>
           /// Author Atta 
           /// Date 14 April
           /// </summary>
           /// <returns></returns>           
           public InstructionAgeTypeResponse LoadInstructionAgeType(UserSessionDTO userSession)
           {
               InstructionAgeTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService InstructionAgeTypeResponse Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadInstructionAgeType();
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new InstructionAgeTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new InstructionAgeTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Atta 
           /// Date 14 April
           /// </summary>
           /// <returns></returns>           
           public InstructionDurationTypeResponse LoadInstructionDurationType(UserSessionDTO userSession)
           {
               InstructionDurationTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService InstructionDurationTypeResponse Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadInstructionDurationType();
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new InstructionDurationTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new InstructionDurationTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }
    
    
        

             /// <summary>
           /// Author Atta 
           /// Date 14 April
           /// </summary>
           /// <returns></returns>           
           public ServiceTypeResponse LoadServiceType(int ClinicBranchID, UserSessionDTO userSession)
           {
               ServiceTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService ServiceTypeResponse Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadServiceType(ClinicBranchID);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new ServiceTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new ServiceTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }


           /// <summary>
           /// Author Atta 
           /// Date 14 April
           /// </summary>
           /// <returns></returns>           
           public InstructionAgeValueTypeResponse LoadInstructionAgeValueType(UserSessionDTO userSession)
           {
               InstructionAgeValueTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService ServiceTypeResponse Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadInstructionAgeValueType();
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new InstructionAgeValueTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new InstructionAgeValueTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Ismail 
           /// Date 4 may 2011
           /// This Method Will be used to load module types Types 
           /// </summary>
           /// <returns></returns>
           public ModuleTypeResponse LoadAllModuleTypes(UserSessionDTO userSession)
           {
               ModuleTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadModuleTypes Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadModuleTypes();
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new ModuleTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new ModuleTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Muhammad Ismail 
           /// Date 18 July 2011
           /// This Method Will be used to load Physical Sub Category
           /// </summary>
           /// <returns></returns>
           public PhysicalCategoryTypeResponse LoadPhysicalCategoryType(UserSessionDTO userSession)
           {
               PhysicalCategoryTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadPhysicalCategoryType Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadPhysicalCategoryType();
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new PhysicalCategoryTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new PhysicalCategoryTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Muhammad Ismail 
           /// Date 18 July 2011
           /// This Method Will be used to load Physical Sub Category
           /// </summary>
           /// <returns></returns>
           public PhysicalCategoryTypeResponse LoadPhysicalCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, UserSessionDTO userSession)
           {
               PhysicalCategoryTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadPhysicalCategoryTypesByGender Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadPhysicalCategoryTypesByGender(LoadUniversal, LoadMaleOnly, LoadFemaleOnly);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new PhysicalCategoryTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new PhysicalCategoryTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }

           /// <summary>
           /// Author Muhammad Ismail 
           /// Date 15 July 2011
           /// This Method Will be used to load ROS Sub Level Categories
           /// </summary>
           /// <returns></returns>
           public ROSSubLevelCategoryTypeResponse LoadROSSubLevelCategoryTypes(int SubCategoryId,UserSessionDTO userSession)
           {
               ROSSubLevelCategoryTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadROSSubLevelCategoryTypes Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadROSSubLevelCategoryTypes(SubCategoryId);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new ROSSubLevelCategoryTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new ROSSubLevelCategoryTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }


           /// <summary>
           /// Author Muhammad Ismail 
           /// Date 18 July 2011
           /// This Method Will be used to load ROS Sub Level Categories By Gender
           /// </summary>
           /// <returns></returns>
           public ROSSubLevelCategoryTypeResponse LoadROSSubLevelCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, int ROSSubCategoryTypeID,UserSessionDTO userSession)
           {
               ROSSubLevelCategoryTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadROSSubLevelCategoryTypesByGender Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadROSSubLevelCategoryTypesByGender(LoadUniversal, LoadMaleOnly, LoadFemaleOnly, ROSSubCategoryTypeID);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new ROSSubLevelCategoryTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new ROSSubLevelCategoryTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }
           public FollowupDataControlTypeResponse LoadFollowupDataControlType(UserSessionDTO userSession)
           {
               FollowupDataControlTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadFollowupDataControlType Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadFollowupDataControlType();
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new FollowupDataControlTypeResponse();
                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new FollowupDataControlTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }
           public ServiceTypeResponse LoadPatientFollowupAutoCompleteByTypeIdAndClinicId(int followupDataControlTypeID, int clinicBranchId, UserSessionDTO userSession)
           {
               ServiceTypeResponse response = null;
               try
               {
                   QuivusLogger.Info("In QuivusManagerService LoadPatientFollowupAutoCompleteByTypeIdAndClinicId Called");

                   if (ApplicationSessionHandler.IsValidUserSession(userSession))
                   {
                       if (StaticDataBL == null)
                       {
                           StaticDataBL = new StaticDataBLImpl();
                       }
                       response = StaticDataBL.LoadPatientFollowupAutoCompleteByTypeIdAndClinicId(followupDataControlTypeID,clinicBranchId);
                   }
               }
               catch (InvalidSessionException exp)
               {
                   QuivusLogger.Error(exp);
                   if (response == null)
                       response = new ServiceTypeResponse();
                   ResponseErrorHandler.HandleException(response, exp, exp.Code);
               }
               catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
               {
                   QuivusLogger.Error(exp);

                   if (response == null)
                       response = new ServiceTypeResponse();

                   ResponseErrorHandler.HandleException(response, exp);
               }
               return response;
           }


           

           //public List<StateDTO> getStates()
           //{
           //    List<StateDTO> temp = new List<StateDTO>();
           //    for (int i = 0; i < 5; i++)
           //    {
           //        StateDTO s = new StateDTO();
           //        s.StateName = "State " + i;
           //        s.StateID = i;

           //        temp.Add(s);
           //    }

           //    return temp;
           //}

       
    }
}