using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer
{
    public partial class frmConnectionString : Form
    {
        public frmConnectionString()
        {
            InitializeComponent();
            string ConnectionString = clsRegistry.ReadFromRegistry("ConnectionString");
            if (ConnectionString != null) 
            {
                txtConnectionString.Text = ConnectionString;
            }
        }

        public Action<clsTables> onConnectionStringSubmit;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            clsTables Tables = clsTables.GetObject(txtConnectionString.Text);
            if (Tables != null) 
            {
                clsRegistry.WriteToRegistry("ConnectionString" , txtConnectionString.Text);
                onConnectionStringSubmit?.Invoke(Tables);

            }
            else
            {
                MessageBox.Show("Invalid connection string" , "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
