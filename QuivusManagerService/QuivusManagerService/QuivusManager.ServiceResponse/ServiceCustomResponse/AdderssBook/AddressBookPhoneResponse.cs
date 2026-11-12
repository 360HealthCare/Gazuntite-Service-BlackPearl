using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.AddressBookService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.AdderssBook
{
    [DataContract]
    public  class AddressBookPhoneResponse : ServiceResponse
    {
        [DataMember]
        public PhoneDTO ResponseData { get; set; }
    }
}
