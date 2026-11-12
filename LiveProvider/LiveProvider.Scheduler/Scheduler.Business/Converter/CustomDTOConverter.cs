using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.Appointment;
using LiveProvider.Utility.Utilities;
using LiveProvider.Model.Appointment.ClinicRoom;

namespace Scheduler.Business.Converter
{
    public class CustomDTOConverter
    {

        public static void PopulatePatientInfo(Scheduler.Business.UserManagerService.PatientProfileDTO source, ref PatientCompleteInfoDTO dest)
        {
            

            dest.DateOfBirth = source.PatientDTO.DateOfBirth;

            dest.FirstName = source.UserDTO.FirstName;

            dest.PatientId = source.PatientDTO.PatientId;

            dest.GenderId = source.UserDTO.Gender.GenderId;
            dest.GenderName = source.UserDTO.Gender.GenderName;

            dest.Billing = new BillingDTO();

            dest.Billing.ClaimsOverDue = source.ClaimsOverDue;

            dest.Billing.InsuranceBalance = source.InsuranceBalance;

            dest.Billing.IsChronicDiagnosis = source.IsChronicDiagnosis;

            dest.Billing.PatientBalance = source.PatientBalance;

            dest.Billing.RejectedClaims = source.RejectedClaims;

            dest.LastName = source.UserDTO.LastName;

            dest.MiddleName = source.UserDTO.MiddleName;

            List<LiveProvider.Model.Appointment.Patient.PatientAllergyDTO> sAllergy = new List<LiveProvider.Model.Appointment.Patient.PatientAllergyDTO>();

            GenericCloneCreator.convertList(source.PatientAllergyList, ref sAllergy, typeof(LiveProvider.Model.Appointment.Patient.PatientAllergyDTO));

            dest.PatientAllergyList = sAllergy;

            

           // dest.PatientId = source.PatientId;

            List<LiveProvider.Model.Appointment.Patient.PatientInsuranceDTO> sInsurance = new List<LiveProvider.Model.Appointment.Patient.PatientInsuranceDTO>();

            GenericCloneCreator.convertList(source.PatientInsuranceList, ref sInsurance, typeof(LiveProvider.Model.Appointment.Patient.PatientInsuranceDTO));

            dest.Visit = new VisitDTO();

            dest.PatientInsuranceList = sInsurance;

            dest.Visit.PreferredLocation = "";

            dest.Visit.PrimaryProvider = "";

            dest.QuivusId = source.PatientDTO.QuivusId;

            //dest.RecGuid = source.PatientDTO.RecGuid; by sami on frontend request

            dest.Visit.ReferringProvider = "";

            dest.SSN = source.UserDTO.SSN;

            dest.UserId = source.UserDTO.UserId;

            dest.PhoneNumber = source.PatientDTO.PatientPhoneNumber;

        }

        public static void PopulateVisitInfo(List<Scheduler.Business.VisitService.PatientLastSeenDTO> source, ref PatientCompleteInfoDTO dest)
        {
            dest.Visit.LastVisit = FindMaxPatientLastSeenDTO(source);
        }

        static string FindMaxPatientLastSeenDTO(List<Scheduler.Business.VisitService.PatientLastSeenDTO> source)
        {
            string sLastSeen = "";

            int iFind = -1;

            DateTime maxDate = new DateTime();

            Scheduler.Business.VisitService.PatientLastSeenDTO obj;

            for (int i = 0; i < source.Count; i++)
            {
                obj = (Scheduler.Business.VisitService.PatientLastSeenDTO)source[i];

                if (obj.LastSeen > maxDate)
                {
                    iFind = i;
                }
            }

            if (iFind != -1)
            {
                sLastSeen = source[iFind].LastSeen.ToString("MM/dd/yyyy");
            }

            return sLastSeen;
        }



        
        public static void PoulateAppointmentDictionary(List<AppointmentDTO> appList, ref List<LocationDTO> locList,List<RoomDTO> rooms)
        {
            //List all locations
            foreach (AppointmentDTO app in appList)
            {
                LocationDTO loc = new LocationDTO();

                //loc.ClinicBranchID = app.StationId;
                loc.StationID = app.StationId;
                loc.StationName = app.StationName;
                if (app.Room.ClinicRoomId > 0)
                {
                    RoomDTO room = GetRoomInfo(app.Room.ClinicRoomId, rooms);
                    app.Room = room;
                }
                else
                    app.Room = null;
                bool isNew = true;

                foreach (LocationDTO locInner in locList)
                {
                    if (locInner.StationID == loc.StationID)
                    {
                        isNew = false;
                        break;
                    }
                }

                if (isNew)
                {
                    locList.Add(loc);
                }
            }

            //List all Providers for Location
            foreach (LocationDTO loc in locList)
            {
                loc.ProviderList = new List<ProviderDTO>();

                loc.ProviderList = FindAllProvidersByLocationID(loc.StationID, appList);

                //List all Days for Provider
                foreach (ProviderDTO pro in loc.ProviderList)
                {
                    //pro.DayList = new List<ScheduleDayDTO>();
                    //List<ScheduleDayDTO> days= FindAllDaysByProviderID(pro.UserId, appList);
                    pro.AppointmentList = new List<AppointmentDTO>();
                    //List all Appointments for Day
                    //foreach (ScheduleDayDTO day in days)
                    //{
                        //day.AppointmentList = new List<AppointmentDTO>();

                    pro.AppointmentList = FindAllAppointmentsByProvider(pro.UserId,loc.StationID,appList);
                   // } 
                }                
            }
            (from app in appList
             select app).ToList().ForEach(app => app.ProviderUserId += null); 

                   
                   
        }

        public static RoomDTO GetRoomInfo(int roomId,List<RoomDTO> rooms)
        {
            RoomDTO room = null;
            try
            {
                foreach (RoomDTO clinicRoom in rooms)
                {
                    if (clinicRoom.ClinicRoomId == roomId)
                    {
                        room = clinicRoom;
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
            return room;
        }

        //Load on station base also
        static List<AppointmentDTO> FindAllAppointmentsByProvider(int providerId,int stationID, List<AppointmentDTO> appList)
        {
            List<AppointmentDTO> appointmentList = new List<AppointmentDTO>();
            foreach (AppointmentDTO app in appList)
            {
                ProviderDTO provider = new ProviderDTO();

                provider.FirstName = app.ProviderFirstName;
                provider.LastName = app.ProviderLastName;
                provider.MiddleName = app.ProviderMiddleName;
                provider.StationId = app.StationId;
                provider.UserId = int.Parse(app.ProviderUserId);
                if (providerId == int.Parse(app.ProviderUserId) && stationID == app.StationId)
                {
                    appointmentList.Add(app);
                }
            }
            return appointmentList;
        }




        static List<ProviderDTO> FindAllProvidersByLocationID(int locationId, List<AppointmentDTO> appList)
        {
            List<ProviderDTO> providerList = new List<ProviderDTO>();

            foreach (AppointmentDTO app in appList)
            {
                ProviderDTO provider = new ProviderDTO();

                provider.FirstName = app.ProviderFirstName;
                provider.LastName = app.ProviderLastName;
                provider.MiddleName = app.ProviderMiddleName;
                provider.StationId = app.StationId;
                provider.UserId = int.Parse(app.ProviderUserId);
               
                if (locationId == app.StationId)
                {
                    bool isNew = true;

                    foreach (ProviderDTO providerInner in providerList)
                    {
                        if (providerInner.UserId == provider.UserId)
                        {
                            isNew = false;
                            break;
                        }
                    }

                    if (isNew)
                    {
                        providerList.Add(provider);
                        app.ProviderLastName = null;
                        app.ProviderFirstName = null;
                        app.ProviderMiddleName = null;
                        //app.ProviderUserId = null;
                    }
                }               
            }
            return providerList;
        }

        static List<ScheduleDayDTO> FindAllDaysByProviderID(int providerId, List<AppointmentDTO> appList)
        {
            List<ScheduleDayDTO> daysList = new List<ScheduleDayDTO>();
            //List all Days
            foreach (AppointmentDTO app in appList)
            {
                ScheduleDayDTO day = new ScheduleDayDTO();
                DateTime tempDate = DateTime.Parse(app.AppointmentDate);

                day.Date = tempDate.ToString("MM/dd/yyyy");
                day.DayName = tempDate.ToString("dddd");

                bool isNew = true;

                if (providerId == int.Parse(app.ProviderUserId))
                {
                    foreach (ScheduleDayDTO dayInner in daysList)
                    {
                        if (dayInner.Date == dayInner.Date)
                        {
                            isNew = false;
                            break;
                        }
                    }

                    if (isNew)
                    {
                        daysList.Add(day);
                    }
                }
            }     

            return daysList;
        }

        static List<AppointmentDTO> FindAllAppointmentsByDay(string date, List<AppointmentDTO> appList)
        {
            List<AppointmentDTO> appointmentList = new List<AppointmentDTO>();
            //List all Days
            foreach (AppointmentDTO app in appList)
            {   
                bool isNew = true;
                DateTime tempDate = DateTime.Parse(app.AppointmentDate);

                if (date == tempDate.ToString("MM/dd/yyyy"))
                {
                    foreach (AppointmentDTO appInner in appointmentList)
                    {
                        if (appInner.AppointmentDate == app.AppointmentDate)
                        {
                            isNew = false;
                            break;
                        }
                    }

                    if (isNew)
                    {
                        appointmentList.Add(app);
                    }
                }
            }

            return appointmentList;
        }

        public PatientCompleteInfoDTO PopulatePatientPhone()
        {
            PatientCompleteInfoDTO patientInfo=null;
            return patientInfo;
        }
    }
}
