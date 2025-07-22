using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer
{
    public partial class frmTables : Form
    {
        clsTables Tables;
        string TableName;
        public frmTables(clsTables Tables)
        {
            InitializeComponent();
            this.Tables = Tables;
        }

        private void onRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if(rb.Checked)
            {
                TableName = rb.Text;
            }
        }

        private void frmTables_Load(object sender, EventArgs e)
        {
            DataTable dtTables = Tables.ListTables();

            foreach (DataRow row in dtTables.Rows) 
            {
                RadioButton rb = new RadioButton();
                rb.Text = row["TABLE_NAME"].ToString();
                rb.ForeColor = Color.White;
                rb.AutoSize = true;
                rb.CheckedChanged += onRadioButtonCheckedChanged;
                flowLayoutPanel1.Controls.Add(rb);
            }
        }

        public Action<clsTable> onTableSelected;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TableName))
                return;

            onTableSelected?.Invoke(Tables.GetTable(TableName));

        }
    }
}
