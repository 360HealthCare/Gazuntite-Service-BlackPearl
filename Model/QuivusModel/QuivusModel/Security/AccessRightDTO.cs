using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;

namespace QuivusModel.Security
{
    public class AccessRightDTO //: QuivusBaseDTO
    {


        public int UserTypeAppModuleAccessRightIdSeq { get; set; }

        public int UserTypeIdSeq { get; set; }

        public int AppModuleIdSeq { get; set; }

        public string AppModuleName { get; set; }



        public bool IsViewable { get; set; }


        public bool IsEditable { get; set; }



        public int ClinicBranchIdSeq { get; set; }

        public int ParentId { get; set; }

        public bool PasswordRequired { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

    }
}
