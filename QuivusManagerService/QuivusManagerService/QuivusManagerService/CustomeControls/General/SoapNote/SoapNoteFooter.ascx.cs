using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuivusManagerService.CustomeControls.General.SoapNote
{
    public partial class SoapNoteFooter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindData()
        {
            lblProviderName.Text = ProviderName;
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

        private string _ProviderName;
    }
}