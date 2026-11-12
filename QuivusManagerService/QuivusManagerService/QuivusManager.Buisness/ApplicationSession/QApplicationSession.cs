using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using Quivus.Utility.Logger;
using QuivusManager.IntegratedServies.UserManagerService;

namespace QuivusManager.Buisness.ApplicationSession
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 07 -Mar -2011
    /// This Class Will be used to Maintain User Session :)
    /// </summary>
    public class QApplicationSession
    {

        private static Dictionary<System.Guid, UserSessionDTO> session = new Dictionary<Guid, UserSessionDTO>();
        

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 Mar 2011
        ///  Add New User Session In SessionDictnry :)
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        public static void AddUserSession(Guid attributeName, UserSessionDTO attributeValue)
        {

            if (session.ContainsKey(attributeName))
            {    // Object  Exists 
                session.Remove(attributeName);
            }
            session.Add(attributeName, attributeValue);
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 Mar 2011
        /// Get User Session Using SessionId :) 
        /// </summary>
        /// <param name="attributeName"></param>

        public static UserSessionDTO GetUserSession(Guid attributeName)
        {
            QuivusLogger.Info("Loading User Session By Id::" + attributeName);

            UserSessionDTO obj = null;

            if (session.ContainsKey(attributeName))
            {
                session.TryGetValue(attributeName, out obj);
            }
            return obj;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 Mar 2011
        /// Remove User Session Using Session Id :)
        /// </summary>
        /// <param name="attributeName"></param>
        public static void RemoveUserSession(Guid attributeName)
        {
            QuivusLogger.Info("Removing User Session By Id::" + attributeName);
            if (session.ContainsKey(attributeName))
            {
                session.Remove(attributeName);
            }

        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 Mar 2011
        /// Clear All Session Be Carefull :)
        /// </summary>
        public static void RemoveAllSessions()
        {
            session.Clear();
        }



        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 Mar 2011
        /// Check If User Session Already Exist Destory It :)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        ///  <param name="clinicBranch"></param>
        public static void RemoveUserSessionIfExist(int userId, int userType, int clinicBranch)
        {
            UserSessionDTO Obj = null;

            QuivusLogger.Info("Chk Removing If Exist User Session By userId::" + userId);
            if (session != null && session.Count > 0)
            {
                var SessionObj =
                       from d in session
                       where d.Value.UserID.Equals(userId) && d.Value.UserTypeID.Equals(userType)
                       && d.Value.ClinicBranchID.Equals(clinicBranch)
                       select d;

                if (SessionObj != null && SessionObj.ToArray().Length > 0)
                {
                    QuivusLogger.Info("Toatl Session For This user are " + SessionObj.ToArray().Length);

                    //foreach (var src in SessionObj)
                    //{
                    //    RemoveUserSession(src.Key);
                 
                    //}

                    Obj = SessionObj.First().Value;
                    if (Obj != null)
                    RemoveUserSession(Obj.SessionID); // Use GUID To Remove Object From Session Dictnry. :)
                }
            }
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 08 Sep 2011
        /// Check If User Active Session Exist Destory It :)
        /// This Method Will be called when second user force to destroy first user session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        ///  <param name="clinicBranch"></param>
        public static void RemoveUserActiveSession(int userId, int userType)
        {
            UserSessionDTO Obj = null;

            QuivusLogger.Info("Chk Removing If Exist User Session By userId::" + userId);
            if (session != null && session.Count > 0)
            {
                var SessionObj =
                       from d in session
                       where d.Value.UserID.Equals(userId) && d.Value.UserTypeID.Equals(userType)
                       && d.Value.IsAlreadyLoggedIn.Equals(false) // Tricky Check i.e "IsAlreadyLoggedIn.Equals(false)" is applied here  
                       select d;                                 //  bcoz at a time there will be only 1 session with IsAlreadyLoggedIn set to false per user             
                       

                if (SessionObj != null && SessionObj.ToArray().Length > 0)
                {
                    QuivusLogger.Info("Toatl Session For This user are " + SessionObj.ToArray().Length);

                    Obj = SessionObj.First().Value;
                    if (Obj!=null)
                    RemoveUserSession(Obj.SessionID); // Use GUID To Remove Object From Session Dictnry. :)
                }
            }
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 08 Sep 2011
        /// Check If User Active Session Exist Destory It :)
        /// This Method Will be called when second user force to destroy first user session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        public static void updateUserActiveSession(int userId, int userType)
        {
            UserSessionDTO Obj = null;

            QuivusLogger.Info("Chk updateUserActiveSession If Exist User Session By userId::" + userId);
            if (session != null && session.Count > 0)
            {
                var SessionObj =
                       from d in session
                       where d.Value.UserID.Equals(userId) && d.Value.UserTypeID.Equals(userType)
                       && d.Value.IsAlreadyLoggedIn.Equals(false) // Tricky Check i.e "IsAlreadyLoggedIn.Equals(false)" is applied here  
                       select d;                                 //  bcoz at a time there will be only 1 session with IsAlreadyLoggedIn set to false per user             


                if (SessionObj != null && SessionObj.ToArray().Length > 0)
                {
                    QuivusLogger.Info("Toatl Session For This user are " + SessionObj.ToArray().Length);

                    Obj = SessionObj.First().Value;
                    if (Obj != null)
                        UpdateUserSessionToLoggedOFF(Obj);     
                }
            }
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 08 Sep 2011
        /// Check If User Active Session Exist Destory It :)
        /// This Method Will be called when second user force to destroy first user session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        public static UserSessionDTO GetUserActiveSession(int userId, int userType)
        {
            UserSessionDTO Obj = null;

            QuivusLogger.Info("Chk Removing If Exist User Session By userId::" + userId);
            if (session != null && session.Count > 0)
            {
                var SessionObj =
                       from d in session
                       where d.Value.UserID.Equals(userId) && d.Value.UserTypeID.Equals(userType)
                       && d.Value.IsAlreadyLoggedIn.Equals(false) // Tricky Check i.e "IsAlreadyLoggedIn.Equals(false)" is applied here  
                       select d;                                 //  bcoz at a time there will be only 1 session with IsAlreadyLoggedIn set to false per user             


                if (SessionObj != null && SessionObj.ToArray().Length > 0)
                {
                    QuivusLogger.Info("Toatl Session For This user are " + SessionObj.ToArray().Length);

                    Obj = SessionObj.First().Value;
                }
            }
            return Obj;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 08 Sep 2011
        /// Check If User Active Session Exist Destory It :)
        /// This Method Will be called when second user force to destroy first user session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        public static UserSessionDTO GetActiveSession(Guid SessionId)
        {
            UserSessionDTO Obj = null;

            QuivusLogger.Info("Chk GetActiveSession If Exist User Session By userId::" + SessionId);
            if (session != null && session.Count > 0)
            {
                var SessionObj =
                       from d in session
                       where d.Value.SessionID.Equals(SessionId)
                       && d.Value.IsAlreadyLoggedIn.Equals(false) // Tricky Check i.e "IsAlreadyLoggedIn.Equals(false)" is applied here  
                       select d;                                 //  bcoz at a time there will be only 1 session with IsAlreadyLoggedIn set to false per user             


                if (SessionObj != null && SessionObj.ToArray().Length > 0)
                {
                    QuivusLogger.Info("Toatl Session For This user are " + SessionObj.ToArray().Length);

                    Obj = SessionObj.First().Value;
                }
            }
            return Obj;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 Sep 2011
        /// Check If User Session Already Exist Update It :)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        ///  <param name="clinicBranch"></param>
        public static void UpdateUserSessionIfExist(int userId, int userType, int clinicBranch)
        {
            UserSessionDTO Obj = null;

            QuivusLogger.Info("Chk Update If Exist User Session By userId::" + userId);

            if (session != null && session.Count > 0)
            {
                var SessionObj =
                       from d in session
                       where d.Value.UserID.Equals(userId) && d.Value.UserTypeID.Equals(userType)
                       && d.Value.ClinicBranchID.Equals(clinicBranch) && d.Value.IsAlreadyLoggedIn.Equals(false)
                       select d;

                if (SessionObj != null && SessionObj.ToArray().Length > 0)
                {
                    QuivusLogger.Info("Toatl Session For This user are " + SessionObj.ToArray().Length);

                    Obj = SessionObj.First().Value;
                    if (Obj != null)
                    UpdateUserSessionToLoggedOFF(Obj);                  
                }
            }
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 Sep 2011
        /// Check If User Session Already Exist Update It :)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        ///  <param name="clinicBranch"></param>
        public static bool IsUserSessionExist(int userId, int userType, int clinicBranch)
        {
            bool IsSessionExist = false;

            QuivusLogger.Info("Chk Update If Exist User Session By userId::" + userId);

            if (session != null && session.Count > 0)
            {
                var SessionObj =
                       from d in session
                       where d.Value.UserID.Equals(userId) && d.Value.UserTypeID.Equals(userType)
                       && d.Value.ClinicBranchID.Equals(clinicBranch) 
                       select d;

                if (SessionObj != null && SessionObj.ToArray().Length > 0)
                {
                    QuivusLogger.Info("Toatl Session For This user are " + SessionObj.ToArray().Length);

                    IsSessionExist = true;
                }
            }
            return IsSessionExist;
        }

        /// <summary>
        /// Author Ismail
        /// Date 07 Sep 2011
        /// Update User Session To Logged Off
        /// </summary>
        /// <param name="Obj"></param>
        private static void UpdateUserSessionToLoggedOFF(UserSessionDTO Obj)
        {
            if (Obj != null)
            {
                Obj.IsAlreadyLoggedIn = true;
            }
        }

        /// <summary>
        /// Mansoor
        /// Date 9 Sep 2011
        /// </summary>
        /// <param name="Obj"></param>
        public static void UpdateUserSession(UserSessionDTO Obj)
        {
            if (session != null && session.Count > 0)
            {
                if (session.ContainsKey(Obj.SessionID))
                {
                    session[Obj.SessionID] = Obj;
                }                
            }
        }
    }
}
