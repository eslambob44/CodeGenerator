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
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
            clsTables Tables = clsTables.GetObject("Server =.;Database = Inventory;User Id = sa ; Password = Eslamyasse1");
            DataTable dt = Tables.ListTables();
            foreach (DataRow row in dt.Rows)
            {
                RadioButton rb = new RadioButton();
                rb.Text = row[0].ToString();
                flowLayoutPanel1.Controls.Add(rb);
            }
        }
    }
}
