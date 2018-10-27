using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimecardsCore;

// I used the pattern set by this example from Microsoft:
// https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-host-controls-in-windows-forms-datagridview-cells

namespace TimecardsUI
{
    public class ActivityCodeCell : DataGridViewTextBoxCell
    {
        public ActivityCodeCell() : base()
        {
        }

        public override void InitializeEditingControl(
            int rowIndex,
            object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            ActivityCodeControl ctl =
                DataGridView.EditingControl as ActivityCodeControl;
            if (this.Value == null)
            {
                ctl.Text = string.Empty;
            }
            else
            {
                ctl.Text = (string)this.Value;
            }
        }

        public override Type EditType => typeof(ActivityCodeControl);

        public override Type ValueType => typeof(string);

        public override object DefaultNewRowValue => string.Empty;
    }
}
