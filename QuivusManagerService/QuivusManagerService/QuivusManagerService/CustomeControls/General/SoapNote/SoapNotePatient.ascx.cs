using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuivusManagerService.CustomeControls.General.SoapNote
{
    public partial class SoapNotePatient : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindData()
        {
            lblPatientName.Text = PatientName;
            lblDOB.Text = DOB;
            lblDOService.Text = DateOfService;
            lblProviderName.Text = ProviderName;
        }


        public string PatientName
        {
            get
            {
                return _PatientName;
            }
            set
            {
                _PatientName = value;
            }
        }

        public string DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
            }
        }

        public string ProviderName
        {
            get
            {
                return _ProviderName;
            }
            set
            {
                _ProviderName = value;
            }
        }

        public string DateOfService
        {
            get
            {
                return _DOS;
            }
            set
            {
                _DOS = value;
            }
        }

        private string _ProviderName;
        private string _DOS;
        private string _DOB;
        private string _PatientName;
    }
}