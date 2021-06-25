using System;
using System.IO;
using System.Windows.Forms;
using TimecardsCore;
using TimecardsCore.ExtensionMethods;
using TimecardsCore.Interfaces;
using TimecardsCore.Logic;

namespace TimecardsUI
{
    public partial class ExportReportForm : Form
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

        private void ExportButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelExportButton_Click(object sender, EventArgs e)
        {

        }
    }
}
