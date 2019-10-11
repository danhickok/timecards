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
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelConfigurationButton = new System.Windows.Forms.Button();
            this.CodeMaskLabel = new System.Windows.Forms.Label();
            this.CodeMaskTextBox = new System.Windows.Forms.TextBox();
            this.RoundTimeLabel = new System.Windows.Forms.Label();
            this.RoundTimeComboBox = new System.Windows.Forms.ComboBox();
            this.DefaultDescriptionsLabel = new System.Windows.Forms.Label();
            this.DefaultCodesListView = new System.Windows.Forms.ListView();
            this.CodeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.TimeMaskTextBox = new System.Windows.Forms.TextBox();
            this.TimeMaskLabel = new System.Windows.Forms.Label();
            this.MidnightTintLabel = new System.Windows.Forms.Label();
            this.MidnightTintPictureBox = new System.Windows.Forms.PictureBox();
            this.MidnightTintColorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.MidnightTintPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(314, 383);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(87, 27);
            this.OKButton.TabIndex = 11;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelConfigurationButton
            // 
            this.CancelConfigurationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelConfigurationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelConfigurationButton.Location = new System.Drawing.Point(408, 383);
            this.CancelConfigurationButton.Name = "CancelConfigurationButton";
            this.CancelConfigurationButton.Size = new System.Drawing.Size(87, 27);
            this.CancelConfigurationButton.TabIndex = 12;
            this.CancelConfigurationButton.Text = "Cancel";
            this.CancelConfigurationButton.UseVisualStyleBackColor = true;
            this.CancelConfigurationButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CodeMaskLabel
            // 
            this.CodeMaskLabel.AutoSize = true;
            this.CodeMaskLabel.Location = new System.Drawing.Point(14, 24);
            this.CodeMaskLabel.Name = "CodeMaskLabel";
            this.CodeMaskLabel.Size = new System.Drawing.Size(74, 15);
            this.CodeMaskLabel.TabIndex = 0;
            this.CodeMaskLabel.Text = "Code format";
            // 
            // CodeMaskTextBox
            // 
            this.CodeMaskTextBox.Location = new System.Drawing.Point(201, 20);
            this.CodeMaskTextBox.Name = "CodeMaskTextBox";
            this.CodeMaskTextBox.Size = new System.Drawing.Size(116, 23);
            this.CodeMaskTextBox.TabIndex = 1;
            // 
            // RoundTimeLabel
            // 
            this.RoundTimeLabel.AutoSize = true;
            this.RoundTimeLabel.Location = new System.Drawing.Point(14, 82);
            this.RoundTimeLabel.Name = "RoundTimeLabel";
            this.RoundTimeLabel.Size = new System.Drawing.Size(124, 15);
            this.RoundTimeLabel.TabIndex = 4;
            this.RoundTimeLabel.Text = "Round current time to";
            // 
            // RoundTimeComboBox
            // 
            this.RoundTimeComboBox.FormattingEnabled = true;
            this.RoundTimeComboBox.Items.AddRange(new object[] {
            "nearest minute",
            "nearest five minutes",
            "nearest six minutes (tenth of hour)",
            "nearest 12 minutes (fifth of hour)",
            "nearest 15 minutes",
            "nearest half hour",
            "nearest hour"});
            this.RoundTimeComboBox.Location = new System.Drawing.Point(201, 78);
            this.RoundTimeComboBox.Name = "RoundTimeComboBox";
            this.RoundTimeComboBox.Size = new System.Drawing.Size(230, 23);
            this.RoundTimeComboBox.TabIndex = 5;
            // 
            // DefaultDescriptionsLabel
            // 
            this.DefaultDescriptionsLabel.AutoSize = true;
            this.DefaultDescriptionsLabel.Location = new System.Drawing.Point(14, 158);
            this.DefaultDescriptionsLabel.Name = "DefaultDescriptionsLabel";
            this.DefaultDescriptionsLabel.Size = new System.Drawing.Size(207, 15);
            this.DefaultDescriptionsLabel.TabIndex = 7;
            this.DefaultDescriptionsLabel.Text = "Default descriptions for specific codes";
            // 
            // DefaultCodesListView
            // 
            this.DefaultCodesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultCodesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CodeColumn,
            this.DescriptionColumn});
            this.DefaultCodesListView.FullRowSelect = true;
            this.DefaultCodesListView.GridLines = true;
            this.DefaultCodesListView.HideSelection = false;
            this.DefaultCodesListView.LabelEdit = true;
            this.DefaultCodesListView.Location = new System.Drawing.Point(14, 181);
            this.DefaultCodesListView.Name = "DefaultCodesListView";
            this.DefaultCodesListView.Size = new System.Drawing.Size(480, 184);
            this.DefaultCodesListView.TabIndex = 8;
            this.DefaultCodesListView.UseCompatibleStateImageBehavior = false;
            this.DefaultCodesListView.View = System.Windows.Forms.View.Details;
            // 
            // CodeColumn
            // 
            this.CodeColumn.Text = "Code";
            this.CodeColumn.Width = 75;
            // 
            // DescriptionColumn
            // 
            this.DescriptionColumn.Text = "Description";
            this.DescriptionColumn.Width = 367;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(439, 148);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(27, 27);
            this.AddButton.TabIndex = 9;
            this.AddButton.Text = "+";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(468, 148);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(27, 27);
            this.DeleteButton.TabIndex = 10;
            this.DeleteButton.Text = "-";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // TimeMaskTextBox
            // 
            this.TimeMaskTextBox.Location = new System.Drawing.Point(201, 49);
            this.TimeMaskTextBox.Name = "TimeMaskTextBox";
            this.TimeMaskTextBox.Size = new System.Drawing.Size(116, 23);
            this.TimeMaskTextBox.TabIndex = 3;
            // 
            // TimeMaskLabel
            // 
            this.TimeMaskLabel.AutoSize = true;
            this.TimeMaskLabel.Location = new System.Drawing.Point(14, 53);
            this.TimeMaskLabel.Name = "TimeMaskLabel";
            this.TimeMaskLabel.Size = new System.Drawing.Size(72, 15);
            this.TimeMaskLabel.TabIndex = 2;
            this.TimeMaskLabel.Text = "Time format";
            // 
            // MidnightTintLabel
            // 
            this.MidnightTintLabel.AutoSize = true;
            this.MidnightTintLabel.Location = new System.Drawing.Point(14, 111);
            this.MidnightTintLabel.Name = "MidnightTintLabel";
            this.MidnightTintLabel.Size = new System.Drawing.Size(156, 15);
            this.MidnightTintLabel.TabIndex = 6;
            this.MidnightTintLabel.Text = "Tint for after midnight items";
            // 
            // MidnightTintPictureBox
            // 
            this.MidnightTintPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MidnightTintPictureBox.Location = new System.Drawing.Point(201, 107);
            this.MidnightTintPictureBox.Name = "MidnightTintPictureBox";
            this.MidnightTintPictureBox.Size = new System.Drawing.Size(100, 23);
            this.MidnightTintPictureBox.TabIndex = 13;
            this.MidnightTintPictureBox.TabStop = false;
            this.MidnightTintPictureBox.Click += new System.EventHandler(this.MidnightTintPictureBox_Click);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(509, 423);
            this.Controls.Add(this.MidnightTintPictureBox);
            this.Controls.Add(this.MidnightTintLabel);
            this.Controls.Add(this.TimeMaskTextBox);
            this.Controls.Add(this.TimeMaskLabel);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.DefaultCodesListView);
            this.Controls.Add(this.DefaultDescriptionsLabel);
            this.Controls.Add(this.RoundTimeComboBox);
            this.Controls.Add(this.RoundTimeLabel);
            this.Controls.Add(this.CodeMaskTextBox);
            this.Controls.Add(this.CodeMaskLabel);
            this.Controls.Add(this.CancelConfigurationButton);
            this.Controls.Add(this.OKButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Timecards - Preferences";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MidnightTintPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelConfigurationButton;
        private System.Windows.Forms.Label CodeMaskLabel;
        private System.Windows.Forms.TextBox CodeMaskTextBox;
        private System.Windows.Forms.Label RoundTimeLabel;
        private System.Windows.Forms.ComboBox RoundTimeComboBox;
        private System.Windows.Forms.Label DefaultDescriptionsLabel;
        private System.Windows.Forms.ListView DefaultCodesListView;
        private System.Windows.Forms.ColumnHeader CodeColumn;
        private System.Windows.Forms.ColumnHeader DescriptionColumn;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.TextBox TimeMaskTextBox;
        private System.Windows.Forms.Label TimeMaskLabel;
        private System.Windows.Forms.Label MidnightTintLabel;
        private System.Windows.Forms.PictureBox MidnightTintPictureBox;
        private System.Windows.Forms.ColorDialog MidnightTintColorDialog;
    }
}