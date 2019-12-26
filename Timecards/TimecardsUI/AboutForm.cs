using System;
using System.Windows.Forms;

namespace TimecardsUI
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            LabelVersion.Text = "Version " + Application.ProductVersion;

            AuthorTextBox.Text = "This is an open source application available on Github";
        }
    }
}
