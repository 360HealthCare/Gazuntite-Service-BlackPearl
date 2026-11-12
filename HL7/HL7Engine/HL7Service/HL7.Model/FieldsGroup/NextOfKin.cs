using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    public class NextOfKin
    {
        public string SetupID { get; set; }

        public ExtendedPersonName Name { get; set; }

        public CodedElement Relationship { get; set; }

        public ExtendedAddress Address { get; set; }

        public ExtendedTelecoNo PhoneNumber { get; set; }


    }
}
