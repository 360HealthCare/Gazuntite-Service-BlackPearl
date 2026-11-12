using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Quivus.Model.User.Patient;
using Quivus.Model.Common;
using Quivus.Model.User;
using Quivus.Model.User;
using QuivusModel.User;
using QuivusModel.User.ClinicUser;
using QuivusModel.Common;
using QuivusModel.Association;
using Quivus.Model.QuickLink;

namespace UserManager.Business.User
{
    public interface IUserManager
    {
        PatientProfileCompleteSettingDTO LoadPatientProfileSettings(int clinicBranchId, int providerId, int PatientId);

        int ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj);

        List<ProfileStaticDataDTO> LoadProfileStaticData();

        String GetUserEmailByUserId(int userId);

        PatientProfileCompleteSettingDTO LoadPatientProfileSettingsBySection(int clinicBranchId, int UserId, int PatientId, int SectionId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param> 
        Guid GetPartyGuidByProfileIdAndTypeId(int roleProfileID, int addressPartyTypeID);

        List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// tell if a user is enrolled as provider on a specific branch
        /// </summary> 
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        bool IsProviderAtClinic(int userId, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// get the RecGuid of respective table, used to load the patient profile customized setting (AB)
        /// </summary>
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns> 
        Guid GetPartyGuidByPatientIdAndPartyTypeId(int patientIdD, int partyTypeID);



        UserVerificationDTO VerifyUser(int UserId, string password);

        Quivus.Model.User.BaseUserDTO LoadBaseUserByUserId(int UserId);

        bool UpdatePatientEPrescriptionDetails(string RCopiaID, int PatientId);

        string LoadQuivusIdByPatientId(int PatientId);

        string LoadRcopiaIdByPatientId(int PatientId);

        QuivusModel.User.Patient.ExternalPatientDTO LoadExternalPatients(int FromRecord, int ToRecord);

        bool UpdatePatientRCopiaId(List<QuivusModel.User.Patient.PatientRcopiaDTO> patientList);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 18/7/2011
        /// Method will return provider List
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        List<ProviderDTO> LoadAllProviderByClinicBranchId(int clinicBranchID);

        Quivus.Model.User.BaseUserDTO LoadPatientBaseUserInfoByPatientIdForCompleteProfile(int patientId);

        List<PatientAllergyDTO> LoadPatientAllergyInfoByPatientIdForCompleteProfile(int patientId);

        /// <summary>
        /// Author Mansoor
        /// Date 23 aug 2011
        /// verify email existance 
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        bool VerifyUserEmail(string email);

        /// <summary>
        /// Author Mansoor
        /// Date 14 sep 2011        
        /// </summary>
        /// <param name="branchCode"></param>
        /// <returns></returns>        
        int GetClinicBranchIdByClinicBranchCode(string branchCode);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>        
        List<ProviderDTO> LoadAllProviderByClinicBranchId(int clinicBranchID, List<int> stationIds, bool isLoadAll);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        List<StationDTO> LoadAllStationsByClinicId(int clinicBranchID);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// </summary>
        /// <param name="providerUserId"></param>
        /// <returns></returns>
        WorkingHourDTO LoadProviderWorkingHours(int providerUserId);


        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        List<PatientProviderAssociationDTO> ListAllPatientProvidersWithLocations(int patientId);


        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        int CheckProviderAuthorityForPatientInfoAccess(int providerUserId, int patientId, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        int ManageProviderAuthorization(PatientProviderAssociationDTO patientProviderAssociationDTO);


        List<ProviderSearchDTO> SearchProviderForAppointment(SearchProviderCriteriaDTO criteria); 

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 25/11/2011
        /// This method will Get Station Names By StationID
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<StationDTO> GetStationNamesByStationID(List<int> stationIDList);



        int UpdateUserStatus(int userId, string update, bool status);


        int InsertUserQuickLinks(List<QuickLinkDTO> list);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This call will load patient user basic info like name email using thier patient Ids :)
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>

        List<PatientBasicUserDTO> GetUserBasicInfoByPatientIds(List<int> PatientIdList);

    }
}
