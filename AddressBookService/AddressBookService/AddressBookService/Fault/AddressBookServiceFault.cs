using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace AddressBookService.Fault
{
    [Serializable]
    public class AddressBookServiceFault : CommunicationException
    {
        public Guid FaultID { get; set; }

        public string FaultMessage { get; set; }

        public string Code { get; set; }

        public AddressBookServiceFault(string msg)
            : base(msg)
        { }

        protected AddressBookServiceFault(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { }
    }
}

