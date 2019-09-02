namespace TimecardsUI
{
   partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.staMain = new System.Windows.Forms.StatusStrip();
            this.staMainLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuFilePreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuFileResetColumnWidths = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuData = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuDataDateFirst = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuDataDatePrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuDataDateNext = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuDataDateLast = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuDataSearchForDate = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuDataSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuDataActivitiesSort = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabMainActivities = new System.Windows.Forms.TabPage();
            this.NavButtonSearch = new System.Windows.Forms.Button();
            this.grdActivities = new System.Windows.Forms.DataGridView();
            this.colCode = new TimecardsUI.ActivityCodeColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new TimecardsUI.ActivityTimeColumn();
            this.NavButtonToday = new System.Windows.Forms.Button();
            this.NavButtonLast = new System.Windows.Forms.Button();
            this.NavButtonNext = new System.Windows.Forms.Button();
            this.NavButtonPrev = new System.Windows.Forms.Button();
            this.NavButtonFirst = new System.Windows.Forms.Button();
            this.lblDayOfWeek = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.tabMainReport = new System.Windows.Forms.TabPage();
            this.lvwReport = new System.Windows.Forms.ListView();
            this.rtpColCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rptColFromDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rptColToDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rptColMinutes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rptColHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReportButtonGo = new System.Windows.Forms.Button();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.staMain.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabMainActivities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdActivities)).BeginInit();
            this.tabMainReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // staMain
            // 
            this.staMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.staMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staMainLabel});
            this.staMain.Location = new System.Drawing.Point(0, 657);
            this.staMain.Name = "staMain";
            this.staMain.Size = new System.Drawing.Size(471, 26);
            this.staMain.TabIndex = 0;
            this.staMain.Text = "statusStrip1";
            // 
            // staMainLabel
            // 
            this.staMainLabel.Name = "staMainLabel";
            this.staMainLabel.Size = new System.Drawing.Size(50, 20);
            this.staMainLabel.Text = "Ready";
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuFile,
            this.MainMenuData,
            this.MainMenuHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(471, 28);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // MainMenuFile
            // 
            this.MainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuFileImport,
            this.MainMenuFileExport,
            this.MainMenuFileSep1,
            this.MainMenuFilePreferences,
            this.MainMenuFileSep2,
            this.MainMenuFileResetColumnWidths,
            this.MainMenuFileSep3,
            this.MainMenuFileExit});
            this.MainMenuFile.Name = "MainMenuFile";
            this.MainMenuFile.Size = new System.Drawing.Size(46, 24);
            this.MainMenuFile.Text = "&File";
            // 
            // MainMenuFileImport
            // 
            this.MainMenuFileImport.Name = "MainMenuFileImport";
            this.MainMenuFileImport.Size = new System.Drawing.Size(228, 26);
            this.MainMenuFileImport.Text = "&Import...";
            this.MainMenuFileImport.Click += new System.EventHandler(this.MainMenuFileImport_Click);
            // 
            // MainMenuFileExport
            // 
            this.MainMenuFileExport.Name = "MainMenuFileExport";
            this.MainMenuFileExport.Size = new System.Drawing.Size(228, 26);
            this.MainMenuFileExport.Text = "&Export...";
            this.MainMenuFileExport.Click += new System.EventHandler(this.MainMenuFileExport_Click);
            // 
            // MainMenuFileSep1
            // 
            this.MainMenuFileSep1.Name = "MainMenuFileSep1";
            this.MainMenuFileSep1.Size = new System.Drawing.Size(225, 6);
            // 
            // MainMenuFilePreferences
            // 
            this.MainMenuFilePreferences.Name = "MainMenuFilePreferences";
            this.MainMenuFilePreferences.Size = new System.Drawing.Size(228, 26);
            this.MainMenuFilePreferences.Text = "&Preferences...";
            this.MainMenuFilePreferences.Click += new System.EventHandler(this.MainMenuFilePreferences_Click);
            // 
            // MainMenuFileSep2
            // 
            this.MainMenuFileSep2.Name = "MainMenuFileSep2";
            this.MainMenuFileSep2.Size = new System.Drawing.Size(225, 6);
            // 
            // MainMenuFileResetColumnWidths
            // 
            this.MainMenuFileResetColumnWidths.Name = "MainMenuFileResetColumnWidths";
            this.MainMenuFileResetColumnWidths.Size = new System.Drawing.Size(228, 26);
            this.MainMenuFileResetColumnWidths.Text = "&Reset column widths";
            this.MainMenuFileResetColumnWidths.Click += new System.EventHandler(this.MainMenuFileResetColumnWidths_Click);
            // 
            // MainMenuFileSep3
            // 
            this.MainMenuFileSep3.Name = "MainMenuFileSep3";
            this.MainMenuFileSep3.Size = new System.Drawing.Size(225, 6);
            // 
            // MainMenuFileExit
            // 
            this.MainMenuFileExit.Name = "MainMenuFileExit";
            this.MainMenuFileExit.Size = new System.Drawing.Size(228, 26);
            this.MainMenuFileExit.Text = "E&xit";
            this.MainMenuFileExit.Click += new System.EventHandler(this.MainMenuFileExit_Click);
            // 
            // MainMenuData
            // 
            this.MainMenuData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuDataDateFirst,
            this.MainMenuDataDatePrevious,
            this.MainMenuDataDateNext,
            this.MainMenuDataDateLast,
            this.MainMenuDataSearchForDate,
            this.MainMenuDataSep1,
            this.MainMenuDataActivitiesSort});
            this.MainMenuData.Name = "MainMenuData";
            this.MainMenuData.Size = new System.Drawing.Size(55, 24);
            this.MainMenuData.Text = "&Data";
            // 
            // MainMenuDataDateFirst
            // 
            this.MainMenuDataDateFirst.Name = "MainMenuDataDateFirst";
            this.MainMenuDataDateFirst.Size = new System.Drawing.Size(241, 26);
            this.MainMenuDataDateFirst.Text = "Move to &first date";
            this.MainMenuDataDateFirst.Click += new System.EventHandler(this.MainMenuDataDateFirst_Click);
            // 
            // MainMenuDataDatePrevious
            // 
            this.MainMenuDataDatePrevious.Name = "MainMenuDataDatePrevious";
            this.MainMenuDataDatePrevious.Size = new System.Drawing.Size(241, 26);
            this.MainMenuDataDatePrevious.Text = "Move to &previous date";
            this.MainMenuDataDatePrevious.Click += new System.EventHandler(this.MainMenuDataDatePrevious_Click);
            // 
            // MainMenuDataDateNext
            // 
            this.MainMenuDataDateNext.Name = "MainMenuDataDateNext";
            this.MainMenuDataDateNext.Size = new System.Drawing.Size(241, 26);
            this.MainMenuDataDateNext.Text = "Move to &next date";
            this.MainMenuDataDateNext.Click += new System.EventHandler(this.MainMenuDataDateNext_Click);
            // 
            // MainMenuDataDateLast
            // 
            this.MainMenuDataDateLast.Name = "MainMenuDataDateLast";
            this.MainMenuDataDateLast.Size = new System.Drawing.Size(241, 26);
            this.MainMenuDataDateLast.Text = "Move to &last date";
            this.MainMenuDataDateLast.Click += new System.EventHandler(this.MainMenuDataDateLast_Click);
            // 
            // MainMenuDataSearchForDate
            // 
            this.MainMenuDataSearchForDate.Name = "MainMenuDataSearchForDate";
            this.MainMenuDataSearchForDate.Size = new System.Drawing.Size(241, 26);
            this.MainMenuDataSearchForDate.Text = "&Search for date";
            this.MainMenuDataSearchForDate.Click += new System.EventHandler(this.MainMenuDataSearchForDate_Click);
            // 
            // MainMenuDataSep1
            // 
            this.MainMenuDataSep1.Name = "MainMenuDataSep1";
            this.MainMenuDataSep1.Size = new System.Drawing.Size(238, 6);
            // 
            // MainMenuDataActivitiesSort
            // 
            this.MainMenuDataActivitiesSort.Name = "MainMenuDataActivitiesSort";
            this.MainMenuDataActivitiesSort.Size = new System.Drawing.Size(241, 26);
            this.MainMenuDataActivitiesSort.Text = "Sort activities by &time";
            this.MainMenuDataActivitiesSort.Click += new System.EventHandler(this.MainMenuDataActivitiesSort_Click);
            // 
            // MainMenuHelp
            // 
            this.MainMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuHelpAbout});
            this.MainMenuHelp.Name = "MainMenuHelp";
            this.MainMenuHelp.Size = new System.Drawing.Size(55, 24);
            this.MainMenuHelp.Text = "&Help";
            // 
            // MainMenuHelpAbout
            // 
            this.MainMenuHelpAbout.Name = "MainMenuHelpAbout";
            this.MainMenuHelpAbout.Size = new System.Drawing.Size(214, 26);
            this.MainMenuHelpAbout.Text = "&About Timecards...";
            this.MainMenuHelpAbout.Click += new System.EventHandler(this.MainMenuHelpAbout_Click);
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
            this.tabMain.TabIndex = 0;
            // 
            // tabMainActivities
            // 
            this.tabMainActivities.Controls.Add(this.NavButtonSearch);
            this.tabMainActivities.Controls.Add(this.grdActivities);
            this.tabMainActivities.Controls.Add(this.NavButtonToday);
            this.tabMainActivities.Controls.Add(this.NavButtonLast);
            this.tabMainActivities.Controls.Add(this.NavButtonNext);
            this.tabMainActivities.Controls.Add(this.NavButtonPrev);
            this.tabMainActivities.Controls.Add(this.NavButtonFirst);
            this.tabMainActivities.Controls.Add(this.lblDayOfWeek);
            this.tabMainActivities.Controls.Add(this.dtpDate);
            this.tabMainActivities.Location = new System.Drawing.Point(4, 30);
            this.tabMainActivities.Name = "tabMainActivities";
            this.tabMainActivities.Padding = new System.Windows.Forms.Padding(3);
            this.tabMainActivities.Size = new System.Drawing.Size(463, 597);
            this.tabMainActivities.TabIndex = 0;
            this.tabMainActivities.Text = "Activities";
            this.tabMainActivities.UseVisualStyleBackColor = true;
            // 
            // NavButtonSearch
            // 
            this.NavButtonSearch.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.NavButtonSearch.Location = new System.Drawing.Point(347, 6);
            this.NavButtonSearch.Name = "NavButtonSearch";
            this.NavButtonSearch.Size = new System.Drawing.Size(28, 28);
            this.NavButtonSearch.TabIndex = 6;
            this.NavButtonSearch.Text = "";
            this.NavButtonSearch.UseVisualStyleBackColor = true;
            this.NavButtonSearch.Click += new System.EventHandler(this.NavButtonSearch_Click);
            // 
            // grdActivities
            // 
            this.grdActivities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdActivities.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdActivities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdActivities.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colDescription,
            this.colTime});
            this.grdActivities.EnableHeadersVisualStyles = false;
            this.grdActivities.Location = new System.Drawing.Point(9, 54);
            this.grdActivities.Name = "grdActivities";
            this.grdActivities.RowHeadersWidth = 30;
            this.grdActivities.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdActivities.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdActivities.Size = new System.Drawing.Size(446, 511);
            this.grdActivities.TabIndex = 8;
            this.grdActivities.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grdActivities_ColumnWidthChanged);
            this.grdActivities.ClientSizeChanged += new System.EventHandler(this.grdActivities_ClientSizeChanged);
            this.grdActivities.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdActivities_KeyDown);
            this.grdActivities.Leave += new System.EventHandler(this.grdActivities_Leave);
            // 
            // colCode
            // 
            this.colCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCode.HeaderText = "Code";
            this.colCode.MinimumWidth = 6;
            this.colCode.Name = "colCode";
            this.colCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCode.Width = 80;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDescription.HeaderText = "Description";
            this.colDescription.MinimumWidth = 6;
            this.colDescription.Name = "colDescription";
            this.colDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDescription.Width = 253;
            // 
            // colTime
            // 
            this.colTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTime.HeaderText = "Time";
            this.colTime.MinimumWidth = 6;
            this.colTime.Name = "colTime";
            this.colTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTime.Width = 80;
            // 
            // NavButtonToday
            // 
            this.NavButtonToday.Location = new System.Drawing.Point(382, 6);
            this.NavButtonToday.Name = "NavButtonToday";
            this.NavButtonToday.Size = new System.Drawing.Size(75, 28);
            this.NavButtonToday.TabIndex = 7;
            this.NavButtonToday.Text = "Today";
            this.NavButtonToday.UseVisualStyleBackColor = true;
            this.NavButtonToday.Click += new System.EventHandler(this.NavButtonToday_Click);
            // 
            // NavButtonLast
            // 
            this.NavButtonLast.Font = new System.Drawing.Font("Segoe UI Symbol", 10F);
            this.NavButtonLast.Location = new System.Drawing.Point(313, 6);
            this.NavButtonLast.Name = "NavButtonLast";
            this.NavButtonLast.Size = new System.Drawing.Size(28, 28);
            this.NavButtonLast.TabIndex = 5;
            this.NavButtonLast.Text = "";
            this.NavButtonLast.UseVisualStyleBackColor = true;
            this.NavButtonLast.Click += new System.EventHandler(this.NavButtonLast_Click);
            // 
            // NavButtonNext
            // 
            this.NavButtonNext.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            this.NavButtonNext.Location = new System.Drawing.Point(285, 6);
            this.NavButtonNext.Name = "NavButtonNext";
            this.NavButtonNext.Size = new System.Drawing.Size(28, 28);
            this.NavButtonNext.TabIndex = 4;
            this.NavButtonNext.Text = "▶";
            this.NavButtonNext.UseVisualStyleBackColor = true;
            this.NavButtonNext.Click += new System.EventHandler(this.NavButtonNext_Click);
            // 
            // NavButtonPrev
            // 
            this.NavButtonPrev.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            this.NavButtonPrev.Location = new System.Drawing.Point(257, 6);
            this.NavButtonPrev.Name = "NavButtonPrev";
            this.NavButtonPrev.Size = new System.Drawing.Size(28, 28);
            this.NavButtonPrev.TabIndex = 3;
            this.NavButtonPrev.Text = "◀";
            this.NavButtonPrev.UseVisualStyleBackColor = true;
            this.NavButtonPrev.Click += new System.EventHandler(this.NavButtonPrev_Click);
            // 
            // NavButtonFirst
            // 
            this.NavButtonFirst.Font = new System.Drawing.Font("Segoe UI Symbol", 10F);
            this.NavButtonFirst.Location = new System.Drawing.Point(229, 6);
            this.NavButtonFirst.Name = "NavButtonFirst";
            this.NavButtonFirst.Size = new System.Drawing.Size(28, 28);
            this.NavButtonFirst.TabIndex = 2;
            this.NavButtonFirst.Text = "";
            this.NavButtonFirst.UseVisualStyleBackColor = true;
            this.NavButtonFirst.Click += new System.EventHandler(this.NavButtonFirst_Click);
            // 
            // lblDayOfWeek
            // 
            this.lblDayOfWeek.AutoSize = true;
            this.lblDayOfWeek.Location = new System.Drawing.Point(131, 12);
            this.lblDayOfWeek.Name = "lblDayOfWeek";
            this.lblDayOfWeek.Size = new System.Drawing.Size(105, 23);
            this.lblDayOfWeek.TabIndex = 1;
            this.lblDayOfWeek.Text = "Day of Week";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(8, 8);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(117, 29);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // tabMainReport
            // 
            this.tabMainReport.Controls.Add(this.lvwReport);
            this.tabMainReport.Controls.Add(this.ReportButtonGo);
            this.tabMainReport.Controls.Add(this.lblEnd);
            this.tabMainReport.Controls.Add(this.dtpEnd);
            this.tabMainReport.Controls.Add(this.dtpStart);
            this.tabMainReport.Controls.Add(this.lblStart);
            this.tabMainReport.Location = new System.Drawing.Point(4, 30);
            this.tabMainReport.Name = "tabMainReport";
            this.tabMainReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabMainReport.Size = new System.Drawing.Size(463, 597);
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
            this.lvwReport.HideSelection = false;
            this.lvwReport.Location = new System.Drawing.Point(22, 104);
            this.lvwReport.MultiSelect = false;
            this.lvwReport.Name = "lvwReport";
            this.lvwReport.Size = new System.Drawing.Size(417, 461);
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
            // ReportButtonGo
            // 
            this.ReportButtonGo.Location = new System.Drawing.Point(233, 34);
            this.ReportButtonGo.Name = "ReportButtonGo";
            this.ReportButtonGo.Size = new System.Drawing.Size(50, 35);
            this.ReportButtonGo.TabIndex = 4;
            this.ReportButtonGo.Text = "Go";
            this.ReportButtonGo.UseVisualStyleBackColor = true;
            this.ReportButtonGo.Click += new System.EventHandler(this.ReportButtonGo_Click);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(19, 59);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(75, 23);
            this.lblEnd.TabIndex = 3;
            this.lblEnd.Text = "Through";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(101, 55);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(116, 29);
            this.dtpEnd.TabIndex = 2;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(101, 24);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(116, 29);
            this.dtpStart.TabIndex = 1;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(19, 28);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(49, 23);
            this.lblStart.TabIndex = 0;
            this.lblStart.Text = "From";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.HeaderText = "Code";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 253;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn3.HeaderText = "Time";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(471, 683);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.staMain);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(487, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Timecards";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.staMain.ResumeLayout(false);
            this.staMain.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
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
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFile;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileImport;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileExport;
        private System.Windows.Forms.ToolStripSeparator MainMenuFileSep1;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileExit;
        private System.Windows.Forms.ToolStripMenuItem MainMenuHelp;
        private System.Windows.Forms.ToolStripMenuItem MainMenuHelpAbout;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabMainActivities;
        private System.Windows.Forms.TabPage tabMainReport;
        private System.Windows.Forms.Button NavButtonToday;
        private System.Windows.Forms.Button NavButtonLast;
        private System.Windows.Forms.Button NavButtonNext;
        private System.Windows.Forms.Button NavButtonPrev;
        private System.Windows.Forms.Button NavButtonFirst;
        private System.Windows.Forms.Label lblDayOfWeek;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.ListView lvwReport;
        private System.Windows.Forms.Button ReportButtonGo;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFilePreferences;
        private System.Windows.Forms.ToolStripSeparator MainMenuFileSep3;
        private System.Windows.Forms.ColumnHeader rtpColCode;
        private System.Windows.Forms.ColumnHeader rptColFromDate;
        private System.Windows.Forms.ColumnHeader rptColToDate;
        private System.Windows.Forms.ColumnHeader rptColMinutes;
        private System.Windows.Forms.ColumnHeader rptColHours;
        private System.Windows.Forms.ToolStripMenuItem MainMenuData;
        private System.Windows.Forms.ToolStripMenuItem MainMenuDataDateFirst;
        private System.Windows.Forms.ToolStripMenuItem MainMenuDataDatePrevious;
        private System.Windows.Forms.ToolStripMenuItem MainMenuDataDateNext;
        private System.Windows.Forms.ToolStripMenuItem MainMenuDataDateLast;
        private System.Windows.Forms.ToolStripSeparator MainMenuDataSep1;
        private System.Windows.Forms.ToolStripMenuItem MainMenuDataActivitiesSort;
        private System.Windows.Forms.ToolStripSeparator MainMenuFileSep2;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileResetColumnWidths;
        private System.Windows.Forms.ToolStripMenuItem MainMenuDataSearchForDate;
        private System.Windows.Forms.Button NavButtonSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ActivityCodeColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private ActivityTimeColumn colTime;
        private System.Windows.Forms.DataGridView grdActivities;
    }
}

