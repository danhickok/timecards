﻿namespace TimecardsUI
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
            this.MainStatus = new System.Windows.Forms.StatusStrip();
            this.MainStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.MainTab = new System.Windows.Forms.TabControl();
            this.MainTabActivities = new System.Windows.Forms.TabPage();
            this.NavButtonSearch = new System.Windows.Forms.Button();
            this.ActivitiesGrid = new System.Windows.Forms.DataGridView();
            this.CodeColumn = new TimecardsUI.ActivityCodeColumn();
            this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeColumn = new TimecardsUI.ActivityTimeColumn();
            this.NavButtonToday = new System.Windows.Forms.Button();
            this.NavButtonLast = new System.Windows.Forms.Button();
            this.NavButtonNext = new System.Windows.Forms.Button();
            this.NavButtonPrev = new System.Windows.Forms.Button();
            this.NavButtonFirst = new System.Windows.Forms.Button();
            this.MainDateLabel = new System.Windows.Forms.Label();
            this.MainDate = new System.Windows.Forms.DateTimePicker();
            this.MainTabReport = new System.Windows.Forms.TabPage();
            this.ReportListView = new System.Windows.Forms.ListView();
            this.ReportColumnCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReportColumnFromDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReportColumnToDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReportColumnMinutes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReportColumnHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReportButtonGo = new System.Windows.Forms.Button();
            this.ReportEndLabel = new System.Windows.Forms.Label();
            this.ReportDateEnd = new System.Windows.Forms.DateTimePicker();
            this.ReportDateStart = new System.Windows.Forms.DateTimePicker();
            this.ReportStartLabel = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainStatus.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.MainTabActivities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActivitiesGrid)).BeginInit();
            this.MainTabReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainStatus
            // 
            this.MainStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainStatusLabel});
            this.MainStatus.Location = new System.Drawing.Point(0, 657);
            this.MainStatus.Name = "MainStatus";
            this.MainStatus.Size = new System.Drawing.Size(471, 26);
            this.MainStatus.TabIndex = 0;
            this.MainStatus.Text = "statusStrip1";
            // 
            // MainStatusLabel
            // 
            this.MainStatusLabel.Name = "MainStatusLabel";
            this.MainStatusLabel.Size = new System.Drawing.Size(50, 20);
            this.MainStatusLabel.Text = "Ready";
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
            this.MainMenu.Size = new System.Drawing.Size(471, 30);
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
            this.MainMenuFile.Size = new System.Drawing.Size(46, 26);
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
            this.MainMenuData.Size = new System.Drawing.Size(55, 26);
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
            this.MainMenuHelp.Size = new System.Drawing.Size(55, 26);
            this.MainMenuHelp.Text = "&Help";
            // 
            // MainMenuHelpAbout
            // 
            this.MainMenuHelpAbout.Name = "MainMenuHelpAbout";
            this.MainMenuHelpAbout.Size = new System.Drawing.Size(214, 26);
            this.MainMenuHelpAbout.Text = "&About Timecards...";
            this.MainMenuHelpAbout.Click += new System.EventHandler(this.MainMenuHelpAbout_Click);
            // 
            // MainTab
            // 
            this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTab.Controls.Add(this.MainTabActivities);
            this.MainTab.Controls.Add(this.MainTabReport);
            this.MainTab.Location = new System.Drawing.Point(0, 27);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(471, 631);
            this.MainTab.TabIndex = 0;
            // 
            // MainTabActivities
            // 
            this.MainTabActivities.Controls.Add(this.NavButtonSearch);
            this.MainTabActivities.Controls.Add(this.ActivitiesGrid);
            this.MainTabActivities.Controls.Add(this.NavButtonToday);
            this.MainTabActivities.Controls.Add(this.NavButtonLast);
            this.MainTabActivities.Controls.Add(this.NavButtonNext);
            this.MainTabActivities.Controls.Add(this.NavButtonPrev);
            this.MainTabActivities.Controls.Add(this.NavButtonFirst);
            this.MainTabActivities.Controls.Add(this.MainDateLabel);
            this.MainTabActivities.Controls.Add(this.MainDate);
            this.MainTabActivities.Location = new System.Drawing.Point(4, 30);
            this.MainTabActivities.Name = "MainTabActivities";
            this.MainTabActivities.Padding = new System.Windows.Forms.Padding(3);
            this.MainTabActivities.Size = new System.Drawing.Size(463, 597);
            this.MainTabActivities.TabIndex = 0;
            this.MainTabActivities.Text = "Activities";
            this.MainTabActivities.UseVisualStyleBackColor = true;
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
            // ActivitiesGrid
            // 
            this.ActivitiesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ActivitiesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ActivitiesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ActivitiesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodeColumn,
            this.DescriptionColumn,
            this.TimeColumn});
            this.ActivitiesGrid.EnableHeadersVisualStyles = false;
            this.ActivitiesGrid.Location = new System.Drawing.Point(9, 54);
            this.ActivitiesGrid.Name = "ActivitiesGrid";
            this.ActivitiesGrid.RowHeadersWidth = 30;
            this.ActivitiesGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ActivitiesGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ActivitiesGrid.Size = new System.Drawing.Size(446, 496);
            this.ActivitiesGrid.TabIndex = 8;
            this.ActivitiesGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grdActivities_ColumnWidthChanged);
            this.ActivitiesGrid.ClientSizeChanged += new System.EventHandler(this.grdActivities_ClientSizeChanged);
            this.ActivitiesGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdActivities_KeyDown);
            this.ActivitiesGrid.Leave += new System.EventHandler(this.grdActivities_Leave);
            // 
            // CodeColumn
            // 
            this.CodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CodeColumn.HeaderText = "Code";
            this.CodeColumn.MinimumWidth = 6;
            this.CodeColumn.Name = "CodeColumn";
            this.CodeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CodeColumn.Width = 80;
            // 
            // DescriptionColumn
            // 
            this.DescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DescriptionColumn.HeaderText = "Description";
            this.DescriptionColumn.MinimumWidth = 6;
            this.DescriptionColumn.Name = "DescriptionColumn";
            this.DescriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DescriptionColumn.Width = 253;
            // 
            // TimeColumn
            // 
            this.TimeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TimeColumn.HeaderText = "Time";
            this.TimeColumn.MinimumWidth = 6;
            this.TimeColumn.Name = "TimeColumn";
            this.TimeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TimeColumn.Width = 80;
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
            // MainDateLabel
            // 
            this.MainDateLabel.AutoSize = true;
            this.MainDateLabel.Location = new System.Drawing.Point(131, 12);
            this.MainDateLabel.Name = "MainDateLabel";
            this.MainDateLabel.Size = new System.Drawing.Size(105, 23);
            this.MainDateLabel.TabIndex = 1;
            this.MainDateLabel.Text = "Day of Week";
            // 
            // MainDate
            // 
            this.MainDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.MainDate.Location = new System.Drawing.Point(8, 8);
            this.MainDate.Name = "MainDate";
            this.MainDate.Size = new System.Drawing.Size(117, 29);
            this.MainDate.TabIndex = 0;
            this.MainDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // MainTabReport
            // 
            this.MainTabReport.Controls.Add(this.ReportListView);
            this.MainTabReport.Controls.Add(this.ReportButtonGo);
            this.MainTabReport.Controls.Add(this.ReportEndLabel);
            this.MainTabReport.Controls.Add(this.ReportDateEnd);
            this.MainTabReport.Controls.Add(this.ReportDateStart);
            this.MainTabReport.Controls.Add(this.ReportStartLabel);
            this.MainTabReport.Location = new System.Drawing.Point(4, 30);
            this.MainTabReport.Name = "MainTabReport";
            this.MainTabReport.Padding = new System.Windows.Forms.Padding(3);
            this.MainTabReport.Size = new System.Drawing.Size(463, 597);
            this.MainTabReport.TabIndex = 1;
            this.MainTabReport.Text = "Report";
            this.MainTabReport.UseVisualStyleBackColor = true;
            // 
            // ReportListView
            // 
            this.ReportListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.ReportListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReportListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ReportColumnCode,
            this.ReportColumnFromDate,
            this.ReportColumnToDate,
            this.ReportColumnMinutes,
            this.ReportColumnHours});
            this.ReportListView.FullRowSelect = true;
            this.ReportListView.GridLines = true;
            this.ReportListView.HideSelection = false;
            this.ReportListView.Location = new System.Drawing.Point(22, 104);
            this.ReportListView.MultiSelect = false;
            this.ReportListView.Name = "ReportListView";
            this.ReportListView.Size = new System.Drawing.Size(417, 456);
            this.ReportListView.TabIndex = 5;
            this.ReportListView.UseCompatibleStateImageBehavior = false;
            this.ReportListView.View = System.Windows.Forms.View.Details;
            // 
            // ReportColumnCode
            // 
            this.ReportColumnCode.Text = "Code";
            this.ReportColumnCode.Width = 75;
            // 
            // ReportColumnFromDate
            // 
            this.ReportColumnFromDate.Text = "From";
            this.ReportColumnFromDate.Width = 90;
            // 
            // ReportColumnToDate
            // 
            this.ReportColumnToDate.Text = "To";
            this.ReportColumnToDate.Width = 90;
            // 
            // ReportColumnMinutes
            // 
            this.ReportColumnMinutes.Text = "Minutes";
            this.ReportColumnMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReportColumnMinutes.Width = 65;
            // 
            // ReportColumnHours
            // 
            this.ReportColumnHours.Text = "Hours";
            this.ReportColumnHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReportColumnHours.Width = 65;
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
            // ReportEndLabel
            // 
            this.ReportEndLabel.AutoSize = true;
            this.ReportEndLabel.Location = new System.Drawing.Point(19, 59);
            this.ReportEndLabel.Name = "ReportEndLabel";
            this.ReportEndLabel.Size = new System.Drawing.Size(75, 23);
            this.ReportEndLabel.TabIndex = 3;
            this.ReportEndLabel.Text = "Through";
            // 
            // ReportDateEnd
            // 
            this.ReportDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ReportDateEnd.Location = new System.Drawing.Point(101, 55);
            this.ReportDateEnd.Name = "ReportDateEnd";
            this.ReportDateEnd.Size = new System.Drawing.Size(116, 29);
            this.ReportDateEnd.TabIndex = 2;
            this.ReportDateEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // ReportDateStart
            // 
            this.ReportDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ReportDateStart.Location = new System.Drawing.Point(101, 24);
            this.ReportDateStart.Name = "ReportDateStart";
            this.ReportDateStart.Size = new System.Drawing.Size(116, 29);
            this.ReportDateStart.TabIndex = 1;
            this.ReportDateStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // ReportStartLabel
            // 
            this.ReportStartLabel.AutoSize = true;
            this.ReportStartLabel.Location = new System.Drawing.Point(19, 28);
            this.ReportStartLabel.Name = "ReportStartLabel";
            this.ReportStartLabel.Size = new System.Drawing.Size(49, 23);
            this.ReportStartLabel.TabIndex = 0;
            this.ReportStartLabel.Text = "From";
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
            this.Controls.Add(this.MainTab);
            this.Controls.Add(this.MainStatus);
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
            this.MainStatus.ResumeLayout(false);
            this.MainStatus.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainTab.ResumeLayout(false);
            this.MainTabActivities.ResumeLayout(false);
            this.MainTabActivities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActivitiesGrid)).EndInit();
            this.MainTabReport.ResumeLayout(false);
            this.MainTabReport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

        #endregion

        private System.Windows.Forms.StatusStrip MainStatus;
        private System.Windows.Forms.ToolStripStatusLabel MainStatusLabel;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFile;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileImport;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileExport;
        private System.Windows.Forms.ToolStripSeparator MainMenuFileSep1;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileExit;
        private System.Windows.Forms.ToolStripMenuItem MainMenuHelp;
        private System.Windows.Forms.ToolStripMenuItem MainMenuHelpAbout;
        private System.Windows.Forms.TabControl MainTab;
        private System.Windows.Forms.TabPage MainTabActivities;
        private System.Windows.Forms.TabPage MainTabReport;
        private System.Windows.Forms.Button NavButtonToday;
        private System.Windows.Forms.Button NavButtonLast;
        private System.Windows.Forms.Button NavButtonNext;
        private System.Windows.Forms.Button NavButtonPrev;
        private System.Windows.Forms.Button NavButtonFirst;
        private System.Windows.Forms.Label MainDateLabel;
        private System.Windows.Forms.DateTimePicker MainDate;
        private System.Windows.Forms.DateTimePicker ReportDateStart;
        private System.Windows.Forms.Label ReportStartLabel;
        private System.Windows.Forms.ListView ReportListView;
        private System.Windows.Forms.Button ReportButtonGo;
        private System.Windows.Forms.Label ReportEndLabel;
        private System.Windows.Forms.DateTimePicker ReportDateEnd;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFilePreferences;
        private System.Windows.Forms.ToolStripSeparator MainMenuFileSep3;
        private System.Windows.Forms.ColumnHeader ReportColumnCode;
        private System.Windows.Forms.ColumnHeader ReportColumnFromDate;
        private System.Windows.Forms.ColumnHeader ReportColumnToDate;
        private System.Windows.Forms.ColumnHeader ReportColumnMinutes;
        private System.Windows.Forms.ColumnHeader ReportColumnHours;
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
        private System.Windows.Forms.DataGridView ActivitiesGrid;
        private ActivityCodeColumn CodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
        private ActivityTimeColumn TimeColumn;
    }
}

