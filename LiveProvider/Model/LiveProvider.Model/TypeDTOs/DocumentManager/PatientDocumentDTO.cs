using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LiveProvider.Model.TypeDTOs.DocumentManager
{
    /// <summary>
    /// Author:Sahibzada Sami ud din
    /// Date:05/07/2011
    /// Description:Model for patient document
    /// </summary>
   
    [DataContract]
    public class PatientDocumentDTO : LiveProviderBaseDTO
    {
        [DataMember]
        public int DocumentId { get; set; }

        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public Guid DocumentVirtualName { get; set; }

        [DataMember]
        public string DocumentName { get; set; }

        [DataMember]
        public string ServerFilePath { get; set; }

        [DataMember]
        public string ClientFilePath { get; set; }

        [DataMember]
        public int DocumentTypeId { get; set; }

        [DataMember]
        public string DocumentTypeName { get; set; }

        [DataMember]
        public int SectionId { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public int SectionParentId { get; set; }

        [DataMember]
        public int UploadedByUserId { get; set; }

        [DataMember]
        public int UploadedByUserTypeId { get; set; }
        [DataMember]

        public int UploadedByClinicBranchId { get; set; }

        [DataMember]
        public string UploadedDate { get; set; }

        [DataMember]
        public int LastSeenByUserId { get; set; }

        [DataMember]
        public string LastSeenDate { get; set; }

        [DataMember]
        public bool IsDocumentFileUploaded { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public string CreatedAt { get; set; }

        [DataMember]
        public string ModifiedAt { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public string CommentNote { get; set; }

        [DataMember]
        public List<PatientDocumentDTO> ChildDocument { get; set; }

        [DataMember]
        public int CommentId { get; set; }

        [DataMember]
        public List<int> ProviderIds { get; set; }

        [DataMember]
        public string PatientFirstName { get; set; }

        [DataMember]
        public string PatientLastName { get; set; }

        [DataMember]
        public string PatientMiddleName { get; set; }

        [DataMember]
        public string UploadedByUserFirstName { get; set; }

        [DataMember]
        public string UploadedByUserLastName { get; set; }

        [DataMember]
        public string UploadedByUserMiddleName { get; set; }

        [DataMember]
        public string PatientFullName { get; set; }

        [DataMember]
        public string UploadedByUserFullName { get; set; }

        [DataMember]
        public string StrUploadedDate { get; set; }

        [DataMember]
        public string StationName { get; set; }

        [DataMember]
        public string PracticeName { get; set; }

        [DataMember]
        public string QuivusId { get; set; }

        [DataMember]
        public string GenderName { get; set; }
    }
}