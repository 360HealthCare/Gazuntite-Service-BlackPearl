using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.AddressBookService;



namespace QuivusManager.ServiceResponse.ServiceCustomResponse
{
    /// <summary>
    /// Author Mansoor
    /// Date 19 Jan 2011
    /// </summary>

    [DataContract]
    public class CountryResponse : ServiceResponse
    {
        [DataMember]
        public List<CountryDTO> ResponseData { get; set; }
    }
}
