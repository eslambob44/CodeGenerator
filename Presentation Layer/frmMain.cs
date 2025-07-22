using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

    
        }


        void onTablesFind(clsTables Tables)
        {
            frm.Close();
            frmTables TablesForm = new frmTables(Tables);
            TablesForm.onTableSelected += onTableSelected;
            frm = TablesForm;
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }

        void onTableSelected(clsTable Table)
        {
            frm.Close();

            frmTable TableForm = new frmTable(Table);
            TableForm.onGenerateCodeComplete += onGenerateCodeComplete;
            frm = TableForm;
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
            
        }

        void onGenerateCodeComplete()
        {
            openConnectionStringFormToolStripMenuItem.Visible = true;
        }


        Form frm = new Form();

        void ShowConnectionStringForm()
        {
            openConnectionStringFormToolStripMenuItem.Visible = false;
            frmConnectionString ConnectionStringForm = new frmConnectionString();
            ConnectionStringForm.onConnectionStringSubmit += onTablesFind;
            frm = ConnectionStringForm;
            frm.MdiParent = this;

            frm.Show();
            frm.BringToFront();
        }

        private void ChangeFormBackColor()
        {
            foreach (Control control in this.Controls)
            {
                // #2
                MdiClient client = control as MdiClient;
                if (!(client == null))
                {
                    // #3
                    client.BackColor = clsMyColors.FormColor;
                    // 4#
                    break;
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ChangeFormBackColor();
            ShowConnectionStringForm();


        }

        private void folderLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) 
            {
                clsRegistry.WriteToRegistry("FolderLocation" , folderBrowserDialog1.SelectedPath);
            }
        }

        private void openConnectionStringFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowConnectionStringForm();
        }

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/eslambob44");
        }

        private void linkedInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/eslam-yasser-143067259/");
        }
    }
}
