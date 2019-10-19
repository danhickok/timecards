using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimecardsCore;

namespace TimecardsUI
{
    public partial class ConfigurationForm : Form
    {
        public bool ConfigurationChanged { get; private set; }

        private ListViewItem _itemBeingEdited;

        public ConfigurationForm()
        {
            InitializeComponent();
            ConfigurationChanged = false;
            _itemBeingEdited = null;
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            CodeMaskTextBox.Text = Configuration.CodeMask;
            TimeMaskTextBox.Text = Configuration.TimeMask;
            MidnightTintPictureBox.BackColor = Configuration.MidnightTint;

            switch (Configuration.RoundCurrentTimeToMinutes)
            {
                case 1:
                    RoundTimeComboBox.SelectedIndex = 0;
                    break;
                case 5:
                    RoundTimeComboBox.SelectedIndex = 1;
                    break;
                case 6:
                    RoundTimeComboBox.SelectedIndex = 2;
                    break;
                case 12:
                    RoundTimeComboBox.SelectedIndex = 3;
                    break;
                case 15:
                    RoundTimeComboBox.SelectedIndex = 4;
                    break;
                case 30:
                    RoundTimeComboBox.SelectedIndex = 5;
                    break;
                case 60:
                    RoundTimeComboBox.SelectedIndex = 6;
                    break;
                default:
                    RoundTimeComboBox.SelectedIndex = 0;
                    break;
            }

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

            Configuration.CodeMask = CodeMaskTextBox.Text;
            Configuration.TimeMask = TimeMaskTextBox.Text;
            Configuration.MidnightTint = MidnightTintPictureBox.BackColor;

            switch (RoundTimeComboBox.SelectedIndex)
            {
                case 0:
                    Configuration.RoundCurrentTimeToMinutes = 1;
                    break;
                case 1:
                    Configuration.RoundCurrentTimeToMinutes = 5;
                    break;
                case 2:
                    Configuration.RoundCurrentTimeToMinutes = 6;
                    break;
                case 3:
                    Configuration.RoundCurrentTimeToMinutes = 12;
                    break;
                case 4:
                    Configuration.RoundCurrentTimeToMinutes = 15;
                    break;
                case 5:
                    Configuration.RoundCurrentTimeToMinutes = 30;
                    break;
                case 6:
                    Configuration.RoundCurrentTimeToMinutes = 60;
                    break;
                default:
                    Configuration.RoundCurrentTimeToMinutes = 1;
                    break;
            }

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

        private void TimeMaskTextBox_Enter(object sender, EventArgs e)
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
    }
}
