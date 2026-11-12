using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.Constant.ProjectConstant
{
    public class SchedulerConstant
    {
        public const string TYPE_LOAD_ALL_APPOINTMENT_CURRENT_STATUS_SP = "[type.AppointmentCurrentStatus_LoadAll]";

        public const string TYPE_LOAD_ALL_APPOINTMENT_REQUEST_STATUS_SP = "[type.AppointmentRequestStatus_LoadAll]";

        public const string TYPE_LOAD_ALL_APPOINTMENT_TYPE_SP = "[type.AppointmentType_LoadAll]";

        public const string TYPE_LOAD_ALL_SEARCH_FIELD_TYPE_SP = "[type.SearchField_LoadAll]";

        public const string APPOINTMENT_LOAD_BY_PROVIDER_AND_LOCATION_SP = "[Appointment_LoadByProviderIdAndLocationID]";

        public const string APPOINTMENT_INSERT_SP = "[Appointment_Insert]";

        public const string APPOINTMENT_UPDATE_SP = "[Appointment_Update]";

        public const string APPOINTMENT_LOAD_NEXT_VISIT_SP = "[Appointment_LoadNextVisit]";

        public const string APPOINTMENT_SEARCH_SP = "[Appointment_Search]";

        //public static string APPOINTMENT_FILTER = "[Appointment_Filter]";

        //public static string APPOINTMENT_DATE_FILTER = "[Appointment_DateFilter]";

        //public static string APPOINTMENT_SECTION_FILTER = "[Appointment_Filter]";

        public static string APPOINTMENT_DATE_SEARCH = "[Appointment_DateSearch]";

        public static string WAITING_ROOM_LOAD_SP = "[WaitingRoom_Load]";


        public const string APP_REQUEST_INSERT_SP = "[AppointmentRequest_Insert]";

        public const string APP_REQUEST_DELETE_SP = "[AppointmentRequest_MarkAsDelete]";

        public const string APP_REQUEST_LOAD_BY_PATIENT_SP = "[AppointmentRequest_LoadByPatientID]";


        public const int PAGE_SIZE = 100;

        public enum ViewTypes
        {
            Today,
            WorkingWeek,
            Week,
            Month
        }


        public static int DATE_SEARCH_VALUE = 1;

        public static string APP_REQUEST_LOAD_BY_PROVIDER_SP = "[AppointmentRequest_LoadByProviderID]";

        public static string APP_REQUEST_LOAD_BY_REQUEST_ID_SP = "[AppointmentRequest_RequestId]";

        public static string APPOINTMENTREQUEST_UPADTESTATUS_SP = "[AppointmentRequest_UpdateStatus]";

        public static string APPOINTMENT_DELETE_SP = "[Appointment_MarkAsDelete]";
    }
}
