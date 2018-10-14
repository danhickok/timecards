namespace TimecardsUI
{
   partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.staMain = new System.Windows.Forms.StatusStrip();
            this.staMainLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuMainFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMainFilePreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileMainExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabMainActivities = new System.Windows.Forms.TabPage();
            this.grdActivities = new System.Windows.Forms.DataGridView();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.lblDayOfWeek = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.tabMainReport = new System.Windows.Forms.TabPage();
            this.lvwReport = new System.Windows.Forms.ListView();
            this.rtpColCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rptColFromDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rptColToDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rptColMinutes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rptColHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGo = new System.Windows.Forms.Button();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.mnuMainData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainDataDateFirst = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainDataDatePrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainDataDateNext = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainDataDateLast = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainDataSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMainDataActivitiesSort = new System.Windows.Forms.ToolStripMenuItem();
            this.staMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabMainActivities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdActivities)).BeginInit();
            this.tabMainReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // staMain
            // 
            this.staMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staMainLabel});
            this.staMain.Location = new System.Drawing.Point(0, 661);
            this.staMain.Name = "staMain";
            this.staMain.Size = new System.Drawing.Size(471, 22);
            this.staMain.TabIndex = 0;
            this.staMain.Text = "statusStrip1";
            // 
            // staMainLabel
            // 
            this.staMainLabel.Name = "staMainLabel";
            this.staMainLabel.Size = new System.Drawing.Size(39, 17);
            this.staMainLabel.Text = "Ready";
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFile,
            this.mnuMainData,
            this.mnuMainHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(471, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuMainFile
            // 
            this.mnuMainFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFileImport,
            this.mnuMainFileExport,
            this.mnuMainFileSep1,
            this.mnuMainFilePreferences,
            this.mnuMainFileSep2,
            this.mnuFileMainExit});
            this.mnuMainFile.Name = "mnuMainFile";
            this.mnuMainFile.Size = new System.Drawing.Size(37, 20);
            this.mnuMainFile.Text = "File";
            // 
            // mnuMainFileImport
            // 
            this.mnuMainFileImport.Name = "mnuMainFileImport";
            this.mnuMainFileImport.Size = new System.Drawing.Size(180, 22);
            this.mnuMainFileImport.Text = "Import...";
            this.mnuMainFileImport.Click += new System.EventHandler(this.mnuMainFileImport_Click);
            // 
            // mnuMainFileExport
            // 
            this.mnuMainFileExport.Name = "mnuMainFileExport";
            this.mnuMainFileExport.Size = new System.Drawing.Size(180, 22);
            this.mnuMainFileExport.Text = "Export...";
            this.mnuMainFileExport.Click += new System.EventHandler(this.mnuMainFileExport_Click);
            // 
            // mnuMainFileSep1
            // 
            this.mnuMainFileSep1.Name = "mnuMainFileSep1";
            this.mnuMainFileSep1.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuMainFilePreferences
            // 
            this.mnuMainFilePreferences.Name = "mnuMainFilePreferences";
            this.mnuMainFilePreferences.Size = new System.Drawing.Size(180, 22);
            this.mnuMainFilePreferences.Text = "Preferences...";
            this.mnuMainFilePreferences.Click += new System.EventHandler(this.mnuMainFilePreferences_Click);
            // 
            // mnuMainFileSep2
            // 
            this.mnuMainFileSep2.Name = "mnuMainFileSep2";
            this.mnuMainFileSep2.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuFileMainExit
            // 
            this.mnuFileMainExit.Name = "mnuFileMainExit";
            this.mnuFileMainExit.Size = new System.Drawing.Size(180, 22);
            this.mnuFileMainExit.Text = "Exit";
            this.mnuFileMainExit.Click += new System.EventHandler(this.mnuFileMainExit_Click);
            // 
            // mnuMainHelp
            // 
            this.mnuMainHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainHelpAbout});
            this.mnuMainHelp.Name = "mnuMainHelp";
            this.mnuMainHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuMainHelp.Text = "Help";
            // 
            // mnuMainHelpAbout
            // 
            this.mnuMainHelpAbout.Name = "mnuMainHelpAbout";
            this.mnuMainHelpAbout.Size = new System.Drawing.Size(180, 22);
            this.mnuMainHelpAbout.Text = "About Timecards...";
            this.mnuMainHelpAbout.Click += new System.EventHandler(this.mnuMainHelpAbout_Click);
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabMainActivities);
            this.tabMain.Controls.Add(this.tabMainReport);
            this.tabMain.Location = new System.Drawing.Point(0, 27);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(471, 631);
            this.tabMain.TabIndex = 2;
            // 
            // tabMainActivities
            // 
            this.tabMainActivities.Controls.Add(this.grdActivities);
            this.tabMainActivities.Controls.Add(this.btnToday);
            this.tabMainActivities.Controls.Add(this.btnLast);
            this.tabMainActivities.Controls.Add(this.btnNext);
            this.tabMainActivities.Controls.Add(this.btnPrev);
            this.tabMainActivities.Controls.Add(this.btnFirst);
            this.tabMainActivities.Controls.Add(this.lblDayOfWeek);
            this.tabMainActivities.Controls.Add(this.dtpDate);
            this.tabMainActivities.Location = new System.Drawing.Point(4, 26);
            this.tabMainActivities.Name = "tabMainActivities";
            this.tabMainActivities.Padding = new System.Windows.Forms.Padding(3);
            this.tabMainActivities.Size = new System.Drawing.Size(463, 601);
            this.tabMainActivities.TabIndex = 0;
            this.tabMainActivities.Text = "Activities";
            this.tabMainActivities.UseVisualStyleBackColor = true;
            // 
            // grdActivities
            // 
            this.grdActivities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdActivities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdActivities.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colDescription,
            this.colTime});
            this.grdActivities.Location = new System.Drawing.Point(9, 54);
            this.grdActivities.Name = "grdActivities";
            this.grdActivities.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdActivities.Size = new System.Drawing.Size(446, 541);
            this.grdActivities.TabIndex = 8;
            // 
            // colCode
            // 
            dataGridViewCellStyle1.Format = "#####";
            dataGridViewCellStyle1.NullValue = null;
            this.colCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.colCode.HeaderText = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Width = 90;
            // 
            // colDescription
            // 
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Width = 240;
            // 
            // colTime
            // 
            dataGridViewCellStyle2.Format = "t";
            dataGridViewCellStyle2.NullValue = null;
            this.colTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTime.HeaderText = "Time";
            this.colTime.Name = "colTime";
            this.colTime.Width = 70;
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(382, 6);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(75, 28);
            this.btnToday.TabIndex = 7;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.btnLast.Location = new System.Drawing.Point(348, 6);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(28, 28);
            this.btnLast.TabIndex = 6;
            this.btnLast.Text = "⏭";
            this.btnLast.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.btnNext.Location = new System.Drawing.Point(317, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(28, 28);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "⏵";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.btnPrev.Location = new System.Drawing.Point(286, 6);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(28, 28);
            this.btnPrev.TabIndex = 4;
            this.btnPrev.Text = "⏴";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnFirst
            // 
            this.btnFirst.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.btnFirst.Location = new System.Drawing.Point(255, 6);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(28, 28);
            this.btnFirst.TabIndex = 3;
            this.btnFirst.Text = "⏮";
            this.btnFirst.UseVisualStyleBackColor = true;
            // 
            // lblDayOfWeek
            // 
            this.lblDayOfWeek.AutoSize = true;
            this.lblDayOfWeek.Location = new System.Drawing.Point(131, 12);
            this.lblDayOfWeek.Name = "lblDayOfWeek";
            this.lblDayOfWeek.Size = new System.Drawing.Size(113, 17);
            this.lblDayOfWeek.TabIndex = 2;
            this.lblDayOfWeek.Text = "Day of Week Here";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(8, 8);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(116, 25);
            this.dtpDate.TabIndex = 1;
            // 
            // tabMainReport
            // 
            this.tabMainReport.Controls.Add(this.lvwReport);
            this.tabMainReport.Controls.Add(this.btnGo);
            this.tabMainReport.Controls.Add(this.lblEnd);
            this.tabMainReport.Controls.Add(this.dtpEnd);
            this.tabMainReport.Controls.Add(this.dtpStart);
            this.tabMainReport.Controls.Add(this.lblStart);
            this.tabMainReport.Location = new System.Drawing.Point(4, 26);
            this.tabMainReport.Name = "tabMainReport";
            this.tabMainReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabMainReport.Size = new System.Drawing.Size(463, 601);
            this.tabMainReport.TabIndex = 1;
            this.tabMainReport.Text = "Report";
            this.tabMainReport.UseVisualStyleBackColor = true;
            // 
            // lvwReport
            // 
            this.lvwReport.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwReport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.rtpColCode,
            this.rptColFromDate,
            this.rptColToDate,
            this.rptColMinutes,
            this.rptColHours});
            this.lvwReport.FullRowSelect = true;
            this.lvwReport.GridLines = true;
            this.lvwReport.Location = new System.Drawing.Point(22, 104);
            this.lvwReport.MultiSelect = false;
            this.lvwReport.Name = "lvwReport";
            this.lvwReport.Size = new System.Drawing.Size(417, 471);
            this.lvwReport.TabIndex = 5;
            this.lvwReport.UseCompatibleStateImageBehavior = false;
            this.lvwReport.View = System.Windows.Forms.View.Details;
            // 
            // rtpColCode
            // 
            this.rtpColCode.Text = "Code";
            this.rtpColCode.Width = 75;
            // 
            // rptColFromDate
            // 
            this.rptColFromDate.Text = "From";
            this.rptColFromDate.Width = 90;
            // 
            // rptColToDate
            // 
            this.rptColToDate.Text = "To";
            this.rptColToDate.Width = 90;
            // 
            // rptColMinutes
            // 
            this.rptColMinutes.Text = "Minutes";
            this.rptColMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.rptColMinutes.Width = 65;
            // 
            // rptColHours
            // 
            this.rptColHours.Text = "Hours";
            this.rptColHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.rptColHours.Width = 65;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(233, 34);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(50, 35);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(19, 59);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(57, 17);
            this.lblEnd.TabIndex = 3;
            this.lblEnd.Text = "Through";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(85, 55);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(116, 25);
            this.dtpEnd.TabIndex = 2;
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(85, 24);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(116, 25);
            this.dtpStart.TabIndex = 1;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(19, 28);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(38, 17);
            this.lblStart.TabIndex = 0;
            this.lblStart.Text = "From";
            // 
            // mnuMainData
            // 
            this.mnuMainData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainDataDateFirst,
            this.mnuMainDataDatePrevious,
            this.mnuMainDataDateNext,
            this.mnuMainDataDateLast,
            this.mnuMainDataSep1,
            this.mnuMainDataActivitiesSort});
            this.mnuMainData.Name = "mnuMainData";
            this.mnuMainData.Size = new System.Drawing.Size(43, 20);
            this.mnuMainData.Text = "Data";
            // 
            // mnuMainDataDateFirst
            // 
            this.mnuMainDataDateFirst.Name = "mnuMainDataDateFirst";
            this.mnuMainDataDateFirst.Size = new System.Drawing.Size(192, 22);
            this.mnuMainDataDateFirst.Text = "Move to first date";
            // 
            // mnuMainDataDatePrevious
            // 
            this.mnuMainDataDatePrevious.Name = "mnuMainDataDatePrevious";
            this.mnuMainDataDatePrevious.Size = new System.Drawing.Size(192, 22);
            this.mnuMainDataDatePrevious.Text = "Move to previous date";
            // 
            // mnuMainDataDateNext
            // 
            this.mnuMainDataDateNext.Name = "mnuMainDataDateNext";
            this.mnuMainDataDateNext.Size = new System.Drawing.Size(192, 22);
            this.mnuMainDataDateNext.Text = "Move to next date";
            // 
            // mnuMainDataDateLast
            // 
            this.mnuMainDataDateLast.Name = "mnuMainDataDateLast";
            this.mnuMainDataDateLast.Size = new System.Drawing.Size(192, 22);
            this.mnuMainDataDateLast.Text = "Move to last date";
            // 
            // mnuMainDataSep1
            // 
            this.mnuMainDataSep1.Name = "mnuMainDataSep1";
            this.mnuMainDataSep1.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuMainDataActivitiesSort
            // 
            this.mnuMainDataActivitiesSort.Name = "mnuMainDataActivitiesSort";
            this.mnuMainDataActivitiesSort.Size = new System.Drawing.Size(192, 22);
            this.mnuMainDataActivitiesSort.Text = "Sort activities by time";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 683);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.staMain);
            this.Controls.Add(this.mnuMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(487, 300);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Timecards";
            this.staMain.ResumeLayout(false);
            this.staMain.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabMainActivities.ResumeLayout(false);
            this.tabMainActivities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdActivities)).EndInit();
            this.tabMainReport.ResumeLayout(false);
            this.tabMainReport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

        #endregion

        private System.Windows.Forms.StatusStrip staMain;
        private System.Windows.Forms.ToolStripStatusLabel staMainLabel;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFile;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFileImport;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFileExport;
        private System.Windows.Forms.ToolStripSeparator mnuMainFileSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMainExit;
        private System.Windows.Forms.ToolStripMenuItem mnuMainHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuMainHelpAbout;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabMainActivities;
        private System.Windows.Forms.TabPage tabMainReport;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Label lblDayOfWeek;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DataGridView grdActivities;
        private System.Windows.Forms.ListView lvwReport;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFilePreferences;
        private System.Windows.Forms.ToolStripSeparator mnuMainFileSep2;
        private System.Windows.Forms.ColumnHeader rtpColCode;
        private System.Windows.Forms.ColumnHeader rptColFromDate;
        private System.Windows.Forms.ColumnHeader rptColToDate;
        private System.Windows.Forms.ColumnHeader rptColMinutes;
        private System.Windows.Forms.ColumnHeader rptColHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.ToolStripMenuItem mnuMainData;
        private System.Windows.Forms.ToolStripMenuItem mnuMainDataDateFirst;
        private System.Windows.Forms.ToolStripMenuItem mnuMainDataDatePrevious;
        private System.Windows.Forms.ToolStripMenuItem mnuMainDataDateNext;
        private System.Windows.Forms.ToolStripMenuItem mnuMainDataDateLast;
        private System.Windows.Forms.ToolStripSeparator mnuMainDataSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuMainDataActivitiesSort;
    }
}

