using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.AddressBookService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData
{
    /// <summary>
    /// Author Mansoor
    /// Date 20 Jan 2011
    /// This class is a custom wrapper of the servcie resposne
    /// </summary>

    [DataContract]
    public class AddressBookStaticDataResponse : ServiceResponse
    {
        [DataMember]
        public AddressBookStaticDataDTO ResponseData { get; set; }
    }
}
