using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BulkUploadingUtility.PopUp
{
    public partial class MessagePopup : Form
    {
        public MessagePopup(string message)
        {
            InitializeComponent();
            MessageTextBox.Text = message;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
