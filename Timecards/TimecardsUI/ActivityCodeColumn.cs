using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimecardsUI
{
    public class ActivityCodeColumn : DataGridViewColumn
    {
        public ActivityCodeColumn() : base(new ActivityCodeCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(ActivityCodeCell)))
                {
                    throw new InvalidCastException("must be an ActivityCodeCell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
