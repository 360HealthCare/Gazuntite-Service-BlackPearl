using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HL7.Model.Messages;
using HL7.Model.EngineResponses;
using HL7.Business.HL7Encoder.Interface;
using HL7.Business.HL7Encoder;
using Quivus.Utility.Logger;
using NHapi.Base;

namespace HL7Service.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HL7Engine" in code, svc and config file together.
    public class HL7Engine : HL7BaseService,IHL7Engine
    {
        IMessageParser messageParser = null;
        
        public OMLMessageResponse ParseOrderMessage(OMLMessage ORMMsg)
        {
            OMLMessageResponse response = new OMLMessageResponse();
            try
            {
                if (messageParser == null)
                {
                    messageParser = new MessageParser();
                }
                response.ResponseHL7 = messageParser.ParseOrderMessage(ORMMsg);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                response.IsError = true;
                response.ErrorMessage = exp.Message;
            }
            return response;
        }

        #region Default Implementation of GetServiceVersion
        public override string GetServiceVersion()
        {
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return strVersion;
        }
        #endregion
    }
}
