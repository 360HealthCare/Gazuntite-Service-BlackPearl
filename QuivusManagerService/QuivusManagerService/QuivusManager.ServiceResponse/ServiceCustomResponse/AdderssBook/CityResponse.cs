using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.AddressBookService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.AddressBook
{
    /// <summary>
    /// Author Mansoor
    /// Date 20 Jan 2011
    /// </summary>
    
    [DataContract] 
    public class CityResponse : ServiceResponse
    {
        [DataMember]
        public List<CityDTO> ResponseData { get; set; }
    }
}
   
