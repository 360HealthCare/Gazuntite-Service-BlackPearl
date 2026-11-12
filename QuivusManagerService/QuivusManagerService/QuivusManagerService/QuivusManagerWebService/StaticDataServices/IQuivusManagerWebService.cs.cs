using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Image;
using QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.ICDCode;

namespace QuivusManagerWebService
{
    public partial interface IQuivusManagerWebService
    {

        /// <summary>
        /// Author Mansoor
        /// Date 23 Aug 2011
        /// Return all static data required for registeration
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        RegistrationStaticDataResponse GetRegisterationStaticData();

        /// <summary>
        /// Author Mansoor
        /// Date 23 Aug 2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        AddressBookStaticDataResponse GetAddressBookStaticData();

        /// <summary>
        /// Author Mansoor
        /// Date 23 Aug 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ImageTypeResponse LoadImageTypes();

        /// <summary>
        /// Author Mansoor
        /// Date 7 Sep 2011
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        [OperationContract]
        CityResponse GetCityListByState(int stateId);

        /// <summary>
        /// Author Mansoor
        /// Date 7 Sep 2011
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        [OperationContract]
        ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName);


        /// <summary>
        /// Author Mansoor
        /// Date 7 Sep 2011
        /// To fetch the list of 
        /// </summary>
        /// <param name="allergyName"></param>
        /// <returns></returns>
        [OperationContract]
        AllergyTypeResponse SearchAllergyTypeByName(string allergyName);

        /// <summary>
        /// Author Mansoor
        /// Date 7 Sep 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        [OperationContract]
        ICDCodeResponse SearchICDCodeByCode(string icdCode);
    }
}
