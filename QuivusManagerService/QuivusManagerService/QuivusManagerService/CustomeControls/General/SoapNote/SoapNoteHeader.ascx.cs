using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuivusManagerService.CustomeControls.General.SoapNote
{
    public partial class SoapNoteHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindData()
        {
            //lblClinicName.Text = ClinicName;
            lblClinicAddress.Text = ClinicAddress;
            lblClinicPhone.Text = "Tel: " + ClinicPhone;
            lblClinicFax.Text = "Fax: " + ClinicFax;
            logo.Src = LogoAddress;
        }

        public string ClinicPhone
        {
            get
            {
                return _ClinicPhone;
            }
            set
            {
                _ClinicPhone = value;
            }
        }

        public string ClinicFax
        {
            get
            {
                return _ClinicFax;
            }
            set
            {
                _ClinicFax = value;
            }
        }

        //public string ClinicName
        //{
        //    get
        //    {
        //        return _ClinicName;
        //    }
        //    set
        //    {
        //        _ClinicName = value;
        //    }
        //}

        public string ClinicAddress
        {
            get
            {
                return _ClinicAddress;
            }
            set
            {
                _ClinicAddress = value;
            }
        }

        public string LogoAddress
        {
            get
            {
                return _LogoAddress;
            }
            set
            {
                _LogoAddress = value;
            }
        }

        private string _LogoAddress;
        private string _ClinicAddress;
        private string _ClinicPhone;
        private string _ClinicFax;
        //private string _ClinicName;
    }
}