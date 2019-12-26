using System;
using System.Windows.Forms;

namespace TimecardsUI
{
    public class ActivityTimeColumn : DataGridViewColumn
    {
        public ActivityTimeColumn() : base(new ActivityTimeCell())
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
                    !value.GetType().IsAssignableFrom(typeof(ActivityTimeCell)))
                {
                    throw new InvalidCastException("must be an ActivityTimeCell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
