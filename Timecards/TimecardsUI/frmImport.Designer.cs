namespace TimecardsUI
{
    partial class frmImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport));
            this.btnImport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblFileType = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnFileDialog = new System.Windows.Forms.Button();
            this.cboFileType = new System.Windows.Forms.ComboBox();
            this.chkEraseExistingData = new System.Windows.Forms.CheckBox();
            this.prgImport = new System.Windows.Forms.ProgressBar();
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(215, 171);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(87, 27);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(309, 171);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(15, 18);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(58, 15);
            this.lblFileName.TabIndex = 2;
            this.lblFileName.Text = "File name";
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Location = new System.Drawing.Point(15, 47);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(51, 15);
            this.lblFileType.TabIndex = 3;
            this.lblFileType.Text = "File type";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(83, 14);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(271, 23);
            this.txtFileName.TabIndex = 4;
            // 
            // btnFileDialog
            // 
            this.btnFileDialog.Location = new System.Drawing.Point(361, 14);
            this.btnFileDialog.Name = "btnFileDialog";
            this.btnFileDialog.Size = new System.Drawing.Size(35, 23);
            this.btnFileDialog.TabIndex = 5;
            this.btnFileDialog.Text = "...";
            this.btnFileDialog.UseVisualStyleBackColor = true;
            // 
            // cboFileType
            // 
            this.cboFileType.FormattingEnabled = true;
            this.cboFileType.Items.AddRange(new object[] {
            "Comma-delimited text",
            "JSON",
            "Tab-delimited text",
            "XML"});
            this.cboFileType.Location = new System.Drawing.Point(83, 43);
            this.cboFileType.Name = "cboFileType";
            this.cboFileType.Size = new System.Drawing.Size(179, 23);
            this.cboFileType.TabIndex = 6;
            // 
            // chkEraseExistingData
            // 
            this.chkEraseExistingData.AutoSize = true;
            this.chkEraseExistingData.Location = new System.Drawing.Point(83, 85);
            this.chkEraseExistingData.Name = "chkEraseExistingData";
            this.chkEraseExistingData.Size = new System.Drawing.Size(122, 19);
            this.chkEraseExistingData.TabIndex = 7;
            this.chkEraseExistingData.Text = "Erase existing data";
            this.chkEraseExistingData.UseVisualStyleBackColor = true;
            // 
            // prgImport
            // 
            this.prgImport.Location = new System.Drawing.Point(18, 131);
            this.prgImport.Name = "prgImport";
            this.prgImport.Size = new System.Drawing.Size(378, 27);
            this.prgImport.TabIndex = 8;
            this.prgImport.Visible = false;
            // 
            // dlgFileOpen
            // 
            this.dlgFileOpen.FileName = "openFileDialog1";
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(413, 211);
            this.Controls.Add(this.prgImport);
            this.Controls.Add(this.chkEraseExistingData);
            this.Controls.Add(this.cboFileType);
            this.Controls.Add(this.btnFileDialog);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblFileType);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnImport);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Timecards - Import Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFileDialog;
        private System.Windows.Forms.ComboBox cboFileType;
        private System.Windows.Forms.CheckBox chkEraseExistingData;
        private System.Windows.Forms.ProgressBar prgImport;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
    }
}