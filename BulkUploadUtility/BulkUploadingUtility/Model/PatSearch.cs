using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkUploadingUtility.Model
{
    public class PatSearch
    {

        public DateTime DOB { get; set; }

        public string FirstName { get; set; }

        public int GenderId { get; set; }

        public string GenderName { get; set; }

        public bool IsAuthorized { get; set; }

        public bool IsChatIdAvailable { get; set; }

        public bool IsEmailIdAvailable { get; set; }

        public bool IsPhoneNoAvailable { get; set; }

        public bool IsSMSNoAvalable { get; set; }

        public bool IsSnailIdAvailable { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public int PatientId { get; set; }

        public string QuivusID { get; set; }

        public string SSN { get; set; }

        public int UserId { get; set; }

        public override string ToString()
        {
            return FirstName + "   " + MiddleName + "   " + LastName + "   " + DOB + "   " + SSN;
        }
    }
}
