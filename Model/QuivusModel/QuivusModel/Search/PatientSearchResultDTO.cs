using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.Search
{
    /// <summary>
    /// Author Mansoor
    /// Date 27 Jan 2011
    /// It contains the result information of the patient search result, contaning the no of records, and page info
    /// </summary>

    [DataContract]
    public class PatientSearchResultDTO
    {
        [DataMember]
        public List<PatientSearchDTO> PatientSearchList { get; set; }

        [DataMember]
        public int CurrentPageNo { get; set; }

        [DataMember]
        public int CurrentFirstRecordNo { get; set; }

        [DataMember]
        public int CurrentLastRecordNo { get; set; }

        [DataMember]
        public int TotalNoOfRecords { get; set; }
    }
}
