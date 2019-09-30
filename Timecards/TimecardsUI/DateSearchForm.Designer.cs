namespace TimecardsUI
{
    partial class DateSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateSearchForm));
            this.CancelDateSearchButton = new System.Windows.Forms.Button();
            this.DatesListView = new System.Windows.Forms.ListView();
            this.TimcardDateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TimecardDayColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // CancelDateSearchButton
            // 
            this.CancelDateSearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelDateSearchButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelDateSearchButton.Location = new System.Drawing.Point(155, 237);
            this.CancelDateSearchButton.Name = "CancelDateSearchButton";
            this.CancelDateSearchButton.Size = new System.Drawing.Size(87, 27);
            this.CancelDateSearchButton.TabIndex = 0;
            this.CancelDateSearchButton.Text = "Cancel";
            this.CancelDateSearchButton.UseVisualStyleBackColor = true;
            this.CancelDateSearchButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DatesListView
            // 
            this.DatesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DatesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TimcardDateColumn,
            this.TimecardDayColumn});
            this.DatesListView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DatesListView.FullRowSelect = true;
            this.DatesListView.GridLines = true;
            this.DatesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DatesListView.HideSelection = false;
            this.DatesListView.LabelWrap = false;
            this.DatesListView.Location = new System.Drawing.Point(15, 15);
            this.DatesListView.MultiSelect = false;
            this.DatesListView.Name = "DatesListView";
            this.DatesListView.Size = new System.Drawing.Size(227, 211);
            this.DatesListView.TabIndex = 1;
            this.DatesListView.UseCompatibleStateImageBehavior = false;
            this.DatesListView.View = System.Windows.Forms.View.Details;
            this.DatesListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.DatesListView_ItemSelectionChanged);
            // 
            // TimcardDateColumn
            // 
            this.TimcardDateColumn.Text = "Date";
            this.TimcardDateColumn.Width = 90;
            // 
            // TimecardDayColumn
            // 
            this.TimecardDayColumn.Text = "Day of week";
            this.TimecardDayColumn.Width = 110;
            // 
            // DateSearchForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(256, 273);
            this.Controls.Add(this.DatesListView);
            this.Controls.Add(this.CancelDateSearchButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(164, 226);
            this.Name = "DateSearchForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Timecards - Date Search";
            this.Load += new System.EventHandler(this.DateSearchForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelDateSearchButton;
        private System.Windows.Forms.ListView DatesListView;
        private System.Windows.Forms.ColumnHeader TimcardDateColumn;
        private System.Windows.Forms.ColumnHeader TimecardDayColumn;
    }
}