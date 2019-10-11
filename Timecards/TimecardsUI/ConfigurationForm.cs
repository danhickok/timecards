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

        public ConfigurationForm()
        {
            InitializeComponent();
            ConfigurationChanged = false;
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
            foreach (var key in Configuration.DefaultCodes.Keys)
            {
                var item = new ListViewItem { Text = key };
                item.SubItems.Add(Configuration.DefaultCodes[key]);
                DefaultCodesListView.Items.Add(item);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
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

            Configuration.Save();
            ConfigurationChanged = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ConfigurationChanged = false;
            this.Close();
        }

        private void MidnightTintPictureBox_Click(object sender, EventArgs e)
        {
            if (MidnightTintColorDialog.ShowDialog() == DialogResult.OK)
            {
                MidnightTintPictureBox.BackColor = MidnightTintColorDialog.Color;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            //TODO: add a new item to list (somehow)
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //TODO: remove selected item from list
        }
    }
}
