using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LiveProvider.Model.Appointment;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.Errors;
using LiveProvider.Model.TypeDTOs.Appointment;

namespace Scheduler.DataAccess.DB_Util
{
    public class DataReaderToDTOConverter
    {
        /// <summary>
        /// Auhtor Mansoor
        /// Date 17 Oct 2011
        /// Convert the reader to User Types DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<AppointmentDTO> AppointmentDTOConverter(IDataReader objReader)
        {
            List<AppointmentDTO> appointmentList = null;
            try
            {
                if (objReader != null)
                {
                    appointmentList = new List<AppointmentDTO>();
                    {
                        while (objReader.Read())
                        {
                            AppointmentDTO appointmentObj = new AppointmentDTO();

                            appointmentObj.AppointmentId = int.Parse(objReader["AppointmentId"].ToString());
                            appointmentObj.StationId = int.Parse(objReader["StationId"].ToString());

                            if (objReader["LocationName"] != DBNull.Value)
                            {
                                appointmentObj.StationName = objReader["LocationName"].ToString();
                            }

                            if (objReader["AppointmentRequestId"] != DBNull.Value)
                            {
                                appointmentObj.AppointmentRequestId = int.Parse(objReader["AppointmentRequestId"].ToString());
                            }

                            appointmentObj.ProviderUserId = objReader["ProviderUserId"].ToString();

                            appointmentObj.Patient = new PatientInfoDTO();

                            appointmentObj.Patient.PatientID = int.Parse(objReader["PatientId"].ToString());

                            //appointmentObj.AppointmentDate = DateTime.Parse(objReader["AppointmentDate"].ToString()); by sami
                            appointmentObj.AppointmentDate = objReader["AppointmentDate"].ToString();

                            //appointmentObj.AppointmentStartTime = DateTime.Parse(objReader["AppointmentStartTime"].ToString()); by Sami

                            appointmentObj.AppointmentStartTime = DateTime.Parse(objReader["AppointmentStartTime"].ToString()).ToString("hh:mm tt");

                            //appointmentObj.AppointmentEndTime = DateTime.Parse(objReader["AppointmentEndTime"].ToString()); by Sami

                            appointmentObj.AppointmentEndTime = DateTime.Parse(objReader["AppointmentEndTime"].ToString()).ToString("hh:mm tt");

                            appointmentObj.AppointmentType = new AppointmentTypeDTO();
                            appointmentObj.AppointmentType.AppointmentTypeId = int.Parse(objReader["AppointmentTypeId"].ToString());
                            appointmentObj.AppointmentType.AppointmentTypeName = objReader["AppointmentTypeName"].ToString();

                            appointmentObj.AppointmentCurrentStatus = new AppointmentCurrentStatusTypeDTO();
                            appointmentObj.Room = new LiveProvider.Model.Appointment.ClinicRoom.RoomDTO();
                            appointmentObj.AppointmentCurrentStatus.AppointmentCurrentStatusId = int.Parse(objReader["AppointmentCurrentStatusId"].ToString());
                            appointmentObj.AppointmentCurrentStatus.AppointmentCurrentStatusName = objReader["AppointmentCurrentStatusName"].ToString();
                            appointmentObj.AppointmentCurrentStatus.NextAction = objReader["NextAction"].ToString();

                            if (objReader["PatientId"] != DBNull.Value)
                                appointmentObj.Patient.PatientID = int.Parse(objReader["PatientId"].ToString());

                            if (objReader["PatientFirstName"] != DBNull.Value)
                            {
                                appointmentObj.Patient.FirstName = objReader["PatientFirstName"].ToString();
                            }

                            if (objReader["PatientLastName"] != DBNull.Value)
                            {
                                appointmentObj.Patient.LastName = objReader["PatientLastName"].ToString();
                            }

                            if (objReader["PatientMiddleName"] != DBNull.Value)
                            {
                                appointmentObj.Patient.MiddleName = objReader["PatientMiddleName"].ToString();
                            }

                            if (objReader["ProviderFirstName"] != DBNull.Value)
                            {
                                appointmentObj.ProviderFirstName = objReader["ProviderFirstName"].ToString();
                            }

                            if (objReader["ProviderLastName"] != DBNull.Value)
                            {
                                appointmentObj.ProviderLastName = objReader["ProviderLastName"].ToString();
                            }

                            if (objReader["ProviderMiddleName"] != DBNull.Value)
                            {
                                appointmentObj.ProviderMiddleName = objReader["ProviderMiddleName"].ToString();
                            }

                            if (objReader["QuivusId"] != DBNull.Value)
                            {
                                appointmentObj.QuivusId = objReader["QuivusId"].ToString();
                            }

                            if (objReader["LastVisit"] != DBNull.Value)
                            {
                                appointmentObj.LastVisit = objReader["LastVisit"].ToString();
                            }

                            if (objReader["Complaint"] != DBNull.Value)
                            {
                                appointmentObj.ChiefComplaint = objReader["Complaint"].ToString();
                            }

                            if (objReader["RoomId"] != DBNull.Value)
                            {
                                appointmentObj.ClinicRoomId = int.Parse(objReader["RoomId"].ToString());
                            }

                            if (objReader["CreatedBy"] != DBNull.Value)
                            {
                                appointmentObj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                            }

                            if (objReader["CreatedAt"] != DBNull.Value)
                            {
                                appointmentObj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                            }

                            if (objReader["ModifiedBy"] != DBNull.Value)
                            {
                                appointmentObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                            }

                            if (objReader["ModifiedAt"] != DBNull.Value)
                            {
                                appointmentObj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                            }

                            if (objReader["IsDeleted"] != DBNull.Value)
                            {
                                appointmentObj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                            }

                            if (objReader["InformationAlert"] != DBNull.Value)
                            {
                                appointmentObj.InformationAlert = objReader["InformationAlert"].ToString();
                            }

                            if (objReader["ClinicRoomId"] != DBNull.Value)
                            {
                                appointmentObj.Room.ClinicRoomId = int.Parse(objReader["ClinicRoomId"].ToString());
                            }

                            appointmentList.Add(appointmentObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //QuivusLogger.Error(ex);
                throw new DataReaderToDTOConversionException(ErrorMessages.DATAREADER_DTO_CONVERSION_ERROR_MSG + ".ConvertToDTO", ErrorMessages.ERROR_CODE.DataReaderToDTOConversion.ToString(), "");
            }
            return appointmentList;
        }


        /// <summary>
        /// Auhtor Mansoor
        /// Date 17 Oct 2011
        /// Convert the reader to User Types DTO
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<AppointmentRequestDTO> AppointmentRequestDTOConverter(IDataReader objReader)
        {
            List<AppointmentRequestDTO> appointmentList = null;
            try
            {
                if (objReader != null)
                {
                    appointmentList = new List<AppointmentRequestDTO>();
                    {
                        while (objReader.Read())
                        {
                            AppointmentRequestDTO appointmentObj = new AppointmentRequestDTO();

                            appointmentObj.RequestID = int.Parse(objReader["RequestId"].ToString());

                            if (objReader["PatientId"] != DBNull.Value)
                            {
                                appointmentObj.PatientID = int.Parse(objReader["PatientId"].ToString());
                            }

                            if (objReader["AppointmentRequestDate"] != DBNull.Value)
                            {
                                appointmentObj.AppRequestDate = DateTime.Parse(objReader["AppointmentRequestDate"].ToString());
                            }

                            if (objReader["AppointmentStartTime"] != DBNull.Value)
                            {
                                appointmentObj.AppointmentStartTime = DateTime.Parse(objReader["AppointmentStartTime"].ToString());
                            }

                            if (objReader["AppointmentEndTime"] != DBNull.Value)
                            {
                                appointmentObj.AppointmentEndTime = DateTime.Parse(objReader["AppointmentEndTime"].ToString());
                            }

                            if (objReader["IsEmailSent"] != DBNull.Value)
                            {
                                appointmentObj.IsEmailSent = bool.Parse(objReader["IsEmailSent"].ToString());
                            }

                            if (objReader["CreatedBy"] != DBNull.Value)
                            {
                                appointmentObj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                            }

                            if (objReader["CreatedAt"] != DBNull.Value)
                            {
                                appointmentObj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                            }

                            if (objReader["ModifiedBy"] != DBNull.Value)
                            {
                                appointmentObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                            }

                            if (objReader["ModifiedAt"] != DBNull.Value)
                            {
                                appointmentObj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                            }

                            if (objReader["IsDeleted"] != DBNull.Value)
                            {
                                appointmentObj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                            }

                            if (objReader["AppointmentRequestStatusId"] != DBNull.Value)
                            {
                                appointmentObj.AppRequestStatus = new AppointmentRequestStatusTypeDTO();

                                appointmentObj.AppRequestStatus.AppointmentRequestStatusId = int.Parse(objReader["AppointmentRequestStatusId"].ToString());
                                appointmentObj.AppRequestStatus.AppointmentRequestStatusName = objReader["AppointmentRequestStatusName"].ToString();
                            }

                            if (objReader["ProviderId"] != DBNull.Value)
                            {
                                appointmentObj.ProviderID = int.Parse(objReader["ProviderId"].ToString());
                            }

                            if (objReader["ProviderName"] != DBNull.Value)
                            {
                                appointmentObj.ProviderName = objReader["ProviderName"].ToString();
                            }

                            if (objReader["ProviderUserID"] != DBNull.Value)
                            {
                                appointmentObj.ProviderUserID = int.Parse(objReader["ProviderUserID"].ToString());
                            }

                            if (objReader["ProviderSpecialityId"] != DBNull.Value)
                            {
                                appointmentObj.ProviderSpecialityId = int.Parse(objReader["ProviderSpecialityId"].ToString());
                            }

                            if (objReader["ProviderSpecialityName"] != DBNull.Value)
                            {
                                appointmentObj.ProviderSpecialityName = objReader["ProviderSpecialityName"].ToString();
                            }

                            if (objReader["ProviderTypeID"] != DBNull.Value)
                            {
                                appointmentObj.ProviderSpecialityId = int.Parse(objReader["ProviderTypeID"].ToString());
                            }

                            if (objReader["ProviderTypeName"] != DBNull.Value)
                            {
                                appointmentObj.ProviderTypeName = objReader["ProviderTypeName"].ToString();
                            }

                            if (objReader["LocationAddress"] != DBNull.Value)
                            {
                                appointmentObj.LocationAddress = objReader["LocationAddress"].ToString();
                            }

                            if (objReader["LocationName"] != DBNull.Value)
                            {
                                appointmentObj.LocationName = objReader["LocationName"].ToString();
                            }
                           
                            if (objReader["AppointmentTypeId"] != DBNull.Value)
                            {
                                appointmentObj.AppointmentTypeId = int.Parse(objReader["AppointmentTypeId"].ToString());
                            }

                            if (objReader["StationId"] != DBNull.Value)
                            {
                                appointmentObj.StationID = int.Parse(objReader["StationId"].ToString());
                            }

                            if (objReader["QuivusID"] != DBNull.Value)
                            {
                                appointmentObj.QuivusId = objReader["QuivusID"].ToString();
                            }

                            appointmentList.Add(appointmentObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //QuivusLogger.Error(ex);
                throw new DataReaderToDTOConversionException(ErrorMessages.DATAREADER_DTO_CONVERSION_ERROR_MSG + ".ConvertToDTO", ErrorMessages.ERROR_CODE.DataReaderToDTOConversion.ToString(), "");
            }
            return appointmentList;
        }
    }
}
