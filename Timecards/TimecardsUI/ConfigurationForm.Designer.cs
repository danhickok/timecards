namespace TimecardsUI
{
    partial class ConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCodeMask = new System.Windows.Forms.Label();
            this.txtCodeMask = new System.Windows.Forms.TextBox();
            this.lblRoundTime = new System.Windows.Forms.Label();
            this.cboRoundTime = new System.Windows.Forms.ComboBox();
            this.lblDefaultDescriptions = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtTimeMask = new System.Windows.Forms.TextBox();
            this.lblTimeMask = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(314, 366);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 27);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(408, 366);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCodeMask
            // 
            this.lblCodeMask.AutoSize = true;
            this.lblCodeMask.Location = new System.Drawing.Point(14, 24);
            this.lblCodeMask.Name = "lblCodeMask";
            this.lblCodeMask.Size = new System.Drawing.Size(93, 20);
            this.lblCodeMask.TabIndex = 0;
            this.lblCodeMask.Text = "Code format";
            // 
            // txtCodeMask
            // 
            this.txtCodeMask.Location = new System.Drawing.Point(152, 20);
            this.txtCodeMask.Name = "txtCodeMask";
            this.txtCodeMask.Size = new System.Drawing.Size(116, 27);
            this.txtCodeMask.TabIndex = 1;
            // 
            // lblRoundTime
            // 
            this.lblRoundTime.AutoSize = true;
            this.lblRoundTime.Location = new System.Drawing.Point(14, 82);
            this.lblRoundTime.Name = "lblRoundTime";
            this.lblRoundTime.Size = new System.Drawing.Size(154, 20);
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
            this.cboRoundTime.Location = new System.Drawing.Point(152, 78);
            this.cboRoundTime.Name = "cboRoundTime";
            this.cboRoundTime.Size = new System.Drawing.Size(216, 28);
            this.cboRoundTime.TabIndex = 5;
            // 
            // lblDefaultDescriptions
            // 
            this.lblDefaultDescriptions.AutoSize = true;
            this.lblDefaultDescriptions.Location = new System.Drawing.Point(14, 141);
            this.lblDefaultDescriptions.Name = "lblDefaultDescriptions";
            this.lblDefaultDescriptions.Size = new System.Drawing.Size(262, 20);
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
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(14, 162);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(480, 186);
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
            this.colDescription.Width = 367;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(439, 131);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 27);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(468, 131);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(27, 27);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "-";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // txtTimeMask
            // 
            this.txtTimeMask.Location = new System.Drawing.Point(152, 49);
            this.txtTimeMask.Name = "txtTimeMask";
            this.txtTimeMask.Size = new System.Drawing.Size(116, 27);
            this.txtTimeMask.TabIndex = 3;
            // 
            // lblTimeMask
            // 
            this.lblTimeMask.AutoSize = true;
            this.lblTimeMask.Location = new System.Drawing.Point(14, 53);
            this.lblTimeMask.Name = "lblTimeMask";
            this.lblTimeMask.Size = new System.Drawing.Size(91, 20);
            this.lblTimeMask.TabIndex = 2;
            this.lblTimeMask.Text = "Time format";
            // 
            // frmConfiguration
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(509, 406);
            this.Controls.Add(this.txtTimeMask);
            this.Controls.Add(this.lblTimeMask);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblDefaultDescriptions);
            this.Controls.Add(this.cboRoundTime);
            this.Controls.Add(this.lblRoundTime);
            this.Controls.Add(this.txtCodeMask);
            this.Controls.Add(this.lblCodeMask);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Timecards - Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCodeMask;
        private System.Windows.Forms.TextBox txtCodeMask;
        private System.Windows.Forms.Label lblRoundTime;
        private System.Windows.Forms.ComboBox cboRoundTime;
        private System.Windows.Forms.Label lblDefaultDescriptions;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtTimeMask;
        private System.Windows.Forms.Label lblTimeMask;
    }
}