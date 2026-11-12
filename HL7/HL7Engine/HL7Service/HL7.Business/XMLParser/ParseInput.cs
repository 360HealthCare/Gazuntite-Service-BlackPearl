using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Model.V251.Message;
using System.Xml;
using NHapi.Model.V251.Segment;
using NHapi.Model.V251.Datatype;

namespace HL7.Business.XMLParser
{
    public static class ParseInput
    {
        public static ORM_O01 ParseORM(XmlDocument ORMXml)
        {
            ORM_O01 ORMMsg = new ORM_O01();
            XmlDocument temp = null;
            try
            {
                _ParseMSH(temp, ref ORMMsg);
                _ParseNTE(temp, ref ORMMsg);
                _ParsePatient(temp, ref ORMMsg);
                _ParseOrder(temp, ref ORMMsg);
            }
            catch
            {
                throw;
            }
            return ORMMsg;
        }

        private static void _ParseOrder(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            _ParseORC(temp, ref ORMMsg);
            _ParseCTI(temp, ref ORMMsg);
            _ParseBLG(temp, ref ORMMsg);
            _ParseOrderDetail(temp, ref ORMMsg);
            
        }

        private static void _ParseBLG(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            throw new NotImplementedException();
        }

        private static void _ParseCTI(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            throw new NotImplementedException();
        }

        private static void _ParseOrderDetail(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            _ParseOBR(temp, ref ORMMsg);
            _ParseNTE(temp, ref ORMMsg);
            _ParseDG1(temp, ref ORMMsg);
            _ParseObservationGroup(temp, ref ORMMsg);
        }

        private static void _ParseObservationGroup(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            _ParseOBX(temp, ref ORMMsg);
            _ParseNTE(temp, ref ORMMsg);
        }

        private static void _ParseOBX(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            throw new NotImplementedException();
        }

        private static void _ParseDG1(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            throw new NotImplementedException();
        }

        private static void _ParseOBR(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author Sahibzada Sami ud din
        /// Date 12 Feb 2012
        /// This method will parse ORC xml to HL7 format.
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="ORMMsg"></param>
        private static void _ParseORC(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            XCN cn = new XCN(ORMMsg);
            ORMMsg.GetORDER().ORC.OrderControl.Value = "";
            //EI ORMMsg.GetORDER().ORC.PlacerOrderNumber = null;
            ORMMsg.GetORDER().ORC.PlacerOrderNumber.EntityIdentifier.Value = "";
            ORMMsg.GetORDER().ORC.PlacerOrderNumber.NamespaceID.Value = "";
            ORMMsg.GetORDER().ORC.PlacerOrderNumber.UniversalID.Value = "";
            ORMMsg.GetORDER().ORC.PlacerOrderNumber.UniversalIDType.Value = "";
            //EI ORMMsg.GetORDER().ORC.PlacerGroupNumber = null;
            ORMMsg.GetORDER().ORC.PlacerGroupNumber.EntityIdentifier.Value = "";
            ORMMsg.GetORDER().ORC.PlacerGroupNumber.NamespaceID.Value = "";
            ORMMsg.GetORDER().ORC.PlacerGroupNumber.UniversalID.Value = "";
            ORMMsg.GetORDER().ORC.PlacerGroupNumber.UniversalIDType.Value = "";
            ORMMsg.GetORDER().ORC.OrderStatus.Value = "";
            ORMMsg.GetORDER().ORC.ResponseFlag.Value = "";
            //EIP ORMMsg.GetORDER().ORC.Parent.PlacerAssignedIdentifier
            ORMMsg.GetORDER().ORC.Parent.PlacerAssignedIdentifier.EntityIdentifier.Value = "";
            ORMMsg.GetORDER().ORC.Parent.PlacerAssignedIdentifier.NamespaceID.Value = "";
            ORMMsg.GetORDER().ORC.Parent.PlacerAssignedIdentifier.UniversalID.Value = "";
            ORMMsg.GetORDER().ORC.Parent.PlacerAssignedIdentifier.UniversalIDType.Value = "";
            //EIP ORMMsg.GetORDER().ORC.Parent.FillerAssignedIdentifier
            ORMMsg.GetORDER().ORC.Parent.FillerAssignedIdentifier.EntityIdentifier.Value = "";
            ORMMsg.GetORDER().ORC.Parent.FillerAssignedIdentifier.NamespaceID.Value = "";
            ORMMsg.GetORDER().ORC.Parent.FillerAssignedIdentifier.UniversalID.Value = "";
            ORMMsg.GetORDER().ORC.Parent.FillerAssignedIdentifier.UniversalIDType.Value = "";
            //TS ORMMsg.GetORDER().ORC.DateTimeOfTransaction
            ORMMsg.GetORDER().ORC.DateTimeOfTransaction.Time.Value = "";
            //XCN ORMMsg.GetORDER().ORC.GetEnteredBy();
            ORMMsg.GetORDER().ORC.GetEnteredBy();
            ORMMsg.GetORDER().ORC.GetVerifiedBy();
            ORMMsg.GetORDER().ORC.GetOrderingProvider();
            //PL ORMMsg.GetORDER().ORC.EntererSLocation.
            // XTN ORMMsg.GetORDER().ORC.GetCallBackPhoneNumber();
            // TS ORMMsg.GetORDER().ORC.OrderEffectiveDateTime();
            //CE ORMMsg.GetORDER().ORC.OrderControlCodeReason
            //CE ORMMsg.GetORDER().ORC.EnteringOrganization
           // CE ORMMsg.GetORDER().ORC.EnteringDevice
            //XCN ORMMsg.GetORDER().ORC.GetActionBy();
            //XON ORMMsg.GetORDER().ORC.GetOrderingFacilityName();
            //XAD ORMMsg.GetORDER().ORC.GetOrderingFacilityAddress();
            //XTN ORMMsg.GetORDER().ORC.GetOrderingFacilityPhoneNumber();
            //XAD ORMMsg.GetORDER().ORC.GetOrderingProviderAddress();
            //CWE ORMMsg.GetORDER().ORC.OrderStatusModifier
            //TS ORMMsg.GetORDER().ORC.FillerSExpectedAvailabilityDateTime
            //CWE ORMMsg.GetORDER().ORC.ConfidentialityCode
            //CWE ORMMsg.GetORDER().ORC.OrderType
            //CNE ORMMsg.GetORDER().ORC.EntererAuthorizationMode
            //CWE ORMMsg.GetORDER().ORC.ParentUniversalServiceIdentifier

        }

        private static void _ParsePatient(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            throw new NotImplementedException();
        }

        private static void _ParseNTE(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            throw new NotImplementedException();
        }

        private static void _ParseMSH(XmlDocument temp, ref ORM_O01 ORMMsg)
        {
            throw new NotImplementedException();
        }

        
    }
}
