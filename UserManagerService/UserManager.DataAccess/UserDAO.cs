using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Quivus.Utility.Logger;
using UserManagerService.DataAccess.DB_Util;
using Quivus.Model.User;
using Quivus.Model.Common;
using Quivus.Model.TypeDTOs;
using Quivus.Utility;
using Quivus.Utility.CustomException;
using Quivus.Model.SearchEngine;
using System.Configuration;
using System.Reflection;
using Quivus.Model.QuickLink;
using Quivus.Model.Search;
using Quivus.Model.User.Patient;
using Quivus.Utility.Utilities;
using UserManagerService.DataAccess;

using Quivus.Model.RecentPatient;
using Quivus.Model.Common;
using Quivus.Model.Image;
using UserManager.DataAccess.DB_Util;
using Quivus.Model.User;
using QuivusModel.User.Patient;
using QuivusModel.User;
using QuivusModel.Common;
using QuivusModel.Association;
using QuivusModel.Communication.MailDTO;
using QuivusModel.Search;



namespace UserManagerService.DataAccess
{
    public class UserDAO : BaseDAO
    {
        /// <summary>
        /// Author Mansoor Ud-Din
        /// Created Date 12 jan 2011
        /// This Method call the DB proc to Verfiy the User Credential
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>

        public LoginDTO AutheticateUser(string userName, string userPassword, string hashPassword)
        {
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_DEFAULT_AUTHENTICATE_SP;
            LoginDTO loginDTO = null;

            bool IsValidUser = false;
            bool IsUserLocked = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@UserLoginID", DbType.String, userName);
                db.AddInParameter(command, "@UserPassword", DbType.String, userPassword);
                db.AddInParameter(command, "@UserHashedPassword", DbType.String, hashPassword);

                using (objIReader = db.ExecuteReader(command))
                {
                    // will always return this row to check if the user is authenticated
                    if (objIReader.Read())
                    {
                        IsValidUser = bool.Parse(objIReader["IsValidUser"].ToString());
                        IsUserLocked = bool.Parse(objIReader["IsUserLockedOut"].ToString());
                    }

                    if (IsValidUser && !IsUserLocked)
                    {
                        loginDTO = DataReaderToDTOConverter.LoginDTOConverter(objIReader);
                    }
                    else if (!IsValidUser)
                    {
                        throw new AuthenticationException(ProjectConstant.INVALID_LOGIN_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidCredentails);
                    }
                    else if (IsUserLocked)
                    {
                        throw new AuthenticationException(ProjectConstant.ACCOUNT_lOCKED_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.AccountLocked);
                    }
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                //objIReader.Close();
            }

            return loginDTO;
        }

        /// <summary>
        /// Author Mansoor Ud-Din
        /// Created Date 14 jan 2011
        /// This Method call the DB proc to log the user login, for audit 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="UserType"></param>
        /// <param name="ClinicBranch"></param>
        /// <param name="isSucessFull"></param>

        public void LogLoginHistroy(int userId, int userType, int clinicBranch, bool isSucessFull)
        {
            try
            {
                QuivusLogger.Info("Calling SP:: " + UserConstant.USER_DEFAULT_LOGIN_LOG_SP);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(UserConstant.USER_DEFAULT_LOGIN_LOG_SP);

                db.AddInParameter(command, "@UserID", DbType.Int32, userId);
                db.AddInParameter(command, "@UserTypeID", DbType.Int32, userType);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranch);
                db.AddInParameter(command, "@IsLoginSuccessful", DbType.Boolean, isSucessFull);

                db.ExecuteNonQuery(command);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public List<SearchEngineDTO> LoadUserSearchEngineByLocation(int UserId, int ClinicBranchId)
        {
            List<SearchEngineDTO> SearchengineList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_LOCATION_SEARCHENGINE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ProviderUserID", DbType.Int32, UserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    SearchengineList = DataReaderToDTOConverter.SearchEngineLocationConverter(objIReader);
                }
            }

            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return SearchengineList;
        }

        /// <summary>
        /// Author Mansoor
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Quick Links List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public List<QuickLinkDTO> LoadUserDefaultQuickLinksByLocation(int UserId, int ClinicBranchId)
        {
            List<QuickLinkDTO> quickLinkList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_DEFUALT_QUICK_LINK_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ProviderUserID", DbType.Int32, UserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    quickLinkList = DataReaderToDTOConverter.QuickLinkConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return quickLinkList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// New Method As we might need different functionality In future
        /// </summary>
        public List<SearchEngineDTO> LoadAllSearchEngines()
        {
            List<SearchEngineDTO> SearchengineList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.ALL_SEARCHENGINE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    SearchengineList = DataReaderToDTOConverter.SearchEngineAllConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return SearchengineList;
        }


        //Change by Mansoor
        //Date 11 Feb 2011
        //ExecuteReader is replaced with ExecuteNonQuery

        public int SetSearchEngineAsDefault(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID)
        {
            int UpdateResult = 0;
            string sProc = UserConstant.SET_DEFAULT_SEARCHENGINE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ProviderUserID", DbType.Int32, ProviderUserID);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchID);
                db.AddInParameter(command, "@UserSearchEngineID", DbType.Int32, UserSearchEngineID);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Jan 2011
        /// Load user quick links from DB based on location
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>

        public List<ContactSearchDTO> LoadUserDefaultQuickLinksByLocation(int ClinicBranchId)
        {
            List<ContactSearchDTO> ContactSearchList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.CLINIC_USER_LIST_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    ContactSearchList = DataReaderToDTOConverter.ContactSearchDTOConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ContactSearchList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Jan 17 2011
        /// Method Used to MAnage the Search engine for user against Location
        /// Changed by Mansoor
        /// Date 11 Feb 2011
        /// connection was not closed
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="SearchEngineTypeIDs"></param>
        /// <param name="UserSearchEngineID"></param>
        /// <param name="IsDefault"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<int> SearchEngineTypeIDs, int CreatedBy)
        {
            bool isConnectionOpen = false;
            int UpdateResult = 0;
            string sProc = UserConstant.ADD_USER_SEARCHENGINE_SP;

            DataTable dtSearchEngineType = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                dtSearchEngineType = DTOToTableConverter.SearchengineTypeListConverter(SearchEngineTypeIDs);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ProviderUserID", ProviderUserID);
                sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchID);

                SqlParameter tvpMedical = sqlCommand.Parameters.AddWithValue("@SearchEngineTypeIDs", dtSearchEngineType);
                tvpMedical.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpMedical.TypeName = ProjectConstant.SEARCH_ENGINE_TYPE_TVP;

                sqlCommand.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }

            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                {
                    connection.Close();
                }
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="clinicBranchID"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int LogEmailSend(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message, DateTime mailSentAt)
        {
            int UpdateResult = 0;
            string sProc = UserConstant.USER_DEFAULT_EMAIL_LOG_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.COMMUNICATION_DB); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID ", DbType.Int32, requestedUserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
                db.AddInParameter(command, "@RecipientEmail", DbType.String, receiptEmail);
                db.AddInParameter(command, "@MailBody", DbType.String, message);
                db.AddInParameter(command, "@MailSubject", DbType.String, subject);
                db.AddInParameter(command, "@MailSentAt", DbType.DateTime, mailSentAt);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, requestedUserId);
                db.AddOutParameter(command, "@UserEmailIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }

            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 18 jan 2011
        /// Method Used To Register User
        /// Changed by Mansoor
        /// Date 11 Feb 2011
        /// Reader is replaced with ExecuteNonQuery
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns></returns>
        public int RegisterNewUser(RegistrationDTO registrationDTO)
        {
            int UpdateResult = 0;
            string sProc = UserConstant.REGISTER_NEW_USER_SP;
            int UserId = 0;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PrefixTypeID", DbType.Int32, registrationDTO.User.Prefix.PrefixId);
                db.AddInParameter(command, "@FirstName", DbType.String, registrationDTO.User.FirstName);
                db.AddInParameter(command, "@LastName", DbType.String, registrationDTO.User.LastName);
                db.AddInParameter(command, "@MiddleName", DbType.String, registrationDTO.User.MiddleName);
                db.AddInParameter(command, "@UserPassword", DbType.String, registrationDTO.User.UserPassword);

                db.AddInParameter(command, "@Suffix", DbType.String, registrationDTO.User.Suffix);
                db.AddInParameter(command, "@UserEmail", DbType.String, registrationDTO.User.UserEmail);
                db.AddInParameter(command, "@GenderTypeID", DbType.Int32, registrationDTO.User.Gender.GenderId);
                db.AddInParameter(command, "@SSN", DbType.String, registrationDTO.User.SSN);
                db.AddInParameter(command, "@LicenseNumber", DbType.String, registrationDTO.User.LicenseNumber);

                db.AddInParameter(command, "@FederalTaxId", DbType.String, registrationDTO.User.FederalTaxId);
                db.AddInParameter(command, "@EIN", DbType.String, registrationDTO.User.EIN);
                db.AddInParameter(command, "@QuivusUserID", DbType.Int32, 1);

                db.AddInParameter(command, "@AccountStatus", DbType.String, registrationDTO.User.AccountStatus.StatusID);
                db.AddInParameter(command, "@TextPassword", DbType.String, registrationDTO.User.UserPassword);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, registrationDTO.User.ActiveUserType.UserTypeId);
                db.AddInParameter(command, "@ActiveUserTypeID", DbType.Int32, registrationDTO.User.ActiveUserType.UserTypeId);
                db.AddInParameter(command, "@CliniBranchID", DbType.Int32, registrationDTO.ClinicBranhId);

                db.AddOutParameter(command, "@UserIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        //void TestLoadTypeDTOsusingReflection()
        //{
        //    try
        //    {
        //        IDataReader objIReader = null;
        //        string sProc = UserConstant.TYPE_LOAD_ALL_PREFIX_SP;

        //        List<PrefixTypeDTO> PrefixTypeList = new List<PrefixTypeDTO>();

        //        // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
        //        Database db = DatabaseFactory.CreateDatabase(); // Load default DB
        //        DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

        //        using (objIReader = db.ExecuteReader(command))
        //        {
        //            if (objIReader != null)
        //            {
        //                GenericDataReaderToDTOConverter.ConvertToDTOList(PrefixTypeList, objIReader, typeof(PrefixTypeDTO));
        //                //SearchengineList = DataReaderToDTOConverter.SearchEngineAllConverter(objIReader);                        
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {

        //    }
        //}


        /// <summary>
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save/update user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>

        public int RegisterBaseUser(BaseUserDTO baseUserDTO, bool IsUpdate)
        {
            int UserId = 0;

            string sProc = UserConstant.REGISTER_BASE_USER_SP;

            try
            {
                if (IsUpdate)
                {
                    QuivusLogger.Info("Update Case for RegisterBaseUser:: ");
                    sProc = UserConstant.UPDATE_BASE_USER_SP;
                }
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                if (baseUserDTO.Prefix != null)
                {
                    db.AddInParameter(command, "@PrefixTypeID", DbType.Int32, baseUserDTO.Prefix.PrefixId);
                }
                else
                {
                    db.AddInParameter(command, "@PrefixTypeID", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@FirstName", DbType.String, baseUserDTO.FirstName);
                db.AddInParameter(command, "@LastName", DbType.String, baseUserDTO.LastName);
                db.AddInParameter(command, "@MiddleName", DbType.String, baseUserDTO.MiddleName);

                db.AddInParameter(command, "@UserPassword", DbType.String, PasswordHashUtil.ComputeHash(baseUserDTO.UserPassword, ProjectConstant.HASH_ALGO_SHA1));

                db.AddInParameter(command, "@Suffix", DbType.String, baseUserDTO.Suffix);
                db.AddInParameter(command, "@GenderTypeID", DbType.Int32, baseUserDTO.Gender.GenderId);
                db.AddInParameter(command, "@SSN", DbType.String, baseUserDTO.SSN);
                db.AddInParameter(command, "@AccountStatus", DbType.Int32, baseUserDTO.AccountStatus.StatusID);
                db.AddInParameter(command, "@LicenseNumber", DbType.String, baseUserDTO.LicenseNumber);
                db.AddInParameter(command, "@FederalTaxId", DbType.String, baseUserDTO.FederalTaxId);
                db.AddInParameter(command, "@EIN", DbType.String, baseUserDTO.EIN);

                db.AddInParameter(command, "@QuivusUserID", DbType.String, baseUserDTO.QuivusUserID);
                db.AddInParameter(command, "@TextPassword", DbType.String, baseUserDTO.UserPassword);
                if (IsUpdate)
                    db.AddInParameter(command, "@ActiveUserTypeID", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(command, "@ActiveUserTypeID", DbType.Int32, baseUserDTO.ActiveUserType.UserTypeId);
                db.AddInParameter(command, "@CliniBranchID", DbType.Int32, baseUserDTO.ClinicBranhId);

                db.AddInParameter(command, "@UserName", DbType.String, baseUserDTO.UserName);

                if (!IsUpdate)// Ignore In case Of Update User Section i.e Insert Case 
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, baseUserDTO.CreatedBy);
                    db.AddInParameter(command, "@UserEmail", DbType.String, baseUserDTO.UserEmail);
                }
                if (IsUpdate) // Update Case 
                {
                    db.AddInParameter(command, "@UserID", DbType.Int32, baseUserDTO.UserId);
                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, baseUserDTO.ModifiedBy);
                }

                if (!IsUpdate)
                    db.AddOutParameter(command, "@UserIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        UserId = int.Parse(db.GetParameterValue(command, "@UserIDOUT").ToString());
                    else
                        UserId = baseUserDTO.UserId;

                }

            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UserId;
        }

        /// <summary>
        /// Author Ismail 
        /// Date Feb 11 2011
        /// Method Used To Save User Image
        /// </summary>
        /// <param name="imageDTO"></param>
        public int ManageUserImage(ImageDTO imageDTO)
        {
            string sProc = UserConstant.USER_IMAGE_INSERT_SP;

            int UserImageId;
            int ImageId;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@UserID", DbType.Int32, imageDTO.UserId);
                db.AddInParameter(command, "@UserTypeID", DbType.Int32, imageDTO.UserTypeID);
                db.AddInParameter(command, "@ImageName", DbType.String, imageDTO.ImageName);
                db.AddInParameter(command, "@ImageCaption", DbType.String, imageDTO.ImageCaption);
                db.AddInParameter(command, "@ImagePath", DbType.String, imageDTO.ImagePath);
                db.AddInParameter(command, "@ImageData", DbType.Binary, imageDTO.ImageData);
                db.AddInParameter(command, "@ImageType", DbType.Int32, imageDTO.ImageTypeId);

                db.AddInParameter(command, "@IsDefault", DbType.Boolean, true);
                db.AddInParameter(command, "@CreatedBy", DbType.String, imageDTO.CreatedBy);

                db.AddOutParameter(command, "@UserImageIDOUT", DbType.Int32, 1);

                db.AddOutParameter(command, "@ImageIDOUT", DbType.Int32, 1);
                if (db.ExecuteNonQuery(command) == 0)
                {
                    //throw new RecordNotInserted
                }

                UserImageId = int.Parse(db.GetParameterValue(command, "@UserImageIDOUT").ToString());
                ImageId = int.Parse(db.GetParameterValue(command, "@ImageIDOUT").ToString());
            }
            catch
            {

                throw;
            }
            finally
            {
                //objIReader.Close();
            }

            return UserImageId;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 jan 2011
        /// This Method call the DB proc to Verfiy the User Email
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>

        public bool VerifyUserEmail(string UserEmail)
        {
            string sProc = UserConstant.USER_EMAIL_VERIFY_SP;

            bool IsValidUser = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@UserEmail", DbType.String, UserEmail);

                bool IsEmailExist = (bool)db.ExecuteScalar(command); // ExecuteScalar Return Only Single Record :)

                IsValidUser = !IsEmailExist;

            }
            catch (ConfigurationException cxp)
            {
                QuivusLogger.Error(cxp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (TargetInvocationException txp)
            {
                QuivusLogger.Error(txp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (SqlException sxp)
            {
                QuivusLogger.Error(sxp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw exp;
            }
            finally
            {
                //objIReader.Close();
            }

            return IsValidUser;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 26 Jan 2011
        /// Load Patient Guid by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Guid GetPatientGuidByPatientId(int patientId)
        {
            Guid guid = System.Guid.Empty;
            string sProc = UserConstant.REC_GUID_PATIENT_GET_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                guid = (Guid)db.ExecuteScalar(command);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return guid;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 26 Jan 2011
        /// Load Patient Guid by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Guid GetGurantorGuidByGurantorId(int gurantorId)
        {
            object o = null;
            Guid guid = System.Guid.Empty;
            string sProc = UserConstant.REC_GUID_GURANTOR_GET_BY_GURANTOR_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientGuarantorID", DbType.Int32, gurantorId);

                guid = (Guid)db.ExecuteScalar(command);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return guid;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public List<PatientProfileSettingDTO> LoadPatientProfileSettings(int clinicBranchId, int UserId, int PatientId, int sectionNo)
        {
            List<PatientProfileSettingDTO> ProfileSettingList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.LOAD_PROFILE_SETTING_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);
                db.AddInParameter(command, "@SectionNo", DbType.Int32, sectionNo);

                using (objIReader = db.ExecuteReader(command))
                {
                    ProfileSettingList = DataReaderToDTOConverter.ProfileSettingDTOConverter(objIReader);
                }
            }
            catch (ConfigurationException cxp)
            {
                QuivusLogger.Error(cxp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (TargetInvocationException txp)
            {
                QuivusLogger.Error(txp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (SqlException sxp)
            {
                QuivusLogger.Error(sxp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ProfileSettingList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public PatientProfileSettingDTO LoadPatientProfileSectionSettings(int clinicBranchId, int UserId, int PatientId, int sectionNo)
        {
            PatientProfileSettingDTO ProfileSettingObj = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.LOAD_PROFILE_SETTING_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);
                db.AddInParameter(command, "@SectionNo", DbType.Int32, sectionNo);

                using (objIReader = db.ExecuteReader(command))
                {
                    //  ProfileSettingList = DataReaderToDTOConverter.ProfileSettingDTOConverter(objIReader);
                    ProfileSettingObj = DataReaderToDTOConverter.ProfileSettingSectionDTOConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ProfileSettingObj;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to Update Patient Profile Settings 
        /// </summary>
        /// <param name="PatientProfileSettingDTO"></param>
        /// <returns></returns>
        public int ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj)
        {
            int UpdateResult = 0;
            string sProc_Insert = UserConstant.INSERT_PROFILE_SETTINGS_SP;
            string sProc_Update = UserConstant.UPDATE_PROFILE_SETTINGS_SP;
            bool IsUpdate = false;
            string sProc = sProc_Insert;
            try
            {
                if (ProfileSettingObj.UserUserProfileCategoryID > 0) /* If UserUserProfileCategoryID is provide Simply Update Record */
                {
                    sProc = sProc_Update;
                    IsUpdate = true;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.Int32, ProfileSettingObj.UserID);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ProfileSettingObj.ClinicBranchID);
                db.AddInParameter(command, "@SectionNo", DbType.Int32, ProfileSettingObj.SectionNo);
                db.AddInParameter(command, "@UserProfileCategoryID", DbType.Int32, ProfileSettingObj.UserProfileCategoryID);

                if (IsUpdate)
                {
                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, ProfileSettingObj.ModifiedBy);
                    db.AddInParameter(command, "@UserUserProfileCategoryID", DbType.Int32, ProfileSettingObj.UserUserProfileCategoryID);
                }
                else
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, ProfileSettingObj.CreatedBy);
                    db.AddOutParameter(command, "@UserUserProfileCategoryIDOUT", DbType.Int32, 1); // Specify Out Param In Insertion Case only
                }

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch (ConfigurationException cxp)
            {
                QuivusLogger.Error(cxp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (TargetInvocationException txp)
            {
                QuivusLogger.Error(txp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (SqlException sxp)
            {
                QuivusLogger.Error(sxp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        public List<ProfileStaticDataDTO> LoadProfileStaticData()
        {
            List<ProfileStaticDataDTO> ProfileStaticList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.PROFILE_STATIC_DATA_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd
                using (objIReader = db.ExecuteReader(command))
                {
                    ProfileStaticList = DataReaderToDTOConverter.ProfileStaticDataDTOConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ProfileStaticList;
        }


        /// <summary>
        /// Author Ali Noor
        /// </summary>
        /// <param name="emailDTO"></param>
        /// <param name="mailSentAt"></param>
        /// <returns></returns>
        public int LogEmailWithAttachment(EmailDTO emailDTO, DateTime mailSentAt)
        {
            int UpdateResult = 0;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_DEFAULT_EMAIL_LOG_WITH_ATTACHMENT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.COMMUNICATION_DB); // Load DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID ", DbType.Int32, emailDTO.UserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, emailDTO.ClinicBranchID);
                db.AddInParameter(command, "@RecipientEmail", DbType.String, emailDTO.RecipientEmail);
                db.AddInParameter(command, "@MailBody", DbType.String, emailDTO.Message);
                db.AddInParameter(command, "@MailSubject", DbType.String, emailDTO.Subject);
                db.AddInParameter(command, "@MailSentAt", DbType.DateTime, mailSentAt);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, emailDTO.UserId);
                db.AddInParameter(command, "@AttachedFileName ", DbType.String, emailDTO.AttachedFileName);
                db.AddInParameter(command, "@Attachment ", DbType.Binary, emailDTO.Attachment);
                db.AddOutParameter(command, "@UserEmailIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch (ConfigurationException cxp)
            {
                QuivusLogger.Error(cxp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (TargetInvocationException txp)
            {
                QuivusLogger.Error(txp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (SqlException sxp)
            {
                QuivusLogger.Error(sxp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Auhtor Mansoor
        /// Date 1 Feb 2011
        /// this method will insert the last patient acccessed in the provider recent patients table
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId)
        {
            bool UpdateResult = false;
            string sProc = UserConstant.USER_RECENT_PATIENT_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserId", DbType.Int32, providerUserId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Auhtor Mansoor
        /// Date 1 Feb 2011
        /// this method will insert the last patient acccessed in the provider recent patients table
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>


        public List<RecentPatientDTO> LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId)
        {
            List<RecentPatientDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_RECENT_PATIENT_LOAD_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<RecentPatientDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.String, providerUserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.String, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    DataReaderToDTOConverter.ConvertToRecentPatientDTO(objIReader);
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        ///// <summary>
        ///// Auhtor Mansoor
        ///// Date 2 Feb 2011
        ///// This Method will load the user base info         
        ///// </summary>
        ///// <param name="patientId"></param>
        ///// <returns></returns>
        //public BaseUserDTO LoadBaseUserByPatientId(int patientId)
        //{
        //    BaseUserDTO baseUserDTOobj = null;

        //    try
        //    {

        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp);
        //        throw; // Just Re-Throw Exception From DAO LEVEL
        //    }
        //    return baseUserDTOobj;
        //}

        /// <summary>
        /// Auhtor Mansoor
        /// Date 2 Feb 2011
        /// this method will get the email for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserEmailByUserId(int userId)
        {
            string userEmail = "";
            object respose = null;

            string sProc = UserConstant.USER_EMAIL_GET_BY_USER_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.Int32, userId);

                respose = db.ExecuteScalar(command);

                if (respose != DBNull.Value && respose != null)
                {
                    userEmail = respose.ToString();
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return userEmail;
        }

        public List<PatientProfileSettingDTO> LoadProfileSettingCollection(int clinicBranchId, int UserID)
        {
            List<PatientProfileSettingDTO> ProfileSettingList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.LOAD_PROFILE_SETTINGS_COLLECTION_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.String, UserID);
                db.AddInParameter(command, "@ClinicBranchID", DbType.String, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    ProfileSettingList = DataReaderToDTOConverter.GeneralProfileSettingDTOConverter(objIReader);
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ProfileSettingList;
        }


        /// <summary>
        /// Author Muhammad ismail 
        /// Date 09 Feb 2011
        /// Method Used TO clear Search engines Added by User
        /// Changed By Mansoor        
        /// Date 11 Feb 2011
        /// Connection was not closed
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public int ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy)
        {
            bool isConnectionOpen = false;
            int UpdateResult = 0;
            string sProc = UserConstant.CLEAR_USER_SEARCHENGINE_SP;

            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ProviderUserID", ProviderUserID);
                sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchID);

                sqlCommand.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }

            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        public List<ImageTypeDTO> LoadImageTypes()
        {
            List<ImageTypeDTO> ImageTypeList = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.IMAGE_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd
                using (objIReader = db.ExecuteReader(command))
                {
                    ImageTypeList = DataReaderToDTOConverter.ImageTypeTOConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ImageTypeList;
        }

        public List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList)
        {
            List<UserBasicInfoDTO> ResponseList = null;

            bool isConnectionOpen = false;

            string sProc = UserConstant.USER_BASIC_INFO_LOAD_SP;

            DataTable UserIdTable = null;
            SqlConnection connection = null;
            SqlDataReader objIReader = null;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                UserIdTable = DTOToTableConverter.UserIdListConverter(ProviderIdList);

                Database DB = DatabaseFactory.CreateDatabase(); // Load default DB
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpMedical = sqlCommand.Parameters.AddWithValue("@Rec_Ids", UserIdTable);
                tvpMedical.SqlDbType = SqlDbType.Structured;
                tvpMedical.TypeName = ProjectConstant.SEARCH_ENGINE_TYPE_TVP;

                connection.Open();
                isConnectionOpen = true;

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    ResponseList = DataReaderToDTOConverter.UserBasicInfoConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return ResponseList;
        }

        public bool IsProviderAtClinic(int userId, int clinicBranchId)
        {
            bool isProvider = false;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_IS_PROVIDER_AT_CLINIC;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd

                db.AddInParameter(command, "@UserID", DbType.Int32, userId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {
                        if (objIReader["IsProvider"] != DBNull.Value)
                            isProvider = bool.Parse(objIReader["IsProvider"].ToString());
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return isProvider;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 26 Jan 2011
        /// Load Patient Guid by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Guid GetEmergencyContactGuidByPatientId(int patientId)
        {

            Guid guid = System.Guid.Empty;
            IDataReader objIReader = null;
            string sProc = UserConstant.REC_GUID_EMERGENCY_CONTACT_GET_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {
                        if (objIReader["RecGuid"] != DBNull.Value)
                        {
                            guid = Guid.Parse(objIReader["RecGuid"].ToString());
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return guid;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 26 Jan 2011
        /// Load Patient Guid by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Guid GetGurantorGuidByPatientId(int patientId)
        {
            Guid guid = System.Guid.Empty;
            IDataReader objIReader = null;
            string sProc = UserConstant.REC_GUID_GURANTOR_GET_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {
                        if (objIReader["RecGuid"] != DBNull.Value)
                        {
                            guid = Guid.Parse(objIReader["RecGuid"].ToString());
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return guid;
        }



        public UserVerificationDTO VerifyUser(int UserId, string password)
        {
            UserVerificationDTO Result = null;

            bool isValid = false;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_VERIFICATION_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd

                db.AddInParameter(command, "@UserLoginID", DbType.Int32, UserId);
                db.AddInParameter(command, "@UserPassword", DbType.String, password);
                db.AddInParameter(command, "@UserHashedPassword", DbType.String, password);

                db.AddOutParameter(command, "@IsAuthenticated", DbType.Boolean, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new Exception();
                }
                else
                {
                    bool.TryParse(db.GetParameterValue(command, "IsAuthenticated").ToString(), out isValid);
                }


                if (Result == null)
                {
                    Result = new UserVerificationDTO();
                }

                if (!isValid)
                {
                    Result.ErrorMessage = ProjectConstant.INVALID_LOGIN_ERROR_MSG;
                }

                Result.IsValidUser = isValid;
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return Result;
        }

        public BaseUserDTO LoadBaseUserByUserId(int UserId)
        {
            BaseUserDTO userInfo = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_LOAD_BY_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd
                db.AddInParameter(command, "@UserID", DbType.Int32, UserId);

                using (objIReader = db.ExecuteReader(command))
                {
                    userInfo = DataReaderToDTOConverter.BaseUserDTOConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return userInfo;
        }

        public bool UpdatePatientEPrescriptionDetails(string RCopiaID, int PatientId)
        {
            bool Result = false;
            string sProc = UserConstant.UPDATE_PATIENT_EPRESCRIPTION_DETAILS_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                db.AddInParameter(command, "@RCOPIAID", DbType.String, RCopiaID);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                if (Result == null)
                {
                    Result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return Result;
        }

        public string LoadQuivusIdByPatientId(int PatientId)
        {
            string Result = string.Empty;
            string sProc = UserConstant.LOAD_QUIVUS_ID_BY_PATIENT_ID_SP;
            IDataReader objIReader = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {


                        if (objIReader["QuivusID"] != DBNull.Value)
                        {
                            Result = objIReader["QuivusID"].ToString();
                        }

                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return Result;
        }

        public string LoadRcopiaIdByPatientId(int PatientId)
        {
            string Result = string.Empty;
            string sProc = UserConstant.LOAD_RCOPIA_ID_BY_PATIENT_ID_SP;
            IDataReader objIReader = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {


                        if (objIReader["RCOPIAID"] != DBNull.Value)
                        {
                            Result = objIReader["RCOPIAID"].ToString();
                        }

                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return Result;
        }

        public ExternalPatientDTO LoadExternalPatients(int FromRecord, int ToRecord)
        {
            ExternalPatientDTO Result = null;

            string sProc = UserConstant.LOAD_EXTERNAL_PATIENT_ID_SP;
            IDataReader objReader = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB

                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Namekd
                db.AddInParameter(command, "@PageNo", DbType.Int32, FromRecord);
                db.AddInParameter(command, "@PageSize", DbType.Int32, ToRecord);

                if (Result == null)
                    Result = new ExternalPatientDTO();
                using (objReader = db.ExecuteReader(command))
                {
                    if (objReader != null && objReader.Read())
                    {
                        if (objReader["TotalRecords"] != DBNull.Value && !String.IsNullOrEmpty(objReader["TotalRecords"].ToString()))
                            Result.TotalRecord = int.Parse(objReader["TotalRecords"].ToString());

                        if (objReader["FirstRecord"] != DBNull.Value && !String.IsNullOrEmpty(objReader["FirstRecord"].ToString()))
                            Result.FirstRecord = int.Parse(objReader["FirstRecord"].ToString());

                        if (objReader["LastRecord"] != DBNull.Value && !String.IsNullOrEmpty(objReader["LastRecord"].ToString()))
                            Result.LastRecord = int.Parse(objReader["LastRecord"].ToString());

                        objReader.NextResult();

                        if (Result.PatientList == null)
                            Result.PatientList = new List<ExternalPatientInfoDTO>();

                        while (objReader.Read())
                        {
                            ExternalPatientInfoDTO obj = new ExternalPatientInfoDTO();

                            if (objReader["PatientID"] != DBNull.Value)
                            {
                                obj.PatientId = Int32.Parse(objReader["PatientID"].ToString());
                            }
                            if (objReader["RecGuid"] != DBNull.Value)
                            {
                                obj.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());
                            }
                            if (objReader["FirstName"] != DBNull.Value && !String.IsNullOrEmpty(objReader["FirstName"].ToString()))
                            {
                                obj.FirstName = objReader["FirstName"].ToString();
                            }
                            if (objReader["LastName"] != DBNull.Value && !String.IsNullOrEmpty(objReader["LastName"].ToString()))
                            {
                                obj.LastName = objReader["LastName"].ToString();
                            }

                            if (objReader["DateOfBirth"] != DBNull.Value && !String.IsNullOrEmpty(objReader["DateOfBirth"].ToString()))
                            {
                                obj.DateOfBirth = DateTime.Parse(objReader["DateOfBirth"].ToString());
                            }
                            if (objReader["GenderName"] != DBNull.Value)
                            {
                                obj.GenderName = objReader["GenderName"].ToString();
                            }
                            Result.PatientList.Add(obj);
                        }
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return Result;
        }

        public bool UpdatePatientRCopiaId(List<PatientRcopiaDTO> patientList)
        {
            bool isConnectionOpen = false;
            bool UpdateResult = false;
            //   string sProc = UserConstant.PATIENT_ALLERGY_INSERT_SP;
            string sProc = UserConstant.PATIENT_EXTERNAL_UPDATE_RCOPIA_SP;

            DataTable dtPatient = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                dtPatient = DTOToTableConverter.PatientExternalDTOListConverter(patientList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpAllergy = sqlCommand.Parameters.AddWithValue("@PatientRcopiaIdsTVP", dtPatient);
                tvpAllergy.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpAllergy.TypeName = ProjectConstant.PATIENT_EXTERNAL_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 18/7/2011
        /// Method will return provider List
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public List<ProviderDTO> LoadAllProviderByClinicBranchId(int clinicBranchID)
        {
            List<ProviderDTO> providerList = null;
            IDataReader objReader = null;
            string sProc = UserConstant.PATIENT_PROVIDER_LOAD_ALL_BY_CLINICID;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
                using (objReader = db.ExecuteReader(command))
                {
                    while (objReader.Read())
                    {
                        ProviderDTO providerObj = new ProviderDTO();
                        if (objReader["FirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FirstName"].ToString()))
                        {
                            providerObj.FirstName = objReader["FirstName"].ToString();
                        }
                        if (objReader["LastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LastName"].ToString()))
                        {
                            providerObj.LastName = objReader["LastName"].ToString();
                        }
                        if (objReader["MiddleName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MiddleName"].ToString()))
                        {
                            providerObj.MiddleName = objReader["MiddleName"].ToString();
                        }
                        if (objReader["UserId"] != DBNull.Value)
                        {
                            providerObj.UserId = int.Parse(objReader["UserId"].ToString());
                        }
                        if (objReader["ProviderId"] != DBNull.Value)
                        {
                            providerObj.ProviderId = int.Parse(objReader["ProviderId"].ToString());
                        }
                        if (providerList == null)
                        {
                            providerList = new List<ProviderDTO>();
                        }
                        providerList.Add(providerObj);
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return providerList;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 14 sep 2011        
        /// </summary>
        /// <param name="branchCode"></param>
        /// <returns></returns>      
        public int GetClinicBranchIdByClinicBranchCode(string branchCode)
        {
            int brID = 0;

            string sProc = UserConstant.CLINIC_BRANCH_LOAD_CLINIC_ID_BY_BRANCH_CODE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@ClinicBrCode", DbType.String, branchCode);

                brID = (int)db.ExecuteScalar(command); // ExecuteScalar Return Only Single Record :)
            }
            catch (ConfigurationException cxp)
            {
                QuivusLogger.Error(cxp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (TargetInvocationException txp)
            {
                QuivusLogger.Error(txp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            catch (SqlException sxp)
            {
                QuivusLogger.Error(sxp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw exp;
            }
            finally
            {
                //objIReader.Close();
            }

            return brID;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public List<ProviderDTO> LoadAllProviderByClinicBranchId(int clinicBranchID, List<int> stationIds, bool isLoadAll)
        {
            List<ProviderDTO> providerList = null;
            IDataReader objReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = UserConstant.PROVIDER_LOAD_ALL_BY_CLINICID_AND_STATION;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
                db.AddInParameter(command, "@IsAllSelected", DbType.Boolean, isLoadAll);


                //SqlParameter isAllSelectedPara = sqlCommand.Parameters.AddWithValue("@IsAllSelected", isLoadAll);
                //isAllSelectedPara.SqlDbType = SqlDbType.Bit;

                if (stationIds != null && stationIds.Count > 0)
                {
                    dtStationIDs = DTOToTableConverter.StationIDsListConverter(stationIds);
                }
                SqlParameter stationIdsTVPPara = sqlCommand.Parameters.AddWithValue("@StationIdsTVP", dtStationIDs);
                stationIdsTVPPara.SqlDbType = SqlDbType.Structured;
                stationIdsTVPPara.TypeName = ProjectConstant.STATION_IDS_TVP;

                using (objReader = db.ExecuteReader(command))
                {
                    while (objReader.Read())
                    {
                        ProviderDTO providerObj = new ProviderDTO();
                        if (objReader["FirstName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["FirstName"].ToString()))
                        {
                            providerObj.FirstName = objReader["FirstName"].ToString();
                        }
                        if (objReader["LastName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["LastName"].ToString()))
                        {
                            providerObj.LastName = objReader["LastName"].ToString();
                        }
                        if (objReader["MiddleName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["MiddleName"].ToString()))
                        {
                            providerObj.MiddleName = objReader["MiddleName"].ToString();
                        }
                        if (objReader["UserId"] != DBNull.Value)
                        {
                            providerObj.UserId = int.Parse(objReader["UserId"].ToString());
                        }
                        if (objReader["StationID"] != DBNull.Value)
                        {
                            providerObj.StationId = int.Parse(objReader["StationID"].ToString());
                        }
                        if (objReader["ProviderId"] != DBNull.Value)
                        {
                            providerObj.ProviderId = int.Parse(objReader["ProviderId"].ToString());
                        }
                        if (providerList == null)
                        {
                            providerList = new List<ProviderDTO>();
                        }
                        providerList.Add(providerObj);
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return providerList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public List<StationDTO> LoadAllStationsByClinicId(int clinicBranchID)
        {
            List<StationDTO> stationList = null;
            IDataReader objReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = UserConstant.STATION_LOAD_ALL_BY_CLINIC_ID;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);

                using (objReader = db.ExecuteReader(command))
                {
                    while (objReader.Read())
                    {
                        StationDTO stationObj = new StationDTO();
                        if (objReader["StationID"] != DBNull.Value)
                        {
                            stationObj.StationID = int.Parse(objReader["StationID"].ToString());
                        }
                        if (objReader["StationName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StationName"].ToString()))
                        {
                            stationObj.StationName = objReader["StationName"].ToString();
                        }
                        if (objReader["ClinicBranchID"] != DBNull.Value)
                        {
                            stationObj.ClinicBranchID = int.Parse(objReader["ClinicBranchID"].ToString());
                        }

                        if (stationList == null)
                        {
                            stationList = new List<StationDTO>();
                        }
                        stationList.Add(stationObj);
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return stationList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public WorkingHourDTO LoadProviderWorkingHours(int providerUserId)
        {
            WorkingHourDTO workingHour = null;
            IDataReader objReader = null;            
            SqlConnection connection = null;
            string sProc = UserConstant.WORKINGHOURS_LOAD_BY_USER_ID;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                db.AddInParameter(command, "@ProviderUserId", DbType.Int32, providerUserId);

                using (objReader = db.ExecuteReader(command))
                {
                    if (objReader.Read())
                    {
                        workingHour = new WorkingHourDTO();

                        //if (objReader["WorkingHourID"] != DBNull.Value)
                        //{
                        //    workingHour.WorkingHourID = int.Parse(objReader["WorkingHourID"].ToString());
                        //}
                        if (objReader["UserID"] != DBNull.Value)
                        {
                            workingHour.UserID = int.Parse(objReader["UserID"].ToString());
                        }
                        if (objReader["WorkStart"] != DBNull.Value)
                        {
                            workingHour.StartTime = objReader["WorkStart"].ToString();
                        }
                        if (objReader["WorkEnd"] != DBNull.Value)
                        {
                            workingHour.EndTime = objReader["WorkEnd"].ToString();
                        }
                        if (objReader["WorkDays"] != DBNull.Value)
                        {
                            workingHour.WeekDay = objReader["WorkDays"].ToString();
                        }
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return workingHour;
        }




        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        public List<PatientProviderAssociationDTO> ListAllPatientProvidersWithLocations(int patientId)
        {
            List<PatientProviderAssociationDTO> list = null;
            IDataReader objReader = null;
            SqlConnection connection = null;
            string sProc = UserConstant.ASSOCIATION_LOAD_PATIENT_PROVIDERS;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                list = new List<PatientProviderAssociationDTO>();

                using (objReader = db.ExecuteReader(command))
                {
                    while (objReader.Read())
                    {
                        PatientProviderAssociationDTO associationDTO = new PatientProviderAssociationDTO();

                        if (objReader["PatientProviderID"] != DBNull.Value)
                        {
                            associationDTO.PatientProviderID = int.Parse(objReader["PatientProviderID"].ToString());
                        }
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            associationDTO.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["PatientUserID"] != DBNull.Value)
                        {
                            associationDTO.PatientUserID = int.Parse(objReader["PatientUserID"].ToString());
                        }
                        if (objReader["ProviderID"] != DBNull.Value)
                        {
                            associationDTO.ProviderID = int.Parse(objReader["ProviderID"].ToString());
                        }
                        if (objReader["ProviderName"] != DBNull.Value)
                        {
                            associationDTO.ProviderName = objReader["ProviderName"].ToString();
                        }
                        if (objReader["ProviderUserID"] != DBNull.Value)
                        {
                            associationDTO.ProviderUserID = int.Parse(objReader["ProviderUserID"].ToString());
                        }
                        if (objReader["ClinicBranchID"] != DBNull.Value)
                        {
                            associationDTO.ClinicBranchID = int.Parse(objReader["ClinicBranchID"].ToString());
                        }
                        //if (objReader["IsAuthorized"] != DBNull.Value)
                        //{
                        //    associationDTO.IsAuthorized = bool.Parse(objReader["IsAuthorized"].ToString());
                        //}
                        if (objReader["ProviderTypeID"] != DBNull.Value)
                        {
                            associationDTO.ProviderTypeID = int.Parse(objReader["ProviderTypeID"].ToString());
                        }
                        //if (objReader["WorkDays"] != DBNull.Value)
                        //{
                        //    associationDTO.WorkDays = objReader["ProviderTWorkDaysypeID"].ToString();
                        //}
                        if (objReader["WorkStart"] != DBNull.Value)
                        {
                            associationDTO.WorkStart = objReader["WorkStart"].ToString();
                        }
                        if (objReader["WorkEnd"] != DBNull.Value)
                        {
                            associationDTO.WorkEnd = objReader["WorkEnd"].ToString();
                        }
                        if (objReader["SpecialityID"] != DBNull.Value)
                        {
                            associationDTO.ProviderSpecialityID = int.Parse(objReader["SpecialityID"].ToString());
                        }
                        if (objReader["SpecialityName"] != DBNull.Value)
                        {
                            associationDTO.ProviderSepcialityName = objReader["SpecialityName"].ToString();
                        }

                        if (objReader["StationID"] != DBNull.Value)
                        {
                            associationDTO.StationID = int.Parse(objReader["StationID"].ToString());
                        }
                        if (objReader["StationName"] != DBNull.Value)
                        {
                            associationDTO.StationName = objReader["StationName"].ToString();
                        }
                        if (objReader["Address"] != DBNull.Value)
                        {
                            associationDTO.Address = objReader["Address"].ToString();
                        }

                        if (objReader["UserTypeName"] != DBNull.Value)
                        {
                            associationDTO.ProviderTypeName = objReader["UserTypeName"].ToString();
                        }
                        if (objReader["UserTypeID"] != DBNull.Value)
                        {
                            associationDTO.ProviderTypeID = int.Parse(objReader["UserTypeID"].ToString());
                        }

                        list.Add(associationDTO);
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        public int CheckProviderAuthorityForPatientInfoAccess(int providerUserId, int patientId, int clinicBranchId)
        {
            int flag = 0;
            IDataReader objReader = null;
            SqlConnection connection = null;
            string sProc = UserConstant.ASSOCIATION_CHECK_PATIENT_PROVIDER;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@ProviderUserId", DbType.Int32, providerUserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                using (objReader = db.ExecuteReader(command))
                {
                    if (objReader.Read())
                    {
                        if (objReader["IsAuthorized"] != DBNull.Value)
                        {
                            if (bool.Parse(objReader["IsAuthorized"].ToString()))
                            {
                                flag = 1;
                            }                            
                        }
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return flag;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        public int ManageProviderAuthorization(PatientProviderAssociationDTO patientProviderAssociationDTO)
        {
            bool isConnectionOpen = false;
            bool isUpdateCase = false;
            int UpdateResult = 0;
            string sProc_Insert = UserConstant.ASSOCIATION_INSERT;
            string sProc_Update = UserConstant.ASSOCIATION_UPDATE;
            string sProc = string.Empty;

            DataTable dtPatient = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (patientProviderAssociationDTO.PatientProviderID > 0)
                {
                    isUpdateCase = true;
                    sProc = sProc_Update;
                }
                else
                {
                    isUpdateCase = false;
                    sProc = sProc_Insert;
                }

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                //connection = new SqlConnection(db.ConnectionString);
                //SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                //sqlCommand.CommandType = CommandType.StoredProcedure;

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientProviderAssociationDTO.PatientID);
                db.AddInParameter(command, "@PatientUserID", DbType.Int32, patientProviderAssociationDTO.PatientUserID);
                db.AddInParameter(command, "@ProviderID", DbType.Int32, patientProviderAssociationDTO.ProviderID);                
                db.AddInParameter(command, "@ProviderUserId", DbType.Int32, patientProviderAssociationDTO.ProviderUserID);                
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, patientProviderAssociationDTO.ClinicBranchID);
                db.AddInParameter(command, "@IsAuthorized", DbType.Int32, patientProviderAssociationDTO.IsAuthorized);
                db.AddInParameter(command, "@ProviderTypeID", DbType.Int32, patientProviderAssociationDTO.ProviderTypeID);

                if (!isUpdateCase)
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, patientProviderAssociationDTO.CreatedBy);
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, patientProviderAssociationDTO.CreatedAt);                
                }
                else
                {
                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, patientProviderAssociationDTO.ModifiedBy);
                    db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, patientProviderAssociationDTO.ModifiedAt);                
                }

                
                //connection.Open();
                //isConnectionOpen = true;

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        public List<ProviderSearchDTO> SearchProviderForAppointment()
        {
            List<ProviderSearchDTO> list = null;
            IDataReader objReader = null;
            SqlConnection connection = null;
            string sProc = UserConstant.PROVIDER_SEARCH;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                //db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                list = new List<ProviderSearchDTO>();

                using (objReader = db.ExecuteReader(command))
                {
                    while (objReader.Read())
                    {
                        ProviderSearchDTO searchDTO = new ProviderSearchDTO();

                        if (objReader["UserId"] != DBNull.Value)
                        {
                            searchDTO.ProviderUserId = int.Parse(objReader["UserId"].ToString());
                        }
                        if (objReader["DoctorID"] != DBNull.Value)
                        {
                            searchDTO.ProviderId = int.Parse(objReader["DoctorID"].ToString());
                        }
                        if (objReader["UserTypeID"] != DBNull.Value)
                        {
                            searchDTO.ProviderTypeId = int.Parse(objReader["UserTypeID"].ToString());
                        }
                        if (objReader["UserTypeName"] != DBNull.Value)
                        {
                            searchDTO.ProviderTypeName = objReader["UserTypeName"].ToString();
                        }
                        if (objReader["FirstName"] != DBNull.Value)
                        {
                            searchDTO.ProviderName = objReader["FirstName"].ToString();
                        }
                        if (objReader["MiddleName"] != DBNull.Value)
                        {
                            searchDTO.ProviderName += objReader["MiddleName"].ToString();
                        }                        
                        if (objReader["LastName"] != DBNull.Value)
                        {
                            searchDTO.ProviderName += objReader["LastName"].ToString();
                        }
                        if (objReader["SpecialityTypeID"] != DBNull.Value)
                        {
                            searchDTO.SpecialityId = int.Parse(objReader["SpecialityTypeID"].ToString());
                        }
                        if (objReader["SpecialityName"] != DBNull.Value)
                        {
                            searchDTO.SpecialityName = objReader["SpecialityName"].ToString();
                        }
                        //if (objReader["WorkDays"] != DBNull.Value)
                        //{
                        //    associationDTO.WorkDays = objReader["ProviderTWorkDaysypeID"].ToString();
                        //}
                        if (objReader["ClinicBranchID"] != DBNull.Value)
                        {
                            searchDTO.ClinicBranchId = int.Parse(objReader["ClinicBranchID"].ToString());
                        }
                        if (objReader["StationID"] != DBNull.Value)
                        {
                            searchDTO.StationId = int.Parse(objReader["StationID"].ToString());
                        }
                        if (objReader["StationName"] != DBNull.Value)
                        {
                            searchDTO.StationName = objReader["StationName"].ToString();
                        }
                        if (objReader["Address"] != DBNull.Value)
                        {
                            searchDTO.StationAddress = objReader["Address"].ToString();
                        }

                        if (objReader["City"] != DBNull.Value)
                        {
                            searchDTO.City = objReader["City"].ToString();
                        }
                        if (objReader["State"] != DBNull.Value)
                        {
                            searchDTO.State = objReader["State"].ToString();
                        }
                        if (objReader["ZIP"] != DBNull.Value)
                        {
                            searchDTO.Zip = objReader["ZIP"].ToString();
                        }

                        list.Add(searchDTO);
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<StationDTO> GetStationNamesByStationID(List<int> stationIDList)
        {
            List<StationDTO> list = null;
            IDataReader objReader = null;
            SqlConnection connection = null;
            string sProc = UserConstant.STATION_LOAD;

            try
            {
                list = new List<StationDTO>();
                foreach (int stationID in stationIDList)
                {
                    QuivusLogger.Info("Calling SP:: " + sProc);
                    Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                    DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                    connection = new SqlConnection(db.ConnectionString);
                    SqlCommand sqlCommand = new SqlCommand(sProc, connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    db.AddInParameter(command, "@StationId", DbType.Int32, stationID);

                    //list = new List<StationDTO>();

                    using (objReader = db.ExecuteReader(command))
                    {
                        while (objReader.Read())
                        {
                            StationDTO station = new StationDTO();
                            if (objReader["StationID"] != DBNull.Value)
                            {
                                station.StationID = int.Parse(objReader["StationID"].ToString());
                            }
                            if (objReader["StationName"] != DBNull.Value)
                            {
                                station.StationName = objReader["StationName"].ToString();
                            }
                            if (objReader["ClinicBranchID"] != DBNull.Value)
                            {
                                station.ClinicBranchID= int.Parse(objReader["ClinicBranchID"].ToString());
                            }
                            if (objReader["CreatedAt"] != DBNull.Value)
                            {
                                station.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                            }
                            if (objReader["CreatedBy"] != DBNull.Value)
                            {
                                station.CreatedBy= int.Parse(objReader["CreatedBy"].ToString());
                            }
                            if (objReader["ModifiedAt"] != DBNull.Value)
                            {
                                station.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                            }
                            if (objReader["ModifiedBy"] != DBNull.Value)
                            {
                                station.ModifiedBy= int.Parse(objReader["ModifiedBy"].ToString());
                            }
                            if (objReader["IsDefault"] != DBNull.Value)
                            {
                                station.IsDefault = bool.Parse(objReader["IsDefault"].ToString());
                            }
                            //if (objReader["Address"] != DBNull.Value)
                            //{
                            //    station. = objReader["Address"].ToString();
                            //}
                            //if (objReader["City"] != DBNull.Value)
                            //{
                            //    station.ci = int.Parse(objReader["City"].ToString());
                            //}
                            //if (objReader["PhoneNumber"] != DBNull.Value)
                            //{
                            //    station.ph = int.Parse(objReader["PhoneNumber"].ToString());
                            //}
                            //if (objReader["BranchCode"] != DBNull.Value)
                            //{
                            //    station.Bra = int.Parse(objReader["BranchCode"].ToString());
                            //}
                            if (objReader["IsDeleted"] != DBNull.Value)
                            {
                                station.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                            }


                            list.Add(station);
                        }
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        ///Author: Zahid Mehmood
        /// Date: 17 Jan 2012
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public ClinicProviderSearchDTO GetClinicUserBasciInfo(int userId, int clinicBranchId)
        {
            ClinicProviderSearchDTO response = null;
            try
            {
                  
               IDataReader objIReader = null;
               string sProc = UserConstant.CLINIC_USER_BASIC_INFO_SP;
               response = new ClinicProviderSearchDTO();

               QuivusLogger.Info("Calling SP:: " + sProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               db.AddInParameter(command, "@UserID", DbType.Int32, userId);
               db.AddInParameter(command, "@BranchID", DbType.Int32, clinicBranchId);
               using (objIReader = db.ExecuteReader(command))
               {
                 response = DataReaderToDTOConverter.ConvertToProviderSearchDTO(objIReader);
               }
               return response;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return response;
        }

        public int ChangeUserActiveStatus(int UserId, bool isActive)
        {
            int changed = 0;

            try
            {
                string sProc = "";

                if (isActive)
                    sProc = UserConstant.USER_MARK_ACTIVE_SP;
                else
                    sProc = UserConstant.USER_MARK_INACTIVE_SP;


                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.Int32, UserId);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_UPDATED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    changed = 1;
                }

            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }

            return changed;
        }



        public int ChangeUserLockedStatus(int UserId, bool isLocked)
        {
            int changed = 0;

            try
            {
                string sProc = "";

                if (isLocked)
                    sProc = UserConstant.USER_MARK_LOCKED_SP;
                else
                    sProc = UserConstant.USER_MARK_UNLOCKED_SP;


                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.Int32, UserId);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_UPDATED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    changed = 1;
                }

            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }

            return changed;
        }


        public int InsertUserQuickLinks(QuickLinkDTO dto)
        {
            int inserted = 0;

            try
            {
                string sProc = UserConstant.USER_INSERT_QUICKLINKS_SP;


                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.Int32, dto.UserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, dto.ClinicBranchId);
                db.AddInParameter(command, "@QuickLinkTypeID", DbType.Int32, dto.QuickLinkTypeID);
                db.AddInParameter(command, "@ListOrder", DbType.Int32, dto.ListOrder);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, dto.CreatedBy);
               


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_UPDATED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    inserted = 1;
                }

            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }

            return inserted;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012</summary>
        /// This call will load patient user basic info like name email using thier patient Ids :)
        /// <param name="ProviderIdList"></param>
        /// <returns></returns>

        public List<PatientBasicUserDTO> GetUserBasicInfoByPatientIds(List<int> ProviderIdList)
        {
            List<PatientBasicUserDTO> ResponseList = null;

            bool isConnectionOpen = false;

            string sProc = UserConstant.PATIENT_USER_BASIC_INFO_LOAD_SP;

            DataTable UserIdTable = null;
            SqlConnection connection = null;
            SqlDataReader objIReader = null;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                UserIdTable = DTOToTableConverter.UserIdListConverter(ProviderIdList);

                Database DB = DatabaseFactory.CreateDatabase(); // Load default DB
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpMedical = sqlCommand.Parameters.AddWithValue("@Rec_Ids", UserIdTable);
                tvpMedical.SqlDbType = SqlDbType.Structured;
                tvpMedical.TypeName = ProjectConstant.SEARCH_ENGINE_TYPE_TVP;

                connection.Open();
                isConnectionOpen = true;

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    ResponseList = DataReaderToDTOConverter.UserPatientBasicInfoConverter(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return ResponseList;
        }
    }
}