using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.Search
{
    [DataContract]
    public class ContactSearchDTO : QuivusBaseDTO
    {

        [DataMember]
        public int UserId { get; set; }

         [DataMember]
        public int PrefixTypeID { get; set; }

         [DataMember]
        public String FirstName { get; set; }

         [DataMember]
         public String LastName { get; set; }

         [DataMember]
         public String MiddleName { get; set; }

         [DataMember]
         public String Suffix { get; set; }

         [DataMember]
         public String UserEmail { get; set; }

    }
}
