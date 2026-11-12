using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using EprescriptionService.Utility;

namespace AdpaterManagers
{
    /// <summary>
    ///   Class:          MessageManager
    ///   Created by:     Waqas Baber 
    ///   Description:    MessageManager for dealing any Message that is passed between eprescription and quivus
    ///   Notes:          
    ///   Copyright 2011 Zigron Inc. All rights reserved  
    /// </summary>
    public class MessageManager
    {


        public static XmlDocument CreatMessage(MessageTypes messageTypes)
        {
            var mess = new XmlDocument();
            XmlTextReader flowTextReader;
            if (messageTypes == MessageTypes.SendPatient)
            {
                flowTextReader = new XmlTextReader(GetFileNameViaType(MessageTypes.SendPatient));
                flowTextReader.Read();
                mess.Load(flowTextReader);
                return mess;
            }
            else
                return null;
        }

        private static string GetFileNameViaType(MessageTypes messageTypes)
        {
            throw new NotImplementedException();
        }
    }
}
