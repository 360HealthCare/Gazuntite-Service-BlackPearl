using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Quivus.Model.User.Patient;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using Quivus.Model.User.Patient;
using QuivusModel.User.Patient;

namespace UserManagerService.DataAccess.DB_Util
{
    /// <summary>
    /// Author Mansoor
    /// Date 21 Jan 2011
    /// Convert the DTO to the required Table
    /// </summary>

    public class DTOToTableConverter
    {
        /// <summary>
        /// Author Mansoor
        /// Updated Date 21 June 2011
        /// TVP Updated by Ismail New property added 
        /// </summary>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        public static DataTable EmergencyContactDTOListConverter(List<PatientEmergencyContactDTO> EmergencyContactList)
        {
            DataTable dtEmergencyContact = null;

            try
            {
                if (EmergencyContactList != null)
                {
                    dtEmergencyContact = new DataTable();

                    dtEmergencyContact.Columns.Add("PatientEmergencyContactID", typeof(System.Int32));
                    dtEmergencyContact.Columns.Add("FirstName", typeof(System.String));
                    dtEmergencyContact.Columns.Add("LastName", typeof(System.String));
                    dtEmergencyContact.Columns.Add("RelationShipID", typeof(System.Int32));
                    //   dtEmergencyContact.Columns.Add("StatusID", typeof(System.Int32));
                    dtEmergencyContact.Columns.Add("AllowCommunication", typeof(System.Boolean));
                    dtEmergencyContact.Columns.Add("RecGuid", typeof(System.Guid));
                    dtEmergencyContact.Columns.Add("IsDeleted", typeof(System.Boolean));

                    foreach (PatientEmergencyContactDTO ObjDTO in EmergencyContactList)
                    {
                        DataRow dRow = dtEmergencyContact.NewRow();

                        dRow["PatientEmergencyContactID"] = ObjDTO.PatientEmergencyContactId;

                        dRow["FirstName"] = ObjDTO.FirstName;
                        dRow["LastName"] = ObjDTO.LastName;
                        dRow["RelationShipID"] = ObjDTO.RelationShipType.RelationshipId;
                      //  dRow["StatusID"] = ObjDTO.StatusType.StatusID;
                        dRow["AllowCommunication"] = ObjDTO.AllowCommunication;
                        dRow["RecGuid"] = ObjDTO.RecGuid; // this will be set from Quivus manager
                        dRow["IsDeleted"] = ObjDTO.IsDeleted;

                        dtEmergencyContact.Rows.Add(dRow);
                    }

                    dtEmergencyContact.AcceptChanges();
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.EMERGENCY_CONTACT_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".EmergencyContactListConverter", ProjectConstant.ERROR_CODE.EmergencyTableConversion.ToString());
            }
            return dtEmergencyContact;
        }

        public static DataTable PatientLanguageDTOListConverter(List<PatientLanguageDTO> PatientLanguageDTOList)
        {
            DataTable dtPhone = null;
            try
            {
                dtPhone = new DataTable();

                dtPhone.Columns.Add("PatientLanguageID", typeof(System.Int32));
                dtPhone.Columns.Add("LanguageID", typeof(System.Int32));
                dtPhone.Columns.Add("IsPreferred", typeof(System.Boolean));
                dtPhone.Columns.Add("LanguageAbilityModeID", typeof(System.Int32));
                dtPhone.Columns.Add("CountryID", typeof(System.Int32));
                dtPhone.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtPhone.AcceptChanges();

                PatientLanguageDTO objLanaguageDTO;

                for (int indexLang = 0; indexLang < PatientLanguageDTOList.Count; indexLang++)
                {
                    objLanaguageDTO = PatientLanguageDTOList[indexLang];
                    DataRow dRow = dtPhone.NewRow();

                    dRow["PatientLanguageID"] = objLanaguageDTO.PatientLanguageId;
                    dRow["LanguageID"] = objLanaguageDTO.LanguageID;
                    dRow["IsPreferred"] = objLanaguageDTO.IsPreffered;

                    if (objLanaguageDTO.PatientLanguageAbilityMode != null)
                    {
                        dRow["LanguageAbilityModeID"] = objLanaguageDTO.PatientLanguageAbilityMode.LanguageAbilityModeId;
                    }

                    if (objLanaguageDTO.Country != null)
                    {
                        dRow["CountryID"] = objLanaguageDTO.Country.CountryID;
                    }
                   
                    dRow["IsDeleted"] = objLanaguageDTO.IsDeleted;

                    dtPhone.Rows.Add(dRow);
                }
                dtPhone.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".PatientLanaguageDTOListConverter", ProjectConstant.ERROR_CODE.UserManagerTableConversion.ToString());
            }
            return dtPhone;
        }

        public static DataTable PatientInsuranceDTOListConverter(List<PatientInsuranceDTO> PatientInsuranceDTOList, int patientID)
        {
            DataTable dtInsurance = null;
            try
            {
                dtInsurance = new DataTable();

                //     [PatientId]
                //,[InsuranceNatureId]
                //,[InsuranceType]
                //,[InsuranceOrganizationId]
                //,[PolicyNumber]
                //,[GroupNumber]
                //,[InsurancePlan]
                //,[PersonName]
                //,[PersonDateOfBirth]
                //,[GenderId]
                //,[StartDate]
                //,[EndDate]
                //,[Copay]
                //,[IsEligible]
                //,[InsuranceStatus]          
                //,[CreatedBy] 
                //,[CreatedAt]
                dtInsurance.Columns.Add("PatientInsuranceId", typeof(System.Int32));
                dtInsurance.Columns.Add("InsuranceType", typeof(System.Int32));
                dtInsurance.Columns.Add("InsuranceNatureId", typeof(System.Int32));
                dtInsurance.Columns.Add("PatientId", typeof(System.Int32));
                dtInsurance.Columns.Add("InsuranceOrganizationId", typeof(System.Int32));
                dtInsurance.Columns.Add("PolicyNumber", typeof(System.String));
                dtInsurance.Columns.Add("GroupNumber", typeof(System.String));
                dtInsurance.Columns.Add("InsurancePlan", typeof(System.String));
                dtInsurance.Columns.Add("PersonName", typeof(System.String));
                dtInsurance.Columns.Add("PersonDateOfBirth", typeof(System.DateTime));
                dtInsurance.Columns.Add("GenderId", typeof(System.Int32));
                dtInsurance.Columns.Add("StartDate", typeof(System.DateTime));
                dtInsurance.Columns.Add("EndDate", typeof(System.DateTime));
                dtInsurance.Columns.Add("Copay", typeof(System.String));
                dtInsurance.Columns.Add("IsEligible", typeof(System.Boolean));
                dtInsurance.Columns.Add("InsuranceStatus", typeof(System.Int32));

                dtInsurance.AcceptChanges();

                PatientInsuranceDTO objInsuranceDTO;

                for (int indexInsurance = 0; indexInsurance < PatientInsuranceDTOList.Count; indexInsurance++)
                {
                    objInsuranceDTO = PatientInsuranceDTOList[indexInsurance];
                    DataRow dRow = dtInsurance.NewRow();

                    dRow["PatientInsuranceId"] = objInsuranceDTO.PatientInsuranceId;
                    QuivusLogger.Info("PatientInsuranceId" + objInsuranceDTO.PatientInsuranceId);

                    if (objInsuranceDTO.InsuranceType != null)
                    {
                        dRow["InsuranceType"] = objInsuranceDTO.InsuranceType.InsuranceTypeId;
                        QuivusLogger.Info("objInsuranceDTO.InsuranceType.InsuranceTypeId" + objInsuranceDTO.InsuranceType.InsuranceTypeId);
                    }

                    if (objInsuranceDTO.InsuranceNature != null)
                    {
                        dRow["InsuranceNatureId"] = objInsuranceDTO.InsuranceNature.InsuranceNatureId;
                        QuivusLogger.Info("objInsuranceDTO.InsuranceNature.InsuranceNatureId" + objInsuranceDTO.InsuranceNature.InsuranceNatureId);
                    }

                    QuivusLogger.Info("Registration PatientInsuranceDTOListConverter Method :: patientIDpatientID-ID" + patientID);

                    dRow["PatientId"] = patientID;

                    if (objInsuranceDTO.InsuranceOrganization != null)
                    {
                        dRow["InsuranceOrganizationId"] = objInsuranceDTO.InsuranceOrganization.InsuranceOrganizationID;
                        QuivusLogger.Info("objInsuranceDTO.InsuranceOrganization.InsuranceOrganizationID" + objInsuranceDTO.InsuranceOrganization.InsuranceOrganizationID);
                    }

                    dRow["PolicyNumber"] = objInsuranceDTO.PolicyNumber;
                    QuivusLogger.Info("objInsuranceDTO.PolicyNumber" + objInsuranceDTO.PolicyNumber);
                    dRow["GroupNumber"] = objInsuranceDTO.GroupNumber;
                    QuivusLogger.Info("GroupNumber" + objInsuranceDTO.GroupNumber);
                    dRow["InsurancePlan"] = objInsuranceDTO.InsurancePlan;
                    QuivusLogger.Info("InsurancePlan" + objInsuranceDTO.InsurancePlan);
                    dRow["PersonName"] = objInsuranceDTO.InsuredPersonName;
                    QuivusLogger.Info("PersonName" + objInsuranceDTO.InsuredPersonName);
                    dRow["PersonDateOfBirth"] = objInsuranceDTO.InsuredPersonDOB;
                    QuivusLogger.Info("PersonDateOfBirth" + objInsuranceDTO.InsuredPersonDOB);

                    if (objInsuranceDTO.InsuredPersonGender != null)
                    {
                        dRow["GenderId"] = objInsuranceDTO.InsuredPersonGender.GenderId;
                        QuivusLogger.Info("objInsuranceDTO.InsuredPersonGender.GenderId" + objInsuranceDTO.InsuredPersonGender.GenderId);
                    }

                    dRow["StartDate"] = objInsuranceDTO.StartDate;
                    QuivusLogger.Info("StartDate" + objInsuranceDTO.StartDate);
                    dRow["EndDate"] = objInsuranceDTO.EndDate;
                    QuivusLogger.Info("EndDate" + objInsuranceDTO.EndDate);
                    dRow["Copay"] = objInsuranceDTO.Copay;
                    QuivusLogger.Info("Copay" + objInsuranceDTO.Copay);

                    if (string.IsNullOrEmpty(objInsuranceDTO.IsEligible))
                    {
                        dRow["IsEligible"] = DBNull.Value;
                        QuivusLogger.Info("IsEligible" + objInsuranceDTO.IsEligible);
                    }
                    else if (objInsuranceDTO.IsEligible.ToLower() == ProjectConstant.STRING_TRUE.ToLower() || objInsuranceDTO.IsEligible.ToLower() == ProjectConstant.STRING_FALSE.ToLower())
                    {
                        QuivusLogger.Info("Parse::IsEligible" + objInsuranceDTO.IsEligible);
                        dRow["IsEligible"] = bool.Parse(objInsuranceDTO.IsEligible);

                    }
                    else
                    {
                        QuivusLogger.Info("IsEligible" + objInsuranceDTO.IsEligible);
                        dRow["IsEligible"] = DBNull.Value;
                    }

                    if (objInsuranceDTO.InsuranceStatus != null)
                    {
                        QuivusLogger.Info("InsuranceStatus" + objInsuranceDTO.InsuranceStatus);
                        dRow["InsuranceStatus"] = objInsuranceDTO.InsuranceStatus.StatusID;
                    }
                    else
                    {
                        QuivusLogger.Info("InsuranceStatus" + objInsuranceDTO.InsuranceStatus);
                        dRow["InsuranceStatus"] = DBNull.Value;
                    }

                    dtInsurance.Rows.Add(dRow);
                }
                dtInsurance.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".PatientInsuranceDTOListConverter", ProjectConstant.ERROR_CODE.UserManagerTableConversion.ToString());
            }
            return dtInsurance;
        }

        public static DataTable PatientAllergyDTOListConverter(List<PatientAllergyDTO> PatientAllergyDTOList)
        {
            DataTable dtAllergy = null;
            try
            {
                dtAllergy = new DataTable();
                QuivusLogger.Info("In PatientAllergyDTOListConverter Method");

                dtAllergy.Columns.Add("PatientHistoryAllergyID", typeof(System.Int32));
                dtAllergy.Columns.Add("AllergyID", typeof(System.Int32));
                dtAllergy.Columns.Add("IsActive", typeof(System.Boolean));
                dtAllergy.Columns.Add("Reaction", typeof(System.String));
                dtAllergy.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtAllergy.AcceptChanges();

                PatientAllergyDTO objAllergyDTO;

                for (int indexAllergy = 0; indexAllergy < PatientAllergyDTOList.Count; indexAllergy++)
                {
                    objAllergyDTO = PatientAllergyDTOList[indexAllergy];
                    DataRow dRow = dtAllergy.NewRow();


                    dRow["PatientHistoryAllergyID"] = objAllergyDTO.PatientHistoryAllergyId;

                    if (objAllergyDTO.AllergyType !=null)
                    dRow["AllergyID"] = objAllergyDTO.AllergyType.AllergyTypeIdSeq;

                    
                    QuivusLogger.Info("In PatientAllergyDTOListConverter Method objAllergyDTO.IsActive::" + objAllergyDTO.IsActive
                        + "objAllergyDTO.Reaction" + objAllergyDTO.Reaction + "objAllergyDTO.IsDeleted" + objAllergyDTO.IsDeleted
                        + "objAllergyDTO.PatientHistoryAllergyId" + objAllergyDTO.PatientHistoryAllergyId + "objAllergyDTO.AllergyType.AllergyTypeIdSeq" + objAllergyDTO.AllergyType);
                    if (string.IsNullOrEmpty(objAllergyDTO.IsActive))
                    {
                        dRow["IsActive"] = DBNull.Value;
                    }
                    else if (objAllergyDTO.IsActive.ToLower() == ProjectConstant.STRING_TRUE.ToLower() || objAllergyDTO.IsActive.ToLower() == ProjectConstant.STRING_FALSE.ToLower())
                    {
                        dRow["IsActive"] = bool.Parse(objAllergyDTO.IsActive);
                    }
                    else
                    {
                        dRow["IsActive"] = DBNull.Value;
                    }

                    dRow["Reaction"] = objAllergyDTO.Reaction;
                    dRow["IsDeleted"] = objAllergyDTO.IsDeleted;

                    dtAllergy.Rows.Add(dRow);
                }
                dtAllergy.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".PatientAllergyDTOListConverter", ProjectConstant.ERROR_CODE.UserManagerTableConversion.ToString());
            }
            return dtAllergy;
        }

        public static DataTable PatientMedicalHistoryDTOListConverter(List<PatientMedicalHistoryDTO> PatientMedicalHistoryDTOList)
        {
            DataTable dtMedicalHistory = null;
            try
            {
                dtMedicalHistory = new DataTable();

                dtMedicalHistory.Columns.Add("PatientHistoryMedicalID", typeof(System.Int32));
                dtMedicalHistory.Columns.Add("ICDCodeID", typeof(System.Int32));
                dtMedicalHistory.Columns.Add("IsActive", typeof(System.Boolean));
                dtMedicalHistory.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtMedicalHistory.AcceptChanges();

                PatientMedicalHistoryDTO objMedicalHistoryDTO;

                for (int indexMedicalHistory = 0; indexMedicalHistory < PatientMedicalHistoryDTOList.Count; indexMedicalHistory++)
                {
                    objMedicalHistoryDTO = PatientMedicalHistoryDTOList[indexMedicalHistory];
                    DataRow dRow = dtMedicalHistory.NewRow();


                    dRow["PatientHistoryMedicalID"] = objMedicalHistoryDTO.PatientHistoryMedicalId;
                    dRow["ICDCodeID"] = objMedicalHistoryDTO.ICDCodeType.IcdCodeIdSeq;

                    if (string.IsNullOrEmpty(objMedicalHistoryDTO.IsActive))
                    {
                        dRow["IsActive"] = DBNull.Value;
                    }
                    else if (objMedicalHistoryDTO.IsActive.ToLower() == ProjectConstant.STRING_TRUE.ToLower() || objMedicalHistoryDTO.IsActive.ToLower() == ProjectConstant.STRING_FALSE.ToLower())
                    {
                        dRow["IsActive"] = bool.Parse(objMedicalHistoryDTO.IsActive);
                    }
                    else
                    {
                        dRow["IsActive"] = DBNull.Value;
                    }

                    dRow["IsDeleted"] = objMedicalHistoryDTO.IsDeleted;

                    dtMedicalHistory.Rows.Add(dRow);
                }
                dtMedicalHistory.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".PatientAllergyDTOListConverter", ProjectConstant.ERROR_CODE.UserManagerTableConversion.ToString());
            }
            return dtMedicalHistory;
        }

        public static DataTable PatientFamilyHistoryDTOListConverter(List<PatientFamilyHistoryDTO> PatientFamilyHistoryDTOList)
        {
            DataTable dtFamilyHistory = null;
            try
            {
                dtFamilyHistory = new DataTable();

                dtFamilyHistory.Columns.Add("PatientHistoryFamilyID", typeof(System.Int32));
                dtFamilyHistory.Columns.Add("ICDCodeID", typeof(System.Int32));
                dtFamilyHistory.Columns.Add("IsActive", typeof(System.Boolean));
                dtFamilyHistory.Columns.Add("RelationshipTypeID", typeof(System.Int32));
                dtFamilyHistory.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtFamilyHistory.AcceptChanges();

                PatientFamilyHistoryDTO objFamilyHistoryDTO;

                for (int indexFamilyHistory = 0; indexFamilyHistory < PatientFamilyHistoryDTOList.Count; indexFamilyHistory++)
                {
                    objFamilyHistoryDTO = PatientFamilyHistoryDTOList[indexFamilyHistory];
                    DataRow dRow = dtFamilyHistory.NewRow();


                    dRow["PatientHistoryFamilyID"] = objFamilyHistoryDTO.PatientHistoryFamilyId;

                    dRow["ICDCodeID"] = objFamilyHistoryDTO.ICDCodeType.IcdCodeIdSeq;

                    if (string.IsNullOrEmpty(objFamilyHistoryDTO.IsActive))
                    {
                        dRow["IsActive"] = DBNull.Value;
                    }
                    else if (objFamilyHistoryDTO.IsActive.ToLower() == ProjectConstant.STRING_TRUE.ToLower() || objFamilyHistoryDTO.IsActive.ToLower() == ProjectConstant.STRING_FALSE.ToLower())
                    {
                        dRow["IsActive"] = bool.Parse(objFamilyHistoryDTO.IsActive);
                    }
                    else
                    {
                        dRow["IsActive"] = DBNull.Value;
                    }

                    dRow["RelationshipTypeID"] = objFamilyHistoryDTO.RelationshipType.RelationshipId;
                    dRow["IsDeleted"] = objFamilyHistoryDTO.IsDeleted;

                    dtFamilyHistory.Rows.Add(dRow);
                }
                dtFamilyHistory.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".PatientAllergyDTOListConverter", ProjectConstant.ERROR_CODE.UserManagerTableConversion.ToString());
            }
            return dtFamilyHistory;
        }

        internal static DataTable SearchengineTypeListConverter(List<int> SearchEngineTypeIDs)
        {
            DataTable dtSearchType = null;
            try
            {
                dtSearchType = new DataTable();

                dtSearchType.Columns.Add("Rec_Id", typeof(System.Int32));

                dtSearchType.AcceptChanges();

                foreach (int obj in SearchEngineTypeIDs)
                {
                    DataRow dRow = dtSearchType.NewRow();

                    dRow["Rec_Id"] = obj;

                    dtSearchType.Rows.Add(dRow);
                }
                dtSearchType.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".PatientAllergyDTOListConverter", ProjectConstant.ERROR_CODE.UserManagerTableConversion.ToString());
            }
            return dtSearchType;
        }

        internal static DataTable UserIdListConverter(List<int> ProviderIdList)
        {
            DataTable dtUser = null;
            try
            {
                dtUser = new DataTable();

                dtUser.Columns.Add("Rec_Id", typeof(System.Int32));

                dtUser.AcceptChanges();

                foreach (int obj in ProviderIdList)
                {
                    DataRow dRow = dtUser.NewRow();

                    dRow["Rec_Id"] = obj;

                    dtUser.Rows.Add(dRow);
                }
                dtUser.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.USER_ID_DTO_CONVERSION_ERROR_MSG + ".UserIdListConverter", ProjectConstant.ERROR_CODE.UserManagerTableConversion.ToString());
            }
            return dtUser;
        }

        internal static DataTable PatientExternalDTOListConverter(List<QuivusModel.User.Patient.PatientRcopiaDTO> patientList)
        {
            DataTable dtUser = null;
            try
            {
                dtUser = new DataTable();

                dtUser.Columns.Add("PatientId", typeof(System.Int32));
                 dtUser.Columns.Add("RcopiaId", typeof(System.String));
                
                dtUser.AcceptChanges();

                foreach (PatientRcopiaDTO obj in patientList)
                {
                    DataRow dRow = dtUser.NewRow();

                    dRow["PatientId"] = obj.PatientId;
                    dRow["RcopiaId"] = obj.RecGuid;

                    dtUser.Rows.Add(dRow);
                }
                dtUser.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.USER_ID_DTO_CONVERSION_ERROR_MSG + ".UserIdListConverter", ProjectConstant.ERROR_CODE.UserManagerTableConversion.ToString());
            }
            return dtUser;
        }

        internal static DataTable StationIDsListConverter(List<int> stationIDsList)
        {
            DataTable stationIDsTable = null;
            try
            {
                stationIDsTable = new DataTable();
                stationIDsTable.Columns.Add("StationId", typeof(System.Int32));
                stationIDsTable.AcceptChanges();
                foreach (int stationID in stationIDsList)
                {
                    DataRow drStationID = stationIDsTable.NewRow();
                    drStationID["StationId"] = stationID;
                    stationIDsTable.Rows.Add(drStationID);
                }
                stationIDsTable.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.ALLERGIES_CONVERSION_ERROR_MSG + ".StationIDsListConverter", ProjectConstant.ERROR_CODE.AllergyTableConversion.ToString());
            }
            return stationIDsTable;
        }
    }
}
