using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimecardsCore.Events;
using TimecardsCore.ExtensionMethods;
using TimecardsCore.Interfaces;
using TimecardsCore.Logic;

namespace TimecardsUI
{
    public partial class ImportForm : Form
    {
        private IFactory _factory;

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
            for (var i = 0; i < formatChoices.Length; ++i)
                FileTypeComboBox.Items.Add(formatChoices[i].GetDescription());
            FileTypeComboBox.SelectedIndex = 0;
        }

        public void SetFactory(IFactory factory)
        {
            _factory = factory;
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if (EraseExistingDataCheckBox.Checked)
            {
                if (MessageBox.Show("Are you sure you want to erase all existing timecards?",
                    this.Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }

            DisableAllControls();
            ShowProgressBar();

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

                var result = logic.Import(content, format);

                if (string.IsNullOrWhiteSpace(result))
                {
                    MessageBox.Show("Import successful",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"One or more problems were encountered with the import:\n{result}",
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisableAllControls()
        {
            this.Enabled = false;
            this.Refresh();

        }

        private void ShowProgressBar()
        {
            ImportProgressBar.Visible = true;
            this.Refresh();
        }

        private void OnProgressUpdated(object sender, ProgressUpdateEventArgs e)
        {
            ImportProgressBar.Minimum = 0;
            ImportProgressBar.Maximum = e.Goal;
            ImportProgressBar.Value = e.Current;

            this.Refresh();
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
            for (var i = 0; i < formatChoices.Length; ++i)
            {
                if (path.EndsWith("." + formatChoices[i].ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    FileTypeComboBox.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
