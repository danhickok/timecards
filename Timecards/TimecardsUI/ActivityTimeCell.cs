using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimecardsUI
{
    public class ActivityTimeCell : DataGridViewTextBoxCell
    {
        public ActivityTimeCell() : base()
        {
        }

        public override object Clone()
        {
            var cloned = base.Clone() as ActivityTimeCell;

            return cloned;
        }

        public override void InitializeEditingControl(
           int rowIndex,
           object initialFormattedValue,
           DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            ActivityTimeControl ctl =
                DataGridView.EditingControl as ActivityTimeControl;
            if (this.Value == null)
            {
                ctl.Text = string.Empty;
            }
            else
            {
                ctl.Text = (string)this.Value;
            }
        }

        public override Type EditType => typeof(ActivityTimeControl);

        public override Type ValueType => typeof(string);

        public override object DefaultNewRowValue => string.Empty;
    }
}
