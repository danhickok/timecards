using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TimecardsCore;
using TimecardsCore.ExtensionMethods;
using TimecardsCore.Interfaces;
using TimecardsCore.Logic;
using TimecardsCore.Models;

namespace TimecardsUI
{
    public partial class ExportReportForm : Form
    {
        private IFactory _factory;
        private bool _loading = false;
        private List<ReportItem> _reportList;

        private readonly BulkLogic.DataFormat[] formatChoices = new[]
        {
            BulkLogic.DataFormat.CSV,
            BulkLogic.DataFormat.TSV,
            BulkLogic.DataFormat.JSON,
            BulkLogic.DataFormat.XML,
        };

        public ExportReportForm()
        {
            InitializeComponent();

            FileTypeComboBox.Items.Clear();
            var defaultFileType = 0;
            for (var i = 0; i < formatChoices.Length; ++i)
            {
                FileTypeComboBox.Items.Add(formatChoices[i].GetDescription());
                if (Configuration.ExportFileType == formatChoices[i].ToString())
                    defaultFileType = i;
            }
            FileTypeComboBox.SelectedIndex = defaultFileType;
        }

        public void SetFactory(IFactory factory)
        {
            _factory = factory;
        }

        private void FileDialogButton_Click(object sender, EventArgs e)
        {
            FileSaveDialog.ShowDialog();

            var path = FileSaveDialog.FileName;
            if (string.IsNullOrWhiteSpace(path))
                return;

            FileNameTextBox.Text = path.Trim();
            SetFileTypeBasedOnExtension(path);
        }

        private void SetFileTypeBasedOnExtension(string path)
        {
            if (_loading)
                return;
            _loading = true;

            for (var i = 0; i < formatChoices.Length; ++i)
            {
                if (path.EndsWith("." + formatChoices[i].ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    FileTypeComboBox.SelectedIndex = i;
                    break;
                }
            }

            _loading = false;
        }

        public void SetReportList(List<ReportItem> reportList)
        {
            _reportList = reportList;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                DisableAllControls();

                var format = formatChoices[FileTypeComboBox.SelectedIndex];

                using (var sw = new StreamWriter(FileNameTextBox.Text.Trim()))
                {
                    var logic = new BulkLogic(_factory);
                    sw.Write(logic.ExportReport(_reportList, format));
                }

                MessageBox.Show("Report export successful",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while exporting the report: {ex.Message}",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void CancelExportButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisableAllControls()
        {
            this.Enabled = false;
            this.Refresh();
        }

        private void FileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            SetFileTypeBasedOnExtension(FileNameTextBox.Text.Trim());
        }

        private void FileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.ExportFileType = formatChoices[FileTypeComboBox.SelectedIndex].ToString();
            Configuration.Save();

            switch (formatChoices[FileTypeComboBox.SelectedIndex])
            {
                case BulkLogic.DataFormat.CSV:
                    FileSaveDialog.Filter =
                        "CSV files|*.csv|TSV files|*.tsv|JSON files|*.json|XML files|*.xml|All files|*.*";
                    break;
                case BulkLogic.DataFormat.TSV:
                    FileSaveDialog.Filter =
                        "TSV files|*.tsv|CSV files|*.csv|JSON files|*.json|XML files|*.xml|All files|*.*";
                    break;
                case BulkLogic.DataFormat.JSON:
                    FileSaveDialog.Filter =
                        "JSON files|*.json|CSV files|*.csv|TSV files|*.tsv|XML files|*.xml|All files|*.*";
                    break;
                case BulkLogic.DataFormat.XML:
                    FileSaveDialog.Filter =
                        "XML files|*.xml|CSV files|*.csv|TSV files|*.tsv|JSON files|*.json|All files|*.*";
                    break;
            }
        }
    }
}
