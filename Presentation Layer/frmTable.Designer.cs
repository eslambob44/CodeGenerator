namespace Presentation_Layer
{
    partial class frmTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.myFormPanel1 = new Presentation_Layer.MyFormPanel();
            this.myFormPanel2 = new Presentation_Layer.MyFormPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.myTextBox1 = new Presentation_Layer.MyTextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkInsert = new Guna.UI2.WinForms.Guna2CheckBox();
            this.chkUpdate = new Guna.UI2.WinForms.Guna2CheckBox();
            this.chkDelete = new Guna.UI2.WinForms.Guna2CheckBox();
            this.chkFind = new Guna.UI2.WinForms.Guna2CheckBox();
            this.chkList = new Guna.UI2.WinForms.Guna2CheckBox();
            this.chkIsExists = new Guna.UI2.WinForms.Guna2CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSubmit = new Presentation_Layer.MyButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.lblOptions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myFormPanel1
            // 
            this.myFormPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.myFormPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myFormPanel1.Location = new System.Drawing.Point(0, 0);
            this.myFormPanel1.Name = "myFormPanel1";
            this.myFormPanel1.Size = new System.Drawing.Size(519, 472);
            this.myFormPanel1.TabIndex = 0;
            this.myFormPanel1.Text = "myFormPanel1";
            // 
            // myFormPanel2
            // 
            this.myFormPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.myFormPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.myFormPanel2.Location = new System.Drawing.Point(0, 536);
            this.myFormPanel2.Name = "myFormPanel2";
            this.myFormPanel2.Size = new System.Drawing.Size(519, 102);
            this.myFormPanel2.TabIndex = 1;
            this.myFormPanel2.Text = "myFormPanel2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.label1.Font = new System.Drawing.Font("Poppins SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Code Generator Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.label2.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(34, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Object Name:";
            // 
            // myTextBox1
            // 
            this.myTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.myTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.myTextBox1.Location = new System.Drawing.Point(38, 93);
            this.myTextBox1.Name = "myTextBox1";
            this.myTextBox1.Size = new System.Drawing.Size(445, 27);
            this.myTextBox1.TabIndex = 0;
            this.myTextBox1.Text = "Enter Object Name:";
            this.myTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.myTextBox1_KeyPress);
            this.myTextBox1.Validating += new System.ComponentModel.CancelEventHandler(this.myTextBox1_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.flowLayoutPanel1.Controls.Add(this.chkInsert);
            this.flowLayoutPanel1.Controls.Add(this.chkUpdate);
            this.flowLayoutPanel1.Controls.Add(this.chkDelete);
            this.flowLayoutPanel1.Controls.Add(this.chkFind);
            this.flowLayoutPanel1.Controls.Add(this.chkList);
            this.flowLayoutPanel1.Controls.Add(this.chkIsExists);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(38, 174);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(445, 208);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // chkInsert
            // 
            this.chkInsert.AutoSize = true;
            this.chkInsert.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkInsert.CheckedState.BorderRadius = 0;
            this.chkInsert.CheckedState.BorderThickness = 0;
            this.chkInsert.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkInsert.ForeColor = System.Drawing.Color.White;
            this.chkInsert.Location = new System.Drawing.Point(3, 3);
            this.chkInsert.Name = "chkInsert";
            this.chkInsert.Size = new System.Drawing.Size(65, 27);
            this.chkInsert.TabIndex = 0;
            this.chkInsert.Text = "Insert";
            this.chkInsert.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkInsert.UncheckedState.BorderRadius = 0;
            this.chkInsert.UncheckedState.BorderThickness = 0;
            this.chkInsert.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkInsert.CheckedChanged += new System.EventHandler(this.chkInsert_CheckedChanged);
            // 
            // chkUpdate
            // 
            this.chkUpdate.AutoSize = true;
            this.chkUpdate.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkUpdate.CheckedState.BorderRadius = 0;
            this.chkUpdate.CheckedState.BorderThickness = 0;
            this.chkUpdate.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkUpdate.ForeColor = System.Drawing.Color.White;
            this.chkUpdate.Location = new System.Drawing.Point(3, 36);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(78, 27);
            this.chkUpdate.TabIndex = 1;
            this.chkUpdate.Text = "Update";
            this.chkUpdate.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkUpdate.UncheckedState.BorderRadius = 0;
            this.chkUpdate.UncheckedState.BorderThickness = 0;
            this.chkUpdate.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkUpdate.CheckedChanged += new System.EventHandler(this.chkInsert_CheckedChanged);
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkDelete.CheckedState.BorderRadius = 0;
            this.chkDelete.CheckedState.BorderThickness = 0;
            this.chkDelete.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkDelete.ForeColor = System.Drawing.Color.White;
            this.chkDelete.Location = new System.Drawing.Point(3, 69);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(70, 27);
            this.chkDelete.TabIndex = 2;
            this.chkDelete.Text = "Delete";
            this.chkDelete.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkDelete.UncheckedState.BorderRadius = 0;
            this.chkDelete.UncheckedState.BorderThickness = 0;
            this.chkDelete.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkDelete.CheckedChanged += new System.EventHandler(this.chkInsert_CheckedChanged);
            // 
            // chkFind
            // 
            this.chkFind.AutoSize = true;
            this.chkFind.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkFind.CheckedState.BorderRadius = 0;
            this.chkFind.CheckedState.BorderThickness = 0;
            this.chkFind.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkFind.ForeColor = System.Drawing.Color.White;
            this.chkFind.Location = new System.Drawing.Point(3, 102);
            this.chkFind.Name = "chkFind";
            this.chkFind.Size = new System.Drawing.Size(56, 27);
            this.chkFind.TabIndex = 3;
            this.chkFind.Text = "Find";
            this.chkFind.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkFind.UncheckedState.BorderRadius = 0;
            this.chkFind.UncheckedState.BorderThickness = 0;
            this.chkFind.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkFind.CheckedChanged += new System.EventHandler(this.chkInsert_CheckedChanged);
            // 
            // chkList
            // 
            this.chkList.AutoSize = true;
            this.chkList.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkList.CheckedState.BorderRadius = 0;
            this.chkList.CheckedState.BorderThickness = 0;
            this.chkList.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkList.ForeColor = System.Drawing.Color.White;
            this.chkList.Location = new System.Drawing.Point(3, 135);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(50, 27);
            this.chkList.TabIndex = 4;
            this.chkList.Text = "List";
            this.chkList.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkList.UncheckedState.BorderRadius = 0;
            this.chkList.UncheckedState.BorderThickness = 0;
            this.chkList.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkList.CheckedChanged += new System.EventHandler(this.chkInsert_CheckedChanged);
            // 
            // chkIsExists
            // 
            this.chkIsExists.AutoSize = true;
            this.chkIsExists.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkIsExists.CheckedState.BorderRadius = 0;
            this.chkIsExists.CheckedState.BorderThickness = 0;
            this.chkIsExists.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkIsExists.ForeColor = System.Drawing.Color.White;
            this.chkIsExists.Location = new System.Drawing.Point(3, 168);
            this.chkIsExists.Name = "chkIsExists";
            this.chkIsExists.Size = new System.Drawing.Size(77, 27);
            this.chkIsExists.TabIndex = 5;
            this.chkIsExists.Text = "Is Exists";
            this.chkIsExists.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkIsExists.UncheckedState.BorderRadius = 0;
            this.chkIsExists.UncheckedState.BorderThickness = 0;
            this.chkIsExists.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.chkIsExists.CheckedChanged += new System.EventHandler(this.chkInsert_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.label3.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(33, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Code Generator Options";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(105)))), ((int)(((byte)(129)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(37, 400);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(446, 29);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.label4.Font = new System.Drawing.Font("Poppins", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(9, 485);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 34);
            this.label4.TabIndex = 8;
            this.label4.Text = "Summary";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.lblTableName.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableName.ForeColor = System.Drawing.Color.White;
            this.lblTableName.Location = new System.Drawing.Point(4, 552);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(115, 23);
            this.lblTableName.TabIndex = 9;
            this.lblTableName.Text = "Selected Table: ";
            // 
            // lblOptions
            // 
            this.lblOptions.AutoSize = true;
            this.lblOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.lblOptions.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptions.ForeColor = System.Drawing.Color.White;
            this.lblOptions.Location = new System.Drawing.Point(12, 584);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(65, 23);
            this.lblOptions.TabIndex = 10;
            this.lblOptions.Text = "Options:";
            // 
            // frmTable
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(519, 638);
            this.Controls.Add(this.lblOptions);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.myTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.myFormPanel2);
            this.Controls.Add(this.myFormPanel1);
            this.Font = new System.Drawing.Font("Poppins", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTable";
            this.Load += new System.EventHandler(this.frmTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyFormPanel myFormPanel1;
        private MyFormPanel myFormPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MyTextBox myTextBox1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2CheckBox chkInsert;
        private System.Windows.Forms.Label label4;
        private MyButton btnSubmit;
        private Guna.UI2.WinForms.Guna2CheckBox chkUpdate;
        private Guna.UI2.WinForms.Guna2CheckBox chkDelete;
        private Guna.UI2.WinForms.Guna2CheckBox chkFind;
        private Guna.UI2.WinForms.Guna2CheckBox chkList;
        private Guna.UI2.WinForms.Guna2CheckBox chkIsExists;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.Label lblTableName;
    }
}