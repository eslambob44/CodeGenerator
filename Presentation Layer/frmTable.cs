using Business_Layer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer
{
    public partial class frmTable : Form
    {
        clsTable Table;
        public frmTable(clsTable Table)
        {
            InitializeComponent();
            this.Table = Table;
        }

        private void myTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(myTextBox1.Text == "Enter Object Name:")
            {
                myTextBox1.Text = string.Empty;

            }
        }

        private void frmTable_Load(object sender, EventArgs e)
        {
            label2.Text += "("+Table.TableName+")";
            lblTableName.Text = "Selected Table: "+Table.TableName;
            foreach (Guna2CheckBox chk in flowLayoutPanel1.Controls)
            {
                chk.Checked = true;
            }
        }

        private void myTextBox1_Validating(object sender, CancelEventArgs e)
        {
            myTextBox1.Text = myTextBox1.Text.Trim();
            if (myTextBox1.Text == "Enter Object Name:" || string.IsNullOrEmpty(myTextBox1.Text)) 
            {
                e.Cancel = true;
                errorProvider1.SetError(myTextBox1,"Object Name Cannot Be Empty");
            }
            else if(myTextBox1.Text.IndexOf(" ") != -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(myTextBox1, "Object Name cannot have spaces");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(myTextBox1, "");
            }
        }

        private void chkInsert_CheckedChanged(object sender, EventArgs e)
        {
            lblOptions.Text = "Options: ";
            foreach(Guna2CheckBox chk in flowLayoutPanel1.Controls)
            {
                if(chk.Checked)
                {
                    lblOptions.Text += chk.Text + ",";
                }
            }
            if(lblOptions.Text .Length> "Options: ".Length)
            {
                lblOptions.Text = lblOptions.Text.Substring(0,lblOptions.Text.Length - 1);
            }
        }

        public void PrepareBusinessAndDataAcessLayerCodeGenerator(
            List<IBusinessLayerCodeGenerator> businessLayerCodeGenerators
            , List<IDataAccessLayerCodeGenerator> dataAccessLayerCodeGenerators)
        {
            if (chkInsert.Checked)
            {
                businessLayerCodeGenerators.Add(new clsBusinessLayerInsertCodeGenerator());
                dataAccessLayerCodeGenerators.Add(new clsDataAccessLayerInsertCodeGenerator());
            }
            if (chkUpdate.Checked)
            {
                businessLayerCodeGenerators.Add(new clsBusinessLayerUpdateCodeGenerator());
                dataAccessLayerCodeGenerators.Add(new clsDataAccessLayerUpdateCodeGenerator());
            }
            if (chkDelete.Checked)
            {
                businessLayerCodeGenerators.Add(new clsBusinessLayerDeleteCodeGenerator());
                dataAccessLayerCodeGenerators.Add(new clsDataAccessLayerDeleteCodeGenerator());
            }
            if (chkFind.Checked)
            {
                businessLayerCodeGenerators.Add(new clsBusinessLayerFindCodeGenerator());
                dataAccessLayerCodeGenerators.Add(new clsDataAccessLayerFindCodeGenerator());
            }
            if (chkList.Checked)
            {
                businessLayerCodeGenerators.Add(new clsBusinessLayerListCodeGenerator());
                dataAccessLayerCodeGenerators.Add(new clsDataAccessLayerListCodeGenerator());
            }
            if (chkIsExists.Checked)
            {
                businessLayerCodeGenerators.Add(new clsBusinessLayerIsExistsCodeGenerator());
                dataAccessLayerCodeGenerators.Add(new clsDataAccessLayerIsExistsCodeGenerator());
            }
        }


        public Action onGenerateCodeComplete;
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
                { return; }


            List<IBusinessLayerCodeGenerator> businessLayerCodeGenerators = new List<IBusinessLayerCodeGenerator>();
            List<IDataAccessLayerCodeGenerator> dataAccessLayerCodeGenerators = new List<IDataAccessLayerCodeGenerator>();
            PrepareBusinessAndDataAcessLayerCodeGenerator(businessLayerCodeGenerators, dataAccessLayerCodeGenerators);

            ICodeGenerator[] codeGenerator =
            {
                new clsDataAccessCodeGenerator(dataAccessLayerCodeGenerators.ToArray())
                , new clsBusinessLayerCodeGenerator(businessLayerCodeGenerators.ToArray())
            };


            string FolderLocation = clsRegistry.ReadFromRegistry("FolderLocation");
            if(FolderLocation == null || !Directory.Exists(FolderLocation))
            {
                FolderLocation = ConfigurationManager.AppSettings["FolderLocation"];
                if(!Directory.Exists(FolderLocation))
                {
                    Directory.CreateDirectory("C:\\CodeGenerator");
                    FolderLocation = "C:\\CodeGenerator";
                }
            }

             FolderLocation = Table.GenerateCode(codeGenerator, myTextBox1.Text , FolderLocation);

            if (!string.IsNullOrEmpty(FolderLocation))
            {
                MessageBox.Show("Code generated successfully");
                Process.Start("explorer.exe", FolderLocation);
                onGenerateCodeComplete?.Invoke();
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to generate code!" , "Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
            }



        }
    }
}
