using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.IntegratedServies.ReferenceFactory;
using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;

using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;
using QuivusManager.IntegratedServies.AddressBookService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Image;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;

namespace QuivusManager.Buisness.StaticDataBL.Impl
{
    public class StaticDataBLImpl : IStaticDataBL
    {
        VisitServiceClient clientVisit = null;

        UserManagerServiceClient UserServiceClient = null;
        AddressBookServiceClient AddressBookClient = null;
        VisitServiceClient VisitClient = null;
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// </summary>
        public SearchEngineResponse LoadAllSearchEngines()
        {
            SearchEngineResponse response = null;
            List<SearchEngineDTO> searchEngineList = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadAllSearchEngines Call TO User Service");

                searchEngineList = UserServiceClient.LoadAllSearchEngines();

                QuivusLogger.Info("Response Recieved searchEngineList::" + searchEngineList);

                if (response == null)
                {
                    response = new SearchEngineResponse();
                }
                if (searchEngineList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = searchEngineList;
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
        /// Date 19 Jan 2011
        /// Return all static data required for registeration
        /// </summary>
        /// <returns></returns>
        public RegistrationStaticDataResponse GetRegisterationStaticData()
        {
            RegistrationStaticDataResponse response = null;
            RegisterationStaticDataDTO DataDTO = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                QuivusLogger.Info("Dispatching GetRegisterationStaticData Call TO User Service");

                DataDTO = UserServiceClient.GetRegisterationStaticData();

                QuivusLogger.Info("Response Recieved DataDTO::" + DataDTO);

                if (response == null)
                {
                    response = new RegistrationStaticDataResponse();
                }
                if (DataDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = DataDTO;
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
        /// Date 21 JAn 2011
        /// To fetch the list of 
        /// </summary>
        /// <param name="allergyName"></param>
        /// <returns></returns>
        public AllergyTypeResponse SearchAllergyTypeByName(string allergyName)
        {
            AllergyTypeResponse response = null;
            List<AllergySearchDTO> AllergyList = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetRegisterationStaticData Call TO User Service");

                AllergyList = UserServiceClient.SearchAllergyTypeByName(allergyName);

                QuivusLogger.Info("Response Recieved AllergyList::" + AllergyList);

                if (response == null)
                {
                    response = new AllergyTypeResponse();
                }
                if (AllergyList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = AllergyList;
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
        /// Date 19 JAn 2011
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public CityResponse GetCityListByState(int stateId)
        {
            CityResponse response = null;
            List<CityDTO> CityList = null;

            try
            {
                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                QuivusLogger.Info("Dispatching GetRegisterationStaticData Call TO AddressBook Service");

                CityList = AddressBookClient.ListCities(stateId);

                QuivusLogger.Info("Response Recieved CityList::" + CityList);

                if (response == null)
                {
                    response = new CityResponse();
                }
                if (CityList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = CityList;
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
        /// Date 20 Jan 2011
        /// </summary>
        /// <returns></returns>
        public AddressBookStaticDataResponse GetAddressBookStaticData()
        {
            AddressBookStaticDataResponse response = null;
            AddressBookStaticDataDTO AddressDTO = null;

            try
            {
                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                QuivusLogger.Info("Dispatching GetAddressBookStaticData Call TO AddressBook Service");

                AddressDTO = AddressBookClient.GetAddressBookStaticData();

                QuivusLogger.Info("Response Recieved AddressDTO::" + AddressDTO);

                if (response == null)
                {
                    response = new AddressBookStaticDataResponse();
                }
                if (AddressDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = AddressDTO;
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
        /// Date 20 Jan 2011
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName)
        {
            ZipCodeResponse response = null;
            List<ZipCodeDTO> ZipList = null;

            try
            {
                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                QuivusLogger.Info("Dispatching GetAddressBookStaticData Call TO AddressBook Service");

                ZipList = AddressBookClient.ListZipCodesByStateIdAndCityName(stateId, cityName);

                QuivusLogger.Info("Response Recieved ZipList::" + ZipList);

                if (response == null)
                {
                    response = new ZipCodeResponse();
                }
                if (ZipList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = ZipList;
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
        /// Date 21 Jan 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        public ICDCodeResponse SearchICDCodeByCode(string icdCode)
        {
            ICDCodeResponse response = null;
            List<ICDCodeSearchDTO> IcdCodeList = null;

            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching SearchICDCodeByCode Call TO User Service");

                IcdCodeList = UserServiceClient.SearchICDCodeByCode(icdCode);

                QuivusLogger.Info("Response Recieved IcdCodeList::" + IcdCodeList);

                if (response == null)
                {
                    response = new ICDCodeResponse();
                }
                if (IcdCodeList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = IcdCodeList;
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
        /// Date 19 Jan 2011
        /// Return static data required for registeration, based on type provided
        /// </summary>
        /// <returns></returns>
        public RegistrationStaticDataResponse GetRegisterationStaticDataByType(List<string> typeList)
        {
            RegistrationStaticDataResponse response = null;
            RegisterationStaticDataDTO RegistrationDTO = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching GetRegisterationStaticDataByType Call TO User Service");

                RegistrationDTO = UserServiceClient.GetRegisterationStaticDataByType(typeList);

                QuivusLogger.Info("Response Recieved RegistrationDTO::" + RegistrationDTO);

                if (response == null)
                {
                    response = new RegistrationStaticDataResponse();
                }
                if (RegistrationDTO != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = RegistrationDTO;
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
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        public ProfileStaticDataResponse LoadProfileStaticData()
        {
            ProfileStaticDataResponse response = null;
            List<ProfileStaticDataDTO> ProfileStaticList = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadProfileStaticData Call TO User Service");

                ProfileStaticList = UserServiceClient.LoadProfileStaticData();

                QuivusLogger.Info("Response Recieved ProfileStaticList::" + ProfileStaticList);

                if (response == null)
                {
                    response = new ProfileStaticDataResponse();
                }
                if (ProfileStaticList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = ProfileStaticList;
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
        /// Author Muhammad ismail
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        public ImageTypeResponse LoadImageTypes()
        {
            ImageTypeResponse response = null;
            List<ImageTypeDTO> ImageTypes = null;
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadImageTypes Call TO User Service");
                ImageTypes = UserServiceClient.LoadImageTypes();

                QuivusLogger.Info("Response Recieved ImageTypes::" + ImageTypes);

                if (response == null)
                {
                    response = new ImageTypeResponse();
                }
                if (ImageTypes != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = ImageTypes;
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
        /// Date 22 Feb 2011
        /// get the module types for notes etc
        /// </summary> 
        //public ModuleTypeResponse LoadAllModuleTypes()
        //{
        //    ModuleTypeResponse response = null;
        //    List<QuivusManager.IntegratedServies.UserManagerService.ModuleTypeDTO> ModuleTypes = null;
        //    try
        //    {
        //        if (UserServiceClient == null)
        //        {
        //            UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
        //        }

        //        QuivusLogger.Info("Dispatching LoadModuleTypes Call TO User Service");

        //        ModuleTypes = UserServiceClient.LoadModuleTypes();

        //        QuivusLogger.Info("Response Recieved ModuleTypes::" + ModuleTypes);

        //        if (response == null)
        //        {
        //            response = new ModuleTypeResponse();
        //        }
        //        if (ModuleTypes != null)
        //        {
        //            response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
        //            response.ModuleTypeData = ModuleTypes;
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

        public DiseaseTypeResponse LoadDiseaseTypes(int ClinicBranchID, int FamilyHistoryTypeID)
        {
            DiseaseTypeResponse response = null;
            List<DiseaseTypeDTO> DiseaseTypes = null;
            try
            {
                if (VisitClient == null)
                {
                    VisitClient = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadDiseaseTypes Call TO User Service");

                DiseaseTypes = VisitClient.LoadDiseaseTypes(ClinicBranchID, FamilyHistoryTypeID);

                QuivusLogger.Info("Response Recieved DiseaseTypes::" + DiseaseTypes);

                if (response == null)
                {
                    response = new DiseaseTypeResponse();
                }
                if (DiseaseTypes != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.DiseaseList = DiseaseTypes;
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

        public LabTestTypeResponse LoadLabTestTypes()
        {
            LabTestTypeResponse response = null;
            List<LabTestTypeDTO> TypeList = null;
            try
            {
                if (VisitClient == null)
                {
                    VisitClient = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadLabTestTypes Call TO User Service");

                TypeList = VisitClient.LoadLabTestTypes();

                QuivusLogger.Info("Response Recieved TypeList::" + TypeList);

                if (response == null)
                {
                    response = new LabTestTypeResponse();
                }
                if (TypeList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseList = TypeList;
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

        public LabTestPriorityTypeResponse LoadLabTestPriorityTypes()
        {
            LabTestPriorityTypeResponse response = null;
            List<LabTestPriorityTypeDTO> TypeList = null;
            try
            {
                if (VisitClient == null)
                {
                    VisitClient = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadLabTestPriorityTypes Call TO User Service");

                TypeList = VisitClient.LoadLabTestPriorityTypes();

                QuivusLogger.Info("Response Recieved TypeList::" + TypeList);

                if (response == null)
                {
                    response = new LabTestPriorityTypeResponse();
                }
                if (TypeList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseList = TypeList;
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

        public LabNatureTypeResponse LoadLabNatureTypes()
        {
            LabNatureTypeResponse response = null;
            List<LabNatureTypeDTO> TypeList = null;
            try
            {
                if (VisitClient == null)
                {
                    VisitClient = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadLabNatureTypes Call TO User Service");

                TypeList = VisitClient.LoadLabNatureTypes();

                QuivusLogger.Info("Response Recieved TypeList::" + TypeList);

                if (response == null)
                {
                    response = new LabNatureTypeResponse();
                }
                if (TypeList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseList = TypeList;
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

        public LabTestSpecimenTypeResponse LoadLabTestSpecimenTypes()
        {
            LabTestSpecimenTypeResponse response = null;
            List<LabTestSpecimenTypeDTO> TypeList = null;
            try
            {
                if (VisitClient == null)
                {
                    VisitClient = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadLabTestSpecimenTypes Call TO User Service");

                TypeList = VisitClient.LoadLabTestSpecimenTypes();

                QuivusLogger.Info("Response Recieved TypeList::" + TypeList);

                if (response == null)
                {
                    response = new LabTestSpecimenTypeResponse();
                }
                if (TypeList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseList = TypeList;
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

        public LabTypeResponse LoadLabTypes()
        {
            LabTypeResponse response = null;
            List<LabTypeDTO> TypeList = null;
            try
            {
                if (VisitClient == null)
                {
                    VisitClient = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadLabTypes Call TO User Service");

                TypeList = VisitClient.LoadLabTypes();

                QuivusLogger.Info("Response Recieved TypeList::" + TypeList);

                if (response == null)
                {
                    response = new LabTypeResponse();
                }
                if (TypeList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseList = TypeList;
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

        public LabTestOrderMethodTypeResponse LoadLabTestOrderMethodTypes()
        {
            LabTestOrderMethodTypeResponse response = null;
            List<LabTestOrderMethodTypeDTO> TypeList = null;
            try
            {
                if (VisitClient == null)
                {
                    VisitClient = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadLabTestOrderMethodTypes Call TO User Service");

                TypeList = VisitClient.LoadLabTestOrderMethodTypes();

                QuivusLogger.Info("Response Recieved TypeList::" + TypeList);

                if (response == null)
                {
                    response = new LabTestOrderMethodTypeResponse();
                }
                if (TypeList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseList = TypeList;
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

        public ICDCodeResponse LoadICDCodeByName(string queryString)
        {
            ICDCodeResponse response = null;
            List<QuivusManager.IntegratedServies.VisitService.ICDCodeDTO> TypeList = null;
            try
            {
                if (VisitClient == null)
                {
                    VisitClient = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadICDCodeByName Call TO User Service");

                TypeList = VisitClient.LoadICDCodeByName(queryString);

                QuivusLogger.Info("Response Recieved TypeList::" + TypeList);

                if (response == null)
                {
                    response = new ICDCodeResponse();
                }
                if (TypeList != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseList = TypeList;
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
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns> 
        public EncounterTypeResponse LoadEncounterTypes()
        {
            EncounterTypeResponse response = null;
            List<EncounterTypeDTO> list = null;
            try
            {
                QuivusLogger.Info("Calling LoadEncounterTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadEncounterTypes Call TO VisitService");

                list = clientVisit.LoadEncounterTypes();

                QuivusLogger.Info("Response Recieved  list::" + list);

                if (response == null)
                {
                    response = new EncounterTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.EncounterTypeList = list;
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
        /// get immunization types static data
        /// </summary>        
        /// <returns></returns>
        public ImmunizationTypesResponse LoadImmunizationTypes()
        {
            ImmunizationTypesResponse response = null;
            List<ImmunizationTypeDTO> ImmunzationTypeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadImmunizationTypes Call TO VisitService");

                ImmunzationTypeList = clientVisit.LoadImmunizationTypes();

                QuivusLogger.Info("Response Recieved  ImmunzationTypeList::" + ImmunzationTypeList);

                if (response == null)
                {
                    response = new ImmunizationTypesResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.ImmunzationTypeList = ImmunzationTypeList;
            }
            catch
            {
                throw;
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
        public MedicalHistoryTypeResponse LoadMedicalHistoryTypes()
        {
            MedicalHistoryTypeResponse response = null;
            List<MedicalHistoryTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadMedicalHistoryTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadMedicalHistoryTypes Call TO VisitService");

                HistoryList = clientVisit.LoadMedicalHistoryTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new MedicalHistoryTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (HistoryList != null)
                {
                    response.MedicalHistoryList = HistoryList;
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
        /// Date 13 March 2011
        /// This Method Will be used to load Family History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        /// 
        public FamilyHistoryTypeResponse LoadFamilyHistoryTypes()
        {
            FamilyHistoryTypeResponse response = null;
            List<FamilyHistoryTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFamilyHistoryTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadFamilyHistoryTypes Call TO VisitService");

                HistoryList = clientVisit.LoadFamilyHistoryTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new FamilyHistoryTypeResponse();
                }

                if (HistoryList != null)
                {
                    response.FamilyHistoryList = HistoryList;
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
        /// This Method Will be used to load ROS Main Category
        /// </summary>
        /// <returns></returns>
        public ROSMainCategoryTypeResponse LoadROSMainCategoryTypes(bool IsMale)
        {
            ROSMainCategoryTypeResponse response = null;
            List<ROSMainCategoryTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadROSMainCategoryTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadROSMainCategoryTypes Call TO VisitService");

                HistoryList = clientVisit.LoadROSMainCategoryTypes(IsMale);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new ROSMainCategoryTypeResponse();
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
        /// This Method Will be used to load ROS Sub Category
        /// </summary>
        /// <returns></returns>
        public ROSSubCategoryTypeResponse LoadROSSubCategoryTypes()
        {
            ROSSubCategoryTypeResponse response = null;
            List<ROSSubCategoryTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadROSSubCategoryTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadROSSubCategoryTypes Call TO VisitService");

                HistoryList = clientVisit.LoadROSSubCategoryTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new ROSSubCategoryTypeResponse();
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

        //Commented by Mansoor
        ///// <summary>
        ///// Author Muhammad Ismail 
        ///// Date 21 March 2011
        ///// This Method Will be used to load HPI Data Control Types
        ///// </summary>
        ///// <returns></returns>
        //public HPIDataControlTypeResponse LoadHPIDataControlTypes()
        //{
        //    HPIDataControlTypeResponse response = null;
        //    List<HPIDataControlTypeDTO> HistoryList = null;
        //    try
        //    {
        //        QuivusLogger.Info("Calling LoadHPIDataControlTypes");

        //        if (clientVisit == null)
        //        {
        //            clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
        //        }

        //        QuivusLogger.Info("Calling service :: VisitService");

        //        QuivusLogger.Info("Dispatching LoadHPIDataControlTypes Call TO VisitService");

        //        HistoryList = clientVisit.LoadHPIDataControlTypes();

        //        QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

        //        if (response == null)
        //        {
        //            response = new HPIDataControlTypeResponse();
        //            response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
        //        }

        //        if (HistoryList != null)
        //        {
        //            response.HPIControlTypeList = HistoryList;
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


        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to load HPI detail types 
        /// </summary>
        /// <returns></returns>
        public HPIDetailTypeResponse LoadHPIDetailTypes()
        {
            HPIDetailTypeResponse response = null;
            List<HPITypeDTO> hpiTypeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadHPIDetailTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadHPIDetailTypes Call TO VisitService");

                hpiTypeList = clientVisit.LoadHPIDetailTypes();

                QuivusLogger.Info("Response Recieved  hpiTypeList::" + hpiTypeList);

                if (response == null)
                {
                    response = new HPIDetailTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (hpiTypeList != null)
                {
                    response.ResponseList = hpiTypeList;
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

        public SocialHistoryAddictionTypeResponse LoadSocialHistoryAddictionTypes()
        {
            SocialHistoryAddictionTypeResponse response = null;
            List<SocialHistoryAddictionTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadSocialHistoryAddictionTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadSocialHistoryAddictionTypes Call TO VisitService");

                HistoryList = clientVisit.LoadSocialHistoryAddictionTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new SocialHistoryAddictionTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.SocialHistoryAddictionList = HistoryList;
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

        public SocialMaritalStatusTypeResponse LoadMaritalStatusTypes()
        {
            SocialMaritalStatusTypeResponse response = null;
            List<SocialMaritalStatusTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadMaritalStatusTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadMaritalStatusTypes Call TO VisitService");

                HistoryList = clientVisit.LoadMaritalStatusTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new SocialMaritalStatusTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.SocialHistoryMaritalList = HistoryList;
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


        public SocialHistoryUsageAmountTypeResponse LoadSocialHistoryUsageAmountTypes()
        {
            SocialHistoryUsageAmountTypeResponse response = null;
            List<SocialHistoryUsageAmountTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadSocialHistoryUsageAmountTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadSocialHistoryUsageAmountTypes Call TO VisitService");

                HistoryList = clientVisit.LoadSocialHistoryUsageAmountTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new SocialHistoryUsageAmountTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.ResponseList = HistoryList;

                    // Split List In to three as requested 

                    SplitSocialHistoryUsageAmountList(response, HistoryList);

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

        private static void SplitSocialHistoryUsageAmountList(SocialHistoryUsageAmountTypeResponse response, List<SocialHistoryUsageAmountTypeDTO> HistoryList)
        {
            var alcoholQuery = from alcohol in HistoryList
                               where alcohol.AddictionName.Trim().ToLower().Equals(ProjectConstant.ADDICTION_ALCOHOL)
                               select alcohol;
            if (alcoholQuery != null)
            {
                response.AlcoholList = alcoholQuery.ToList();
            }

            var tobaccoQuery = from tobacco in HistoryList
                               where tobacco.AddictionName.Trim().ToLower().Equals(ProjectConstant.ADDICTION_TOBACCO)
                               select tobacco;
            if (tobaccoQuery != null)
            {
                response.TobacooList = tobaccoQuery.ToList();
            }

            var drugQuery = from drug in HistoryList
                            where drug.AddictionName.Trim().ToLower().Equals(ProjectConstant.ADDICTION_DRUGS)
                            select drug;
            if (drugQuery != null)
            {
                response.DrugsList = drugQuery.ToList();
            }
        }

        public SocialHistoryMediumTypeResponse LoadSocialHistoryMediumTypes()
        {
            SocialHistoryMediumTypeResponse response = null;
            List<SocialHistoryMediumTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadSocialHistoryMediumTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadSocialHistoryMediumTypes Call TO VisitService");

                HistoryList = clientVisit.LoadSocialHistoryMediumTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new SocialHistoryMediumTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (HistoryList != null)
                {
                    response.ResponseList = HistoryList;

                    // Split List In to three as requested 
                    SplitSocialHistoryMediumTypes(response, HistoryList);

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

        private static void SplitSocialHistoryMediumTypes(SocialHistoryMediumTypeResponse response, List<SocialHistoryMediumTypeDTO> HistoryList)
        {
            var alcoholQuery = from alcohol in HistoryList
                               where alcohol.AddictionName.Trim().ToLower().Equals(ProjectConstant.ADDICTION_ALCOHOL)
                               select alcohol;
            if (alcoholQuery != null)
            {
                response.AlcoholList = alcoholQuery.ToList();
            }

            var tobaccoQuery = from tobacco in HistoryList
                               where tobacco.AddictionName.Trim().ToLower().Equals(ProjectConstant.ADDICTION_TOBACCO)
                               select tobacco;
            if (tobaccoQuery != null)
            {
                response.TobaccoList = tobaccoQuery.ToList();
            }

            var drugQuery = from drug in HistoryList
                            where drug.AddictionName.Trim().ToLower().Equals(ProjectConstant.ADDICTION_DRUGS)
                            select drug;
            if (drugQuery != null)
            {
                response.DrugsList = drugQuery.ToList();
            }
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// This Method Will be used to load Plan types 
        /// </summary>
        /// <returns></returns>     
        public PlanTypeResponse LoadAllRecommendationPlanTypes()
        {
            PlanTypeResponse response = null;
            List<PlanTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadAllRecommendationPlanTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadAllRecommendationPlanTypes Call TO VisitService");

                typeList = clientVisit.LoadAllRecommendationPlanTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + typeList);

                if (response == null)
                {
                    response = new PlanTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.PlanTypeList = typeList;
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
        /// Date 22 Mar 2011
        /// This Method Will be used to load Duration types 
        /// </summary>
        /// <returns></returns>    
        public DurationTypeResponse LoadAllRecommendationDurationTypes()
        {
            DurationTypeResponse response = null;
            List<DurationTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadAllRecommendationDurationTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadAllRecommendationDurationTypes Call TO VisitService");

                typeList = clientVisit.LoadAllRecommendationDurationTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + typeList);

                if (response == null)
                {
                    response = new DurationTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.DurationTypeList = typeList;
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
        /// Date 22 Mar 2011
        /// This Method Will be used to load Method types 
        /// </summary>
        /// <returns></returns>      
        public MethodTypeResponse LoadAllRecommendationMethodTypes()
        {
            MethodTypeResponse response = null;
            List<MethodTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadAllRecommendationMethodTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadAllRecommendationMethodTypes Call TO VisitService");

                typeList = clientVisit.LoadAllRecommendationMethodTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + typeList);

                if (response == null)
                {
                    response = new MethodTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.MethodTypeList = typeList;
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


        public PhysicalMainCategoryTypeResponse LoadPhysicalMainCategoryTypes(bool IsMale)
        {
            PhysicalMainCategoryTypeResponse response = null;
            List<PhysicalMainCategoryTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPhysicalMainCategoryTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPhysicalMainCategoryTypes Call TO VisitService");

                HistoryList = clientVisit.LoadPhysicalMainCategoryTypes(IsMale);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new PhysicalMainCategoryTypeResponse();
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

        public PhysicalSubCategoryTypeResponse LoadPhysicalSubCategoryTypes()
        {
            PhysicalSubCategoryTypeResponse response = null;
            List<PhysicalSubCategoryTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPhysicalSubCategoryTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPhysicalSubCategoryTypes Call TO VisitService");

                HistoryList = clientVisit.LoadPhysicalSubCategoryTypes();

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new PhysicalSubCategoryTypeResponse();
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

        public LIONICLabTestTypeResponse LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName)
        {
            LIONICLabTestTypeResponse response = null;
            List<LIONICLabTestTypeDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadLIONICLabTestType");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadLIONICLabTestType Call TO VisitService");

                HistoryList = clientVisit.LoadLIONICLabTestType(LabtestTypeID, LabTestCodeOrName);

                QuivusLogger.Info("Response Recieved  HistoryList::" + HistoryList);

                if (response == null)
                {
                    response = new LIONICLabTestTypeResponse();
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
        /// Date 25 March 2011
        /// This Method Will be used to load Follow Up Method
        /// </summary>
        /// <returns></returns>           
        public FollowUpMethodTypesResponse LoadFollowUpMethodType()
        {
            FollowUpMethodTypesResponse response = null;
            List<FollowUpMethodTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpMethodType");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadFollowUpMethodType Call TO VisitService");

                typeList = clientVisit.LoadFollowUpMethodTypes();

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new FollowUpMethodTypesResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.FolowUpMethodTypeList = typeList;
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
        /// Method Load In House Lab Types By Clinic branch Id
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        public InHouseLabTestTypeResponse LoadInHouseLabTestTypeByClinicBranchId(int ClinicBranchId)
        {
            InHouseLabTestTypeResponse response = null;
            List<InHouseLabTestTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadInHouseLabTestTypeByClinicBranchId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadFollowUpMethodType Call TO VisitService");

                typeList = clientVisit.LoadInHouseLabTestTypeByClinicBranchId(ClinicBranchId);

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new InHouseLabTestTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.ResponseList = typeList;
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
           /// Date 06 april 2011
           /// Method Load CPT Medical Types
           /// </summary>
           /// <returns></returns>
        public CPTMedicalTypeResponse LoadCPTMedicalTypes(String CPTName)
        {
            CPTMedicalTypeResponse response = null;
            List<CPTMedicalTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadCPTMedicalTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadCPTMedicalTypes Call TO VisitService");

                typeList = clientVisit.LoadCPTMedicalTypes(CPTName);

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new CPTMedicalTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.ResponseList = typeList;
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
           /// Date 07 April 2011
           /// This Method Will be used to load ROS  Categories :)
           /// </summary>
           /// <returns></returns>
        public PatientROSResponse LoadROSCategoryTypes(bool IsMale)
        {
            PatientROSResponse response = null;
            List<ROSDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadROSMainCategoryTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadROSMainCategoryTypes Call TO VisitService");

                HistoryList = clientVisit.LoadROSCategoryTypes(IsMale);

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

          /// <summary>
           /// Author Muhammad Ismail 
           /// Date 07 April 2011
           /// This Method Will be used to load ROS Categories :)
           /// </summary>
           /// <returns></returns>
        public PatientPhysicalResponse LoadPhysicalCategoryTypes(bool IsMale)
        {
            PatientPhysicalResponse response = null;
            List<PhysicalsDTO> HistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPhysicalCategoryTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPhysicalCategoryTypes Call TO VisitService");

                HistoryList = clientVisit.LoadPhysicalCategoryTypes(IsMale);

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
        /// Author Atta 
        /// Date 14 April 2011
        /// </summary>
        /// <returns></returns>           
        public InstructionAgeTypeResponse LoadInstructionAgeType()
        {
            InstructionAgeTypeResponse response = null;
            List<InstructionAgeTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpMethodType");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadInstructionAgeType Call TO VisitService");

                typeList = clientVisit.LoadFollowUpInstructionAgeTypes();

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new InstructionAgeTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.InstructionAgeTypeList = typeList;
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
        public InstructionDurationTypeResponse LoadInstructionDurationType()
        {
            InstructionDurationTypeResponse response = null;
            List<InstructionDurationTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpMethodType");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadInstructionAgeType Call TO VisitService");

                typeList = clientVisit.LoadFollowUpInstructionDurationTypes();

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new InstructionDurationTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.InstructionDurationTypeList = typeList;
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
        public ServiceTypeResponse LoadServiceType(int ClinicBranchID)
        {
            ServiceTypeResponse response = null;
            List<ServiceTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowUpMethodType");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadInstructionAgeType Call TO VisitService");

                //M TODO
                //typeList = clientVisit.LoadFollowUpServiceTypes(ClinicBranchID);

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new ServiceTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.ServiceTypeList = typeList;
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
        public InstructionAgeValueTypeResponse LoadInstructionAgeValueType()
        {
            InstructionAgeValueTypeResponse response = null;
            List<InstructionAgeValueTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadInstructionAgeValueType");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadInstructionAgeType Call TO VisitService");

                typeList = clientVisit.LoadFollowUpInstructionAgeValueTypes();

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new InstructionAgeValueTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.InstructionAgeValueTypeList = typeList;
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
           /// Date 4 may 2011
           /// This Method Will be used to load module types Types 
           /// </summary>
           /// <returns></returns>
        public ModuleTypeResponse LoadModuleTypes()
        {
            ModuleTypeResponse response = null;
            List<QuivusManager.IntegratedServies.VisitService.ModuleTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadModuleTypes Call TO VisitService");

                typeList = clientVisit.LoadVisitModuleTypes();

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new ModuleTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.ModuleTypeData = typeList;
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
           /// Date 18 July 2011
           /// This Method Will be used to load Physical Sub Category
           /// </summary>
           /// <returns></returns>
        public PhysicalCategoryTypeResponse LoadPhysicalCategoryType()
        {
            PhysicalCategoryTypeResponse response = null;
            List<PhysicalCategoryTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPhysicalCategoryType Call TO VisitService");

                typeList = clientVisit.LoadPhysicalCategory();

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new PhysicalCategoryTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.ResponseList = typeList;
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
           /// Date 18 July 2011
           /// This Method Will be used to load Physical Sub Category
           /// </summary>
           /// <returns></returns>
        public PhysicalCategoryTypeResponse LoadPhysicalCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly)
        {
            PhysicalCategoryTypeResponse response = null;
            List<PhysicalCategoryTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadPhysicalCategoryTypesByGender Call TO VisitService");

                typeList = clientVisit.LoadPhysicalCategoryTypesByGender(LoadUniversal, LoadMaleOnly, LoadFemaleOnly);

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new PhysicalCategoryTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.ResponseList = typeList;
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
           /// Date 15 July 2011
           /// This Method Will be used to load ROS Sub Level Categories
           /// </summary>
           /// <returns></returns>
        public ROSSubLevelCategoryTypeResponse LoadROSSubLevelCategoryTypes(int SubCategoryId)
        {
            ROSSubLevelCategoryTypeResponse response = null;
            List<ROSSubLevelCategoryTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadROSSubLevelCategoryTypes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadROSSubLevelCategoryTypes Call TO VisitService");

                typeList = clientVisit.LoadROSSubLevelCategoryTypes(SubCategoryId);

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new ROSSubLevelCategoryTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.ResponseList = typeList;
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
           /// Date 18 July 2011
           /// This Method Will be used to load ROS Sub Level Categories By Gender
           /// </summary>
           /// <returns></returns>
        public ROSSubLevelCategoryTypeResponse LoadROSSubLevelCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, int ROSSubCategoryTypeID)
        {
            ROSSubLevelCategoryTypeResponse response = null;
            List<ROSSubLevelCategoryTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadROSSubLevelCategoryTypesByGender");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadROSSubLevelCategoryTypesByGender Call TO VisitService");

                typeList = clientVisit.LoadROSSubLevelCategoryTypesByGender(LoadUniversal,  LoadMaleOnly,  LoadFemaleOnly, ROSSubCategoryTypeID);

                QuivusLogger.Info("Response Recieved  typeList::" + typeList);

                if (response == null)
                {
                    response = new ROSSubLevelCategoryTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (typeList != null)
                {
                    response.ResponseList = typeList;
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
        public FollowupDataControlTypeResponse LoadFollowupDataControlType()
        {
            FollowupDataControlTypeResponse response = null;
            List<FollowupDataControlTypeDTO> typeList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowupDataControlType");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }
                QuivusLogger.Info("Calling service :: VisitService");
                QuivusLogger.Info("Dispatching LoadFollowupDataControlType Call TO VisitService");
                typeList = clientVisit.LoadFollowupDataControlType();
                if (response == null)
                {
                    response = new FollowupDataControlTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                if (typeList != null)
                {
                    response.ResponseData = typeList;
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
        public ServiceTypeResponse LoadPatientFollowupAutoCompleteByTypeIdAndClinicId(int followupDataControlTypeID, int clinicBranchId)
        {
            ServiceTypeResponse response = null;
            List<ServiceTypeDTO> followupList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientFollowupAutoCompleteByTypeIdAndClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }
                QuivusLogger.Info("Calling service :: VisitService");
                QuivusLogger.Info("Dispatching LoadPatientFollowupAutoCompleteByTypeIdAndClinicId Call TO VisitService");
                followupList = clientVisit.LoadPatientFollowupAutoCompleteByTypeIdAndClinicId(followupDataControlTypeID,clinicBranchId);
                if (response == null)
                {
                    response = new ServiceTypeResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                if (followupList != null)
                {
                    response.ServiceTypeList = followupList;
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
        /// Author Baber Waqas
        /// to accomodate custom diagnosis 
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="clinicBranchID"></param>
        /// <returns></returns>
        public ICDCodeResponse LoadICDCodeByName(string queryString, int clinicBranchID)
        {
            ICDCodeResponse response = null;
            try
            {
                if (VisitClient == null)
                {
                    VisitClient = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadICDCodeByName Call TO visit Service");

                List<QuivusManager.IntegratedServies.VisitService.ICDCodeDTO> typeList = VisitClient.LoadICDCodeByNameAndClinicBranch(queryString,clinicBranchID);
                foreach(var item in typeList)
                {
                    //TODO: need to remove this code on production
                    QuivusLogger.Info("Response Recieved TypeList::" + item.IcdCodeName + " and description is " + item.IcdCodeDescription );
                }
                QuivusLogger.Info("Response Recieved TypeList::" + typeList);

               
                    response = new ICDCodeResponse();
               
                if (typeList != null )
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseList = typeList;
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
            // checking response
            return response;
        }
    }
}
