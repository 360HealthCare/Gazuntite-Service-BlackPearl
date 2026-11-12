using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using Quivus.Model.Common;
using Quivus.Model.AddressBook;
using Quivus.Utility.Fault;
using QuivusModel.AddressBook;

//using AddressBookService.Fault;

namespace AddressBookService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAddressBookService
    {
        [OperationContract]
        bool PingService();

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        AddressBookStaticDataDTO GetAddressBookStaticData();

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<CityDTO> ListCities(int stateId);

        // Not need now
        //[OperationContract]
        //[FaultContract(typeof(AddressBookServiceFault))]
        //int GetStateIdByZIPCode(string zipCode);

        // Not need now
        //[OperationContract]
        //[FaultContract(typeof(AddressBookServiceFault))]
        //List<ZipCodeDTO> ListZipCodes(string city);

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ZipCodeDTO> ListZipCodesByStateIdAndCityName(int stateId, string cityName);

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int SaveUserAddressBook(int managedByUserId, AddressBookDTO objAddBookDTO);

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PhoneDTO GetPartyPrimaryPhoneNoByRegGuid(Guid guid);

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        AddressBookDTO LoadAddressBookByPartyRegGuid(Guid guid);

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// load users address primary fields for tagging/customization
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="settingTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        string LoadUserProfileSettingsAddressByRegGuidAndType(Guid guid, int settingTypeId);

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// load users phone fields for tagging/customization
        /// </summary>       
        /// <param name="guid"></param>
        /// <param name="partyTypeId"></param>
        /// <param name="settingTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        string LoadPartyProfileSettingsPhoneByRegGuidAndType(Guid guid, int ownerTypeId);

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientAddressBookInfoDTO> LoadPatientAddressBookByPartyRegGuid(List<Guid> GuidList);

    }
}
