using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.Search
{
    /// <summary>
    /// Author Mansoor
    /// Date 27 Jan 2011
    /// Contains the patient primary info for the patient search use case
    /// </summary>

    [DataContract]
    public class PatientSearchDTO : QuivusBaseDTO
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int PatientId { get; set; }        

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string QuivusID { get; set; }

        [DataMember]
        public DateTime DOB { get; set; }

        [DataMember]
        public bool IsAuthorized { get; set; }

        [DataMember]
        public bool IsEmailIdAvailable { get; set; }

        [DataMember]
        public bool IsSnailIdAvailable { get; set; }

        [DataMember]
        public bool IsSMSNoAvalable { get; set; }

        [DataMember]
        public bool IsChatIdAvailable { get; set; }

        [DataMember]
        public bool IsPhoneNoAvailable { get; set; }

        [DataMember]
        public string SSN { get; set; }

        [DataMember]
        public int GenderId { get; set; }

        [DataMember]
        public string GenderName { get; set; }
    }


    

}
