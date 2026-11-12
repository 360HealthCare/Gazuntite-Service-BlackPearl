using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Quivus.Model.SearchEngine
{
    /// <summary>
    /// Author Muhammad Ismail
    /// created Date 14 jan 2011
    /// Model Class for Search Engine
    /// Changed By : Mansoor
    /// Date : 11 Mar 2011
    /// Change : Thumbnail is added
    /// </summary>
    public class SearchEngineDTO : QuivusBaseDTO
    {

        [DataMember]
        public int UserSearchEngineID { get; set; }

        [DataMember]
        public int SearchEngineTypeID { get; set; }

        [DataMember]
        public String label { get; set; } // From DB It is SearchEngineName changed to label bcoz requested by Air Team 

        [DataMember]
        public byte[] icon { get; set; }

        [DataMember]
        public byte[] Thumbnail { get; set; }

        [DataMember]
        public String description { get; set; }

        [DataMember]
        public String URL { get; set; }

        [DataMember]
        public bool IsPreferred { get; set; }

        [DataMember]
        public bool IsSystemGenerated { get; set; }

        [DataMember]
        public bool IsActive { get; set; }


        //[DataMember]
        //public int UserID { get; set; }

        //[DataMember]
        //public int ClinicBranchID { get; set; }

        //[DataMember]
        //public bool IsSystemGenerated { get; set; }

        //[DataMember]
        //public int CreatedBy { get; set; }

        //[DataMember]
        //public DateTime CreatedAt { get; set; }

        //[DataMember]
        //public int ModifiedBy { get; set; }

        //[DataMember]
        //public DateTime ModifiedAt { get; set; }

        //[DataMember]
        //public bool IsUserPreferred { get; set; }




    }
}
