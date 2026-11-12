using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuivusManager.IntegratedServies.ReferenceFactory;
using QuivusManager.IntegratedServies.AddressBookService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AdderssBook;
using Quivus.Utility;
using QuivusManager.Buisness.UserBL;
using QuivusManager.Buisness.UserBL.Impl;
using Quivus.Utility.Logger;


namespace QuivusManager.Buisness.AddressBookBL.Impl
{
    public class AddressBookBLImpl : IAddressBookBL
    {
        IUserBL UserBL = null;

        AddressBookServiceClient AddressBookClient = null;

        /// <summary>
        /// Author Ismail  :(
        /// Date 18 Feb 2011
        /// this method just call the address book to save
        /// </summary>
        /// <param name="managedByUserId"></param>
        /// <param name="objAddBookDTO"></param>
        /// <returns></returns>
        public int SaveUserAddressBook(int managedByUserId, AddressBookDTO objAddBookDTO)
        {
            int Result = 0;
            try
            {
                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                QuivusLogger.Info("Dispatching SaveAddressBook Call TO Address Book Service");
                QuivusLogger.Info("SaveAddressBook Param managedByUserId:: " + managedByUserId + "objAddBookDTO" + objAddBookDTO);

                Result = AddressBookClient.SaveUserAddressBook(managedByUserId, objAddBookDTO);
            }
            catch 
            {
                throw;
            }

            return Result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 2 Feb 2011
        /// this method call the user manager to get party guid, calle din case when to save patient/ user AB
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="managedByUserId"></param>
        /// <param name="objAddBookDTO"></param>
        /// <returns></returns>
        public int SaveUserAddressBook(int roleProfileID, int managedByUserId, AddressBookDTO objAddBookDTO)
        {
            int Result = 0;
            try
            {
                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                //M
                if (UserBL == null)
                {
                    UserBL = new UserBLImpl();
                }

                //call the UserManager to get party GUID
                objAddBookDTO.PartyID = UserBL.GetPartyGuidByProfileIdAndTypeId(roleProfileID, objAddBookDTO.AddressPartyTypeID);
                //

                Result = AddressBookClient.SaveUserAddressBook(managedByUserId, objAddBookDTO);
            }
            catch
            {
                throw;
            }

            return Result;
        }

        public AddressBookPhoneResponse GetPartyPrimaryPhoneNoByRegGuid(Guid guid)
        {
            AddressBookPhoneResponse response = null;
            PhoneDTO PhoneObj = null;
            try
            {
                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }


                QuivusLogger.Info("Dispatching GetPartyPrimaryPhoneNoByRegGuid Call TO Address Book Service");
                QuivusLogger.Info("GetPartyPrimaryPhoneNoByRegGuid Param guid::" + guid);

                PhoneObj = AddressBookClient.GetPartyPrimaryPhoneNoByRegGuid(guid);

                if (response == null)
                {
                    response = new AddressBookPhoneResponse();
                }

                if (PhoneObj != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = PhoneObj;
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

        public AddressBookAddressResponse LoadAddressBookByPartyRegGuid(Guid guid)
        {
            AddressBookAddressResponse response = null;
            AddressBookDTO AddressObj = null;
            try
            {
                QuivusLogger.Info("Dispatching LoadAddressBookByPartyRegGuid Call TO Address Book Service");
                QuivusLogger.Info("LoadAddressBookByPartyRegGuid Param guid::" + guid);

                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                AddressObj = AddressBookClient.LoadAddressBookByPartyRegGuid(guid);

                if (response == null)
                {
                    response = new AddressBookAddressResponse();
                }

                if (AddressObj != null)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseData = AddressObj;
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
        /// Date 28 Feb 2011
        /// load users address primary fields for tagging/customization
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="settingTypeId"></param>
        /// <returns></returns>      
        public string LoadUserProfileSettingsAddressByRegGuidAndType(Guid guid, int settingTypeId)
        {
            string settingFieldData = "";
         
            try
            {
                QuivusLogger.Info("Dispatching LoadUserProfileSettingsAddressByRegGuidAndType Call TO Address Book Service");              

                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                settingFieldData = AddressBookClient.LoadUserProfileSettingsAddressByRegGuidAndType(guid, settingTypeId);               
            }
            catch
            {
                throw;
            }
            return settingFieldData;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// load users address primary fields for tagging/customization
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="settingTypeId"></param>
        /// <returns></returns>      
        public string LoadPartyProfileSettingsPhoneByRegGuidAndType(Guid guid, int ownerTypeId)
        {
            string settingFieldData = "";

            try
            {
                QuivusLogger.Info("Dispatching LoadPartyProfileSettingsPhoneByRegGuidAndType Call TO Address Book Service");

                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                settingFieldData = AddressBookClient.LoadPartyProfileSettingsPhoneByRegGuidAndType(guid, ownerTypeId);
            }
            catch
            {
                throw;
            }
            return settingFieldData;
        }


        public List<PatientAddressBookInfoDTO> LoadPatientAddressBookByRegGuidAndType(List<Guid> guidList)
        {
            List<PatientAddressBookInfoDTO> data = null;

            try
            {
                QuivusLogger.Info("Dispatching LoadPatientAddressBookByRegGuidAndType Call TO Address Book Service");

                if (AddressBookClient == null)
                {
                    AddressBookClient = (AddressBookServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(AddressBookServiceClient));
                }

                data = AddressBookClient.LoadPatientAddressBookByPartyRegGuid(guidList);
            }
            catch
            {
                throw;
            }
            return data;
        }
    }
}
