using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TimecardsCore;

// Reference for masked text box format characters:
// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.maskedtextbox.mask?view=netframework-4.8

namespace TimecardsUI
{
    public partial class ConfigurationForm : Form
    {
        public bool ConfigurationChanged { get; private set; }

        private readonly (int Minutes, string Description)[] _timeRoundingChoices = new[]
        {
            (1,  "nearest minute"),
            (5,  "nearest five minutes"),
            (6,  "nearest six minutes (tenth of hour)"),
            (12, "nearest 12 minutes (fifth of hour)"),
            (15, "nearest 15 minutes"),
            (30, "nearest half hour"),
            (60, "nearest hour"),
        };

        private ListViewItem _itemBeingEdited;

        public ConfigurationForm()
        {
            InitializeComponent();
            ConfigurationChanged = false;
            _itemBeingEdited = null;
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            TimeFormatComboBox.Items.Clear();
            foreach (var (Format, Description) in Constants.TimeFormatChoices)
                TimeFormatComboBox.Items.Add(Description);
            SetTimeFormatIndexFromConfiguration();

            RoundTimeComboBox.Items.Clear();
            foreach (var (Minutes, Description) in _timeRoundingChoices)
                RoundTimeComboBox.Items.Add(Description);
            SetTimeRoundingIndexFromConfiguration();

            CodeMaskTextBox.Text = Configuration.CodeMask;
            MidnightTintPictureBox.BackColor = Configuration.MidnightTint;

            DefaultCodesListView.Items.Clear();

            var keys = new List<string>();
            foreach (var key in Configuration.DefaultCodes.Keys)
                keys.Add(key);
            keys.Sort();

            foreach (var key in keys)
            {
                var item = new ListViewItem { Text = key };
                item.SubItems.Add(Configuration.DefaultCodes[key]);
                DefaultCodesListView.Items.Add(item);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            StopItemEdit();

            SetConfigurationFromTimeFormatIndex();
            SetConfigurationFromTimeRoundingIndex();

            Configuration.CodeMask = CodeMaskTextBox.Text;
            Configuration.MidnightTint = MidnightTintPictureBox.BackColor;

            Configuration.DefaultCodes.Clear();
            foreach (ListViewItem item in DefaultCodesListView.Items)
            {
                var code = item.Text;
                var description = item.SubItems[1].Text;
                if (!string.IsNullOrWhiteSpace(code) &&
                    !string.IsNullOrWhiteSpace(description))
                {
                    Configuration.DefaultCodes[code] = description;
                }
            }

            Configuration.Save();
            ConfigurationChanged = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            StopItemEdit();

            ConfigurationChanged = false;
            this.Close();
        }

        private void CodeMaskTextBox_Enter(object sender, EventArgs e)
        {
            StopItemEdit();
        }

        private void TimeFormatComboBox_Enter(object sender, EventArgs e)
        {
            StopItemEdit();
        }

        private void MidnightTintPictureBox_Click(object sender, EventArgs e)
        {
            StopItemEdit();

            if (MidnightTintColorDialog.ShowDialog() == DialogResult.OK)
            {
                MidnightTintPictureBox.BackColor = MidnightTintColorDialog.Color;
            }
        }

        private void DefaultCodesListView_Enter(object sender, EventArgs e)
        {
            StopItemEdit();
        }

        private void NewCodeTextBox_Enter(object sender, EventArgs e)
        {
            NewCodeTextBox.SelectAll();
        }

        private void NewDescriptionTextBox_Enter(object sender, EventArgs e)
        {
            NewDescriptionTextBox.SelectAll();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            StopItemEdit();

            var item = new ListViewItem(string.Empty);
            item.SubItems.Add(string.Empty);
            DefaultCodesListView.Items.Add(item);
            item.EnsureVisible();
            _itemBeingEdited = item;
            StartItemEdit();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            StopItemEdit();

            if (DefaultCodesListView.SelectedItems.Count == 0)
                return;
            var item = DefaultCodesListView.SelectedItems[0];
            DefaultCodesListView.Items.Remove(item);
        }

        private void StartItemEdit()
        {
            NewCodeTextBox.Text = _itemBeingEdited.Text;
            NewDescriptionTextBox.Text = _itemBeingEdited.SubItems[1].Text;

            NewCodeTextBox.Mask = CodeMaskTextBox.Text;

            var codeTop = DefaultCodesListView.Location.Y + _itemBeingEdited.Position.Y + 2;
            var codeLeft = DefaultCodesListView.Location.X + _itemBeingEdited.Position.X + 4;
            var codeWidth = DefaultCodesListView.Columns[0].Width - 8;

            var descriptionTop = codeTop;
            var descriptionLeft = codeLeft + DefaultCodesListView.Columns[0].Width;
            var descriptionWidth = DefaultCodesListView.Columns[1].Width - 8;

            NewCodeTextBox.Location = new Point(codeLeft, codeTop);
            NewDescriptionTextBox.Location = new Point(descriptionLeft, descriptionTop);

            NewCodeTextBox.Width = codeWidth;
            NewDescriptionTextBox.Width = descriptionWidth;

            NewCodeTextBox.Visible = true;
            NewDescriptionTextBox.Visible = true;

            NewCodeTextBox.Focus();
        }

        private void StopItemEdit()
        {
            if (_itemBeingEdited != null)
            {
                _itemBeingEdited.Text = NewCodeTextBox.Text;
                _itemBeingEdited.SubItems[1].Text = NewDescriptionTextBox.Text;

                _itemBeingEdited.Selected = true;
                _itemBeingEdited.Focused = true;
            }

            _itemBeingEdited = null;

            NewCodeTextBox.Visible = false;
            NewDescriptionTextBox.Visible = false;
        }

        private void SetTimeFormatIndexFromConfiguration()
        {
            int index = 0;

            for (var i = 0; i < Constants.TimeFormatChoices.Length; ++i)
            {
                if (Constants.TimeFormatChoices[i].Format == Configuration.TimeMask)
                {
                    index = i;
                    break;
                }
            }

            TimeFormatComboBox.SelectedIndex = index;
        }

        private void SetConfigurationFromTimeFormatIndex()
        {
            var index = TimeFormatComboBox.SelectedIndex;
            Configuration.TimeMask = Constants.TimeFormatChoices[index].Format;
            Configuration.TimeSeparator = (index == 2 || index == 3) ? '.' : ':';
            Configuration.Use24HourTime = (index == 1 || index == 3);
        }

        private void SetTimeRoundingIndexFromConfiguration()
        {
            int index = 0;

            for (var i = 0; i < _timeRoundingChoices.Length; ++i)
            {
                if (_timeRoundingChoices[i].Minutes == Configuration.RoundCurrentTimeToMinutes)
                {
                    index = i;
                    break;
                }
            }

            RoundTimeComboBox.SelectedIndex = index;
        }

        private void SetConfigurationFromTimeRoundingIndex()
        {
            var index = RoundTimeComboBox.SelectedIndex;
            Configuration.RoundCurrentTimeToMinutes = _timeRoundingChoices[index].Minutes;
        }
    }
}
