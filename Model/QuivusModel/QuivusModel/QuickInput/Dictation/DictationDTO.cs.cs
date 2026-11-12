using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput.Dictation
{
    [DataContract]
    public class DictationDTO : QuivusBaseDTO
    {
        [DataMember]
        public Guid DictationId { get; set; }

        [DataMember]
        public byte[] DictationData { get; set; }

        [DataMember]
        public DateTime DictationTimeStamp { get; set; }

       

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }
    }
}
