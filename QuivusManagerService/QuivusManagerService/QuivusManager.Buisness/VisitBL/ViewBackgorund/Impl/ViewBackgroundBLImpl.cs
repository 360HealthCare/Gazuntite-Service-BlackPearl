using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using Quivus.Utility.Logger;
using QuivusManager.IntegratedServies.ReferenceFactory;
using Quivus.Utility;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.Buisness.Utility;
using QuivusManager.Buisness.UserBL.Impl;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.Buisness.UserBL;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.ChronicMedication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Prescription;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Procedure;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.ModuleDictation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.ModuleNotes;
using StationDTO = QuivusManager.IntegratedServies.UserManagerService.StationDTO;


namespace QuivusManager.Buisness.VisitBL.ViewBackgorund.Impl
{
    public class ViewBackgroundBLImpl : IViewBackgroundBL
    {

        VisitServiceClient clientVisit;
        IUserBL UserBL = null;

        UserManagerServiceClient UserServiceClient = null;
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>  
        /// <codereview> Code review and adjusted by baber waqas</codereview>
        public SoapTemplateResponse LoadSoapTemplateByClinicBranchId(int clinicBranchId)
        {
            SoapTemplateResponse response = null;
            List<SoapTemplateTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadSoapTemplateByClinicBranchId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadSoapTemplateByClinicBranchId Call TO VisitService");

                list = clientVisit.LoadSoapTemplateByClinicBranchId(clinicBranchId);

                QuivusLogger.Info("Response Recieved  list::" + list);

                ////if (response == null)
                ////{
                ////    response = new SoapTemplateResponse();
                ////    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                ////}
                ////response.SoapTemplateList = list;
              
                //// CODEREVIEWED and ADJUSTED BY BABER WAQAS
                response = new SoapTemplateResponse { ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS, SoapTemplateList = list };

            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns> 
        public PatientEncountersResponse LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll, int UserId, int UserTypeId)
        {
            PatientEncountersResponse response = null;
            PatientEncounterResult list = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVisitsByPatientIdAndClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientVisitsByPatientIdAndClinicId Call TO VisitService");

                list = clientVisit.LoadPatientVisitsByPatientIdAndClinicId(patientId, clinicBranchId, showAll, UserId, UserTypeId);

                QuivusLogger.Info("Response Recieved  list::" + list);

                if (response == null)
                {
                    response = new PatientEncountersResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
               

                UpdateVisitProvider(ref list); // This Method Will Update Visit Provider Names
                response.EncounterResult = list;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date Feb 22 2011
        /// Methos Used To Load Visit Provider Names Using UserID From User Sevice And Set Provider NAme in Encounter List
        /// </summary>
        /// <param name="list"></param>
        /// <codereview>Code reviewed and adjusted by baber waqas  </codereview>
        private void UpdateVisitProvider(ref PatientEncounterResult list)
        {
            QuivusLogger.Info("UpdateVisitProvider Called To UpdATE Visit Provider Name in List");

            if (list != null && list.PatientEncounterList != null && list.PatientEncounterList.Count > 0) /* Load Visit Provider Names Using UserID From User Sevice  */
            {
                List<int> userIdList = null;
                List<int> stationIDList = null;           
                    userIdList = EliminateCahceUserIds(list, null, QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA); // This Method WIll Skip USer Id That Already Exist in Cache
                    stationIDList = EliminateCahceStationIds(list, null, QApplicationDataCache.STATION_CACHE_DATA);
                if (userIdList != null && userIdList.Count > 0)
                {
                    if (UserBL == null)
                    {
                        UserBL = new UserBLImpl();
                    }

                    QuivusLogger.Info("Forward GetUserBasicInfoById Call TO User bL ");

                    List<UserBasicInfoDTO> UserBasicList = UserBL.GetUserBasicInfoById(userIdList); // Load User Basic Info Using Ids

                    QuivusLogger.Info("Response Recieved UserBasicList::" + UserBasicList);

                    if (UserBasicList != null && UserBasicList.Count > 0)
                    {
                        if (QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA == null)
                        {
                            QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA = UserBasicList;
                        }
                        else
                        {
                            var result = QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA.Concat(UserBasicList);
                            // QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA = result.ToList();
                        }
                    }
                }


                if (stationIDList != null && stationIDList.Count > 0)
                {
                    if (UserBL == null)
                    {
                        UserBL = new UserBLImpl();
                    }

                    QuivusLogger.Info("Forward station Call TO User bL ");

                    List<QuivusManager.IntegratedServies.UserManagerService.StationDTO> stationList = UserBL.GetStationNamesByStationID(stationIDList); // Load User Basic Info Using Ids

                    QuivusLogger.Info("Response Recieved station list::" + stationList);

                    if (stationList != null && stationList.Count > 0)
                    {
                        if (QApplicationDataCache.STATION_CACHE_DATA == null)
                        {
                            QApplicationDataCache.STATION_CACHE_DATA = stationList;
                        }
                        else
                        {
                            var result = QApplicationDataCache.STATION_CACHE_DATA.Concat(stationList);
                            QApplicationDataCache.STATION_CACHE_DATA =  result.ToList();
                        }
                    }
                }

                if (QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA != null)
                {
                    foreach (PatientEncounterDTO Obj in list.PatientEncounterList)
                    {
                        QuivusLogger.Info("Updating Provider Name Loop OwnerUserId:: " + Obj.OwnerUserId + " VisitId:: " + Obj.VisitId);


                        if (QApplicationDataCache.STATION_CACHE_DATA != null)
                        {
                            var cacheStationQuery =
                        from d in QApplicationDataCache.STATION_CACHE_DATA
                        where d.StationID.Equals(Obj.StationID)
                        select d;
                            if ( cacheStationQuery.ToArray().Length > 0)
                            {
                                QuivusManager.IntegratedServies.UserManagerService.StationDTO resultObj = cacheStationQuery.First();
                                Obj.StationName = resultObj.StationName;
                                QuivusLogger.Info("Setting station  Name For station id:: " + Obj.StationID + " VisitId:: " + Obj.VisitId + "VisitProvider::" + Obj.VisitProvider);
                            }
                            else
                            {
                                QuivusLogger.Info("error updating station  Name For station id:: " + Obj.StationID + " VisitId:: " + Obj.VisitId + "VisitProvider::" + Obj.VisitProvider);
                            }
                        }
                        // var PatientEncounterQuery =
                        //from d in list.PatientEncounterList
                        //where d.OwnerUserId.Equals(obj.UserId)
                        //select d;
                        var cacheQuery =
                          from d in QApplicationDataCache.USER_BASIC_INFO_CACHE_DATA
                          where d.UserId.Equals(Obj.OwnerUserId)
                          select d;


                        if ( cacheQuery.ToArray().Length > 0)
                        {
                            UserBasicInfoDTO resultObj = cacheQuery.First();
                            Obj.VisitProvider = resultObj.FirstName + " " + resultObj.LastName;
                            QuivusLogger.Info("Setting Provider Name For OwnerUserId:: " + Obj.OwnerUserId + " VisitId:: " + Obj.VisitId + "VisitProvider::" + Obj.VisitProvider);
                        }
                        else
                        {
                            QuivusLogger.Info("Error Updating Provider Name For OwnerUserId:: " + Obj.OwnerUserId + " VisitId:: " + Obj.VisitId);
                        }
                    }
                }

            }
        }

        private List<int> EliminateCahceStationIds(PatientEncounterResult list, List<int> stationIDList, List<QuivusManager.IntegratedServies.UserManagerService.StationDTO> stationList)
        {
            QuivusLogger.Info("EliminateCahceStationIds Called To Eliminate station In Cache");

            if (list != null && list.PatientEncounterList != null && list.PatientEncounterList.Count > 0)
            {
                foreach (PatientEncounterDTO Obj in list.PatientEncounterList)
                {
                    if (stationList != null)
                    {
                        var CacheQuery =
                           from d in stationList
                           where d.StationID.Equals(Obj.StationID)
                           select d;

                        if (CacheQuery != null && CacheQuery.ToArray().Length > 0)
                        {
                            // Record Already Exist in Cache SKIP
                        }
                        else
                        {
                            if (stationIDList == null)
                            {
                                stationIDList = new List<int>();
                            }
                            stationIDList.Add(Obj.StationID);
                        }
                    }
                    else
                    {
                        if (stationIDList == null)
                        {
                            stationIDList = new List<int>();
                        }
                        stationIDList.Add(Obj.StationID);
                    }
                }
            }
            return stationIDList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date Feb 22 2011
        /// This Method WIll Skip USer Id That Already Exist in Cache
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userIdList"></param>
        /// <param name="cacheUserList"></param>
        /// <returns></returns>
        /// <code
        private static List<int> EliminateCahceUserIds(PatientEncounterResult list, List<int> userIdList, List<UserBasicInfoDTO> cacheUserList)
        {
            QuivusLogger.Info("EliminateCahceUserIds Called To Eliminate User In Cache");

            if (list != null && list.PatientEncounterList != null && list.PatientEncounterList.Count > 0)
            {
                foreach (PatientEncounterDTO Obj in list.PatientEncounterList)
                {
                    if (cacheUserList != null)
                    {
                        var cacheQuery =
                           from d in cacheUserList
                           where d.UserId.Equals(Obj.OwnerUserId)
                           select d;

                        if ( cacheQuery.ToArray().Length > 0)
                        {
                            // Record Already Exist in Cache SKIP
                        }
                        else
                        {
                            if (userIdList == null)
                            {
                                userIdList = new List<int>();
                            }
                            userIdList.Add(Obj.OwnerUserId);
                        }
                    }
                    else
                    {
                        if (userIdList == null)
                        {
                            userIdList = new List<int>();
                        }
                        userIdList.Add(Obj.OwnerUserId);
                    }
                }
            }
            return userIdList;
        }




        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public ImmunizationAutoCompleteResponse LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationLotNumberByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationLotNumberByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationLotNumberByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved  LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch
            {
                throw;
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
        public ImmunizationAutoCompleteResponse LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationDoseByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationDoseByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationDoseByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationDoseByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch
            {
                throw;
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
        public ImmunizationAutoCompleteResponse LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationRefusalReasonByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationRefusalReasonByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationRefusalReasonByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationRefusalReasonByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch
            {
                throw;
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
        public ImmunizationAutoCompleteResponse LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationSiteByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationSiteByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationSiteByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationSiteByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch
            {
                throw;
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
        public ImmunizationAutoCompleteResponse LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationSeriesByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationSeriesByClinicId Call TO VisitService");


                LotList = clientVisit.LoadImmunizationSeriesByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationSeriesByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        public ImmunizationAutoCompleteResponse LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationManufacturerByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationManufacturerByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationManufacturerByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationManufacturerByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        public ImmunizationAutoCompleteResponse LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId)
        {
            ImmunizationAutoCompleteResponse response = null;
            List<string> LotList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationRoutByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationRoutByClinicId Call TO VisitService");

                LotList = clientVisit.LoadImmunizationRoutByClinicId(searchCriteria, clinicBranchId);

                QuivusLogger.Info("Response Recieved LoadImmunizationRoutByClinicId LotList::" + LotList);

                if (response == null)
                {
                    response = new ImmunizationAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.AutoCompleteList = LotList;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit        
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>    
        public PatientImmunizationResponse LoadImmunizationsByVisitId(int visitId, int providerUserId, int userTypeId)
        {
            PatientImmunizationResponse response = null;
            List<ImmunizationDTO> PatientImmunizationList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationsByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationsByVisitId Call TO VisitService");

                PatientImmunizationList = clientVisit.LoadImmunizationsByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  PatientImmunizationList::" + PatientImmunizationList);

                if (response == null)
                {
                    response = new PatientImmunizationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.PatientImmunizationData = PatientImmunizationList;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>    
        public VisitNotesResponse LoadModuleNotesByVisitId(int visitId)
        {
            VisitNotesResponse response = null;
            List<ModuleNoteDTO> ModuleNoteList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleNotesByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadModuleNotesByVisitId Call TO VisitService");

                ModuleNoteList = clientVisit.LoadModuleNotesByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  ModuleNoteList::" + ModuleNoteList);

                if (response == null)
                {
                    response = new VisitNotesResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.ModuleNoteData = ModuleNoteList;
            }
            catch
            {
                throw;
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
        public PatientImmunizationResponse LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            PatientImmunizationResponse response = null;
            List<ImmunizationDTO> PatientImmunizationList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientImmunizationsByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientImmunizationsByPatientId Call TO VisitService");


                PatientImmunizationList = clientVisit.LoadPatientImmunizationsByPatientId(patientId, providerUserId, userTypeId, clinicBranchID, stationIDs,isAllSelected);


                QuivusLogger.Info("Response Recieved  PatientImmunizationList::" + PatientImmunizationList);


                //TODO - Mansoor please check why you are setting REsponse status to FAlse in case of null response, because the data can be empty as well
                //as in this case if patient doesn't have any immunization then the operation succeeded but your logic considers it a failure. please explain if there is
                //any other reason for this logic which i have failed to understand.--ALI NOOR
                if (response == null)
                {
                    response = new PatientImmunizationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (PatientImmunizationList != null)
                {
                    response.PatientImmunizationData = PatientImmunizationList;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 
        /// Method Will be used to load patient medical info by visit id :)
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public PatientMedicalHistoryResponse LoadPatientMedicalHistoryByVisit(int VisitId)
        {
            PatientMedicalHistoryResponse response = null;
            List<MedicalHistoryDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientMedicalHistoryByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientMedicalHistoryByVisit Call TO VisitService");

                HistoryList = clientVisit.LoadPatientMedicalHistoryByVisit(VisitId);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new PatientMedicalHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.ResponseList = HistoryList;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                //  response.ResponseList = HistoryList;
            }
            catch
            {
                throw;
            }
            return response;
        }

        public PatientFamilyHistoryResponse LoadPatientFamilyHistoryByVisit(int VisitId)
        {
            PatientFamilyHistoryResponse response = null;
            List<FamilyHistoryDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientFamilyHistoryByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientFamilyHistoryByVisit Call TO VisitService");

                HistoryList = clientVisit.LoadPatientFamilyHistoryByVisit(VisitId);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new PatientFamilyHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.ResponseList = HistoryList;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                //  response.ResponseList = HistoryList;
            }
            catch
            {
                throw;
            }
            return response;
        }

        public PatientVitalResponse LoadPatientVitalByVisit(int VisitId)
        {
            PatientVitalResponse response = null;
            List<VitalDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVitalByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientVitalByVisit Call TO VisitService");

                HistoryList = clientVisit.LoadPatientVitalByVisit(VisitId);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new PatientVitalResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.ResponseList = HistoryList;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                //  response.ResponseList = HistoryList;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        public PatientLabResponse LoadPatientLabsByVisit(int VisitId)
        {
            PatientLabResponse response = null;
            List<LabDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientLabsByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientLabsByVisit Call TO VisitService");

                HistoryList = clientVisit.LoadPatientLabsByVisit(VisitId);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new PatientLabResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.ResponseList = HistoryList;
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
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load Patient Previous 10 Vitals History 
        /// </summary>
        /// <returns></returns>
        public PatientVitalResponse LoadPatientPreviousVitals(int PatientId)
        {
            PatientVitalResponse response = null;
            List<RecentVitalDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVitalByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientVitalByVisit Call TO VisitService");

                HistoryList = clientVisit.LoadPatientPreviousVitals(PatientId);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new PatientVitalResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.ResponseRecentList = HistoryList;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                //  response.ResponseList = HistoryList;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load Patient HPI by Visit 
        /// </summary>
        /// <returns></returns>
        public PatientHPIResponse LoadPatientHPIByVisit(int visitId)
        {
            PatientHPIResponse response = null;
            List<HPI_CCDTO> hpiList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientHPIByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientHPIByVisit Call TO VisitService");

                hpiList = clientVisit.LoadPatientHPIByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  hpiList::" + hpiList);

                if (response == null)
                {
                    response = new PatientHPIResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (hpiList != null)
                {
                    response.HPIList = hpiList;
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


        public SocialHistoryAddictionResponse LoadSocialHistoryAddictionBySocialHistoryID(int PatientSocialHistoryId)
        {
            SocialHistoryAddictionResponse response = null;
            List<SocialHistoryAddictionDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadSocialHistoryAddictionBySocialHistoryID");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadSocialHistoryAddictionBySocialHistoryID Call TO VisitService");

                TypeList = clientVisit.LoadSocialHistoryAddictionBySocialHistoryID(PatientSocialHistoryId);

                QuivusLogger.Info("Response Recieved  TypeList::" + TypeList);

                if (response == null)
                {
                    response = new SocialHistoryAddictionResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (TypeList != null)
                {
                    response.SocialHistoryAddictionList = TypeList;
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

        public SocialHistoryResponse LoadSocialHistoryByVisitId(int VisitId)
        {
            SocialHistoryResponse response = null;
            List<SocialHistoryDTO> TypeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadSocialHistoryByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadSocialHistoryByVisitId Call TO VisitService");

                TypeList = clientVisit.LoadSocialHistoryByVisitId(VisitId);

                QuivusLogger.Info("Response Recieved  TypeList::" + TypeList);

                if (response == null)
                {
                    response = new SocialHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (TypeList != null)
                {
                    response.SocialHistoryList = TypeList;
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
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Recommendation by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>  
        public PatientRecommendationResponse LoadPatientRecommendationByVisit(int visitId)
        {
            PatientRecommendationResponse response = null;
            List<PatientRecommendationDTO> RecommendationList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientRecommendationByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientRecommendationByVisit Call TO VisitService");

                RecommendationList = clientVisit.LoadPatientRecommendationByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  RecommendationList::" + RecommendationList);

                if (response == null)
                {
                    response = new PatientRecommendationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (RecommendationList != null)
                {
                    response.RecommendationList = RecommendationList;
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

        public PatientPhysicalResponse LoadPatientPhysicalsByVisit(int VisitId)
        {
            PatientPhysicalResponse response = null;
            List<PhysicalsDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientPhysicalsByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientPhysicalsByVisit Call TO VisitService");

                HistoryList = clientVisit.LoadPatientPhysicalsByVisit(VisitId);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new PatientPhysicalResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.ResponseList = HistoryList;
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
        /// Author Mansoor
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>           
        public PatientDiagnosisResponse LoadPatientDiagnosisByVisitId(int visitId)
        {
            PatientDiagnosisResponse response = null;
            List<PatientDiagnosisDTO> patientDiagnosisList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientDiagnosisByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientPhysicalsByVisit Call TO VisitService");

                patientDiagnosisList = clientVisit.LoadPatientDiagnosisByVisitId(visitId);

                QuivusLogger.Info("Response Recieved  HistoryList::" + patientDiagnosisList);

                if (response == null)
                {
                    response = new PatientDiagnosisResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (patientDiagnosisList != null)
                {
                    response.PatientDiagnosisList = patientDiagnosisList;
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

        /// Author Ismail
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        //public PatientDiagnosisResponse LoadPatientDiagnosisByVisitId(int VisitId)
        //{
        //    PatientDiagnosisResponse response = null;
        //    List<PatientDiagnosisDTO> HistoryList = null;
        //    try
        //    {
        //        QuivusLogger.Info("Calling LoadPatientDiagnosisByVisitId");

        //        if (clientVisit == null)
        //        {
        //            clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
        //        }

        //        QuivusLogger.Info("Calling service :: VisitService");

        //        QuivusLogger.Info("Dispatching LoadPatientDiagnosisByVisitId Call TO VisitService");

        //        HistoryList = clientVisit.LoadPatientDiagnosisByVisitId(VisitId);

        //        QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

        //        if (response == null)
        //        {
        //            response = new PatientDiagnosisResponse();
        //            response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
        //        }

        //        if (HistoryList != null)
        //        {
        //            response.ResponseList = HistoryList;
        //            response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
        //        }
        //        else
        //        {
        //            response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return response;
        //}

        public PatientROSResponse LoadPatientROSByVisit(int VisitId)
        {
            PatientROSResponse response = null;
            List<ROSDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientROSByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientROSByVisit Call TO VisitService");

                HistoryList = clientVisit.LoadPatientROSByVisit(VisitId);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new PatientROSResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.ResponseList = HistoryList;
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

        public QuickInputResponse LoadQuickInputByVisit(int VisitId)
        {
            QuickInputResponse response = null;
            QuickInputDTO Data = null;
            try
            {
                QuivusLogger.Info("Calling LoadQuickInputByVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadQuickInputByVisit Call TO VisitService");

                Data = clientVisit.LoadQuickInputByVisit(VisitId);

                QuivusLogger.Info("Response Recieved  Data::" + Data);

                if (response == null)
                {
                    response = new QuickInputResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (Data != null)
                {
                    response.ResponseData = Data;
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
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load Patient Follow UP by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public PatientFollowUpResponse LoadPatientFollowUpByVisitId(int visitId)
        {
            PatientFollowUpResponse response = null;
            PatientFollowUpDTO followUp = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientFollowUpByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientFollowUpByVisitId Call TO VisitService");

                followUp = clientVisit.LoadFollowUpByVisitId(visitId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                if (response == null)
                {
                    response = new PatientFollowUpResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (followUp != null)
                {
                    response.FollowUp = followUp;
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
        /// Author Ismail
        /// Date 28 March 2011
        /// This Method Will be used to load visit stake holder by visit by Id
        /// </summary>
        /// <returns></returns>
        public VisistStakeHolderResponse LoadVisitStakeHolderByVisitId(int VisitId, int UserTypeId)
        {
            VisistStakeHolderResponse response = null;
            List<VisistStakeHolderDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadVisitStakeHolderByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadVisitStakeHolderByVisitId Call TO VisitService");

                lst = clientVisit.LoadVisitStakeHolderByVisitId(VisitId, UserTypeId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                if (response == null)
                {
                    response = new VisistStakeHolderResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient medication prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        public MedicationPrescribedResponse LoadPatientMedicationPrescribedByPatient(int PatientId)
        {
            MedicationPrescribedResponse response = null;
            List<MedicationPrescribedDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientMedicationPrescribedByPatient");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientMedicationPrescribedByPatient Call TO VisitService");

                lst = clientVisit.LoadPatientMedicationPrescribedByPatient(PatientId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                if (response == null)
                {
                    response = new MedicationPrescribedResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseData = lst;
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
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        public AllergyResponse LoadPatientAllergyByPatientId(int PatientId, int ClinicBranchID)
        {
            AllergyResponse response = null;
            List<AllergyDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientAllergyByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientAllergyByPatientId Call TO VisitService");


                lst = clientVisit.LoadPatientAllergyByPatientId(PatientId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                if (response == null)
                {
                    response = new AllergyResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                MergePatientAllergies(PatientId, lst, ClinicBranchID);

                if (lst != null)
                {
                   // response.ResponseData = lst;
                    if (lst.Count > 1)
                    {
                       lst.RemoveAll(allergen => allergen.AllergyName.Equals(ProjectConstant.VISIT_NKDA));
                        if(lst.Count >1)
                            lst.RemoveAll(allergen => allergen.AllergyName.Equals(ProjectConstant.REGISTRATION_NKDA));

                        //var qResults = from allergy in lst
                        //               where allergy.AllergyName.Equals(ProjectConstant.REGISTRATION_NKDA) || allergy.AllergyName.Equals(ProjectConstant.VISIT_NKDA)
                        //               select allergy;
                        
                    }
                    response.ResponseData = lst;
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

        private void MergePatientAllergies(int PatientId, List<AllergyDTO> lst, int ClinicBranchID)
        {
            // Load Patient Allergies Given At Registration & Merge
            if (UserServiceClient == null)
            {
                UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
            }
            // Load User Id By Patient Id 
           // BaseUserDTO user = UserServiceClient.LoadPatientBaseUserInfoByPatientIdForCompleteProfile(PatientId);

            List<PatientAllergyDTO> allergyHistoryList = UserServiceClient.LoadPatientAllergyInfoByPatientIdForCompleteProfile(PatientId);

            if (allergyHistoryList != null)
            {

                foreach (PatientAllergyDTO src in allergyHistoryList)
                    {
                        AllergyDTO dest = new AllergyDTO();

                    
                        dest.PatientAllergyId = src.PatientHistoryAllergyId;
                        if (src.AllergyType != null)
                        dest.AllergyName = src.AllergyType.AllergyName;
                        dest.Reaction = src.Reaction;
                        dest.IsDeleted = src.IsDeleted;
                        dest.IsDirty = src.IsDirty;
                        dest.IsActive = src.IsActive;

                        if (lst == null)
                            lst = new List<AllergyDTO>();
                        
                        lst.Add(dest);
                    }
                
            }
        }

        /// <summary>
        /// Author Ismail
        /// Date 1 April 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        public PatientVisitResponse LoadPatientVisitByClinicBranchId(int VisitId, int ClinicBranchId)
        {
            PatientVisitResponse response = null;
            List<VisitDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVisitByClinicBranchId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientVisitByClinicBranchId Call TO VisitService");

                lst = clientVisit.LoadPatientVisitByClinicBranchId(VisitId, ClinicBranchId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientVisitResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 1 April 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        public PatientVitalResponse LoadPatientVitals(int PatientId, int ClinicBranchId)
        {
            PatientVitalResponse response = null;
            List<VitalDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVitals");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientVitals Call TO VisitService");

                lst = clientVisit.LoadPatientVitals(PatientId, ClinicBranchId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientVitalResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient presription by visit by Id
        /// </summary>
        /// <returns></returns>
        public PatientPrescriptionResponse LoadPateintPrescriptionByVisitId(int VisitId)
        {
            PatientPrescriptionResponse response = null;
            List<PrescriptionDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPateintPrescriptionByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPateintPrescriptionByVisitId Call TO VisitService");

                lst = clientVisit.LoadPateintPrescriptionByVisitId(VisitId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientPrescriptionResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load presription medication by patient prescription Id
        /// </summary>
        /// <returns></returns>

        public PatientPrescriptionMedicationResponse LoadPrescriptionMedicationByPatientPrescriptionId(int PatientPrescriptionId)
        {
            PatientPrescriptionMedicationResponse response = null;
            List<PrescriptionMedicationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPrescriptionMedicationByPatientPrescriptionId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPrescriptionMedicationByPatientPrescriptionId Call TO VisitService");

                lst = clientVisit.LoadPrescriptionMedicationByPatientPrescriptionId(PatientPrescriptionId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientPrescriptionMedicationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 07 April 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns>

        public PatientMedicationReponse LoadPatientMedicationByPatientId(int PatientId)
        {
            PatientMedicationReponse response = null;
            List<MedicationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientMedicationByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientMedicationByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientMedicationByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientMedicationReponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>

        public PatientDiagnosisResponse LoadPatientDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientDiagnosisResponse response = null;
            List<PatientDiagnosisDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientDiagnosisByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientDiagnosisByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientDiagnosisByPatientId(PatientId, ClinicBranchId,  stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientDiagnosisResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.PatientDiagnosisList = lst;
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
        /// Author Sami
        /// Date 8/12/2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>

        public PatientDiagnosisResponse LoadPatientDiagnosisByPatientIdANDForRecommendation(int PatientId, int ClinicBranchId)
        {
            PatientDiagnosisResponse response = null;
            List<PatientDiagnosisDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientDiagnosisByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientDiagnosisByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientDiagnosisByPatientIdANDForRecommendation(PatientId, ClinicBranchId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientDiagnosisResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.PatientDiagnosisList = lst;
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
        /// Author Atta
        /// Date 14 April 2011
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public PatientFollowupInstructionResponse LoadPatientFollowUpInstructionByVisitId(int visitId)
        {
            PatientFollowupInstructionResponse response = null;
            List<PatientFollowupInstructionDTO> PatientFollowupInstructionList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientFollowUpByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientFollowUpByVisitId Call TO VisitService");

                PatientFollowupInstructionList = clientVisit.LoadFollowupInstructionDTOByVisitId(visitId);

                //QuivusLogger.Info("Response Recieved  HistoryList::" + followUp);

                if (response == null)
                {
                    response = new PatientFollowupInstructionResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (PatientFollowupInstructionList != null)
                {
                    response.PatientFollowupInstructionList = PatientFollowupInstructionList;
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
        /// Author Atta
        /// Date 14 April 2011
        /// This Method Will be used to load presription medication by patient prescription Id
        /// </summary>
        /// <returns></returns>

        public PatientPrescriptionMedicationResponse LoadPrescriptionMedicationByPatientId(int PatientId, int ClinicalBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientPrescriptionMedicationResponse response = null;
            List<PrescriptionMedicationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPrescriptionMedicationByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPrescriptionMedicationByPatientId Call TO VisitService");

                lst = clientVisit.LoadPrescriptionMedicationByPatientId(PatientId, ClinicalBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientPrescriptionMedicationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Atta
        /// Date 14 April 2011
        /// This Method Will be used to load presription medication by patient prescription Id
        /// </summary>
        /// <returns></returns>

        public FollowupLabsResponse LoadPatientLabTestsForFollowupByPatient(int PatientId)
        {
            FollowupLabsResponse response = null;
            List<FollowupLabsDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientLabTestsForFollowupByPatient");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientLabTestsForFollowupByPatient Call TO VisitService");

                lst = clientVisit.LoadPatientLabTestsForFollowupByPatient(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new FollowupLabsResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by visit Id
        /// </summary>
        /// <returns></returns>
        public PatientInHouseLabResponse LoadPateintInHouseLabsByVisitId(int VisitId)
        {
            PatientInHouseLabResponse response = null;
            List<InHouseLabDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPateintInHouseLabsByVisitId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPateintInHouseLabsByVisitId Call TO VisitService");

                lst = clientVisit.LoadPateintInHouseLabsByVisitId(VisitId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientInHouseLabResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientInHouseLabResponse LoadPateintInHouseLabsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientInHouseLabResponse response = null;
            List<InHouseLabDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPateintInHouseLabsByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPateintInHouseLabsByVisitId Call TO VisitService");

                lst = clientVisit.LoadPateintInHouseLabsByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientInHouseLabResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient vital by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientVitalResponse LoadVitalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientVitalResponse response = null;
            List<VitalDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadVitalsByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadVitalsByPatientId Call TO VisitService");

                lst = clientVisit.LoadVitalsByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientVitalResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientHPIResponse LoadHPIByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientHPIResponse response = null;
            List<HPI_CCDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadHPIByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadHPIByPatientId Call TO VisitService");

                lst = clientVisit.LoadHPIByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientHPIResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.HPIList = lst;
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

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientROSResponse LoadROSByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientROSResponse response = null;
            List<ROSDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadROSByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadROSByPatientId Call TO VisitService");

                lst = clientVisit.LoadROSByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientROSResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientPhysicalResponse LoadPhysicalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientPhysicalResponse response = null;
            List<PhysicalsDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPhysicalsByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPhysicalsByPatientId Call TO VisitService");

                lst = clientVisit.LoadPhysicalsByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientPhysicalResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public PatientDiagnosisResponse LoadPatientResolvedDiagnosisByPatientId(int PatientId)
        {
            PatientDiagnosisResponse response = null;
            List<PatientDiagnosisDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientResolvedDiagnosisByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientResolvedDiagnosisByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientResolvedDiagnosisByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientDiagnosisResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.PatientDiagnosisList = lst;
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
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public PatientDiagnosisResponse LoadPatientChronicDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientDiagnosisResponse response = null;
            List<PatientDiagnosisDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientChronicDiagnosisByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientChronicDiagnosisByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientChronicDiagnosisByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientDiagnosisResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.PatientDiagnosisList = lst;
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
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public PatientDiagnosisResponse LoadPatientAcuteDiagnosisByPatientId(int PatientId)
        {
            PatientDiagnosisResponse response = null;
            List<PatientDiagnosisDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientAcuteDiagnosisByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientAcuteDiagnosisByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientAcuteDiagnosisByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientDiagnosisResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.PatientDiagnosisList = lst;
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
        /// Author Ismail
        /// Date 5 may 2011
        /// This Method Will be used to load patient chronic diagnosis by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientProcedureResponse LoadPatientProcedureByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientProcedureResponse response = null;
            List<ProcedureDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientProcedureByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientProcedureByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientProcedureByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientProcedureResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Muhammad Ismail 
        /// Date 06 May 2011
        /// This Method Will be used to import visit By template
        /// </summary>
        /// <returns></returns>

        public QuickInputResponse ImportVisitByTemplate(int TemplateId, int ClinicBranchId)
        {
            QuickInputResponse response = null;
            QuickInputDTO quickInputObj = null;
            try
            {
                QuivusLogger.Info("Calling ImportVisitByTemplate");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ImportVisitByTemplate Call TO VisitService");

                quickInputObj = clientVisit.ImportVisitByTemplate(TemplateId, ClinicBranchId);

                QuivusLogger.Info("Response Recieved  quickInputObj::" + quickInputObj);

                if (response == null)
                {
                    response = new QuickInputResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (quickInputObj != null)
                {
                    response.ResponseData = quickInputObj;
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

        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient medical history by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientMedicalHistoryResponse LoadMedicalHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientMedicalHistoryResponse response = null;
            List<MedicalHistoryDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadMedicalHistoryByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadMedicalHistoryByPatientId Call TO VisitService");

                lst = clientVisit.LoadMedicalHistoryByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientMedicalHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientFamilyHistoryResponse LoadFamilyHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientFamilyHistoryResponse response = null;
            List<FamilyHistoryDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadFamilyHistoryByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadFamilyHistoryByPatientId Call TO VisitService");

                lst = clientVisit.LoadFamilyHistoryByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientFamilyHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    // Load RelationSHipType Data 
                    List<RelationshipTypeDTO> RelationShipList = QApplicationDataCache.RELATIONSHIP_INFO_CACHE_DATA;
                    if (RelationShipList != null)
                    {
                        foreach (FamilyHistoryDTO Obj in lst)
                        {
                            var CacheQuery =
                             from d in RelationShipList
                             where d.RelationshipId.Equals(Obj.ReletionshipTypeID)
                             select d;

                            if (CacheQuery != null && CacheQuery.ToArray().Length > 0)
                            {
                                RelationshipTypeDTO resultObj = CacheQuery.First();
                                Obj.ReletionshipTypeName = resultObj.RelationshipName;
                            }
                        }
                    }
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient social history by patient Id
        /// </summary>
        /// <returns></returns>
        public SocialHistoryResponse LoadPatientSocialHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            SocialHistoryResponse response = null;
            List<SocialHistoryDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientSocialHistoryByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientSocialHistoryByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientSocialHistoryByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.SocialHistoryList = lst;
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
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientRecommendationResponse LoadPatientRecommendationByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientRecommendationResponse response = null;
            List<PatientRecommendationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientRecommendationByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientRecommendationByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientRecommendationByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientRecommendationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.RecommendationList = lst;
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
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientFollowUpResponse LoadPatientFollowUpByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientFollowUpResponse response = null;
            List<PatientFollowUpDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientFollowUpByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientFollowUpByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientFollowUpByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientFollowUpResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Muhammad Ismail 
        /// Date 10 May 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>
        public PatientLabResponse LoadPatientLabsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            PatientLabResponse response = null;
            List<LabDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientLabsByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientLabsByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientLabsByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientLabResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module dictation by patrint Id
        /// </summary>
        /// <returns></returns>

        public ModuleDictationResponse LoadModuleDictationByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            ModuleDictationResponse response = null;
            List<ModuleDictationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleDictationByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadModuleDictationByPatientId Call TO VisitService");

                lst = clientVisit.LoadModuleDictationByPatientId(PatientId, clinicBranchID, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new ModuleDictationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>

        public ModuleNotesResponse LoadModuleNotesByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            ModuleNotesResponse response = null;
            List<ModuleNoteDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleNotesByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadModuleNotesByPatientId Call TO VisitService");

                lst = clientVisit.LoadModuleNotesByPatientId(PatientId, clinicBranchID, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new ModuleNotesResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.ResponseList = lst;
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
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public PatientFollowupInstructionResponse LoadFollowupInstructionByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            PatientFollowupInstructionResponse response = null;
            List<PatientFollowupInstructionDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowupInstructionByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadFollowupInstructionByPatientId Call TO VisitService");

                lst = clientVisit.LoadFollowupInstructionByPatientId(PatientId, clinicBranchID, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new PatientFollowupInstructionResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.PatientFollowupInstructionList = lst;
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
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public SocialHistoryVBResponse LoadSocialHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            SocialHistoryVBResponse response = null;
            List<SocialhistoryVBDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadSocialHistoryByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadSocialHistoryByPatientId Call TO VisitService");

                lst = clientVisit.LoadSocialHistoryByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    response.SocialHistoryList = lst;
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
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>                
        public List<AllergyDTO> LoadPatientAllergyAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<AllergyDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientAllergyAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientAllergyAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientAllergyByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient medical history by patient Id
        /// </summary>
        /// <returns></returns>        
        public List<MedicalHistoryDTO> LoadMedicalHistoryAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<MedicalHistoryDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientAllergyAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadMedicalHistoryAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadMedicalHistoryAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>        
        public List<FamilyHistoryDTO> LoadFamilyHistoryAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<FamilyHistoryDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientAllergyAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadFamilyHistoryAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadFamilyHistoryAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>    
        public List<SocialhistoryVBDTO> LoadSocialHistoryAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<SocialhistoryVBDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadSocialHistoryAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadSocialHistoryAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadSocialHistoryAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        public List<MedicationDTO> LoadPatientMedicationAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<MedicationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientMedicationByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientMedicationByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientMedicationByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public List<ImmunizationDTO> LoadPatientImmunizationAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<ImmunizationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientImmunizationAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientImmunizationAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientImmunizationAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        public List<VitalDTO> LoadPatientVitalAll(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<VitalDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVitalAll");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientVitalAll Call TO VisitService");

                lst = clientVisit.LoadPatientVitalAll(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>
        public List<HPI_CCDTO> LoadHPIAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<HPI_CCDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadHPIAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadHPIAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadHPIAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>
        public List<ROSDTO> LoadROSAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<ROSDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadROSAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadROSAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadROSAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>
        public List<PhysicalsDTO> LoadPhysicalsAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<PhysicalsDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPhysicalsAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPhysicalsAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPhysicalsAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient module dictation by patrint Id
        /// </summary>
        /// <returns></returns>
        public List<ModuleDictationDTO> LoadModuleDictationAllByPatientId(int PatientId)
        {            
            SocialHistoryVBResponse response = null;
            List<ModuleDictationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleDictationAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadModuleDictationAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadModuleDictationAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }
        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>
        public List<ModuleNoteDTO> LoadModuleNotesAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<ModuleNoteDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleNotesAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadModuleNotesAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadModuleNotesAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>       
        public List<InHouseLabDTO> LoadPatientInHouseLabsAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<InHouseLabDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientInHouseLabsAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientInHouseLabsAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientInHouseLabsAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>       
        public List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<LabDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientLabsAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientLabsAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientLabsAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientDiagnosisDTO> LoadPatientResolvedDiagnosisAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<PatientDiagnosisDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientResolvedDiagnosisAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientResolvedDiagnosisAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientResolvedDiagnosisByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<PatientDiagnosisDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientChronicDiagnosisAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientChronicDiagnosisAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientChronicDiagnosisAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientDiagnosisDTO> LoadPatientAcuteDiagnosisAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<PatientDiagnosisDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientAcuteDiagnosisAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientAcuteDiagnosisAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientAcuteDiagnosisByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient procedure by patient Id
        /// </summary>
        /// <returns></returns>       
        public List<ProcedureDTO> LoadPatientProcedureAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<ProcedureDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientProcedureAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientProcedureAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientProcedureAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>       
        public List<PrescriptionMedicationDTO> LoadPrescriptionMedicationAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<PrescriptionMedicationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPrescriptionMedicationAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPrescriptionMedicationAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPrescriptionMedicationAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientRecommendationDTO> LoadPatientRecommendationAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<PatientRecommendationDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientRecommendationAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientRecommendationAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientRecommendationAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 26 Sep 2011
        /// This Method Will be used to load patient FollowUp by patient Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientFollowUpDTO> LoadPatientFollowUpAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<PatientFollowUpDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientFollowUpAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPatientFollowUpAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadPatientFollowUpAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }

        public List<PatientFollowupInstructionDTO> LoadFollowupInstructionAllByPatientId(int PatientId)
        {
            SocialHistoryVBResponse response = null;
            List<PatientFollowupInstructionDTO> lst = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowupInstructionAllByPatientId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadFollowupInstructionAllByPatientId Call TO VisitService");

                lst = clientVisit.LoadFollowupInstructionAllByPatientId(PatientId);

                QuivusLogger.Info("Response Recieved  lst::" + lst);

                if (response == null)
                {
                    response = new SocialHistoryVBResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (lst != null)
                {
                    //response.SocialHistoryList = lst;
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
            return lst;
        }


    }
}
