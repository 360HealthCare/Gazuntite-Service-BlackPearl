using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuivusManagerService.CustomeControls.General.SoapNote
{
    public class SoapNoteCatagoryDTO
    {
        public SoapNoteCatagoryDTO()
        {
            Name = "";
            Particulars = "";
            Notes = "";
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public string Particulars
        {
            get
            {
                return _Particulars;
            }
            set
            {
                _Particulars = value;
            }
        }

        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                _Notes = value;
            }
        }

        private string _Name = "";
        private string _Particulars = "";
        private string _Notes = "";
    }
}