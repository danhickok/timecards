using System;
using System.IO;
using System.Windows.Forms;
using TimecardsCore;
using TimecardsCore.ExtensionMethods;
using TimecardsCore.Interfaces;
using TimecardsCore.Logic;

namespace TimecardsUI
{
    public partial class ExportForm : Form
    {
        private IFactory _factory;
        private bool _loading = false;

        private readonly BulkLogic.DataFormat[] formatChoices = new[]
        {
            BulkLogic.DataFormat.CSV,
            BulkLogic.DataFormat.TSV,
            BulkLogic.DataFormat.JSON,
            BulkLogic.DataFormat.XML,
        };

        public ExportForm()
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

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                DisableAllControls();

                DateTime? startDate = null;
                DateTime? endDate = null;
                BulkLogic.DataFormat format = formatChoices[FileTypeComboBox.SelectedIndex];

                if (RadioButtonDateRange.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(DateFrom.Text))
                        startDate = DateFrom.Value.Date;
                    if (!string.IsNullOrWhiteSpace(DateThrough.Text))
                        endDate = DateThrough.Value.Date;
                }

                using (var sw = new StreamWriter(FileNameTextBox.Text.Trim()))
                {
                    var logic = new BulkLogic(_factory);
                    sw.Write(logic.Export(startDate, endDate, format));
                }

                MessageBox.Show("Export successful",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while exporting the data: {ex.Message}",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void FileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            SetFileTypeBasedOnExtension(FileNameTextBox.Text.Trim());
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

        private void RadioButtonAllData_CheckedChanged(object sender, EventArgs e)
        {
            SetDateControlsState(false);
        }

        private void RadioButtonDateRange_CheckedChanged(object sender, EventArgs e)
        {
            SetDateControlsState(true);
        }

        private void SetDateControlsState(bool newState)
        {
            this.DateFrom.Enabled = newState;
            this.DateThrough.Enabled = newState;
        }

        private void DisableAllControls()
        {
            this.Enabled = false;
            this.Refresh();
        }
    }
}
