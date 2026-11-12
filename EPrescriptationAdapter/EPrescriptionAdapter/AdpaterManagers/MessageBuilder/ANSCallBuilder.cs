using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using EprescriptionService.Common.DTOs;

namespace AdpaterManagers.MessageBuilder
{
   
        /// <summary>
        ///   Class:          ANSCallBuilder
        ///   Created by:     Waqas Baber 
        ///   Description:    Implementingn Builder pattern for building custom Messages to send to eprescription adapter 
        ///   Notes:          
        ///   Copyright 2011 Zigron Inc. All rights reserved  
        /// </summary>
        class ANSCallBuilder : MessageBuilder
        {
            private readonly Message _message = new Message();
            /// <summary>
            /// Building Script
            /// </summary>
            public override void BuildScript(object partnerInfo)
            {
                var vendorInfo = (PartnerInfo)partnerInfo;
                #region Fields
                var xmlDocoment = new XmlDocument();
                XmlNode vxml;
                XmlAttribute version;
                XmlNode rcExtRequestNode;
                XmlNode CallerNode;
                XmlNode varNode;
                XmlAttribute varNameAttrib;
                XmlAttribute varExprAttrib;
                XmlNode rcopiaPracticeUSername;
                XmlNode requestNode;
                XmlNode command;
                var counter = 0;
               
                #endregion
                try
                {

                    #region initalizing header of the script Node
                    vxml = xmlDocoment.CreateElement("xml");
                    version = xmlDocoment.CreateAttribute("version");
                    version.InnerXml = "1.0";
                    vxml.Attributes.Append(version);
                    #endregion
                  
                    #region initializing RCExt request tag
                    rcExtRequestNode = xmlDocoment.CreateElement("RCExt");
                    CallerNode = xmlDocoment.CreateElement("Caller");

                  
                    #endregion

                    rcExtRequestNode.AppendChild(CallerNode);
                    vxml.AppendChild(rcExtRequestNode);

                    xmlDocoment.AppendChild(vxml);

                    this._message.Content = xmlDocoment;
                }
                catch (Exception ex)
                {

                    //TODO: implement Exception machanizm over here

                   // ErrHandlerBlock.WriteException(ex.Message, ex.StackTrace, " Exception in Creating dosage selection menu");



                }
            }
            /// <summary>
            /// used for getting xml content
            /// </summary>
            /// <returns></returns>
            public override Message GetContent()
            {
                return this._message;
            }
        }
   
}
