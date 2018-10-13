namespace TimecardsUI
{
    partial class frmConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfiguration));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCodeMask = new System.Windows.Forms.Label();
            this.txtCodeFormat = new System.Windows.Forms.TextBox();
            this.lblRoundTime = new System.Windows.Forms.Label();
            this.cboRoundTime = new System.Windows.Forms.ComboBox();
            this.lblDefaultDescriptions = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(269, 286);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(350, 286);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblCodeMask
            // 
            this.lblCodeMask.AutoSize = true;
            this.lblCodeMask.Location = new System.Drawing.Point(12, 21);
            this.lblCodeMask.Name = "lblCodeMask";
            this.lblCodeMask.Size = new System.Drawing.Size(64, 13);
            this.lblCodeMask.TabIndex = 2;
            this.lblCodeMask.Text = "Code format";
            // 
            // txtCodeFormat
            // 
            this.txtCodeFormat.Location = new System.Drawing.Point(130, 17);
            this.txtCodeFormat.Name = "txtCodeFormat";
            this.txtCodeFormat.Size = new System.Drawing.Size(100, 20);
            this.txtCodeFormat.TabIndex = 3;
            // 
            // lblRoundTime
            // 
            this.lblRoundTime.AutoSize = true;
            this.lblRoundTime.Location = new System.Drawing.Point(12, 47);
            this.lblRoundTime.Name = "lblRoundTime";
            this.lblRoundTime.Size = new System.Drawing.Size(109, 13);
            this.lblRoundTime.TabIndex = 4;
            this.lblRoundTime.Text = "Round current time to";
            // 
            // cboRoundTime
            // 
            this.cboRoundTime.FormattingEnabled = true;
            this.cboRoundTime.Items.AddRange(new object[] {
            "nearest minute",
            "nearest five minutes",
            "nearest 15 minutes",
            "nearest half hour",
            "nearest hour"});
            this.cboRoundTime.Location = new System.Drawing.Point(130, 43);
            this.cboRoundTime.Name = "cboRoundTime";
            this.cboRoundTime.Size = new System.Drawing.Size(186, 21);
            this.cboRoundTime.TabIndex = 5;
            // 
            // lblDefaultDescriptions
            // 
            this.lblDefaultDescriptions.AutoSize = true;
            this.lblDefaultDescriptions.Location = new System.Drawing.Point(12, 94);
            this.lblDefaultDescriptions.Name = "lblDefaultDescriptions";
            this.lblDefaultDescriptions.Size = new System.Drawing.Size(186, 13);
            this.lblDefaultDescriptions.TabIndex = 6;
            this.lblDefaultDescriptions.Text = "Default descriptions for specific codes";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCode,
            this.colDescription});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(12, 110);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(412, 161);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // colCode
            // 
            this.colCode.Text = "Code";
            this.colCode.Width = 75;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 310;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(376, 85);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(401, 85);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "-";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // frmConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(436, 321);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblDefaultDescriptions);
            this.Controls.Add(this.cboRoundTime);
            this.Controls.Add(this.lblRoundTime);
            this.Controls.Add(this.txtCodeFormat);
            this.Controls.Add(this.lblCodeMask);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConfiguration";
            this.Text = "Timecards - Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCodeMask;
        private System.Windows.Forms.TextBox txtCodeFormat;
        private System.Windows.Forms.Label lblRoundTime;
        private System.Windows.Forms.ComboBox cboRoundTime;
        private System.Windows.Forms.Label lblDefaultDescriptions;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
    }
}