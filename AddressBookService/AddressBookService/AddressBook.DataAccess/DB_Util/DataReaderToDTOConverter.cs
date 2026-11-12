using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Model.AddressBook;
using Quivus.Utility.CustomException;
using QuivusModel.AddressBook;


namespace AddressBook.DataAccess.DB_Util
{
    /// <summary>
    /// this class is used to convert the Reader object to DTO
    /// </summary>
    public class DataReaderToDTOConverter
    {
        public static AddressBookDTO ConverterToAddressBookDTO(IDataReader objReader)
        {
            AddressBookDTO objAddressBook = null;

            try
            {
                if (objReader != null)
                {
                    //Single Record will be returned.
                    if (objReader.Read())
                    {
                        objAddressBook = new AddressBookDTO();

                        //these values cann't be null so DBNull check is ommited
                        objAddressBook.AddressBookID = int.Parse(objReader["AddressBookID"].ToString());
                        objAddressBook.PartyID = Guid.Parse(objReader["PartyGuid"].ToString());
                        objAddressBook.AddressPartyTypeID = int.Parse(objReader["AddressPartyTypeID"].ToString());
                    }

                    if (objAddressBook != null)
                    {
                        if (objReader.NextResult()) // Get All Phone nos
                        {
                            List<PhoneDTO> PhoneDTOList = new List<PhoneDTO>();

                            PhoneDTO objPhoneDTO = null;

                            //Multiple Records can be returned
                            while (objReader.Read())
                            {
                                objPhoneDTO = new PhoneDTO();

                                //these values cann't be null so DBNull check is ommited
                                objPhoneDTO.PhoneID = int.Parse(objReader["PhoneID"].ToString());
                                objPhoneDTO.AddressBookID = int.Parse(objReader["AddressBookID"].ToString());
                                // objPhoneDTO.PhoneTypeID = int.Parse(objReader["PhoneTypeID"].ToString()); // not in use
                                objPhoneDTO.NumberTypeID = int.Parse(objReader["NumberTypeID"].ToString());
                                objPhoneDTO.PhoneNumber = objReader["PhoneNumber"].ToString();
                                objPhoneDTO.Extension = objReader["PhoneExtension"].ToString();

                                objPhoneDTO.NumberTypeName = objReader["NumberTypeName"].ToString();

                                objPhoneDTO.IsPrimary = bool.Parse(objReader["IsPrimary"].ToString());
                                objPhoneDTO.OwnerTypeID = int.Parse(objReader["OwnerTypeID"].ToString());

                                PhoneDTOList.Add(objPhoneDTO);
                            }
                            objAddressBook.PhoneDTOlist = PhoneDTOList;
                        }

                        if (objReader.NextResult()) // Get All Address
                        {
                            List<AddressDTO> AddressDTOList = new List<AddressDTO>();

                            AddressDTO objAddressDTO = null;

                            //Multiple Records can be returned
                            while (objReader.Read())
                            {
                                objAddressDTO = new AddressDTO();

                                //these values cann't be null so DBNull check is ommited
                                objAddressDTO.AddressID = int.Parse(objReader["AddressID"].ToString());
                                objAddressDTO.AddressBookID = int.Parse(objReader["AddressBookID"].ToString());
                                objAddressDTO.ZipCodeID = int.Parse(objReader["ZipCodeID"].ToString());
                                // objAddressDTO.AddressTypeID = int.Parse(objReader["AddressTypeID"].ToString()); // not in use

                                if (objReader["StreetNo"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StreetNo"].ToString()))
                                {
                                    objAddressDTO.StreetNo = objReader["StreetNo"].ToString();
                                }

                                if (objReader["StreetName"] != DBNull.Value && !string.IsNullOrEmpty(objReader["StreetName"].ToString()))
                                {
                                    objAddressDTO.StreetName = objReader["StreetName"].ToString();
                                }

                                objAddressDTO.IsPrimary = bool.Parse(objReader["IsPrimary"].ToString());
                                objAddressDTO.CityCode = objReader["CityCode"].ToString();
                                objAddressDTO.CityName = objReader["CityName"].ToString();
                                objAddressDTO.StateID = int.Parse(objReader["StateID"].ToString());
                                objAddressDTO.ZipCode = objReader["ZipCode"].ToString();
                                objAddressDTO.CountryID = int.Parse(objReader["CountryID"].ToString());
                                objAddressDTO.StateCode = objReader["StateCode"].ToString();
                                objAddressDTO.StateID = int.Parse(objReader["StateID"].ToString());
                                objAddressDTO.StateName = objReader["StateName"].ToString();
                                objAddressDTO.CountryID = int.Parse(objReader["CountryID"].ToString());
                                objAddressDTO.CountryCode = objReader["CountryCode"].ToString();
                                objAddressDTO.CountryName = objReader["CountryName"].ToString();

                                AddressDTOList.Add(objAddressDTO);
                            }
                            objAddressBook.AddressDTOlist = AddressDTOList;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new DTOConversionException(ProjectConstant.ADDRESS_BOOK_DTO_CONVERSION_ERROR_MSG + ".ConverterToAddressBookDTO", ProjectConstant.ERROR_CODE.AddressBookDTOConversion.ToString());
            }
            return objAddressBook;
        }

        internal static List<PatientAddressBookInfoDTO> ConverterToPatientAddressBookDTO(IDataReader objReader)
        {
            List<PatientAddressBookInfoDTO> patientList = null;
            try
            {
                if (objReader != null)
                {
                    patientList = new List<PatientAddressBookInfoDTO>();
                    {
                        while (objReader.Read())
                        {
                            PatientAddressBookInfoDTO objPatient = new PatientAddressBookInfoDTO();

                            if (objReader["AddressBookID"] != DBNull.Value)
                            objPatient.AddressBookID = int.Parse(objReader["AddressBookID"].ToString());

                            if (objReader["RecGuid"] != DBNull.Value)
                            objPatient.RecGuid = Guid.Parse(objReader["RecGuid"].ToString());

                            objPatient.HomePhone = objReader["AddressBookID"].ToString();
                            objPatient.Address = objReader["ADDRESS"].ToString();
                            objPatient.State = objReader["STATE"].ToString();
                            objPatient.Zip = objReader["Zip"].ToString();
                            objPatient.City = objReader["City"].ToString();
                            
                            patientList.Add(objPatient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.VISIT_DTO_CONVERSION_ERROR_MSG + ".ConverterToPatientAddressBookDTO", ProjectConstant.ERROR_CODE.VisitDTOConversion.ToString());
            }
            return patientList;
        }
    }
}
