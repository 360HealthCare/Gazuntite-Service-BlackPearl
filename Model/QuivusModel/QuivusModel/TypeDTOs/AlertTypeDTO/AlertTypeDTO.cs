using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace QuivusModel.TypeDTOs.AlertTypeDTO
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 21 November 2011
    /// Description: this contains information of alert type
    /// </summary>
    [DataContract]
    class AlertTypeDTO
    {
        [DataMember]
        public string AlertTypeDescription { get;set;}
    }
}
