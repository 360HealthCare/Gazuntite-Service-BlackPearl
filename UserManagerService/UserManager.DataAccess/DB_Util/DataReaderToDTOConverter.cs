using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Quivus.Model.User;
using Quivus.Model.Common;
using Quivus.Model.Clinic;
using Quivus.Model.TypeDTOs;
using Quivus.Model.SearchEngine;
using Quivus.Model.QuickLink;
using System.IO;
using Quivus.Utility.Logger;
using Quivus.Model.Search;
using Quivus.Utility.CustomException;
using Quivus.Utility;

using Quivus.Model.User.Patient;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.RecentPatient;
using Quivus.Model.Insurance;

using Quivus.Model.AddressBook;
using System.Data.SqlClient;
using QuivusModel.Common;
using QuivusModel.User.Patient;
using Quivus.Model.Clinic;
using QuivusModel.Clinic;
using QuivusModel.User;
using QuivusModel.Search;

namespace UserManagerService.DataAccess.DB_Util
{
    public class DataReaderToDTOConverter
    {
        /// <summary>
        /// Author Mansoor Ud-Din
        /// Created Date 13 jan 2011
        /// This Method Verfiy the User Credential &
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>

        public static LoginDTO LoginDTOConverter(IDataReader objReader)
        {
            LoginDTO loginDTO = null;

            try
            {
                if (objReader.NextResult())
                {
                    //objReader.NextResult();

                    //Single Record will be returned.
                    if (objReader.Read())
                    {
                        loginDTO = new LoginDTO();
                        loginDTO.UserId = int.Parse(objReader["UserId"].ToString());
                        loginDTO.FirstName = objReader["FirstName"].ToString();
                        loginDTO.LastName = objReader["LastName"].ToString();

                        if (objReader["MiddleName"] != DBNull.Value)
                        {
                            loginDTO.MiddleName = objReader["MiddleName"].ToString();
                        }

                        if (objReader["Suffix"] != DBNull.Value)
                        {
                            loginDTO.Suffix = objReader["Suffix"].ToString();
                        }

                        if (objReader["PrefixName"] != DBNull.Value)
                        {
                            loginDTO.Prefix = objReader["PrefixName"].ToString();
                        }

                        loginDTO.Gender = objReader["GenderName"].ToString();
                        loginDTO.SSN = objReader["SSN"].ToString();
                        loginDTO.UserEmail = objReader["UserEmail"].ToString();
                    }

                    if (loginDTO != null)
                    {
                        objReader.NextResult();
                        List<UserClinicBranchDTO> UserClinicBranchesList = new List<UserClinicBranchDTO>();

                        UserClinicBranchDTO clinicBranchDTO = null;
                        //Multiple Records will be returned
                        while (objReader.Read())
                        {
                            clinicBranchDTO = new UserClinicBranchDTO();
                            clinicBranchDTO.ClinicBranch = new ClinicBranchDTO();

                            clinicBranchDTO.ClinicBranch.ClinicBranchIdSeq = int.Parse(objReader["ClinicBranchId"].ToString());

                            //clinicBranchDTO.UserClinicBranch.Clinic.ClinicIdSeq = int.Parse(objReader["CLINIC_ID"].ToString());
                            //clinicBranchDTO.UserClinicBranch.Clinic.ClinicName = objReader["CLINIC_NAME"].ToString();
                            clinicBranchDTO.ClinicBranch.ClinicBranchName = objReader["ClinicName"].ToString(); //chanage after login flow
                            //clinicBranchDTO.UserClinicBranch.Clinic.ClinicStatus = objReader["CLINIC_STATUS"].ToString();

                            UserClinicBranchesList.Add(clinicBranchDTO);
                        }
                        // station info
                        objReader.NextResult();
                        if (UserClinicBranchesList != null)
                        {
                            StationDTO station = null;
                            List<StationDTO> stationList = new List<StationDTO>();
                            while (objReader.Read())
                            {
                                station = new StationDTO();
                                station.ClinicBranchID = int.Parse(objReader["ClinicBranchID"].ToString());
                                station.StationName = objReader["StationName"].ToString();
                                station.IsDefault = bool.Parse(objReader["IsDefault"].ToString());
                                station.StationID = int.Parse(objReader["StationID"].ToString());
                                if (stationList != null)
                                {
                                    stationList.Add(station);
                                }
                            }
                            for (int index = 0; index < UserClinicBranchesList.Count; index++)
                            {
                                UserClinicBranchDTO clinicBranch = UserClinicBranchesList[index];
                                for (int stationIndex = 0; stationIndex < stationList.Count; stationIndex++)
                                {
                                    StationDTO stationObj = stationList[stationIndex];
                                    if (stationObj.ClinicBranchID == clinicBranch.ClinicBranch.ClinicBranchIdSeq)
                                    {
                                        if (clinicBranch.StationList == null)
                                        {
                                            clinicBranch.StationList = new List<StationDTO>();
                                        }
                                        clinicBranch.StationList.Add(stationObj);
                                    }
                                }
                            }
                        }


                        objReader.NextResult(); // Get All profiles

                        List<UserClinicRoleDTO> UserClinicRoles = new List<UserClinicRoleDTO>();
                        UserClinicRoleDTO objUserClinicRoleDTO = null;

                        //Multiple Records will be returned
                        while (objReader.Read())
                        {
                            objUserClinicRoleDTO = new UserClinicRoleDTO();
                            objUserClinicRoleDTO.UserTypeRole = new UserTypeDTO();

                            objUserClinicRoleDTO.UserTypeRole.UserTypeId = int.Parse(objReader["UserTypeId"].ToString());
                            objUserClinicRoleDTO.UserTypeRole.UserTypeName = objReader["UserTypeName"].ToString();
                            objUserClinicRoleDTO.UserTypeRole.ClinicBranchID = int.Parse(objReader["ClinicBranchId"].ToString());

                            objUserClinicRoleDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                            objUserClinicRoleDTO.UserTypeProfileID = int.Parse(objReader["UserTypeRoleID"].ToString());

                            UserClinicRoles.Add(objUserClinicRoleDTO);
                        }


                        UserClinicBranchDTO objClinicBranch;
                        for (int indexClinic = 0; indexClinic < UserClinicBranchesList.Count; indexClinic++)
                        {
                            objClinicBranch = UserClinicBranchesList[indexClinic];
                            objClinicBranch.ClinicBranchUserRole = new UserClinicRoleDTO();

                            UserClinicRoleDTO objRole;
                            for (int indexRole = 0; indexRole < UserClinicRoles.Count; indexRole++)
                            {
                                objRole = UserClinicRoles[indexRole];

                                if (objRole.UserTypeRole.ClinicBranchID == objClinicBranch.ClinicBranch.ClinicBranchIdSeq)
                                {
                                    objClinicBranch.ClinicBranchUserRole = objRole;
                                }
                            }
                        }



                        loginDTO.UserClinicBranchesList = UserClinicBranchesList;

                        //
                        objReader.NextResult();
                        List<UserTypeDTO> AllUserTypes = new List<UserTypeDTO>();

                        UserClinicRoleDTO objRole1;
                        for (int indexRole = 0; indexRole < UserClinicRoles.Count; indexRole++)
                        {
                            objRole1 = UserClinicRoles[indexRole];

                            UserTypeDTO objUserType;
                            bool isFound = false;

                            for (int indexType = 0; indexType < AllUserTypes.Count && !isFound; indexType++)
                            {
                                objUserType = AllUserTypes[indexType];

                                if (objRole1.UserTypeRole.UserTypeId == objUserType.UserTypeId)
                                {
                                    isFound = true;
                                }
                            }
                            if (!isFound)
                            {
                                objUserType = new UserTypeDTO();

                                objUserType.UserTypeId = objRole1.UserTypeRole.UserTypeId;
                                objUserType.UserTypeName = objRole1.UserTypeRole.UserTypeName;

                                AllUserTypes.Add(objUserType);
                            }
                        }

                        if (AllUserTypes.Count > 0)
                        {
                            loginDTO.AllUserTypes = AllUserTypes;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".LoginDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return loginDTO;
        }

        /// <summary>
        /// author Muhammad Ismail
        /// Create Date 14 Jan 2011
        /// this Method Populate Search Engine DTO by reading Object Reader By Location
        /// Mansoor
        /// Date 11 Feb 2011
        /// NULL value checks added
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<SearchEngineDTO> SearchEngineLocationConverter(IDataReader objReader)
        {
            List<SearchEngineDTO> SearchengineList = null;
            try
            {
                if (objReader != null)
                {
                    SearchengineList = new List<SearchEngineDTO>();
                    {
                        while (objReader.Read())
                        {
                            SearchEngineDTO SearchEngineObj = new SearchEngineDTO();

                            # region Parse String Properties
                            SearchEngineObj.label = objReader["SearchEngineName"].ToString();

                            if (!string.IsNullOrEmpty(objReader["Icon"].ToString()))
                            {
                                SearchEngineObj.icon = (byte[])objReader["Icon"];
                            }

                            //Mansoor
                            if (objReader["Thumbnail"] != DBNull.Value)
                            {
                                SearchEngineObj.Thumbnail = (byte[])objReader["Thumbnail"];
                            }
                            //M

                            if (objReader["Description"] != DBNull.Value) //check added, this value can be NULL 
                            {
                                SearchEngineObj.description = objReader["Description"].ToString();
                            }
                            SearchEngineObj.URL = objReader["URL"].ToString();
                            # endregion

                            # region Parse Interger Properties

                            if (!string.IsNullOrEmpty(objReader["UserSearchEngineID"].ToString()))
                            {
                                SearchEngineObj.UserSearchEngineID = int.Parse(objReader["UserSearchEngineID"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["SearchEngineTypeID"].ToString()))
                            {
                                SearchEngineObj.SearchEngineTypeID = int.Parse(objReader["SearchEngineTypeID"].ToString());
                            }
                            # endregion

                            # region Parse Bool Properties

                            if (!string.IsNullOrEmpty(objReader["IsPreferred"].ToString()))
                            {
                                SearchEngineObj.IsPreferred = bool.Parse(objReader["IsPreferred"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["IsSystemGenerated"].ToString()))
                            {
                                SearchEngineObj.IsSystemGenerated = bool.Parse(objReader["IsSystemGenerated"].ToString());
                            }
                            #endregion

                            # region Parse DateTime Properties
                            /* String createdAt = objReader["CreatedAt"].ToString();
                            if (!string.IsNullOrEmpty(createdAt))
                            {
                                DateTime create = DateTime.Parse(createdAt);
                                SearchEngineObj.CreatedAt = create;
                            }

                            String ModifyAt = objReader["ModifiedAt"].ToString();
                            if (!string.IsNullOrEmpty(ModifyAt))
                            {
                                DateTime Modify = DateTime.Parse(ModifyAt);
                                SearchEngineObj.ModifiedAt = Modify;
                            }*/
                            #endregion

                            SearchengineList.Add(SearchEngineObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".SearchEngineLocationConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return SearchengineList;
        }

        /// <summary>
        /// author Muhammad Ismail
        /// Create Date 17 Jan 2011
        /// this Method Populate Search Engine DTO by reading Object Reader For All Search Engines
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<SearchEngineDTO> SearchEngineAllConverter(IDataReader objReader)
        {
            List<SearchEngineDTO> SearchengineList = null;
            try
            {
                if (objReader != null)
                {
                    SearchengineList = new List<SearchEngineDTO>();
                    {
                        while (objReader.Read())
                        {
                            SearchEngineDTO SearchEngineObj = new SearchEngineDTO();

                            # region Parse String Properties
                            SearchEngineObj.label = objReader["SearchEngineName"].ToString();

                            if (!string.IsNullOrEmpty(objReader["Icon"].ToString()))
                            {
                                SearchEngineObj.icon = (byte[])objReader["Icon"];
                            }

                            //Mansoor
                            if (objReader["Thumbnail"] != DBNull.Value)
                            {
                                SearchEngineObj.icon = (byte[])objReader["Thumbnail"];
                            }
                            //M

                            if (objReader["Description"] != DBNull.Value) //check added, this value can be NULL 
                            {
                                SearchEngineObj.description = objReader["Description"].ToString();
                            }

                            SearchEngineObj.URL = objReader["URL"].ToString();
                            # endregion

                            # region Parse Interger Properties
                            if (!string.IsNullOrEmpty(objReader["SearchEngineTypeID"].ToString()))
                            {
                                SearchEngineObj.SearchEngineTypeID = int.Parse(objReader["SearchEngineTypeID"].ToString());
                            }
                            # endregion

                            # region Parse Bool Properties
                            if (!string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                            {
                                SearchEngineObj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["IsDeleted"].ToString()))
                            {
                                SearchEngineObj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                            }


                            if (!string.IsNullOrEmpty(objReader["IsSystemGenerated"].ToString()))
                            {
                                SearchEngineObj.IsSystemGenerated = bool.Parse(objReader["IsSystemGenerated"].ToString());
                            }
                            #endregion

                            SearchengineList.Add(SearchEngineObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".SearchEngineAllConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return SearchengineList;
        }

        /// <summary>
        /// author Muhammad Ismail
        /// Create Date 14 Jan 2011
        /// this Method Populate Quick linkDTO by reading Object Reader
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<QuickLinkDTO> QuickLinkConverter(IDataReader objReader)
        {
            List<QuickLinkDTO> QuickLinkList = null;
            try
            {
                if (objReader != null)
                {
                    QuickLinkList = new List<QuickLinkDTO>();
                    {
                        while (objReader.Read())
                        {
                            QuickLinkDTO QuickLinkObj = new QuickLinkDTO();

                            # region Parse String Properties
                            QuickLinkObj.label = objReader["QuickLinkName"].ToString();
                            QuickLinkObj.description = objReader["Description"].ToString();

                            if (!string.IsNullOrEmpty(objReader["Icon"].ToString()))
                            {
                                QuickLinkObj.icon = (byte[])objReader["Icon"]; // Convert String To Byte Array
                            }

                            #endregion

                            # region Parse Int Properties
                            if (!string.IsNullOrEmpty(objReader["UserQuickLinks"].ToString()))
                            {
                                QuickLinkObj.UserQuickLinks = int.Parse(objReader["UserQuickLinks"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["QuickLinkTypeID"].ToString()))
                            {
                                QuickLinkObj.QuickLinkTypeID = int.Parse(objReader["QuickLinkTypeID"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["CreatedBy"].ToString()))
                            {
                                QuickLinkObj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["ModifiedBy"].ToString()))
                            {
                                QuickLinkObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["ModifiedBy"].ToString()))
                            {
                                QuickLinkObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                            }
                            #endregion

                            #region Parse Date Time Properties
                            String createdAt = objReader["CreatedAt"].ToString();

                            if (!string.IsNullOrEmpty(createdAt))
                            {
                                DateTime create = DateTime.Parse(createdAt);
                                QuickLinkObj.CreatedAt = create;
                            }
                            String ModifyAt = objReader["ModifiedAt"].ToString();
                            if (!string.IsNullOrEmpty(ModifyAt))
                            {
                                DateTime Modify = DateTime.Parse(ModifyAt);
                                QuickLinkObj.ModifiedAt = Modify;
                            }
                            #endregion

                            QuickLinkList.Add(QuickLinkObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".QuickLinkConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return QuickLinkList;
        }

        /// <summary>
        /// author Muhammad Ismail
        /// Create Date 14 Jan 2011
        /// this Method Convert String to Byte Array
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static byte[] StrToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        internal static List<ContactSearchDTO> ContactSearchDTOConverter(IDataReader objReader)
        {
            List<ContactSearchDTO> ContactSearchList = null;
            try
            {
                if (objReader != null)
                {
                    ContactSearchList = new List<ContactSearchDTO>();
                    {
                        while (objReader.Read())
                        {
                            ContactSearchDTO QuickLinkObj = new ContactSearchDTO();

                            # region Parse String Properties
                            QuickLinkObj.FirstName = objReader["FirstName"].ToString();
                            QuickLinkObj.LastName = objReader["LastName"].ToString();
                            QuickLinkObj.MiddleName = objReader["MiddleName"].ToString();
                            QuickLinkObj.Suffix = objReader["Suffix"].ToString();
                            QuickLinkObj.UserEmail = objReader["UserEmail"].ToString();
                            #endregion

                            # region Parse Int Properties
                            if (!string.IsNullOrEmpty(objReader["UserId"].ToString()))
                            {
                                QuickLinkObj.UserId = int.Parse(objReader["UserId"].ToString());
                            }
                            if (!string.IsNullOrEmpty(objReader["PrefixTypeID"].ToString()))
                            {
                                QuickLinkObj.PrefixTypeID = int.Parse(objReader["PrefixTypeID"].ToString());
                            }

                            #endregion

                            ContactSearchList.Add(QuickLinkObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ContactSearchDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return ContactSearchList;
        }

        /// <summary>
        /// Auhtor Mansoor
        /// Date 24 Jan 2011
        /// Convert the reader to User Types DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<UserTypeDTO> UserTypeDTOConverter(IDataReader objReader)
        {
            List<UserTypeDTO> UserTypeList = null;
            try
            {
                if (objReader != null)
                {
                    UserTypeList = new List<UserTypeDTO>();
                    {
                        while (objReader.Read())
                        {
                            UserTypeDTO UserTypeObj = new UserTypeDTO();

                            UserTypeObj.UserTypeId = int.Parse(objReader["UserTypeId"].ToString());
                            UserTypeObj.UserTypeName = objReader["UserTypeName"].ToString();

                            UserTypeObj.IsProvider = bool.Parse(objReader["IsProvider"].ToString());

                            UserTypeList.Add(UserTypeObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".UserTypeDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return UserTypeList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 28 Jan 2011
        /// Convert the reader to Pateient Search Result
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static PatientSearchResultDTO ConvertToPatientSearchResultDTO(IDataReader objReader)
        {
            PatientSearchResultDTO searchResult = null;
            List<PatientSearchDTO> patientSearchList = null;

            try
            {
                if (objReader != null && objReader.Read())
                {
                    searchResult = new PatientSearchResultDTO();

                    if (objReader["TotalRecords"] != DBNull.Value && !String.IsNullOrEmpty(objReader["TotalRecords"].ToString()))
                        searchResult.TotalNoOfRecords = int.Parse(objReader["TotalRecords"].ToString());

                    if (objReader["FirstRecord"] != DBNull.Value && !String.IsNullOrEmpty(objReader["FirstRecord"].ToString()))
                        searchResult.CurrentFirstRecordNo = int.Parse(objReader["FirstRecord"].ToString());

                    if (objReader["LastRecord"] != DBNull.Value && !String.IsNullOrEmpty(objReader["LastRecord"].ToString()))
                        searchResult.CurrentLastRecordNo = int.Parse(objReader["LastRecord"].ToString());

                    objReader.NextResult();
                    patientSearchList = new List<PatientSearchDTO>();

                    while (objReader.Read())
                    {
                        PatientSearchDTO PatientSearchObj = new PatientSearchDTO();

                        PatientSearchObj.UserId = int.Parse(objReader["UserId"].ToString());
                        PatientSearchObj.PatientId = int.Parse(objReader["PatientID"].ToString());
                        PatientSearchObj.FirstName = objReader["FirstName"].ToString();
                        PatientSearchObj.LastName = objReader["LastName"].ToString();

                        if (objReader["MiddleName"] != DBNull.Value)
                        {
                            PatientSearchObj.MiddleName = objReader["MiddleName"].ToString();
                        }

                        PatientSearchObj.QuivusID = objReader["QuivusID"].ToString();
                        PatientSearchObj.DOB = DateTime.Parse(objReader["DateOfBirth"].ToString());

                        PatientSearchObj.IsEmailIdAvailable = Boolean.Parse(objReader["AllowEmailCommunication"].ToString());

                        if (objReader["SSN"] != DBNull.Value)
                        {
                            PatientSearchObj.SSN = objReader["SSN"].ToString();
                        }
                        if (objReader["GenderId"] != DBNull.Value)
                        {
                            PatientSearchObj.GenderId = int.Parse(objReader["GenderId"].ToString());
                        }
                        if (objReader["GenderName"] != DBNull.Value)
                        {
                            PatientSearchObj.GenderName = objReader["GenderName"].ToString();
                        }

                        patientSearchList.Add(PatientSearchObj);
                    }
                    searchResult.PatientSearchList = patientSearchList;
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientSearchResultDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return searchResult;
        }


        /// <summary>
        /// Author Muhammad ismail
        /// Updated Date Feb 02 2011
        /// Method Used To convert Reader Set To Profile-SettingDTO :)
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<ProfileStaticDataDTO> ProfileStaticDataDTOConverter(IDataReader objReader)
        {
            List<ProfileStaticDataDTO> patientStaticDataList = null;
            try
            {
                if (objReader != null)
                {
                    patientStaticDataList = new List<ProfileStaticDataDTO>();

                    while (objReader.Read())
                    {
                        ProfileStaticDataDTO PatientSearchObj = new ProfileStaticDataDTO();

                        #region Parse Integer Properties

                        if (!string.IsNullOrEmpty(objReader["UserProfileCategoryID"].ToString()))
                        {
                            PatientSearchObj.UserProfileCategoryID = int.Parse(objReader["UserProfileCategoryID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(objReader["MainUserProfileCategoryID"].ToString()))
                        {
                            PatientSearchObj.MainUserProfileCategoryID = int.Parse(objReader["MainUserProfileCategoryID"].ToString());
                        }
                        #endregion

                        #region Parse String Properties
                        PatientSearchObj.MainCategoryName = objReader["MainCategoryName"].ToString();
                        PatientSearchObj.MainCategoryDisplayText = objReader["MainCategoryDisplayText"].ToString();

                        PatientSearchObj.CategoryName = objReader["CategoryName"].ToString();
                        PatientSearchObj.CategoryDisplayText = objReader["CategoryDisplayText"].ToString();
                        #endregion

                        patientStaticDataList.Add(PatientSearchObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + "ProfileStaticDataDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientStaticDataList;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Created Date 02 Feb 2011
        /// Method Used to Convert Profile Setting List
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<PatientProfileSettingDTO> ProfileSettingDTOConverter(IDataReader objReader)
        {
            List<PatientProfileSettingDTO> patientStaticDataList = null;
            try
            {
                if (objReader != null)
                {
                    patientStaticDataList = new List<PatientProfileSettingDTO>();

                    while (objReader.Read())
                    {
                        PatientProfileSettingDTO ProfileObj = new PatientProfileSettingDTO();

                        #region Parse Integer Properties


                        if (objReader["UserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserID"].ToString()))
                        {
                            ProfileObj.UserID = int.Parse(objReader["UserID"].ToString());
                        }
                        if (objReader["SectionNo"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SectionNo"].ToString()))
                        {
                            ProfileObj.SectionNo = int.Parse(objReader["SectionNo"].ToString());
                        }
                        if (objReader["DisplayText"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DisplayText"].ToString()))
                        {
                            ProfileObj.DisplayText = objReader["DisplayText"].ToString();
                        }
                        if (objReader["FieldData"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FieldData"].ToString()))
                        {
                            ProfileObj.FieldData = objReader["FieldData"].ToString();
                        }
                        if (objReader["UserProfileCategoryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryID"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryID = int.Parse(objReader["UserProfileCategoryID"].ToString());
                        }
                        if (objReader["UserProfileCategoryName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryName"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryName = objReader["UserProfileCategoryName"].ToString();
                        }

                        if (objReader["TooltipText"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TooltipText"].ToString()))
                        {
                            ProfileObj.TooltipText = objReader["TooltipText"].ToString();
                        }

                        #endregion

                        //ProfileObj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());

                        #region Parse Date Time

                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    String createdAt = objReader["CreatedAt"].ToString();
                        //    if (!string.IsNullOrEmpty(createdAt))
                        //    {
                        //        DateTime create = DateTime.Parse(createdAt);
                        //        ProfileObj.CreatedAt = create;
                        //    }
                        //}

                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    String modifyAt = objReader["ModifiedAt"].ToString();
                        //    if (!string.IsNullOrEmpty(modifyAt))
                        //    {
                        //        DateTime Modified = DateTime.Parse(modifyAt);
                        //        ProfileObj.ModifiedAt = Modified;
                        //    }
                        //}
                        #endregion

                        patientStaticDataList.Add(ProfileObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.PROFILE_SETTING_ERROR_MSG + "ProfileStaticDataDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientStaticDataList;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Created Date 02 Feb 2011
        /// Method Used to Convert Profile Setting List
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<PatientProfileSettingDTO> GeneralProfileSettingDTOConverter(IDataReader objReader)
        {
            List<PatientProfileSettingDTO> patientStaticDataList = null;
            try
            {
                if (objReader != null)
                {
                    patientStaticDataList = new List<PatientProfileSettingDTO>();

                    while (objReader.Read())
                    {
                        PatientProfileSettingDTO ProfileObj = new PatientProfileSettingDTO();

                        #region Parse Integer Properties

                        if (objReader["UserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserID"].ToString()))
                        {
                            ProfileObj.UserID = int.Parse(objReader["UserID"].ToString());
                        }
                        if (objReader["SectionNo"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SectionNo"].ToString()))
                        {
                            ProfileObj.SectionNo = int.Parse(objReader["SectionNo"].ToString());
                        }
                        if (objReader["UserProfileCategoryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryID"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryID = int.Parse(objReader["UserProfileCategoryID"].ToString());
                        }
                        if (objReader["UserProfileCategoryName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryName"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryName = objReader["UserProfileCategoryName"].ToString();
                        }
                        //M 07 Feb 2011
                        if (objReader["UserUserProfileCategoryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserUserProfileCategoryID"].ToString()))
                        {
                            ProfileObj.UserUserProfileCategoryID = int.Parse(objReader["UserUserProfileCategoryID"].ToString());
                        }

                        #endregion

                        patientStaticDataList.Add(ProfileObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.PROFILE_SETTING_ERROR_MSG + "ProfileStaticDataDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientStaticDataList;
        }
        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Convert the reader to Pateient Search Result
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<RecentPatientDTO> ConvertToRecentPatientDTO(IDataReader objReader)
        {
            List<RecentPatientDTO> patientSearchList = null;

            try
            {
                if (objReader != null)
                {
                    patientSearchList = new List<RecentPatientDTO>();
                    {
                        while (objReader.Read())
                        {
                            RecentPatientDTO RecentPatientDTOObj = new RecentPatientDTO();

                            RecentPatientDTOObj.Index = int.Parse(objReader["Index"].ToString());
                            RecentPatientDTOObj.PatientId = int.Parse(objReader["PatientID"].ToString());

                            if (objReader["UserID"] != DBNull.Value)
                                RecentPatientDTOObj.UserId = int.Parse(objReader["UserID"].ToString());

                            if (objReader["ClinicBranchID"] != DBNull.Value)
                                RecentPatientDTOObj.ClinicBranchId = int.Parse(objReader["ClinicBranchID"].ToString());

                            if (objReader["PatientUserID"] != DBNull.Value)
                                RecentPatientDTOObj.PatientUserID = int.Parse(objReader["PatientUserID"].ToString());


                            RecentPatientDTOObj.UserRecentPatientId = int.Parse(objReader["UserRecentPatientID"].ToString());

                            RecentPatientDTOObj.PatientName = objReader["PatientName"].ToString();
                            RecentPatientDTOObj.QuivusId = objReader["QuivusID"].ToString();

                            if (objReader["DateOfBirth"] != DBNull.Value)
                            {
                                RecentPatientDTOObj.PatientDOB = DateTime.Parse(objReader["DateOfBirth"].ToString());
                            }

                            if (objReader["LastVisitDate"] != DBNull.Value)
                            {
                                RecentPatientDTOObj.LastVisitDate = objReader["LastVisitDate"].ToString();
                            }

                            if (objReader["AllowEmailCommunication"] != DBNull.Value)
                            {
                                RecentPatientDTOObj.AllowEmailCommunication = Boolean.Parse(objReader["AllowEmailCommunication"].ToString());
                                RecentPatientDTOObj.IsEmailIdAvailable = Boolean.Parse(objReader["AllowEmailCommunication"].ToString());
                            }

                            patientSearchList.Add(RecentPatientDTOObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.RECENT_PATIENT_DTO_CONVERSION_ERROR_MSG + ".ConvertToRecentPatientDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientSearchList;
        }

        internal static PatientProfileBasicInfoDTO ConvertToPatientProfileBasicInfoDTO(IDataReader objReader)
        {
            PatientProfileBasicInfoDTO patientBasicProfileInfo = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        patientBasicProfileInfo = new PatientProfileBasicInfoDTO();

                        patientBasicProfileInfo.FirstName = objReader["FirstName"].ToString();
                        patientBasicProfileInfo.LastName = objReader["LastName"].ToString();

                        if (objReader["MiddleName"] != DBNull.Value)
                        {
                            patientBasicProfileInfo.MiddleName = objReader["MiddleName"].ToString();
                        }

                        patientBasicProfileInfo.DateOfBirth = DateTime.Parse(objReader["DateOfBirth"].ToString());
                        patientBasicProfileInfo.QuivusID = objReader["QuivusID"].ToString();
                        patientBasicProfileInfo.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileBasicInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientBasicProfileInfo;
        }

        internal static PatientInsuracneInfoDTO ConvertToPatientInsuranceInfoDTO(IDataReader objReader)
        {
            PatientInsuracneInfoDTO patientInsuranceInfo = null;

            try
            {

                //     PTI.[PatientInsuranceId]  
                //,PTI.[PatientId]
                //,IT.InsuranceTypeName
                //,PTI.PolicyNumber
                //,IORG.InsuranceOrganizationName          
                //,PTI.[EndDate]     
                //,PTI.[IsEligible]
                //,0 as InsuranceBalance
                //,0 as PatientBalance

                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        patientInsuranceInfo = new PatientInsuracneInfoDTO();

                        //M these fields cann't be null, so null check is ommited
                        patientInsuranceInfo.PatientInsuranceId = int.Parse(objReader["PatientInsuranceId"].ToString());
                        patientInsuranceInfo.InsuranceOrganizationName = objReader["InsuranceOrganizationName"].ToString();
                        patientInsuranceInfo.EndDate = DateTime.Parse(objReader["EndDate"].ToString());
                        if (objReader["IsEligible"] != DBNull.Value)
                        {
                            bool value = bool.Parse(objReader["IsEligible"].ToString());
                            if (value)
                                patientInsuranceInfo.IsEligible = ProjectConstant.SELECTION_TRUE;
                            else
                                patientInsuranceInfo.IsEligible = ProjectConstant.SELECTION_False;
                        }
                        else
                        {
                            patientInsuranceInfo.IsEligible = ProjectConstant.SELECTION_NULL;
                        }
                        patientInsuranceInfo.InsuranceBalance = int.Parse(objReader["InsuranceBalance"].ToString());
                        patientInsuranceInfo.PatientBalance = int.Parse(objReader["PatientBalance"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientInsuranceInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientInsuranceInfo;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Date 04 Feb 2011
        /// Method used to Convert Profile Setting Section
        /// </summary>
        /// <param name="objIReader"></param>
        /// <returns></returns>
        internal static PatientProfileSettingDTO ProfileSettingSectionDTOConverter(IDataReader objReader)
        {
            PatientProfileSettingDTO ProfileObj = null;
            try
            {
                if (objReader != null)
                {
                    ProfileObj = new PatientProfileSettingDTO();

                    while (objReader.Read())
                    {

                        #region Parse Integer Properties


                        if (objReader["UserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserID"].ToString()))
                        {
                            ProfileObj.UserID = int.Parse(objReader["UserID"].ToString());
                        }
                        if (objReader["SectionNo"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SectionNo"].ToString()))
                        {
                            ProfileObj.SectionNo = int.Parse(objReader["SectionNo"].ToString());
                        }

                        if (objReader["DisplayText"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DisplayText"].ToString()))
                        {
                            ProfileObj.DisplayText = objReader["DisplayText"].ToString();
                        }
                        if (objReader["FieldData"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FieldData"].ToString()))
                        {
                            ProfileObj.FieldData = objReader["FieldData"].ToString();
                        }
                        if (objReader["UserProfileCategoryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserProfileCategoryID"].ToString()))
                        {
                            ProfileObj.UserProfileCategoryID = int.Parse(objReader["UserProfileCategoryID"].ToString());
                        }
                        if (objReader["TooltipText"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TooltipText"].ToString()))
                        {
                            ProfileObj.TooltipText = objReader["TooltipText"].ToString();
                        }
                        #endregion

                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.PROFILE_SETTING_ERROR_MSG + "ProfileStaticDataDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return ProfileObj;
        }


        internal static List<PatientAllergyInfoDTO> ConvertToPatientProfileAllergyInfo(IDataReader objReader)
        {
            List<PatientAllergyInfoDTO> allergyList = null;
            PatientAllergyInfoDTO allergyObj = null;

            try
            {
                if (objReader != null)
                {
                    allergyList = new List<PatientAllergyInfoDTO>();

                    while (objReader.Read())
                    {
                        allergyObj = new PatientAllergyInfoDTO();

                        allergyObj.PatientAllergyId = int.Parse(objReader["PatientHistoryAllergyID"].ToString());

                        if (objReader["AllergyTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllergyTypeName"].ToString()))
                        {
                            allergyObj.AllergyName = objReader["AllergyTypeName"].ToString();
                        }

                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            allergyObj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                        }
                        allergyList.Add(allergyObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return allergyList;
        }

        internal static List<PatientDiagnosisInfoDTO> ConvertToPatientProfileDiagnosisInfo(IDataReader objReader)
        {
            List<PatientDiagnosisInfoDTO> diagnosisList = null;
            PatientDiagnosisInfoDTO diagnosisObj = null;

            try
            {
                if (objReader != null)
                {
                    diagnosisList = new List<PatientDiagnosisInfoDTO>();

                    while (objReader.Read())
                    {
                        diagnosisObj = new PatientDiagnosisInfoDTO();

                        diagnosisObj.PatientHistoryMedicalID = float.Parse(objReader["ICDCodeName"].ToString());

                        if (objReader["ICDCodeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                        {
                            diagnosisObj.ICDCodeId = objReader["ICDCodeName"].ToString();
                        }
                        if (objReader["ICDCodeDescription"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeDescription"].ToString()))
                        {
                            diagnosisObj.Description = objReader["ICDCodeDescription"].ToString();
                        }
                        //if (objReader["Nature"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Nature"].ToString()))
                        //{
                        //    diagnosisObj.Nature = objReader["Nature"].ToString();
                        //}
                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            diagnosisObj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                        }
                        diagnosisList.Add(diagnosisObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileDiagnosisInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return diagnosisList;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Date 4 feb 2011
        /// Method Used to convert insurance Info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        internal static List<PatientInsuracneInfoDTO> ConvertToPatientInsuranceInfo(IDataReader objReader)
        {
            List<PatientInsuracneInfoDTO> insuranceList = null;
            PatientInsuracneInfoDTO insuranceObj = null;

            try
            {
                if (objReader != null)
                {
                    insuranceList = new List<PatientInsuracneInfoDTO>();

                    while (objReader.Read())
                    {
                        insuranceObj = new PatientInsuracneInfoDTO();

                        if (objReader["PatientInsuranceId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PatientInsuranceId"].ToString()))
                        {
                            insuranceObj.PatientInsuranceId = int.Parse(objReader["PatientInsuranceId"].ToString());
                        }
                        if (objReader["PatientId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PatientId"].ToString()))
                        {
                            insuranceObj.PatientId = int.Parse(objReader["PatientId"].ToString());
                        }
                        if (objReader["InsuranceTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceTypeName"].ToString()))
                        {
                            insuranceObj.InsuranceTypeName = objReader["InsuranceTypeName"].ToString();
                        }
                        if (objReader["PolicyNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PolicyNumber"].ToString()))
                        {
                            insuranceObj.PolicyNumber = objReader["PolicyNumber"].ToString();
                        }
                        if (objReader["InsuranceOrganizationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceOrganizationName"].ToString()))
                        {
                            insuranceObj.InsuranceOrganizationName = objReader["InsuranceOrganizationName"].ToString();
                        }
                        if (objReader["EndDate"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EndDate"].ToString()))
                        {
                            insuranceObj.EndDate = DateTime.Parse(objReader["EndDate"].ToString());
                        }
                        if (objReader["IsEligible"] != DBNull.Value)
                        {
                            bool value = bool.Parse(objReader["IsEligible"].ToString());
                            if (value)
                                insuranceObj.IsEligible = ProjectConstant.SELECTION_TRUE;
                            else
                                insuranceObj.IsEligible = ProjectConstant.SELECTION_False;
                        }
                        else
                        {
                            insuranceObj.IsEligible = ProjectConstant.SELECTION_NULL;
                        }
                        //if (objReader["IsEligible"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsEligible"].ToString()))
                        //{
                        //    insuranceObj.IsEligible = bool.Parse(objReader["IsEligible"].ToString());
                        //}
                        insuranceList.Add(insuranceObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return insuranceList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Base User DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static BaseUserDTO ConvertToBaseUserDTO(IDataReader objReader)
        {
            BaseUserDTO baseUser = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        baseUser = new BaseUserDTO();

                        baseUser.UserId = int.Parse(objReader["UserId"].ToString());

                        if (objReader["PrefixTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PrefixTypeID"].ToString()))
                        {
                            baseUser.Prefix = new PrefixTypeDTO();
                            baseUser.Prefix.PrefixId = int.Parse(objReader["PrefixTypeID"].ToString());

                            if (objReader["PrefixName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PrefixName"].ToString()))
                            {
                                baseUser.Prefix.PrefixName = objReader["PrefixName"].ToString();
                            }
                        }

                        baseUser.FirstName = objReader["FirstName"].ToString();
                        baseUser.LastName = objReader["LastName"].ToString();

                        if (objReader["MiddleName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MiddleName"].ToString()))
                        {
                            baseUser.MiddleName = objReader["MiddleName"].ToString();
                        }

                        if (objReader["Suffix"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Suffix"].ToString()))
                        {
                            baseUser.Suffix = objReader["Suffix"].ToString();
                        }

                        baseUser.UserEmail = objReader["UserEmail"].ToString();

                        if (objReader["GenderTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderTypeID"].ToString()))
                        {
                            baseUser.Gender = new GenderTypeDTO();
                            baseUser.Gender.GenderId = int.Parse(objReader["GenderTypeID"].ToString());

                            if (objReader["GenderName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderName"].ToString()))
                            {
                                baseUser.Gender.GenderName = objReader["GenderName"].ToString();
                            }
                        }

                        if (objReader["SSN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SSN"].ToString()))
                        {
                            baseUser.SSN = objReader["SSN"].ToString();
                        }

                        if (objReader["AccountStatus"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AccountStatus"].ToString()))
                        {
                            baseUser.AccountStatus = new StatusTypeDTO();
                            baseUser.AccountStatus.StatusID = int.Parse(objReader["AccountStatus"].ToString());
                        }

                        if (objReader["LicenseNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LicenseNumber"].ToString()))
                        {
                            baseUser.LicenseNumber = objReader["LicenseNumber"].ToString();
                        }

                        if (objReader["FederalTaxId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FederalTaxId"].ToString()))
                        {
                            baseUser.FederalTaxId = objReader["FederalTaxId"].ToString();
                        }

                        if (objReader["EIN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EIN"].ToString()))
                        {
                            baseUser.FederalTaxId = objReader["EIN"].ToString();
                        }
                        if (objReader["UserName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserName"].ToString()))
                        {
                            baseUser.UserName = objReader["UserName"].ToString();
                        }
                        if (objReader["QuivusUserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["QuivusUserID"].ToString()))
                        {
                            baseUser.QuivusUserID = objReader["QuivusUserID"].ToString();
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToBaseUserDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return baseUser;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static PatientDTO ConvertToPatientDTO(IDataReader objReader)
        {
            PatientDTO patientDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        patientDTO = new PatientDTO();

                        patientDTO.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["MaritalStatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusID"].ToString()))
                        {
                            patientDTO.MaritalStatusType = new MaritalStatusTypeDTO();
                            patientDTO.MaritalStatusType.MaritalStatusId = int.Parse(objReader["MaritalStatusID"].ToString());

                            if (objReader["MaritalStatusName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusName"].ToString()))
                            {
                                patientDTO.MaritalStatusType.MaritalStatusName = objReader["MaritalStatusName"].ToString();
                            }
                        }

                        if (objReader["DateOfBirth"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DateOfBirth"].ToString()))
                        {
                            patientDTO.DateOfBirth = DateTime.Parse(objReader["DateOfBirth"].ToString());
                        }

                        if (objReader["QuivusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["QuivusID"].ToString()))
                        {
                            patientDTO.QuivusId = objReader["QuivusID"].ToString();
                        }

                        if (objReader["AllowEmailCommunication"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllowEmailCommunication"].ToString()))
                        {
                            patientDTO.AllowEmailCommunication = bool.Parse(objReader["AllowEmailCommunication"].ToString());
                        }

                        if (objReader["SpouseName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpouseName"].ToString()))
                        {
                            patientDTO.SpouseName = objReader["SpouseName"].ToString();
                        }

                        if (objReader["MotherMaidenName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MotherMaidenName"].ToString()))
                        {
                            patientDTO.MotherMaidenName = objReader["MotherMaidenName"].ToString();
                        }

                        if (objReader["FatherName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FatherName"].ToString()))
                        {
                            patientDTO.FatherName = objReader["FatherName"].ToString();
                        }

                        if (objReader["MedicareNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MedicareNumber"].ToString()))
                        {
                            patientDTO.MedicareNumber = objReader["MedicareNumber"].ToString();
                        }

                        if (objReader["IVR_PINCode"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IVR_PINCode"].ToString()))
                        {
                            patientDTO.IVR_PINCODE = int.Parse(objReader["IVR_PINCode"].ToString());
                        }

                        if (objReader["PatientStatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PatientStatusID"].ToString()))
                        {
                            patientDTO.StatusType = new StatusTypeDTO();
                            patientDTO.StatusType.StatusID = int.Parse(objReader["PatientStatusID"].ToString());
                        }

                        if (objReader["ExemptReporting"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ExemptReporting"].ToString()))
                        {
                            patientDTO.IsReportingExempt = bool.Parse(objReader["ExemptReporting"].ToString());
                        }

                        if (objReader["RaceID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RaceID"].ToString()))
                        {
                            patientDTO.RaceType = new RaceTypeDTO();
                            patientDTO.RaceType.RaceId = int.Parse(objReader["RaceID"].ToString());

                            if (objReader["RaceName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RaceName"].ToString()))
                            {
                                patientDTO.RaceType.RaceName = objReader["RaceName"].ToString();
                            }
                        }

                        if (objReader["EthinicityID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EthinicityID"].ToString()))
                        {
                            patientDTO.EthinicityType = new EthinicityTypeDTO();
                            patientDTO.EthinicityType.EthinicityId = int.Parse(objReader["EthinicityID"].ToString());

                            if (objReader["EthinicityName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EthinicityName"].ToString()))
                            {
                                patientDTO.EthinicityType.EthinicityName = objReader["EthinicityName"].ToString();
                            }
                        }

                        if (objReader["OccupationID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["OccupationID"].ToString()))
                        {
                            patientDTO.OccupationType = new OccupationTypeDTO();
                            patientDTO.OccupationType.OccupationId = int.Parse(objReader["OccupationID"].ToString());

                            if (objReader["OccupationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["OccupationName"].ToString()))
                            {
                                patientDTO.OccupationType.OccupationName = objReader["OccupationName"].ToString();
                            }
                        }

                        if (objReader["IsAlcoholStatusYes"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsAlcoholStatusYes"].ToString()))
                        {
                            patientDTO.AlcoholStatus = objReader["IsAlcoholStatusYes"].ToString();
                        }
                        else
                        {
                            patientDTO.AlcoholStatus = null;
                        }

                        if (objReader["AlcoholStatusData"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AlcoholStatusData"].ToString()))
                        {
                            patientDTO.AlcoholStatusData = objReader["AlcoholStatusData"].ToString();
                        }
                        else
                        {
                            patientDTO.AlcoholStatusData = null;
                        }

                        if (objReader["IsDrugStatusYes"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsDrugStatusYes"].ToString()))
                        {
                            patientDTO.DrugStatus = objReader["IsDrugStatusYes"].ToString();
                        }
                        else
                        {
                            patientDTO.DrugStatus = null;
                        }

                        if (objReader["DrugStatusData"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DrugStatusData"].ToString()))
                        {
                            patientDTO.DrugStatusData = objReader["DrugStatusData"].ToString();
                        }
                        else
                        {
                            patientDTO.DrugStatusData = null;
                        }

                        if (objReader["EmployerName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EmployerName"].ToString()))
                        {
                            patientDTO.EmployerName = objReader["EmployerName"].ToString();
                        }

                        if (objReader["IsProfileComplete"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsProfileComplete"].ToString()))
                        {
                            patientDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }

                        patientDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());

                        //   if (objReader["SmokingStatus"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SmokingStatus"].ToString()))
                        if (objReader["SmokingTypeID"] != DBNull.Value)
                        {
                            if (objReader["SmokingTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SmokingTypeID"].ToString()))
                            {
                                if (patientDTO.SmokingType == null)
                                    patientDTO.SmokingType = new SmokingTypeDTO();

                                patientDTO.SmokingType.SmokingTypeId = int.Parse(objReader["SmokingTypeID"].ToString());
                            }

                            if (objReader["SmokingTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SmokingTypeName"].ToString()))
                            {
                                if (patientDTO.SmokingType == null)
                                    patientDTO.SmokingType = new SmokingTypeDTO();

                                patientDTO.SmokingType.SmokingTypeName = objReader["SmokingTypeName"].ToString();
                            }

                        }

                        if (objReader["UserID"] != DBNull.Value)
                        {
                            patientDTO.UserId = int.Parse(objReader["UserID"].ToString());
                        }

                        if (objReader["FileID"] != DBNull.Value)
                        {
                            patientDTO.FileId = objReader["FileID"].ToString();
                        }

                        if (objReader["SmokingStatus"] != DBNull.Value)
                        {
                            patientDTO.SmokingStatus = objReader["SmokingStatus"].ToString();
                        }
                        if (objReader["DefaultPharmacyRCOPIAID"] != DBNull.Value)
                        {
                            patientDTO.DefaultPharmacyRCOPIAID = objReader["DefaultPharmacyRCOPIAID"].ToString();
                        }
                        if (objReader["RCOPIAID"] != DBNull.Value)
                        {
                            patientDTO.RCOPIAID = objReader["RCOPIAID"].ToString();
                        }
                        if (objReader["RCOPIAPracticeID"] != DBNull.Value)
                        {
                            patientDTO.RCOPIAPracticeID = objReader["RCOPIAPracticeID"].ToString();
                        }
                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            patientDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            patientDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            patientDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            patientDTO.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            patientDTO.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            patientDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        //# region New Fields added By Ismail :)

                        //if (objReader["MaritalStatusName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusName"].ToString()))
                        //{
                        //    patientDTO.MaritalStatusType = new MaritalStatusTypeDTO();
                        //    patientDTO.MaritalStatusType.MaritalStatusName = objReader["MaritalStatusName"].ToString();
                        //}

                        //if (objReader["RaceName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RaceName"].ToString()))
                        //{
                        //    patientDTO.RaceType = new RaceTypeDTO();
                        //    patientDTO.RaceType.RaceName = objReader["RaceName"].ToString();
                        //}

                        //if (objReader["EthinicityName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EthinicityName"].ToString()))
                        //{
                        //    patientDTO.RaceType = new RaceTypeDTO();
                        //    patientDTO.EthinicityType.EthinicityName = objReader["EthinicityName"].ToString();
                        //}

                        //if (objReader["OccupationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["OccupationName"].ToString()))
                        //{
                        //    patientDTO.OccupationType = new OccupationTypeDTO();
                        //    patientDTO.OccupationType.OccupationName = objReader["OccupationName"].ToString();
                        //}

                        //if (objReader["SmokingTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SmokingTypeName"].ToString()))
                        //{
                        //    patientDTO.SmokingType = new SmokingTypeDTO();
                        //    patientDTO.SmokingType.SmokingTypeName = objReader["SmokingTypeName"].ToString();
                        //}

                        //# endregion New Fields added By Ismail
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientDTO;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static PatientGuarantorDTO ConvertToPatientGurantorDTO(IDataReader objReader)
        {
            PatientGuarantorDTO guarantorDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        guarantorDTO = new PatientGuarantorDTO();

                        guarantorDTO.GuarantorId = int.Parse(objReader["PatientGuarantorID"].ToString());
                        guarantorDTO.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["GuarantorFirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GuarantorFirstName"].ToString()))
                        {
                            guarantorDTO.GurantorFirstName = objReader["GuarantorFirstName"].ToString();
                        }

                        if (objReader["GuarantorLastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GuarantorLastName"].ToString()))
                        {
                            guarantorDTO.GuarantorLastName = objReader["GuarantorLastName"].ToString();
                        }

                        if (objReader["GenderID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderID"].ToString()))
                        {
                            guarantorDTO.GenderType = new GenderTypeDTO();
                            guarantorDTO.GenderType.GenderId = int.Parse(objReader["GenderID"].ToString());

                            if (objReader["GenderName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderName"].ToString()))
                            {
                                guarantorDTO.GenderType.GenderName = objReader["GenderName"].ToString();
                            }
                        }

                        if (objReader["RelationShipID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationShipID"].ToString()))
                        {
                            guarantorDTO.RelationshipType = new RelationshipTypeDTO();
                            guarantorDTO.RelationshipType.RelationshipId = int.Parse(objReader["RelationShipID"].ToString());

                            if (objReader["RelationshipName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationshipName"].ToString()))
                            {
                                guarantorDTO.RelationshipType.RelationshipName = objReader["RelationshipName"].ToString();
                            }
                        }

                        if (objReader["DateOfBirth"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DateOfBirth"].ToString()))
                        {
                            guarantorDTO.DOB = DateTime.Parse(objReader["DateOfBirth"].ToString());
                        }

                        if (objReader["StatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StatusID"].ToString()))
                        {
                            guarantorDTO.StatusType = new StatusTypeDTO();
                            guarantorDTO.StatusType.StatusID = int.Parse(objReader["StatusID"].ToString());
                        }

                        if (objReader["MaritalStatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusID"].ToString()))
                        {
                            guarantorDTO.MaritalStatusType = new MaritalStatusTypeDTO();
                            guarantorDTO.MaritalStatusType.MaritalStatusId = int.Parse(objReader["MaritalStatusID"].ToString());

                            if (objReader["MaritalStatusName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MaritalStatusName"].ToString()))
                            {
                                guarantorDTO.MaritalStatusType.MaritalStatusName = objReader["MaritalStatusName"].ToString();
                            }
                        }

                        guarantorDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientGurantorDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return guarantorDTO;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Lanaguage DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientLanguageDTO> ConvertToPatientLanguageDTO(IDataReader objReader)
        {
            List<PatientLanguageDTO> PatientLanguageList = null;
            try
            {
                if (objReader != null)
                {
                    PatientLanguageList = new List<PatientLanguageDTO>();

                    while (objReader.Read())
                    {
                        PatientLanguageDTO objPatientLanguage = new PatientLanguageDTO();

                        objPatientLanguage.PatientLanguageId = int.Parse(objReader["PatientLanguageID"].ToString());
                        objPatientLanguage.PatientId = int.Parse(objReader["PatientID"].ToString());
                        objPatientLanguage.LanguageID = int.Parse(objReader["LanguageID"].ToString());

                        if (objReader["IsPreferred"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsPreferred"].ToString()))
                        {
                            objPatientLanguage.IsPreffered = bool.Parse(objReader["IsPreferred"].ToString());
                        }

                        if (objReader["LanguageName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LanguageName"].ToString()))
                        {
                            objPatientLanguage.LanguageName = objReader["LanguageName"].ToString();
                        }

                        if (objReader["LanguageAbilityModeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LanguageAbilityModeID"].ToString()))
                        {
                            objPatientLanguage.PatientLanguageAbilityMode = new LanguageAbilityModeDTO();
                            objPatientLanguage.PatientLanguageAbilityMode.LanguageAbilityModeId = int.Parse(objReader["LanguageAbilityModeID"].ToString());

                            if (objReader["LangaugeAbilityModeDescription"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LangaugeAbilityModeDescription"].ToString()))
                            {
                                objPatientLanguage.PatientLanguageAbilityMode.LangaugeAbilityModeDescription = objReader["LangaugeAbilityModeDescription"].ToString();
                            }
                        }

                        if (objReader["CountryID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["CountryID"].ToString()))
                        {
                            objPatientLanguage.Country = new CountryDTO();
                            objPatientLanguage.Country.CountryID = int.Parse(objReader["CountryID"].ToString());

                            //CountryName cann't be loaded
                        }

                        PatientLanguageList.Add(objPatientLanguage);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientLanguageDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientLanguageList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Emergency Contact DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientEmergencyContactDTO> ConvertToPatientEmergencyContactDTO(IDataReader objReader)
        {
            List<PatientEmergencyContactDTO> PatientEmergencyContactList = null;
            try
            {
                if (objReader != null)
                {
                    PatientEmergencyContactList = new List<PatientEmergencyContactDTO>();

                    while (objReader.Read())
                    {
                        PatientEmergencyContactDTO objPatientEmergencyContact = new PatientEmergencyContactDTO();

                        objPatientEmergencyContact.PatientEmergencyContactId = int.Parse(objReader["PatientEmergencyContactID"].ToString());
                        objPatientEmergencyContact.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["FirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FirstName"].ToString()))
                        {
                            objPatientEmergencyContact.FirstName = objReader["FirstName"].ToString();
                        }

                        if (objReader["LastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LastName"].ToString()))
                        {
                            objPatientEmergencyContact.LastName = objReader["LastName"].ToString();
                        }

                        if (objReader["RelationShipID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationShipID"].ToString()))
                        {
                            objPatientEmergencyContact.RelationShipType = new RelationshipTypeDTO();
                            objPatientEmergencyContact.RelationShipType.RelationshipId = int.Parse(objReader["RelationShipID"].ToString());

                            if (objReader["RelationshipName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationshipName"].ToString()))
                            {
                                objPatientEmergencyContact.RelationShipType.RelationshipName = objReader["RelationshipName"].ToString();
                            }
                        }

                        if (objReader["StatusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StatusID"].ToString()))
                        {
                            objPatientEmergencyContact.StatusType = new StatusTypeDTO();
                            objPatientEmergencyContact.StatusType.StatusID = int.Parse(objReader["StatusID"].ToString());
                        }

                        if (objReader["AllowCommunication"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllowCommunication"].ToString()))
                        {
                            objPatientEmergencyContact.AllowCommunication = bool.Parse(objReader["AllowCommunication"].ToString());
                        }

                        objPatientEmergencyContact.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());

                        PatientEmergencyContactList.Add(objPatientEmergencyContact);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientEmergencyContactDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientEmergencyContactList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Allergy DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientAllergyDTO> ConvertToPatientAllergyDTO(IDataReader objReader)
        {
            List<PatientAllergyDTO> PatientAllergyList = null;
            try
            {
                if (objReader != null)
                {
                    PatientAllergyList = new List<PatientAllergyDTO>();

                    while (objReader.Read())
                    {
                        PatientAllergyDTO objPatientAllergy = new PatientAllergyDTO();

                        objPatientAllergy.PatientHistoryAllergyId = int.Parse(objReader["PatientHistoryAllergyID"].ToString());
                        objPatientAllergy.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["AllergyID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllergyID"].ToString()))
                        {
                            objPatientAllergy.AllergyType = new AllergyTypeDTO();
                            objPatientAllergy.AllergyType.AllergyTypeIdSeq = int.Parse(objReader["AllergyID"].ToString());

                            if (objReader["AllergyTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["AllergyTypeName"].ToString()))
                            {
                                objPatientAllergy.AllergyType.AllergyName = objReader["AllergyTypeName"].ToString();
                            }
                        }

                        if (objReader["Reaction"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Reaction"].ToString()))
                        {
                            objPatientAllergy.Reaction = objReader["Reaction"].ToString();
                        }

                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            objPatientAllergy.IsActive = objReader["IsActive"].ToString();
                        }

                        PatientAllergyList.Add(objPatientAllergy);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientAllergyDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientAllergyList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Medical History DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientMedicalHistoryDTO> ConvertToPatientMedicalHistoryDTO(IDataReader objReader)
        {
            List<PatientMedicalHistoryDTO> PatientMedicalHistoryList = null;
            try
            {
                if (objReader != null)
                {
                    PatientMedicalHistoryList = new List<PatientMedicalHistoryDTO>();

                    while (objReader.Read())
                    {
                        PatientMedicalHistoryDTO objPatientMedicalHistory = new PatientMedicalHistoryDTO();

                        objPatientMedicalHistory.PatientHistoryMedicalId = int.Parse(objReader["PatientHistoryMedicalID"].ToString());
                        objPatientMedicalHistory.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["ICDCodeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                        {
                            objPatientMedicalHistory.ICDCodeType = new ICDCodeDTO();
                            objPatientMedicalHistory.ICDCodeType.IcdCodeName = objReader["ICDCodeName"].ToString();
                            if (objReader["ICDCodeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeID"].ToString()))
                                objPatientMedicalHistory.ICDCodeType.IcdCodeIdSeq = Int32.Parse(objReader["ICDCodeID"].ToString());
                            objPatientMedicalHistory.ICDCodeType.ProblemType = objReader["ProblemType"].ToString();

                            if (objReader["Description"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                            {
                                objPatientMedicalHistory.ICDCodeType.IcdCodeDescription = objReader["Description"].ToString();
                                if (String.IsNullOrEmpty(objPatientMedicalHistory.ICDCodeType.ProblemType))
                                    objPatientMedicalHistory.ICDCodeType.ProblemType = objReader["Description"].ToString();
                            }
                        }


                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            objPatientMedicalHistory.IsActive = objReader["IsActive"].ToString();
                        }

                        PatientMedicalHistoryList.Add(objPatientMedicalHistory);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientMedicalHistoryDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientMedicalHistoryList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Family History DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientFamilyHistoryDTO> ConvertToPatientFamilyHistoryDTO(IDataReader objReader)
        {
            List<PatientFamilyHistoryDTO> PatientFamilyHistoryList = null;
            try
            {
                if (objReader != null)
                {
                    PatientFamilyHistoryList = new List<PatientFamilyHistoryDTO>();

                    while (objReader.Read())
                    {
                        PatientFamilyHistoryDTO objPatientFamilyHistory = new PatientFamilyHistoryDTO();

                        objPatientFamilyHistory.PatientHistoryFamilyId = int.Parse(objReader["PatientHistoryFamilyID"].ToString());
                        objPatientFamilyHistory.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["ICDCodeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                        {
                            objPatientFamilyHistory.ICDCodeType = new ICDCodeDTO();
                            objPatientFamilyHistory.ICDCodeType.IcdCodeName = objReader["ICDCodeName"].ToString();
                            if (objReader["ICDCodeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeID"].ToString()))
                                objPatientFamilyHistory.ICDCodeType.IcdCodeIdSeq = Int32.Parse(objReader["ICDCodeID"].ToString());
                            objPatientFamilyHistory.ICDCodeType.ProblemType = objReader["ProblemType"].ToString();

                            if (objReader["Description"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ICDCodeName"].ToString()))
                            {
                                objPatientFamilyHistory.ICDCodeType.IcdCodeDescription = objReader["Description"].ToString();
                                if(String.IsNullOrEmpty(objPatientFamilyHistory.ICDCodeType.ProblemType))
                                    objPatientFamilyHistory.ICDCodeType.ProblemType = objReader["Description"].ToString();

                            }
                        }

                        if (objReader["RelationshipTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationshipTypeID"].ToString()))
                        {
                            objPatientFamilyHistory.RelationshipType = new RelationshipTypeDTO();
                            objPatientFamilyHistory.RelationshipType.RelationshipId = int.Parse(objReader["RelationshipTypeID"].ToString());

                            if (objReader["RelationshipName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RelationshipName"].ToString()))
                            {
                                objPatientFamilyHistory.RelationshipType.RelationshipName = objReader["RelationshipName"].ToString();
                            }
                        }

                        if (objReader["IsActive"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsActive"].ToString()))
                        {
                            objPatientFamilyHistory.IsActive = objReader["IsActive"].ToString();
                        }

                        PatientFamilyHistoryList.Add(objPatientFamilyHistory);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientFamilyHistoryDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientFamilyHistoryList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient Insurance DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientInsuranceDTO> ConvertToPatientInsuranceDTO(IDataReader objReader)
        {
            List<PatientInsuranceDTO> PatientInsuranceList = null;
            try
            {
                if (objReader != null)
                {
                    PatientInsuranceList = new List<PatientInsuranceDTO>();

                    while (objReader.Read())
                    {
                        PatientInsuranceDTO objPatientInsurance = new PatientInsuranceDTO();

                        objPatientInsurance.PatientInsuranceId = int.Parse(objReader["PatientInsuranceId"].ToString());
                        objPatientInsurance.PatientId = int.Parse(objReader["PatientID"].ToString());

                        if (objReader["InsuranceType"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceType"].ToString()))
                        {
                            objPatientInsurance.InsuranceType = new InsuranceTypeDTO();
                            objPatientInsurance.InsuranceType.InsuranceTypeId = int.Parse(objReader["InsuranceType"].ToString());

                            if (objReader["InsuranceTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceTypeName"].ToString()))
                            {
                                objPatientInsurance.InsuranceType.InsuranceTypeName = objReader["InsuranceTypeName"].ToString();
                            }
                        }

                        if (objReader["InsuranceNatureId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceNatureId"].ToString()))
                        {
                            objPatientInsurance.InsuranceNature = new InsuranceNatureDTO();
                            objPatientInsurance.InsuranceNature.InsuranceNatureId = int.Parse(objReader["InsuranceNatureId"].ToString());

                            if (objReader["InsuranceNatureName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceNatureName"].ToString()))
                            {
                                objPatientInsurance.InsuranceNature.Description = objReader["InsuranceNatureName"].ToString();
                            }
                        }

                        if (objReader["InsuranceOrganizationId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceOrganizationId"].ToString()))
                        {
                            objPatientInsurance.InsuranceOrganization = new InsuranceOrganizationDTO();
                            objPatientInsurance.InsuranceOrganization.InsuranceOrganizationID = int.Parse(objReader["InsuranceOrganizationId"].ToString());

                            if (objReader["InsuranceOrganizationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceOrganizationName"].ToString()))
                            {
                                objPatientInsurance.InsuranceOrganization.InsuranceOrganizationName = objReader["InsuranceOrganizationName"].ToString();
                            }
                        }

                        if (objReader["PolicyNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PolicyNumber"].ToString()))
                        {
                            objPatientInsurance.PolicyNumber = objReader["PolicyNumber"].ToString();
                        }

                        if (objReader["GroupNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GroupNumber"].ToString()))
                        {
                            objPatientInsurance.GroupNumber = objReader["GroupNumber"].ToString();
                        }

                        if (objReader["InsurancePlan"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsurancePlan"].ToString()))
                        {
                            objPatientInsurance.InsurancePlan = objReader["InsurancePlan"].ToString();
                        }

                        if (objReader["PersonName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PersonName"].ToString()))
                        {
                            objPatientInsurance.InsuredPersonName = objReader["PersonName"].ToString();
                        }

                        if (objReader["PersonDateOfBirth"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PersonDateOfBirth"].ToString()))
                        {
                            objPatientInsurance.InsuredPersonDOB = DateTime.Parse(objReader["PersonDateOfBirth"].ToString());
                        }

                        if (objReader["GenderId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderId"].ToString()))
                        {
                            objPatientInsurance.InsuredPersonGender = new GenderTypeDTO();
                            objPatientInsurance.InsuredPersonGender.GenderId = int.Parse(objReader["GenderId"].ToString());

                            //GenderTypeName cann't be loaded
                        }

                        if (objReader["StartDate"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StartDate"].ToString()))
                        {
                            objPatientInsurance.StartDate = DateTime.Parse(objReader["StartDate"].ToString());
                        }

                        if (objReader["EndDate"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EndDate"].ToString()))
                        {
                            objPatientInsurance.EndDate = DateTime.Parse(objReader["EndDate"].ToString());
                        }

                        if (objReader["Copay"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Copay"].ToString()))
                        {
                            objPatientInsurance.Copay = objReader["Copay"].ToString();
                        }

                        if (objReader["IsEligible"] != DBNull.Value && !string.IsNullOrEmpty(objReader["IsEligible"].ToString()))
                        {
                            objPatientInsurance.IsEligible = objReader["IsEligible"].ToString();
                        }

                        if (objReader["InsuranceStatus"] != DBNull.Value && !string.IsNullOrEmpty(objReader["InsuranceStatus"].ToString()))
                        {
                            objPatientInsurance.InsuranceStatus = new StatusTypeDTO();
                            objPatientInsurance.InsuranceStatus.StatusID = int.Parse(objReader["InsuranceStatus"].ToString());
                        }

                        PatientInsuranceList.Add(objPatientInsurance);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientInsuranceDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientInsuranceList;
        }

        internal static List<ImageTypeDTO> ImageTypeTOConverter(IDataReader objReader)
        {
            List<ImageTypeDTO> ImageTypeList = null;
            ImageTypeDTO imageTypeObj = null;

            try
            {
                if (objReader != null)
                {
                    ImageTypeList = new List<ImageTypeDTO>();

                    while (objReader.Read())
                    {
                        imageTypeObj = new ImageTypeDTO();

                        if (objReader["ImageTypeId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ImageTypeId"].ToString()))
                        {
                            imageTypeObj.ImageTypeId = int.Parse(objReader["ImageTypeId"].ToString());
                        }

                        if (objReader["TypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TypeName"].ToString()))
                        {
                            imageTypeObj.TypeName = objReader["TypeName"].ToString();
                        }
                        if (objReader["TypeDescription"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TypeDescription"].ToString()))
                        {
                            imageTypeObj.TypeDescription = objReader["TypeDescription"].ToString();
                        }

                        ImageTypeList.Add(imageTypeObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return ImageTypeList;
        }

        internal static List<UserBasicInfoDTO> UserBasicInfoConverter(SqlDataReader objReader)
        {
            List<UserBasicInfoDTO> UserList = null;
            UserBasicInfoDTO UserObj = null;

            try
            {
                if (objReader != null)
                {
                    UserList = new List<UserBasicInfoDTO>();

                    while (objReader.Read())
                    {
                        UserObj = new UserBasicInfoDTO();

                        if (objReader["UserId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserId"].ToString()))
                        {
                            UserObj.UserId = int.Parse(objReader["UserId"].ToString());
                        }
                        if (objReader["PrefixTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PrefixTypeID"].ToString()))
                        {
                            UserObj.PrefixTypeID = int.Parse(objReader["PrefixTypeID"].ToString());
                        }
                        if (objReader["GenderTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderTypeID"].ToString()))
                        {
                            UserObj.GenderTypeID = int.Parse(objReader["GenderTypeID"].ToString());
                        }
                        if (objReader["GenderName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderName"].ToString()))
                        {
                            UserObj.GenderName = objReader["GenderName"].ToString();
                        }
                        if (objReader["FirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FirstName"].ToString()))
                        {
                            UserObj.FirstName = objReader["FirstName"].ToString();
                        }
                        if (objReader["LastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LastName"].ToString()))
                        {
                            UserObj.LastName = objReader["LastName"].ToString();
                        }
                        if (objReader["MiddleName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MiddleName"].ToString()))
                        {
                            UserObj.MiddleName = objReader["MiddleName"].ToString();
                        }
                        if (objReader["Suffix"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Suffix"].ToString()))
                        {
                            UserObj.Suffix = objReader["Suffix"].ToString();
                        }

                        if (objReader["SSN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SSN"].ToString()))
                        {
                            UserObj.SSN = objReader["SSN"].ToString();
                        }

                        if (objReader["UserName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserName"].ToString()))
                        {
                            UserObj.UserName = objReader["UserName"].ToString();
                        }

                        UserList.Add(UserObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return UserList;
        }

        internal static BaseUserDTO BaseUserDTOConverter(IDataReader objReader)
        {
            BaseUserDTO UserObj = null;
            try
            {
                if (objReader != null && objReader.Read())
                {
                    UserObj = new BaseUserDTO();

                    if (objReader["UserId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserId"].ToString()))
                    {
                        UserObj.UserId = int.Parse(objReader["UserId"].ToString());
                    }

                    if (objReader["PrefixTypeID"] != DBNull.Value)
                    {
                        if (UserObj.Prefix == null)
                            UserObj.Prefix = new PrefixTypeDTO();

                        UserObj.Prefix.PrefixId = int.Parse(objReader["PrefixTypeID"].ToString());
                        UserObj.Prefix.PrefixName = objReader["PrefixName"].ToString();

                    }


                    if (objReader["FirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FirstName"].ToString()))
                    {
                        UserObj.FirstName = objReader["FirstName"].ToString();
                    }
                    if (objReader["LastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LastName"].ToString()))
                    {
                        UserObj.LastName = objReader["LastName"].ToString();
                    }
                    if (objReader["MiddleName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MiddleName"].ToString()))
                    {
                        UserObj.MiddleName = objReader["MiddleName"].ToString();
                    }
                    if (objReader["Suffix"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Suffix"].ToString()))
                    {
                        UserObj.Suffix = objReader["Suffix"].ToString();
                    }

                    if (objReader["SSN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SSN"].ToString()))
                    {
                        UserObj.SSN = objReader["SSN"].ToString();
                    }
                    if (objReader["UserEmail"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserEmail"].ToString()))
                    {
                        UserObj.UserEmail = objReader["UserEmail"].ToString();
                    }
                    if (objReader["UserPassword"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserPassword"].ToString()))
                    {
                        UserObj.UserPassword = objReader["UserPassword"].ToString();
                    }
                    if (objReader["GenderTypeID"] != DBNull.Value)
                    {
                        if (UserObj.Gender == null)
                            UserObj.Gender = new GenderTypeDTO();

                        UserObj.Gender.GenderId = int.Parse(objReader["GenderTypeID"].ToString());
                        UserObj.Gender.GenderName = objReader["GenderName"].ToString();
                    }

                    if (objReader["AccountStatus"] != DBNull.Value)
                    {
                        if (UserObj.AccountStatus == null)
                            UserObj.AccountStatus = new StatusTypeDTO();

                        UserObj.AccountStatus.StatusID = int.Parse(objReader["AccountStatus"].ToString());
                    }
                    if (objReader["CreatedDate"] != DBNull.Value)
                    {
                        UserObj.CreatedDate = DateTime.Parse(objReader["CreatedDate"].ToString());
                    }

                    if (objReader["LicenseNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LicenseNumber"].ToString()))
                    {
                        UserObj.LicenseNumber = objReader["LicenseNumber"].ToString();
                    }
                    if (objReader["FederalTaxId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FederalTaxId"].ToString()))
                    {
                        UserObj.FederalTaxId = objReader["FederalTaxId"].ToString();
                    }
                    if (objReader["EIN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["EIN"].ToString()))
                    {
                        UserObj.EIN = objReader["EIN"].ToString();
                    }
                    if (objReader["QuivusUserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["QuivusUserID"].ToString()))
                    {
                        UserObj.QuivusUserID = objReader["QuivusUserID"].ToString();
                    }
                    if (objReader["IsActive"] != DBNull.Value)
                    {
                        UserObj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                    }

                    if (objReader["IsLockedOut"] != DBNull.Value)
                    {
                        UserObj.IsLockedOut = bool.Parse(objReader["IsLockedOut"].ToString());
                    }
                    if (objReader["TextPassword"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TextPassword"].ToString()))
                    {
                        UserObj.TextPassword = objReader["TextPassword"].ToString();
                    }
                    if (objReader["CreatedBy"] != DBNull.Value)
                    {
                        UserObj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                    }

                    if (objReader["CreatedAt"] != DBNull.Value)
                    {
                        UserObj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                    }

                    if (objReader["ModifiedBy"] != DBNull.Value)
                    {
                        UserObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                    }

                    if (objReader["ModifiedAt"] != DBNull.Value)
                    {
                        UserObj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                    }

                    if (objReader["IsDeleted"] != DBNull.Value)
                    {
                        UserObj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                    }
                    if (objReader["UserName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserName"].ToString()))
                    {
                        UserObj.UserName = objReader["UserName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return UserObj;
        }

        internal static List<PatientImmunizationInfoDTO> ConvertToPatientProfileImmunizationInfo(IDataReader objReader)
        {
            List<PatientImmunizationInfoDTO> TypeList = null;
            PatientImmunizationInfoDTO TypeObj = null;

            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PatientImmunizationInfoDTO>();

                    while (objReader.Read())
                    {
                        TypeObj = new PatientImmunizationInfoDTO();

                        if (objReader["ImmunizationTypeName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ImmunizationTypeName"].ToString()))
                        {
                            TypeObj.ImmunizationName = objReader["ImmunizationTypeName"].ToString();
                        }

                        if (objReader["DateAdmin"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DateAdmin"].ToString()))
                        {
                            TypeObj.AdminDate = DateTime.Parse(objReader["DateAdmin"].ToString());
                        }
                        if (objReader["NextDoseDue"] != DBNull.Value && !string.IsNullOrEmpty(objReader["NextDoseDue"].ToString()))
                        {
                            TypeObj.NextDueDate = DateTime.Parse(objReader["NextDoseDue"].ToString());
                        }
                        if (objReader["Series"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Series"].ToString()))
                        {
                            TypeObj.Series = objReader["Series"].ToString();
                        }
                        TypeList.Add(TypeObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileImmunizationInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientChronicMedicationInfoDTO> ConvertToPatientProfileMedicationInfo(IDataReader objReader)
        {
            List<PatientChronicMedicationInfoDTO> TypeList = null;
            PatientChronicMedicationInfoDTO TypeObj = null;

            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PatientChronicMedicationInfoDTO>();

                    while (objReader.Read())
                    {
                        TypeObj = new PatientChronicMedicationInfoDTO();

                        //if (objReader["BrandName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["BrandName"].ToString()))
                        //{
                        //    TypeObj.BrandName = objReader["BrandName"].ToString();
                        //}

                        if (objReader["GenericName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenericName"].ToString()))
                        {
                            TypeObj.MedicationName = objReader["GenericName"].ToString();
                        }

                        if (objReader["Strength"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Strength"].ToString()))
                        {
                            TypeObj.MedicationStrength = objReader["Strength"].ToString();
                        }
                        if (objReader["Dose"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Dose"].ToString()))
                        {
                            TypeObj.Dose = objReader["Dose"].ToString();
                        }
                        // if (objReader["Status"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Status"].ToString()))
                        //{
                        //    TypeObj.Status = objReader["Status"].ToString();
                        //}
                        TypeList.Add(TypeObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileImmunizationInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return TypeList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static PatientBasicInfoDTO ConvertToPatientBasicInfoDTO(IDataReader objReader)
        {
            PatientBasicInfoDTO patientDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        patientDTO = new PatientBasicInfoDTO();


                        if (objReader["QuivusID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["QuivusID"].ToString()))
                        {
                            patientDTO.QuivusId = objReader["QuivusID"].ToString();
                        }

                        if (objReader["GenderName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderName"].ToString()))
                        {
                            patientDTO.GenderName = objReader["GenderName"].ToString();
                        }
                        if (objReader["LastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LastName"].ToString()))
                        {
                            patientDTO.LastName = objReader["LastName"].ToString();
                        }
                        if (objReader["FirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FirstName"].ToString()))
                        {
                            patientDTO.FirstName = objReader["FirstName"].ToString();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.PATIENT_BASIC_INFO_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientBasicInfoDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientDTO;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<ClinicRoomDTO> ConvertToClinicRoomList(IDataReader objReader)
        {
            ClinicRoomDTO objClinicRoom = null;
            List<ClinicRoomDTO> clinicRoomList = null;
            try
            {
                if (objReader != null)
                {
                    while (objReader.Read())
                    {
                        objClinicRoom = new ClinicRoomDTO();

                        if (clinicRoomList == null)
                        {
                            clinicRoomList = new List<ClinicRoomDTO>();
                        }

                        if (objReader["ClinicRoomID"] != DBNull.Value)
                        {
                            objClinicRoom.ClinicRoomId = int.Parse(objReader["ClinicRoomID"].ToString());
                        }

                        if (objReader["StationID"] != DBNull.Value)
                        {
                            objClinicRoom.StationId = int.Parse(objReader["StationID"].ToString());
                        }

                        if (objReader["RoomTitle"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RoomTitle"].ToString()))
                        {
                            objClinicRoom.RoomTitle = objReader["RoomTitle"].ToString();
                        }
                        if (objReader["RoomStatus"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RoomStatus"].ToString()))
                        {
                            objClinicRoom.RoomStatus = objReader["RoomStatus"].ToString();
                        }
                        if (objReader["RoomCode"] != DBNull.Value && !string.IsNullOrEmpty(objReader["RoomCode"].ToString()))
                        {
                            objClinicRoom.RoomCode = objReader["RoomCode"].ToString();
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            objClinicRoom.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value && !string.IsNullOrEmpty(objReader["CreatedAt"].ToString()))
                        {
                            objClinicRoom.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            objClinicRoom.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ModifiedAt"].ToString()))
                        {
                            objClinicRoom.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }
                        clinicRoomList.Add(objClinicRoom);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.PATIENT_BASIC_INFO_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientBasicInfoDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return clinicRoomList;
        }

        internal static PatientUserInfo ConvertToPatientUserInfoDTO(IDataReader objReader)
        {
            PatientUserInfo patientDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        patientDTO = new PatientUserInfo();


                        if (objReader["PatientID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PatientID"].ToString()))
                        {
                            patientDTO.PatientID = Int32.Parse(objReader["PatientID"].ToString());
                        }

                        if (objReader["UserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserID"].ToString()))
                        {
                            patientDTO.UserID = Int32.Parse(objReader["UserID"].ToString());
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.PATIENT_BASIC_INFO_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientUserInfoDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return patientDTO;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 8 Feb 2011
        /// Method Used to convert into Patient DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static DoctorDTO ConvertToDoctorDTO(IDataReader objReader)
        {
            DoctorDTO doctorDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        doctorDTO = new DoctorDTO();

                        doctorDTO.DoctorId = int.Parse(objReader["DoctorId"].ToString());

                        doctorDTO.UserId = int.Parse(objReader["UserID"].ToString());

                        if (objReader["SpecialityTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpecialityTypeID"].ToString()))
                        {
                            doctorDTO.SpecialityType = new SpecialityTypeDTO();
                            doctorDTO.SpecialityType.SpecialityTypeID = int.Parse(objReader["SpecialityTypeID"].ToString());

                            if (objReader["SpecialityName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpecialityName"].ToString()))
                            {
                                doctorDTO.SpecialityType.SpecialityName = objReader["SpecialityName"].ToString();
                            }
                        }

                        if (objReader["NPI"] != DBNull.Value && !string.IsNullOrEmpty(objReader["NPI"].ToString()))
                        {
                            doctorDTO.NPI = objReader["NPI"].ToString();
                        }

                        if (objReader["UPIN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UPIN"].ToString()))
                        {
                            doctorDTO.UPIN = objReader["UPIN"].ToString();
                        }

                        if (objReader["DAE"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DAE"].ToString()))
                        {
                            doctorDTO.DAE = objReader["DAE"].ToString();
                        }

                        if (objReader["TPI"] != DBNull.Value && !string.IsNullOrEmpty(objReader["TPI"].ToString()))
                        {
                            doctorDTO.TPI = objReader["TPI"].ToString();
                        }

                        if (objReader["DentistLicenseNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["DentistLicenseNumber"].ToString()))
                        {
                            doctorDTO.DentistLicenseNumber = objReader["DentistLicenseNumber"].ToString();
                        }

                        if (objReader["PriorAuthorization"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PriorAuthorization"].ToString()))
                        {
                            doctorDTO.PriorAuthorization = objReader["PriorAuthorization"].ToString();
                        }

                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            doctorDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }

                        if (objReader["WorkStart"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkStart"].ToString()))
                        {
                            doctorDTO.WorkStartTime = objReader["WorkStart"].ToString();
                        }

                        if (objReader["WorkEnd"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkEnd"].ToString()))
                        {
                            doctorDTO.WorkEndTime = objReader["WorkEnd"].ToString();
                        }

                        if (objReader["WorkDays"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkDays"].ToString()))
                        {
                            doctorDTO.WorkingDays = objReader["WorkDays"].ToString();
                        }

                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            doctorDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }

                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            doctorDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            doctorDTO.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            doctorDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            doctorDTO.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            doctorDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }


                        if (objReader["Active"] != DBNull.Value)
                        {
                            doctorDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            doctorDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToDoctorDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return doctorDTO;
        }



        internal static NursePractitionerDTO ConvertToNursePractionerDTO(IDataReader objReader)
        {
            NursePractitionerDTO npDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        npDTO = new NursePractitionerDTO();

                        npDTO.NursePractitionerId = int.Parse(objReader["NursePractitionerID"].ToString());

                        npDTO.UserId = int.Parse(objReader["UserID"].ToString());

                        if (objReader["SpecialityTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpecialityTypeID"].ToString()))
                        {
                            npDTO.SpecialityType = new SpecialityTypeDTO();
                            npDTO.SpecialityType.SpecialityTypeID = int.Parse(objReader["SpecialityTypeID"].ToString());

                            if (objReader["SpecialityName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpecialityName"].ToString()))
                            {
                                npDTO.SpecialityType.SpecialityName = objReader["SpecialityName"].ToString();
                            }
                        }


                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            npDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }


                        if (objReader["WorkStart"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkStart"].ToString()))
                        {
                            npDTO.WorkStartTime = objReader["WorkStart"].ToString();
                        }

                        if (objReader["WorkEnd"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkEnd"].ToString()))
                        {
                            npDTO.WorkEndTime = objReader["WorkEnd"].ToString();
                        }

                        if (objReader["WorkDays"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkDays"].ToString()))
                        {
                            npDTO.WorkingDays = objReader["WorkDays"].ToString();
                        }




                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            npDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            npDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            npDTO.CreatedDate = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            npDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            npDTO.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            npDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            npDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            npDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }



                        



                      
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".NursePractitionerDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return npDTO;
        }



        internal static NurseDTO ConvertToNurseDTO(IDataReader objReader)
        {
            NurseDTO nDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        nDTO = new NurseDTO();

                        nDTO.NURSE_ID_SEQ = int.Parse(objReader["NurseID"].ToString());

                        nDTO.USER_ID = int.Parse(objReader["UserID"].ToString());

                        if (objReader["SpecialityTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpecialityTypeID"].ToString()))
                        {
                            nDTO.SpecialityType = new SpecialityTypeDTO();
                            nDTO.SpecialityType.SpecialityTypeID = int.Parse(objReader["SpecialityTypeID"].ToString());

                            if (objReader["SpecialityName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpecialityName"].ToString()))
                            {
                                nDTO.SpecialityType.SpecialityName = objReader["SpecialityName"].ToString();
                            }
                        }


                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            nDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }


                        if (objReader["WorkStart"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkStart"].ToString()))
                        {
                            nDTO.WorkStartTime = objReader["WorkStart"].ToString();
                        }

                        if (objReader["WorkEnd"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkEnd"].ToString()))
                        {
                            nDTO.WorkEndTime = objReader["WorkEnd"].ToString();
                        }

                        if (objReader["WorkDays"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkDays"].ToString()))
                        {
                            nDTO.WorkingDays = objReader["WorkDays"].ToString();
                        }




                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            nDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            nDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            nDTO.CREATE_DT = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            nDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            nDTO.ModifedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }








                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToNurseDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return nDTO;
        }


        internal static StudentDTO ConvertToStudentDTO(IDataReader objReader)
        {
            StudentDTO nDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        nDTO = new StudentDTO();

                        nDTO.StudentIdSeq = int.Parse(objReader["StudentID"].ToString());

                        nDTO.USER_ID = int.Parse(objReader["UserID"].ToString());

                        if (objReader["SpecialityTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpecialityTypeID"].ToString()))
                        {
                            nDTO.SpecialityType = new SpecialityTypeDTO();
                            nDTO.SpecialityType.SpecialityTypeID = int.Parse(objReader["SpecialityTypeID"].ToString());

                            if (objReader["SpecialityName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SpecialityName"].ToString()))
                            {
                                nDTO.SpecialityType.SpecialityName = objReader["SpecialityName"].ToString();
                            }
                        }


                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            nDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }


                        if (objReader["WorkStart"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkStart"].ToString()))
                        {
                            nDTO.WorkStartTime = objReader["WorkStart"].ToString();
                        }

                        if (objReader["WorkEnd"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkEnd"].ToString()))
                        {
                            nDTO.WorkEndTime = objReader["WorkEnd"].ToString();
                        }

                        if (objReader["WorkDays"] != DBNull.Value && !string.IsNullOrEmpty(objReader["WorkDays"].ToString()))
                        {
                            nDTO.WorkingDays = objReader["WorkDays"].ToString();
                        }




                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            nDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            nDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            nDTO.CREATE_DT = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            nDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            nDTO.ModifedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }








                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToStudentDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return nDTO;
        }

        internal static LabTechnicianDTO ConvertToLabTechDTO(IDataReader objReader)
        {
            LabTechnicianDTO nDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        nDTO = new LabTechnicianDTO();

                        nDTO.LabTechnicianId = int.Parse(objReader["LabTechID"].ToString());

                        nDTO.UserId = int.Parse(objReader["UserID"].ToString());



                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            nDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }


                      




                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            nDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            nDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            nDTO.CreatedDate = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }



                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            nDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            nDTO.ModifiedDate = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToLabTechDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return nDTO;
        }


        internal static BillingManagerDTO ConvertToBillingManagerDTO(IDataReader objReader)
        {
            BillingManagerDTO nDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        nDTO = new BillingManagerDTO();

                        nDTO.BillingManagerId = int.Parse(objReader["BillingManagerID"].ToString());

                        nDTO.UserId = int.Parse(objReader["UserID"].ToString());



                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            nDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }







                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            nDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            nDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            nDTO.CreatedDate = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }



                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            nDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            nDTO.ModifiedDate = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToBillingManagerDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return nDTO;
        }

        internal static JnrSecurityAdminDTO ConvertToJnrSecurityAdminDTO(IDataReader objReader)
        {
            JnrSecurityAdminDTO nDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        nDTO = new JnrSecurityAdminDTO();

                        nDTO.JrSecurityAdminId = int.Parse(objReader["JrSAId"].ToString());

                        nDTO.UserId = int.Parse(objReader["UserID"].ToString());



                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            nDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }







                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            nDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            nDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            nDTO.CreatedDate = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }



                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            nDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            nDTO.ModifiedDate = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToJnrSecurityAdminDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return nDTO;
        }

        internal static PracticeManagerDTO ConvertToPracticeManagerDTO(IDataReader objReader)
        {
            PracticeManagerDTO nDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        nDTO = new PracticeManagerDTO();

                        nDTO.PracticeManagerId = int.Parse(objReader["PracticeManagerID"].ToString());

                        nDTO.UserId = int.Parse(objReader["UserID"].ToString());



                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            nDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }







                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            nDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            nDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            nDTO.CreatedDate = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }



                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            nDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            nDTO.ModifiedDate = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPracticeManagerDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return nDTO;
        }


        internal static BillingAssistantDTO ConvertToBillingAssistant(IDataReader objReader)
        {
            BillingAssistantDTO nDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        nDTO = new BillingAssistantDTO();

                        nDTO.BillingAssistantId = int.Parse(objReader["BillingAssistantID"].ToString());

                        nDTO.UserId = int.Parse(objReader["UserID"].ToString());



                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            nDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }







                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            nDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            nDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            nDTO.CreatedDate = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }



                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            nDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            nDTO.ModifiedDate = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToBillingAssistant", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return nDTO;
        }


        internal static SecurityAdminDTO ConvertToSecurityAdminDTO(IDataReader objReader)
        {
            SecurityAdminDTO nDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        nDTO = new SecurityAdminDTO();

                        nDTO.SecurityAdminId = int.Parse(objReader["SecurityAdminID"].ToString());

                        nDTO.UserId = int.Parse(objReader["UserID"].ToString());



                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            nDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }







                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            nDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            nDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            nDTO.CreatedDate = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }



                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            nDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            nDTO.ModifiedDate = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToSecurityAdminDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return nDTO;
        }

        internal static ReceptionistDTO ConvertToReceptionistDTO(IDataReader objReader)
        {
            ReceptionistDTO nDTO = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        nDTO = new ReceptionistDTO();

                        nDTO.ReceptionistId = int.Parse(objReader["ReceptionistId"].ToString());

                        nDTO.UserId = int.Parse(objReader["UserID"].ToString());



                        if (objReader["RecGuid"] != DBNull.Value)
                        {
                            nDTO.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                        }







                        if (objReader["IsProfileComplete"] != DBNull.Value)
                        {
                            nDTO.IsProfileComplete = bool.Parse(objReader["IsProfileComplete"].ToString());
                        }




                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            nDTO.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            nDTO.CreatedDate = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }



                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            nDTO.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            nDTO.ModifiedDate = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Active"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Active"].ToString());
                        }

                        if (objReader["Locked"] != DBNull.Value)
                        {
                            nDTO.IsDeleted = bool.Parse(objReader["Locked"].ToString());
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToReceptionistDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return nDTO;
        }

        /// <summary>
        /// Author Sami
        /// Date 15 DEC 2011
        /// Method Used to convert into Clinic DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static ClinicDTO ConvertToClinicDTO(IDataReader objReader)
        {
            ClinicDTO clinic = null;

            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        clinic = new ClinicDTO();
                        clinic.ClinicBranchInfo = new ClinicBranchDTO();
                        clinic.ClinicBranchInfo.DefaultStation = new StationDTO();

                        clinic.ClinicBranchInfo.ClinicBranchIdSeq = int.Parse(objReader["ClinicBranchID"].ToString());

                        if (objReader["ClinicBranchAddress"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicBranchAddress"].ToString()))
                        {
                            clinic.ClinicBranchInfo.Address = objReader["ClinicBranchAddress"].ToString();
                        }

                        if (objReader["BranchName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["BranchName"].ToString()))
                        {
                            clinic.ClinicBranchInfo.ClinicBranchName = objReader["BranchName"].ToString();
                        }

                        if (objReader["FaxNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FaxNumber"].ToString()))
                        {
                            clinic.ClinicBranchInfo.FaxNumber = objReader["FaxNumber"].ToString();
                        }

                        if (objReader["BranchCode"] != DBNull.Value && !string.IsNullOrEmpty(objReader["BranchCode"].ToString()))
                        {
                            clinic.ClinicBranchInfo.BranchCode = objReader["BranchCode"].ToString();
                        }

                        if (objReader["ClinicBranchPhoneNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicBranchPhoneNumber"].ToString()))
                        {
                            clinic.ClinicBranchInfo.PhoneNumber = objReader["ClinicBranchPhoneNumber"].ToString();
                        }

                        if (objReader["ClinicBranchState"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicBranchState"].ToString()))
                        {
                            clinic.ClinicBranchInfo.State = objReader["ClinicBranchState"].ToString();
                        }


                        if (objReader["ClinicStatus"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicStatus"].ToString()))
                        {
                            clinic.ClinicBranchInfo.ClinicStatus = objReader["ClinicStatus"].ToString();
                        }

                        if (objReader["ClinicBranchCity"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicBranchCity"].ToString()))
                        {
                            clinic.ClinicBranchInfo.City = objReader["ClinicBranchCity"].ToString();
                        }

                        if (objReader["ContactPerson"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ContactPerson"].ToString()))
                        {
                            clinic.ClinicBranchInfo.ContactPerson = objReader["ContactPerson"].ToString();
                        }

                        if (objReader["ClinicBranchZip"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicBranchZip"].ToString()))
                        {
                            clinic.ClinicBranchInfo.ZIP = objReader["ClinicBranchZip"].ToString();
                        }

                        clinic.ClinicIdSeq = int.Parse(objReader["ClinicID"].ToString());

                        if (objReader["ClinicName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicName"].ToString()))
                        {
                            clinic.ClinicName = objReader["ClinicName"].ToString();
                        }

                        if (objReader["ClinicStatus"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicStatus"].ToString()))
                        {
                            clinic.ClinicStatus = objReader["ClinicStatus"].ToString();
                        }

                        clinic.ClinicBranchInfo.DefaultStation.ClinicBranchID = int.Parse(objReader["StationID"].ToString());

                        if (objReader["StationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StationName"].ToString()))
                        {
                            clinic.ClinicBranchInfo.DefaultStation.StationName = objReader["StationName"].ToString();
                        }

                        if (objReader["StationAddress"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StationAddress"].ToString()))
                        {
                            clinic.ClinicBranchInfo.DefaultStation.Address = objReader["StationAddress"].ToString();
                        }

                        if (objReader["StationCity"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StationCity"].ToString()))
                        {
                            clinic.ClinicBranchInfo.DefaultStation.City = objReader["StationCity"].ToString();
                        }

                        if (objReader["StationPhoneNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StationPhoneNumber"].ToString()))
                        {
                            clinic.ClinicBranchInfo.DefaultStation.PhoneNumber = objReader["StationPhoneNumber"].ToString();
                        }

                        if (objReader["StationFaxNumber"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StationFaxNumber"].ToString()))
                        {
                            clinic.ClinicBranchInfo.DefaultStation.FaxNumber = objReader["StationFaxNumber"].ToString();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToDoctorDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return clinic;
        }


        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 17 Jan 2012
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static ClinicProviderSearchDTO ConvertToProviderSearchDTO(IDataReader objReader)
        {
            ClinicProviderSearchDTO providerSearchDTO = null;

            try
            {
                if (objReader != null)
                {
                    providerSearchDTO = new ClinicProviderSearchDTO();

                    while (objReader.Read())
                    {
                        providerSearchDTO.EIN = objReader["EIN"].ToString();

                        providerSearchDTO.DAE = objReader["DAE"].ToString();
                        providerSearchDTO.FederalTaxId = objReader["FederalTaxId"].ToString();
                        providerSearchDTO.FirstName = objReader["FirstName"].ToString();
                        providerSearchDTO.LastName = objReader["LastName"].ToString();
                        providerSearchDTO.MiddleName = objReader["MiddleName"].ToString();
                        providerSearchDTO.GenderName = objReader["GenderName"].ToString();

                        if (!string.IsNullOrEmpty(objReader["ImageData"].ToString()))
                            providerSearchDTO.ImageData = (byte[])objReader["ImageData"];// objReader["ImageData"]);

                        providerSearchDTO.LicenseNumber = objReader["LicenseNumber"].ToString();
                        if (!string.IsNullOrEmpty(objReader["NPI"].ToString()))
                            providerSearchDTO.NPI = int.Parse(objReader["NPI"].ToString());
                        if (!string.IsNullOrEmpty(objReader["PrefixTypeID"].ToString()))
                            providerSearchDTO.PrefixTypeID = int.Parse(objReader["PrefixTypeID"].ToString());
                        providerSearchDTO.RecGuid = objReader["RecGuid"].ToString();
                        providerSearchDTO.SSN = objReader["SSN"].ToString();
                        providerSearchDTO.Suffix = objReader["Suffix"].ToString();
                        providerSearchDTO.TPI = objReader["TPI"].ToString();
                        providerSearchDTO.UPIN = objReader["UPIN"].ToString();

                        providerSearchDTO.UserEmail = objReader["UserEmail"].ToString();
                        if (!string.IsNullOrEmpty(objReader["UserId"].ToString()))
                            providerSearchDTO.UserId = int.Parse(objReader["UserId"].ToString());
                        providerSearchDTO.WorkDays = objReader["WorkDays"].ToString();
                        providerSearchDTO.WorkEnd = objReader["WorkEnd"].ToString();
                        providerSearchDTO.WorkStart = objReader["WorkStart"].ToString();
                        providerSearchDTO.Title = objReader["Title"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertProviderSearchDTO", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return providerSearchDTO;
        }


        internal static List<PatientBasicUserDTO> UserPatientBasicInfoConverter(SqlDataReader objReader)
        {
            List<PatientBasicUserDTO> PatientList = null;
            PatientBasicUserDTO PatientObj = null;

            try
            {
                if (objReader != null)
                {
                    PatientList = new List<PatientBasicUserDTO>();

                    while (objReader.Read())
                    {
                        PatientObj = new PatientBasicUserDTO();

                        if (objReader["UserId"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserId"].ToString()))
                        {
                            PatientObj.UserId = int.Parse(objReader["UserId"].ToString());
                        }
                        if (objReader["PrefixTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PrefixTypeID"].ToString()))
                        {
                            PatientObj.PrefixTypeID = int.Parse(objReader["PrefixTypeID"].ToString());
                        }
                        if (objReader["GenderTypeID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderTypeID"].ToString()))
                        {
                            PatientObj.GenderTypeID = int.Parse(objReader["GenderTypeID"].ToString());
                        }
                        if (objReader["GenderName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["GenderName"].ToString()))
                        {
                            PatientObj.GenderName = objReader["GenderName"].ToString();
                        }
                        if (objReader["FirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FirstName"].ToString()))
                        {
                            PatientObj.FirstName = objReader["FirstName"].ToString();
                        }
                        if (objReader["LastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LastName"].ToString()))
                        {
                            PatientObj.LastName = objReader["LastName"].ToString();
                        }
                        if (objReader["MiddleName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MiddleName"].ToString()))
                        {
                            PatientObj.MiddleName = objReader["MiddleName"].ToString();
                        }
                        if (objReader["Suffix"] != DBNull.Value && !string.IsNullOrEmpty(objReader["Suffix"].ToString()))
                        {
                            PatientObj.Suffix = objReader["Suffix"].ToString();
                        }

                        if (objReader["SSN"] != DBNull.Value && !string.IsNullOrEmpty(objReader["SSN"].ToString()))
                        {
                            PatientObj.SSN = objReader["SSN"].ToString();
                        }

                        if (objReader["UserName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserName"].ToString()))
                        {
                            PatientObj.UserName = objReader["UserName"].ToString();
                        }

                        if (objReader["PatientID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["PatientID"].ToString()))
                        {
                            PatientObj.PatientId = int.Parse(objReader["PatientID"].ToString());
                        }

                        PatientList.Add(PatientObj);
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientProfileAllergyInfo", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return PatientList;
        }
    }
}
