using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EprescriptionService.Common.DTOs
{
    [DataContract]
    public class PatientInfoDTO
    {
        /* 
         * <Deleted> {‘y’ if patient was deleted} </Deleted> (optional)
<RcopiaID> {Rcopia patient ID} </RcopiaID> (optional)
<ExternalID> {External System patient ID} </ExternalID> (optional)
<RcopiaPracticeID> {Rcopia numeric practice ID} </RcopiaPracticeID> (optional)
<FirstName> {First name} </FirstName>
<MiddleName> {Middle name or initial} </MiddleName> (optional)
<LastName> {last name} </LastName>
<Suffix>Sr.</Suffix>
<DOB> {mm/dd/yyyy} <DOB>
<Sex> {sex, either M, F, or U)</Sex> (optional but required to get formulary information)
<Weight> {weight in pounds or kilograms} </Weight> (optional)
<WeightUnit> {‘lb’ or ‘kg’} </WeightUnit> (optional)
©2010 DrFirst, Inc.
Proprietary and confidential, for use by contracted DrFirst partners only
17 Appendix B: Rcopia Engine API Template for Uploading Data | Two Way Portal
Integration
<HomePhone> {home phone} </HomePhone> (optional)
<WorkPhone> {work phone} </WorkPhone> (optional)
<OtherPhone> {other phone} </OtherPhone> (optional)
<Address1> {address line1} </Address1> (optional)
<Address2> {address line 2} </Address2> (optional)
<City> {city} </City> (O)
<State> {two-letter state code} </State> (optional)
<Zip> {zip} </Zip> (optional but required to get formulary information)
<SSN> {ssn} </SSN> (optional)
<Pregnant> {y or n} </ Pregnant > (optional)
<Breastfeeding> {y or n} </ Breastfeeding > (optional)
<DefaultPharmacy> (optional)
<RcopiaID> {Rcopia default pharmacy ID} </RcopiaID>
</DefaultPharmacy>
<PharmacyCardNumber> {Patient pharmacy card number} </PharmacyCardNumber>
<LastVisitDate> {mm/dd/yyyy} </LastVisitDate> (optional)
<Location> {patient primary location} </Location>
<PrimaryCareProvider> (optional)
<Username> {PCP Rcopia ID} </Username>
<NPI> {Provider’s NPI} </NPI>
</PrimaryCareProvider>
<Guarantor> (optional)
<RcopiaID> {Guarantor Rcopia ID</RcopiaID>
<ExternalID> {Guarantor external system ID</ExternalID> (optional)
<Relationship> {relationship} </Relationship>
</Guarantor>
<InsuranceList> {patient insurance data} </InsuranceList> (optional)
         * */

         [DataMember]
        public String RocpiaID { get; set; }
         [DataMember]
        public String ExternalID { get; set; }
         [DataMember]
        public String RcopiaPracticeID { get; set; }
        
        [DataMember]
        public String FirstName { get; set; }
         [DataMember]
        public String LastName { get; set; }
        /// <summary>
        /// Either M or F or U
        /// </summary>
        [DataMember]
        public String Sex { get; set; }
         [DataMember]
        public String Sufffix { get; set; }
         [DataMember]
        public String DOB { get; set; }
         [DataMember]
        public String Address { get; set; }
         [DataMember]
        public String State { get; set; }
         [DataMember]
        public String City { get; set; }

         [DataMember]
         public String Zip { get; set; }

         [DataMember]
         public String PhoneNumber { get; set; }
        [DataMember]
        public String WorkPhoneNumber { get; set; }
        [DataMember]
         public String DefaultPharmacy { get; set; }

        [DataMember]
        public String NPI { get; set; }

        [DataMember]
        public String PatientUserName { get; set; }

        [DataMember]
        public String SSN { get; set; }

        [DataMember]
        public String Location { get; set; }
     

    }
}
