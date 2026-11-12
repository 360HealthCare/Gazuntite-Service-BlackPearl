using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.AddressBookService;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.AdderssBook
{
    [DataContract]
    public class AddressBookAddressResponse: ServiceResponse
    {
        [DataMember]
        public AddressBookDTO ResponseData { get; set; }
    }
}
