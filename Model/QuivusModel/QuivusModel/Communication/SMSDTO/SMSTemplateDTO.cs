using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace QuivusModel.Communication.SMSDTO
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 17 November 2011
    /// Description: SMS Template
    /// </summary>
    /// 
    [DataContract]
    class SMSTemplateDTO
    {
    //    [SMSTemplateID] [int] NOT NULL,
    //[Text] [varchar](1000) NOT NULL,
    //[CreatedBy] [int] NOT NULL,
    //[CreatedAt] [datetime] NOT NULL,
    //[ModifiedBy] [int] NULL,
    //[ModifiedDate] [datetime] NULL,
    //[IsDeleted] [bit] NOT NULL,


        [DataMember]
        public string Text { get;set;}

        }
}
