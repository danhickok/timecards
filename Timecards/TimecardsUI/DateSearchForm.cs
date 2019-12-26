using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TimecardsUI
{
    public partial class DateSearchForm : Form
    {
        public int SelectedTimecardID { get; private set; }
        public bool Canceled { get; private set; }
        public List<(int Key, DateTime Date)> TimecardList { get; set; }

        public DateSearchForm()
        {
            InitializeComponent();
            SelectedTimecardID = 0;
            Canceled = true;
            TimecardList = null;
        }

        private void DateSearchForm_Load(object sender, EventArgs e)
        {
            PopulateList();
        }

        private void PopulateList()
        {
            foreach (var item in TimecardList)
            {
                var entry = new ListViewItem
                {
                    Tag = item.Key.ToString(),
                    Text = item.Date.ToString("d"),
                };

                entry.SubItems.Add(new ListViewItem.ListViewSubItem
                {
                    Text = item.Date.DayOfWeek.ToString(),
                });

                DatesListView.Items.Add(entry);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DatesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SelectedTimecardID = int.Parse(e.Item.Tag.ToString());
            Canceled = false;
            Close();
        }
    }
}
