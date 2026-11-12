using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// Contains the information of cities and zipcodes.
    /// </summary>
    [DataContract]
    public class ZipCodeDTO : QuivusBaseDTO
    {
        [DataMember]
        public int ZipCodeID { get; set; }

        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public string CityName { get; set; }

        [DataMember]
        public string CityCode { get; set; }

        [DataMember]
        public int StateID { get; set; }
    }
}
