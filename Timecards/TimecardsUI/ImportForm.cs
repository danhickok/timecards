using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimecardsCore;
using TimecardsCore.Events;
using TimecardsCore.ExtensionMethods;
using TimecardsCore.Interfaces;
using TimecardsCore.Logic;

namespace TimecardsUI
{
    public partial class ImportForm : Form
    {
        private IFactory _factory;
        private bool _loading = false;
        private bool _running = false;
        private bool _canceled = false;
        private (int Current, int Goal) _progress;

        private readonly BulkLogic.DataFormat[] formatChoices = new[]
        {
            BulkLogic.DataFormat.CSV,
            BulkLogic.DataFormat.TSV,
            BulkLogic.DataFormat.JSON,
            BulkLogic.DataFormat.XML,
        };

        public ImportForm()
        {
            InitializeComponent();

            FileTypeComboBox.Items.Clear();
            var defaultFileType = 0;
            for (var i = 0; i < formatChoices.Length; ++i)
            {
                FileTypeComboBox.Items.Add(formatChoices[i].GetDescription());
                if (Configuration.ImportFileType == formatChoices[i].ToString())
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
            FileOpenDialog.ShowDialog();

            var path = FileOpenDialog.FileName;
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
            Configuration.ImportFileType = formatChoices[FileTypeComboBox.SelectedIndex].ToString();
            Configuration.Save();

            switch (formatChoices[FileTypeComboBox.SelectedIndex])
            {
                case BulkLogic.DataFormat.CSV:
                    FileOpenDialog.Filter =
                        "CSV files|*.csv|TSV files|*.tsv|JSON files|*.json|XML files|*.xml|All files|*.*";
                    break;
                case BulkLogic.DataFormat.TSV:
                    FileOpenDialog.Filter =
                        "TSV files|*.tsv|CSV files|*.csv|JSON files|*.json|XML files|*.xml|All files|*.*";
                    break;
                case BulkLogic.DataFormat.JSON:
                    FileOpenDialog.Filter =
                        "JSON files|*.json|CSV files|*.csv|TSV files|*.tsv|XML files|*.xml|All files|*.*";
                    break;
                case BulkLogic.DataFormat.XML:
                    FileOpenDialog.Filter =
                        "XML files|*.xml|CSV files|*.csv|TSV files|*.tsv|JSON files|*.json|All files|*.*";
                    break;
            }
        }

        private async void ImportButton_Click(object sender, EventArgs e)
        {
            if (EraseExistingDataCheckBox.Checked)
            {
                if (MessageBox.Show("Are you sure you want to erase all existing timecards?",
                    this.Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }

            DisableAllControlsExceptCancelButton();
            ShowProgressBar();

            _running = true;
            _canceled = false;

            BulkLogic.DataFormat format = formatChoices[FileTypeComboBox.SelectedIndex];

            try
            {
                if (EraseExistingDataCheckBox.Checked)
                {
                    var tcl = new TimecardLogic(_factory);
                    tcl.DeleteAllTimecards();
                }

                var logic = new BulkLogic(_factory);
                logic.ProgressUpdated += this.OnProgressUpdated;

                string content;
                using (var sr = new StreamReader(FileNameTextBox.Text.Trim()))
                {
                    content = sr.ReadToEnd();
                }

                ProgressTimer.Enabled = true;
                var result = await PerformImportAsync(logic, content, format);
                ProgressTimer.Enabled = false;

                if (string.IsNullOrWhiteSpace(result))
                {
                    CancelImportButton.Enabled = false;
                    Refresh();

                    MessageBox.Show("Import successful",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"A problem was encountered during the import:\n{result}",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while importing the data: {ex.Message}",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        private async Task<string> PerformImportAsync(BulkLogic logic, string content, BulkLogic.DataFormat format)
        {
            var result = await Task.Run(() => logic.Import(content, format));
            return result;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (_running)
            {
                _canceled = true;
            }
            else
            {
                this.Close();
            }
        }

        private void DisableAllControlsExceptCancelButton()
        {
            FileNameTextBox.Enabled = false;
            FileDialogButton.Enabled = false;
            FileTypeComboBox.Enabled = false;
            EraseExistingDataCheckBox.Enabled = false;
            ImportButton.Enabled = false;

            this.Refresh();

        }

        private void ShowProgressBar()
        {
            ImportProgressBar.Visible = true;
            ImportProgressBar.Enabled = true;

            this.Refresh();
        }

        private void OnProgressUpdated(object sender, ProgressUpdateEventArgs e)
        {
            _progress.Current = e.Current;
            _progress.Goal = e.Goal;

            e.Cancel = _canceled;
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            ImportProgressBar.Minimum = 0;
            ImportProgressBar.Maximum = _progress.Goal;
            ImportProgressBar.Value = _progress.Current;
        }
    }
}
