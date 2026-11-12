using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Model.Common;


namespace UserManagerService.Business.ICDCode
{
    /// <summary>
    /// Author Mansoor
    /// Date 21 Jan 2011
    /// </summary>
    public interface IICDCodeManager
    {
        List<ICDCodeSearchDTO> searchICDCODEByCode(string icdCode);
    }
}
