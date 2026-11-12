using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using QuivusManagerService.CustomeControls.General.SoapNote;


namespace QuivusManagerService.CustomeControls.General
{
    public partial class SoapNoteView : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindData()
        {
            this.header.BindData();
            this.subjective.DataBind();
            this.objective.DataBind();
            this.assessment.DataBind();
            this.plan.DataBind();
            this.signedBy.BindData();
            this.footer.BindData();
        }

                
    }
}