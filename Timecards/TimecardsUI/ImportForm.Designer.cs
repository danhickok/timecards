namespace TimecardsUI
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            this.ImportButton = new System.Windows.Forms.Button();
            this.CancelImportButton = new System.Windows.Forms.Button();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.FileTypeLabel = new System.Windows.Forms.Label();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.FileDialogButton = new System.Windows.Forms.Button();
            this.FileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.EraseExistingDataCheckBox = new System.Windows.Forms.CheckBox();
            this.ImportProgressBar = new System.Windows.Forms.ProgressBar();
            this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportButton.Location = new System.Drawing.Point(215, 171);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(87, 27);
            this.ImportButton.TabIndex = 0;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // CancelImportButton
            // 
            this.CancelImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelImportButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelImportButton.Location = new System.Drawing.Point(309, 171);
            this.CancelImportButton.Name = "CancelImportButton";
            this.CancelImportButton.Size = new System.Drawing.Size(87, 27);
            this.CancelImportButton.TabIndex = 1;
            this.CancelImportButton.Text = "Cancel";
            this.CancelImportButton.UseVisualStyleBackColor = true;
            this.CancelImportButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(15, 18);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(58, 15);
            this.FileNameLabel.TabIndex = 2;
            this.FileNameLabel.Text = "File name";
            // 
            // FileTypeLabel
            // 
            this.FileTypeLabel.AutoSize = true;
            this.FileTypeLabel.Location = new System.Drawing.Point(15, 47);
            this.FileTypeLabel.Name = "FileTypeLabel";
            this.FileTypeLabel.Size = new System.Drawing.Size(51, 15);
            this.FileTypeLabel.TabIndex = 3;
            this.FileTypeLabel.Text = "File type";
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Location = new System.Drawing.Point(83, 14);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(271, 23);
            this.FileNameTextBox.TabIndex = 4;
            // 
            // FileDialogButton
            // 
            this.FileDialogButton.Location = new System.Drawing.Point(361, 14);
            this.FileDialogButton.Name = "FileDialogButton";
            this.FileDialogButton.Size = new System.Drawing.Size(35, 23);
            this.FileDialogButton.TabIndex = 5;
            this.FileDialogButton.Text = "...";
            this.FileDialogButton.UseVisualStyleBackColor = true;
            // 
            // FileTypeComboBox
            // 
            this.FileTypeComboBox.FormattingEnabled = true;
            this.FileTypeComboBox.Items.AddRange(new object[] {
            "Comma-delimited text",
            "JSON",
            "Tab-delimited text",
            "XML"});
            this.FileTypeComboBox.Location = new System.Drawing.Point(83, 43);
            this.FileTypeComboBox.Name = "FileTypeComboBox";
            this.FileTypeComboBox.Size = new System.Drawing.Size(179, 23);
            this.FileTypeComboBox.TabIndex = 6;
            // 
            // EraseExistingDataCheckBox
            // 
            this.EraseExistingDataCheckBox.AutoSize = true;
            this.EraseExistingDataCheckBox.Location = new System.Drawing.Point(83, 85);
            this.EraseExistingDataCheckBox.Name = "EraseExistingDataCheckBox";
            this.EraseExistingDataCheckBox.Size = new System.Drawing.Size(123, 19);
            this.EraseExistingDataCheckBox.TabIndex = 7;
            this.EraseExistingDataCheckBox.Text = "Erase existing data";
            this.EraseExistingDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImportProgressBar
            // 
            this.ImportProgressBar.Location = new System.Drawing.Point(18, 131);
            this.ImportProgressBar.Name = "ImportProgressBar";
            this.ImportProgressBar.Size = new System.Drawing.Size(378, 27);
            this.ImportProgressBar.TabIndex = 8;
            this.ImportProgressBar.Visible = false;
            // 
            // FileOpenDialog
            // 
            this.FileOpenDialog.FileName = "openFileDialog1";
            // 
            // ImportForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(413, 211);
            this.Controls.Add(this.ImportProgressBar);
            this.Controls.Add(this.EraseExistingDataCheckBox);
            this.Controls.Add(this.FileTypeComboBox);
            this.Controls.Add(this.FileDialogButton);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.FileTypeLabel);
            this.Controls.Add(this.FileNameLabel);
            this.Controls.Add(this.CancelImportButton);
            this.Controls.Add(this.ImportButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Timecards - Import Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button CancelImportButton;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.Label FileTypeLabel;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.Button FileDialogButton;
        private System.Windows.Forms.ComboBox FileTypeComboBox;
        private System.Windows.Forms.CheckBox EraseExistingDataCheckBox;
        private System.Windows.Forms.ProgressBar ImportProgressBar;
        private System.Windows.Forms.OpenFileDialog FileOpenDialog;
    }
}