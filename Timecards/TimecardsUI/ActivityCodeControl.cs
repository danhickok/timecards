using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimecardsUI
{
    public class ActivityCodeControl : MaskedTextBox, IDataGridViewEditingControl
    {
        public DataGridView EditingControlDataGridView { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object EditingControlFormattedValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int EditingControlRowIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool EditingControlValueChanged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Cursor EditingPanelCursor => throw new NotImplementedException();

        public bool RepositionEditingControlOnValueChange => throw new NotImplementedException();

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            throw new NotImplementedException();
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            throw new NotImplementedException();
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            throw new NotImplementedException();
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            throw new NotImplementedException();
        }
    }
}
