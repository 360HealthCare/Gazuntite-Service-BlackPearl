using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EprescriptionService.Common.DTOs
{
    [DataContract]
    public class AllergyInfoDTO
    {

        /*
   
<Deleted>n</Deleted>
<RcopiaID>291617</RcopiaID>
<ExternalID/>
<Patient>
<RcopiaID>3076970</RcopiaID>
<ExternalID>test1234</ExternalID>
<FirstName>Bruce</FirstName>
<LastName>Paltrow</LastName>
</Patient>
<Allergen>
<Name>aspirin</Name>
−
<Drug>
<RcopiaID>210369</RcopiaID>
−
<NDCID>
00536330510,41163042856,41163041690,32363014811,11383014811,63739043301,00904200960,47682080548,10939024033,00363015712,00363015717,10939004101,54569000506,10135015005,00247060630,49348000123,10939009701,11845093001,11822317510
</NDCID>
<FirstDataBankMedID>172480</FirstDataBankMedID>
</Drug>
</Allergen>
<Reaction>nausea: difficulty breathing</Reaction>
<OnsetDate>02/04/2009</OnsetDate>
<LastModifiedBy>qdoctor2</LastModifiedBy>
<LastModifiedDate>04/06/2011</LastModifiedDate>
</Allergy>

</AllergyList>
         * */

        [DataMember]
        public String RcopiaID { get; set; }

        [DataMember]
        public String PatientRcopiaID { get; set; }

        [DataMember]
        public String PatientUserID { get; set; }

        [DataMember]
        public Boolean IsDeleted { get; set; }

        [DataMember]
        public String Reaction { get; set; }

        [DataMember]
        public String AllergyName { get; set; }

        [DataMember]
        public String AllergyDrug { get; set; }

        [DataMember]

        public String NDCIDCode { get; set; }

        [DataMember]
        public String FirstDataBankMedID { get; set; }

        [DataMember]
        public String OnsetDate { get; set; }

        [DataMember]
        public String LastModifiedBy { get; set; }
        [DataMember]
        public String LastModifiedDate { get; set; }


    }
}
