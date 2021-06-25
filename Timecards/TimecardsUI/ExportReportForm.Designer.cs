
namespace TimecardsUI
{
    partial class ExportReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportReportForm));
            this.FileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.FileTypeLabel = new System.Windows.Forms.Label();
            this.CancelExportButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.FileDialogButton = new System.Windows.Forms.Button();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.FileSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // FileTypeComboBox
            // 
            this.FileTypeComboBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileTypeComboBox.FormattingEnabled = true;
            this.FileTypeComboBox.Location = new System.Drawing.Point(94, 42);
            this.FileTypeComboBox.Name = "FileTypeComboBox";
            this.FileTypeComboBox.Size = new System.Drawing.Size(208, 23);
            this.FileTypeComboBox.TabIndex = 17;
            // 
            // FileTypeLabel
            // 
            this.FileTypeLabel.AutoSize = true;
            this.FileTypeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileTypeLabel.Location = new System.Drawing.Point(15, 48);
            this.FileTypeLabel.Name = "FileTypeLabel";
            this.FileTypeLabel.Size = new System.Drawing.Size(51, 15);
            this.FileTypeLabel.TabIndex = 16;
            this.FileTypeLabel.Text = "File type";
            // 
            // CancelExportButton
            // 
            this.CancelExportButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelExportButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelExportButton.Location = new System.Drawing.Point(362, 80);
            this.CancelExportButton.Name = "CancelExportButton";
            this.CancelExportButton.Size = new System.Drawing.Size(101, 31);
            this.CancelExportButton.TabIndex = 25;
            this.CancelExportButton.Text = "Cancel";
            this.CancelExportButton.UseVisualStyleBackColor = true;
            this.CancelExportButton.Click += new System.EventHandler(this.CancelExportButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportButton.Location = new System.Drawing.Point(254, 80);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(101, 31);
            this.ExportButton.TabIndex = 24;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // FileDialogButton
            // 
            this.FileDialogButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileDialogButton.Location = new System.Drawing.Point(420, 12);
            this.FileDialogButton.Name = "FileDialogButton";
            this.FileDialogButton.Size = new System.Drawing.Size(41, 23);
            this.FileDialogButton.TabIndex = 15;
            this.FileDialogButton.Text = "...";
            this.FileDialogButton.UseVisualStyleBackColor = true;
            this.FileDialogButton.Click += new System.EventHandler(this.FileDialogButton_Click);
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileNameTextBox.Location = new System.Drawing.Point(94, 12);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(318, 23);
            this.FileNameTextBox.TabIndex = 14;
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileNameLabel.Location = new System.Drawing.Point(15, 16);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(58, 15);
            this.FileNameLabel.TabIndex = 13;
            this.FileNameLabel.Text = "File name";
            // 
            // ExportReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 126);
            this.Controls.Add(this.FileTypeComboBox);
            this.Controls.Add(this.FileTypeLabel);
            this.Controls.Add(this.CancelExportButton);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.FileDialogButton);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.FileNameLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExportReportForm";
            this.Text = "Export Report";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox FileTypeComboBox;
        private System.Windows.Forms.Label FileTypeLabel;
        private System.Windows.Forms.Button CancelExportButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button FileDialogButton;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.SaveFileDialog FileSaveDialog;
    }
}