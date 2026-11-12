using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.ELabs.QuickInput
{
    [DataContract]
    public class LabTestTypeDTO
    {
        [DataMember]
        public int LabTestTypeID { get; set; }

        [DataMember]
        public string Code { get; set; }

        //[DataMember]
        //public string LOINCCode { get; set; }

        [DataMember]
        public string TestTypeName { get; set; }

        //[DataMember]
        //public string TestType { get; set; }

        //[DataMember]
        //public string TestShortName { get; set; }

        //[DataMember]
        //public string MAXPrice { get; set; }

        //[DataMember]
        //public string CPTCode { get; set; }

        //[DataMember]
        //public string TestGroupType { get; set; }

        //[DataMember]
        //public int GroupTestID { get; set; }

        //[DataMember]
        //public string PanelName { get; set; }

        //[DataMember]
        //public string TestTypeName { get; set; }
    }
}
