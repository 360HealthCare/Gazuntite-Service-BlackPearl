using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    [DataContract]
    public class CityDTO : QuivusBaseDTO
    {
        //[DataMember]
        //private int ZipCodeID { get; set; }

        [DataMember]
        public string CityName { get; set; }

        //[DataMember]
        //private string ZIP { get; set; }
    }
}
