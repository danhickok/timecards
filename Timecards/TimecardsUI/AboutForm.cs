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

            AuthorTextBox.Text =
@"Created by Dan Hickok

Source for this program is available at:
https://github.com/danhickok/timecards

NewtonSoft JSON library by James Newton-King
SQLite CodeFirst library by Marc Sallin
SQLite database engine, EF, and LINQ interfaces by SQLite Development Team
Entity Framework and other .NET components by Microsoft";

            AuthorTextBox.Select(0, 0);
            OKButton.Focus();
        }
    }
}
