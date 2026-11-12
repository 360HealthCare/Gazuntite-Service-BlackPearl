using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Quivus.Model.AddressBook;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;

namespace AddressBook.DataAccess.DB_Util
{
    /// <summary>
    /// Author Mansoor
    /// Date 20 Jan 2011
    /// Convert the DTO to the required Table
    /// </summary>

    public class DTOToTableConverter
    {
        //public DataTable PhoneDTOConverter(PhoneDTO DTO)
        //{

        //}

        public static DataTable PhoneDTOListConverter(List<PhoneDTO> phoneDTOList)
        {
            DataTable dtPhone = null;

            try
            {
                dtPhone = new DataTable();
                dtPhone.Columns.Add("PhoneID", typeof(System.Int32));
                dtPhone.Columns.Add("AddressBookID", typeof(System.Int32));
                dtPhone.Columns.Add("PhoneTypeID", typeof(System.Int32));
                dtPhone.Columns.Add("NumberTypeID", typeof(System.Int32));
                dtPhone.Columns.Add("PhoneNumber", typeof(System.String));
                dtPhone.Columns.Add("PhoneExtension", typeof(System.String));
                dtPhone.Columns.Add("IsPrimary", typeof(System.Boolean));
                dtPhone.Columns.Add("CreatedBy", typeof(System.Int32));
                dtPhone.Columns.Add("UpdatedBy", typeof(System.Int32));
                dtPhone.Columns.Add("OwnerTypeID", typeof(System.Int32));
                dtPhone.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtPhone.AcceptChanges();

                PhoneDTO objPhoneDTO;

                for (int indexPhone = 0; indexPhone < phoneDTOList.Count; indexPhone++)
                {
                    objPhoneDTO = phoneDTOList[indexPhone];
                    DataRow dRow = dtPhone.NewRow();

                    dRow["PhoneID"] = objPhoneDTO.PhoneID;
                    dRow["AddressBookID"] = objPhoneDTO.AddressBookID;
                    //M dRow["PhoneTypeID"] = objPhoneDTO.PhoneTypeID;
                    dRow["NumberTypeID"] = objPhoneDTO.NumberTypeID;
                    dRow["PhoneNumber"] = objPhoneDTO.PhoneNumber;
                    dRow["PhoneExtension"] = objPhoneDTO.Extension;
                    dRow["IsPrimary"] = objPhoneDTO.IsPrimary;
                    dRow["CreatedBy"] = objPhoneDTO.CreatedBy;
                    dRow["UpdatedBy"] = objPhoneDTO.UpdatedBy;
                    dRow["OwnerTypeID"] = objPhoneDTO.OwnerTypeID;
                    dRow["IsDeleted"] = objPhoneDTO.IsDeleted;

                    dtPhone.Rows.Add(dRow);
                }
                dtPhone.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.ADDRESS_BOOK_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".PhoneDTOListConverter", ProjectConstant.ERROR_CODE.AddressBookTableConversion.ToString());
            }
            return dtPhone;
        }

        public static DataTable AddressDTOListConverter(List<AddressDTO> addressDTOList)
        {
            DataTable dtAddress = null;

            try
            {
                dtAddress = new DataTable();
                dtAddress.Columns.Add("AddressID", typeof(System.Int32));
                dtAddress.Columns.Add("AddressBookID", typeof(System.Int32));
                dtAddress.Columns.Add("ZipCodeID", typeof(System.Int32));
                dtAddress.Columns.Add("AddressTypeID", typeof(System.Int32));
                dtAddress.Columns.Add("StreetNo", typeof(System.String));
                dtAddress.Columns.Add("StreetName", typeof(System.String));
                dtAddress.Columns.Add("IsPrimary", typeof(System.Boolean));
                dtAddress.Columns.Add("CreatedBy", typeof(System.Int32));
                dtAddress.Columns.Add("UpdatedBy", typeof(System.Int32));
                dtAddress.Columns.Add("IsDeleted", typeof(System.Boolean));           
                
                dtAddress.AcceptChanges();

                AddressDTO objAddressDTO;

                for (int indexAddress = 0; indexAddress < addressDTOList.Count; indexAddress++)
                {
                    objAddressDTO = addressDTOList[indexAddress];
                    DataRow dRow = dtAddress.NewRow();

                    dRow["AddressID"] = objAddressDTO.AddressID;
                    dRow["AddressBookID"] = objAddressDTO.AddressBookID;
                    dRow["ZipCodeID"] = objAddressDTO.ZipCodeID;
                    //M dRow["AddressTypeID"] = objAddressDTO.AddressTypeID;
                    dRow["StreetNo"] = objAddressDTO.StreetNo;
                    dRow["StreetName"] = objAddressDTO.StreetName;
                    dRow["IsPrimary"] = objAddressDTO.IsPrimary;
                    dRow["CreatedBy"] = objAddressDTO.CreatedBy;
                    dRow["UpdatedBy"] = objAddressDTO.UpdatedBy;
                    dRow["IsDeleted"] = objAddressDTO.IsDeleted;             

                    dtAddress.Rows.Add(dRow);
                }
                dtAddress.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.ADDRESS_BOOK_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".AddressDTOListConverter", ProjectConstant.ERROR_CODE.AddressBookTableConversion.ToString());
            }
            return dtAddress;
        }

        internal static DataTable PateintAdressInfoDTOListConverter(List<Guid> GuidList)
        {
            DataTable dtAddress = null;

            try
            {
                dtAddress = new DataTable();
                dtAddress.Columns.Add("AddressPartyRecGuid", typeof(System.Guid));               
                dtAddress.AcceptChanges();
               
                foreach (Guid obj in GuidList)
                {
                    DataRow dRow = dtAddress.NewRow();
                    dRow["AddressPartyRecGuid"] = obj;
                    dtAddress.Rows.Add(dRow);
                }
                dtAddress.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.ADDRESS_BOOK_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".AddressDTOListConverter", ProjectConstant.ERROR_CODE.AddressBookTableConversion.ToString());
            }
            return dtAddress;
        }
    }
}
