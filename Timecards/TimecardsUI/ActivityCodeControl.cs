using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimecardsCore;

namespace TimecardsUI
{
    public class ActivityCodeControl : MaskedTextBox, IDataGridViewEditingControl
    {
        public ActivityCodeControl()
        {
            this.Mask = Configuration.CodeMask;
        }

        public DataGridView EditingControlDataGridView { get; set; }
        public int EditingControlRowIndex { get; set; }
        public bool EditingControlValueChanged { get; set; }
        public Cursor EditingPanelCursor => base.Cursor;
        public bool RepositionEditingControlOnValueChange => false;

        public object EditingControlFormattedValue
        {
            get => this.Text;
            set => this.Text = value.ToString();
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            return false;
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // nothing to do here
        }
    }
}
