using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuivusManagerService.CustomeControls.General.SoapNote
{
    public partial class SoapNoteGridView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            base.RenderControl(writer);
        }

        public override void DataBind()
        {
            this.GridView1.DataSource = Data;
            this.Label1.Text = Title;
            this.GridView1.DataBind();
            base.DataBind();
        }

        public List<SoapNoteCatagoryDTO> Data
        {
            get
            {
                return _Data;
            }
            set
            {
                _Data = value;
            }
        }

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        private List<SoapNoteCatagoryDTO> _Data;
        private string _Title;
    }
}