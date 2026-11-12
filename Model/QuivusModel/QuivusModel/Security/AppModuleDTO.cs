using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace QuivusModel.Security
{

     [DataContract]
    public class AppModuleDTO : QuivusBaseDTO 
    {
         
         [DataMember]
         public int IdSeq { get; set; }

          [DataMember]
         public string Name { get; set; }

          [DataMember]
         public int ParentId { get; set; }

          [DataMember]
         public bool PasswordRequired { get; set; }

    }
}
