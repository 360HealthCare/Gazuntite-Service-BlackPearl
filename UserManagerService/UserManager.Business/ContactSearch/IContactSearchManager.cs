using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Model.Search;


namespace UserManagerService.Business.Search
{
    public interface IContactSearchManager
    {
        List<ContactSearchDTO> LoadUsersByClinicBranchID(int ClinicBranchId);
    }
}
