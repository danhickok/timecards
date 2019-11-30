namespace TimecardsUI
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            this.FileDialogButton = new System.Windows.Forms.Button();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.FileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.FileTypeLabel = new System.Windows.Forms.Label();
            this.CancelExportButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.FileSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.RadioButtonAllData = new System.Windows.Forms.RadioButton();
            this.RadioButtonDateRange = new System.Windows.Forms.RadioButton();
            this.LabelFromDate = new System.Windows.Forms.Label();
            this.LabelToDate = new System.Windows.Forms.Label();
            this.DateFrom = new System.Windows.Forms.DateTimePicker();
            this.DateThrough = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // FileDialogButton
            // 
            this.FileDialogButton.Location = new System.Drawing.Point(419, 14);
            this.FileDialogButton.Name = "FileDialogButton";
            this.FileDialogButton.Size = new System.Drawing.Size(41, 23);
            this.FileDialogButton.TabIndex = 2;
            this.FileDialogButton.Text = "...";
            this.FileDialogButton.UseVisualStyleBackColor = true;
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Location = new System.Drawing.Point(93, 14);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(318, 23);
            this.FileNameTextBox.TabIndex = 1;
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(14, 18);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(58, 15);
            this.FileNameLabel.TabIndex = 0;
            this.FileNameLabel.Text = "File name";
            // 
            // FileTypeComboBox
            // 
            this.FileTypeComboBox.FormattingEnabled = true;
            this.FileTypeComboBox.Items.AddRange(new object[] {
            "Comma-delimited text",
            "JSON",
            "Tab-delimited text",
            "XML"});
            this.FileTypeComboBox.Location = new System.Drawing.Point(93, 44);
            this.FileTypeComboBox.Name = "FileTypeComboBox";
            this.FileTypeComboBox.Size = new System.Drawing.Size(208, 23);
            this.FileTypeComboBox.TabIndex = 4;
            // 
            // FileTypeLabel
            // 
            this.FileTypeLabel.AutoSize = true;
            this.FileTypeLabel.Location = new System.Drawing.Point(14, 50);
            this.FileTypeLabel.Name = "FileTypeLabel";
            this.FileTypeLabel.Size = new System.Drawing.Size(51, 15);
            this.FileTypeLabel.TabIndex = 3;
            this.FileTypeLabel.Text = "File type";
            // 
            // CancelExportButton
            // 
            this.CancelExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelExportButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelExportButton.Location = new System.Drawing.Point(358, 227);
            this.CancelExportButton.Name = "CancelExportButton";
            this.CancelExportButton.Size = new System.Drawing.Size(101, 31);
            this.CancelExportButton.TabIndex = 12;
            this.CancelExportButton.Text = "Cancel";
            this.CancelExportButton.UseVisualStyleBackColor = true;
            this.CancelExportButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportButton.Location = new System.Drawing.Point(250, 227);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(101, 31);
            this.ExportButton.TabIndex = 11;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // RadioButtonAllData
            // 
            this.RadioButtonAllData.AutoSize = true;
            this.RadioButtonAllData.Checked = true;
            this.RadioButtonAllData.Location = new System.Drawing.Point(17, 90);
            this.RadioButtonAllData.Name = "RadioButtonAllData";
            this.RadioButtonAllData.Size = new System.Drawing.Size(100, 19);
            this.RadioButtonAllData.TabIndex = 5;
            this.RadioButtonAllData.TabStop = true;
            this.RadioButtonAllData.Text = "Export all data";
            this.RadioButtonAllData.UseVisualStyleBackColor = true;
            // 
            // RadioButtonDateRange
            // 
            this.RadioButtonDateRange.AutoSize = true;
            this.RadioButtonDateRange.Location = new System.Drawing.Point(17, 115);
            this.RadioButtonDateRange.Name = "RadioButtonDateRange";
            this.RadioButtonDateRange.Size = new System.Drawing.Size(134, 19);
            this.RadioButtonDateRange.TabIndex = 6;
            this.RadioButtonDateRange.Text = "Export data for dates";
            this.RadioButtonDateRange.UseVisualStyleBackColor = true;
            // 
            // LabelFromDate
            // 
            this.LabelFromDate.AutoSize = true;
            this.LabelFromDate.Enabled = false;
            this.LabelFromDate.Location = new System.Drawing.Point(90, 153);
            this.LabelFromDate.Name = "LabelFromDate";
            this.LabelFromDate.Size = new System.Drawing.Size(35, 15);
            this.LabelFromDate.TabIndex = 7;
            this.LabelFromDate.Text = "From";
            // 
            // LabelToDate
            // 
            this.LabelToDate.AutoSize = true;
            this.LabelToDate.Enabled = false;
            this.LabelToDate.Location = new System.Drawing.Point(90, 183);
            this.LabelToDate.Name = "LabelToDate";
            this.LabelToDate.Size = new System.Drawing.Size(52, 15);
            this.LabelToDate.TabIndex = 9;
            this.LabelToDate.Text = "Through";
            // 
            // DateFrom
            // 
            this.DateFrom.Enabled = false;
            this.DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateFrom.Location = new System.Drawing.Point(170, 147);
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Size = new System.Drawing.Size(109, 23);
            this.DateFrom.TabIndex = 8;
            // 
            // DateThrough
            // 
            this.DateThrough.Enabled = false;
            this.DateThrough.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateThrough.Location = new System.Drawing.Point(170, 177);
            this.DateThrough.Name = "DateThrough";
            this.DateThrough.Size = new System.Drawing.Size(109, 23);
            this.DateThrough.TabIndex = 10;
            // 
            // ExportForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(474, 272);
            this.Controls.Add(this.DateThrough);
            this.Controls.Add(this.DateFrom);
            this.Controls.Add(this.LabelToDate);
            this.Controls.Add(this.LabelFromDate);
            this.Controls.Add(this.RadioButtonDateRange);
            this.Controls.Add(this.RadioButtonAllData);
            this.Controls.Add(this.FileTypeComboBox);
            this.Controls.Add(this.FileTypeLabel);
            this.Controls.Add(this.CancelExportButton);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.FileDialogButton);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.FileNameLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Timecards - Export Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FileDialogButton;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.ComboBox FileTypeComboBox;
        private System.Windows.Forms.Label FileTypeLabel;
        private System.Windows.Forms.Button CancelExportButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.SaveFileDialog FileSaveDialog;
        private System.Windows.Forms.RadioButton RadioButtonAllData;
        private System.Windows.Forms.RadioButton RadioButtonDateRange;
        private System.Windows.Forms.Label LabelFromDate;
        private System.Windows.Forms.Label LabelToDate;
        private System.Windows.Forms.DateTimePicker DateFrom;
        private System.Windows.Forms.DateTimePicker DateThrough;
    }
}