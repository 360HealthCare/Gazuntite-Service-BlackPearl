using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.PatientProfileCustomization
{
    /// <summary>
    /// Author Mansoor
    /// Date 03 Feb 2011
    /// This class is a custom wrapper of the Patient profile info for tagged windows
    /// </summary>

    [DataContract]
    public class PatientProfileInfoResposne : ServiceResponse
    {
        [DataMember]
        public PatientProfileTaggedDTO TaggedProfile { get; set; }
        //[DataMember]
        //public int PatientId { get; set; }

        //[DataMember]
        //public PatientProfileBasicInfoDTO PatientProfileBasicInfoData { get; set; }

        //[DataMember]
        //public PhoneDTO PrimaryPhoneNo { get; set; }

        //[DataMember]
        //public PatientInsuracneInfoDTO PatientInsuracneInfoData { get; set; }
    }
}
