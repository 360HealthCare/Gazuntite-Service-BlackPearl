using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.User.Patient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Quivus.Utility;

namespace UserManager.DataAccess.DB_Util
{
    public class ParameterPopulator : BasePopulator
    {

        internal static void PopulatePatientDTOParameters(PatientDTO patientDTO, bool IsUpdate, Database db, DbCommand command)
        {
            try
            {


                db.AddInParameter(command, "@UserID", DbType.Int32, patientDTO.UserId);

                if (patientDTO.MaritalStatusType != null)
                    SetValueIfNotNull("@MaritalStatusID", patientDTO.MaritalStatusType.MaritalStatusId, db, command);
                else
                    SetNullValue("@MaritalStatusID", db, command);

                db.AddInParameter(command, "@DateOfBirth", DbType.DateTime, patientDTO.CreatedBy);

                db.AddInParameter(command, "@QuivusID", DbType.String, patientDTO.QuivusId);

                db.AddInParameter(command, "@AllowEmailCommunication", DbType.Boolean, patientDTO.AllowEmailCommunication);

                db.AddInParameter(command, "@SpouseName", DbType.String, patientDTO.SpouseName);


                db.AddInParameter(command, "@MotherMaidenName", DbType.String, patientDTO.MotherMaidenName);
                db.AddInParameter(command, "@FatherName", DbType.String, patientDTO.FatherName);
                db.AddInParameter(command, "@MedicareNumber", DbType.String, patientDTO.MedicareNumber);
                db.AddInParameter(command, "@IVR_PINCODE", DbType.Int32, patientDTO.IVR_PINCODE);

                db.AddInParameter(command, "@FileId", DbType.String, patientDTO.FileId);

                db.AddInParameter(command, "@ExemptReporting", DbType.Boolean, patientDTO.IsReportingExempt);


                if (patientDTO.StatusType != null)
                    SetValueIfNotNull("@PatientStatusId", patientDTO.StatusType.StatusID, db, command);
                else
                    SetNullValue("@PatientStatusId", db, command);

                if (patientDTO.RaceType != null)
                    SetValueIfNotNull("@RaceId", patientDTO.RaceType.RaceId, db, command);
                else
                    SetNullValue("@RaceId", db, command);

                if (patientDTO.EthinicityType != null)
                    SetValueIfNotNull("@EthinicityId", patientDTO.EthinicityType.EthinicityId, db, command);
                else
                    SetNullValue("@EthinicityId", db, command);

                if (patientDTO.OccupationType != null)
                    SetValueIfNotNull("@OccupationId", patientDTO.OccupationType.OccupationId, db, command);
                else
                    SetNullValue("@OccupationId", db, command);


                if (string.IsNullOrEmpty(patientDTO.AlcoholStatus))
                {
                    SetNullValue("@IsAlcoholStatusYes", db, command, DbType.Boolean);
                }
                else if (patientDTO.AlcoholStatus.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsAlcoholStatusYes", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsAlcoholStatusYes", DbType.Boolean, false);
                }

                db.AddInParameter(command, "@AlcoholStatusData", DbType.String, patientDTO.AlcoholStatusData);

                if (string.IsNullOrEmpty(patientDTO.DrugStatus))
                {
                    SetNullValue("@IsDrugStatusYes", db, command, DbType.Boolean);
                }
                else if (patientDTO.AlcoholStatus.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsDrugStatusYes", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsDrugStatusYes", DbType.Boolean, false);
                }

                db.AddInParameter(command, "@DrugStatusData", DbType.String, patientDTO.DrugStatusData);

                db.AddInParameter(command, "@EmployerName", DbType.String, patientDTO.EmployerName);

                db.AddInParameter(command, "@IsProfileComplete", DbType.Boolean, patientDTO.IsProfileComplete);



                db.AddInParameter(command, "@RecGuid", DbType.Guid, patientDTO.RecGuid);

                if (patientDTO.SmokingType != null)
                    SetValueIfNotNull("@SmokingTypeId", patientDTO.SmokingType.SmokingTypeId, db, command);
                else
                    SetNullValue("@SmokingTypeId", db, command);

                db.AddInParameter(command, "@SmokingStatus", DbType.String, patientDTO.SmokingStatus);

                db.AddInParameter(command, "@DefaultPharmacyRCOPIAID", DbType.String, patientDTO.DefaultPharmacyRCOPIAID);
                db.AddInParameter(command, "@RCOPIAID", DbType.String, patientDTO.RCOPIAID);
                db.AddInParameter(command, "@RCOPIAPracticeID", DbType.String, patientDTO.RCOPIAPracticeID);


                if (!IsUpdate)
                {

                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, patientDTO.CreatedAt);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, patientDTO.CreatedBy);
                    db.AddInParameter(command, "@PatientIDOUT", DbType.Int32, 1);

                }
                else
                {

                    db.AddInParameter(command, "@PatientID", DbType.Int32, patientDTO.PatientId);

                    SetValueIfNotNull("@ModifiedBy", patientDTO.ModifiedBy, db, command);
                    SetDateIfNotNull("@ModifiedAt", patientDTO.ModifiedAt, db, command);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
