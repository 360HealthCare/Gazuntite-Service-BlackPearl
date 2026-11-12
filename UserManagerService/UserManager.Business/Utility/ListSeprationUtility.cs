using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.User.Patient;


namespace UserManager.Business.Utility
{
    public class ListSeprationUtility
    {
        /// <summary>
         /// Author Muhammad Ismail 
        /// Created Date 25 Jan 2011
        /// Method Used to seprate the Insert/Update List
        /// </summary>
        /// <param name="OriginalListList"></param>
        /// <param name="InsertList"></param>
        /// <param name="UpdateList"></param>
        public static void SeprateEmergencyContactList(List<PatientEmergencyContactDTO> OriginalListList, ref List<PatientEmergencyContactDTO> InsertList
            , ref List<PatientEmergencyContactDTO> UpdateList)
        {
            if (InsertList == null)
            {
                InsertList = new List<PatientEmergencyContactDTO>();
            }
            if (UpdateList == null)
            {
                UpdateList = new List<PatientEmergencyContactDTO>();
            }

            foreach (PatientEmergencyContactDTO obj in OriginalListList)
            {
                if (obj.PatientEmergencyContactId > 0)
                {
                    UpdateList.Add(obj);
                }
                else
                {
                   // obj.RecGuid = Guid.NewGuid();
                    InsertList.Add(obj);
                }
            }
        }




        public static void SeprateProfileSettingList(List<PatientProfileSettingDTO> settingsList, ref List<PatientProfileSettingDTO> InsertList, ref List<PatientProfileSettingDTO> UpdateList)
        {
            if (InsertList == null)
            {
                InsertList = new List<PatientProfileSettingDTO>();
            }
            if (UpdateList == null)
            {
                UpdateList = new List<PatientProfileSettingDTO>();
            }

            foreach (PatientProfileSettingDTO obj in settingsList)
            {
                if (obj.UserUserProfileCategoryID > 0)
                    UpdateList.Add(obj);
                else
                    InsertList.Add(obj);
            }
        }
    }
}