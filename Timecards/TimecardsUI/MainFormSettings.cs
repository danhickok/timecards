namespace TimecardsUI
{
    /// <summary>
    /// This class maintains the last known position of the main form and its grid column sizes
    /// </summary>
    public static class MainFormSettings
    {
        /// <summary>
        /// True if a previously known position of the form has been recorded
        /// </summary>
        public static bool HaveBeenSet
        {
            get
            {
                return Properties.Settings.Default.MainFormSettingsHaveBeenSet;
            }

            private set
            {
                Properties.Settings.Default.MainFormSettingsHaveBeenSet = value;
            }
        }

        /// <summary>
        /// Top of form
        /// </summary>
        public static int Top
        {
            get
            {
                return Properties.Settings.Default.MainFormTop;
            }

            set
            {
                Properties.Settings.Default.MainFormTop = value;
                HaveBeenSet = true;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Left edge of form
        /// </summary>
        public static int Left
        {
            get
            {
                return Properties.Settings.Default.MainFormLeft;
            }

            set
            {
                Properties.Settings.Default.MainFormLeft = value;
                HaveBeenSet = true;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Height of form
        /// </summary>
        public static int Height
        {
            get
            {
                return Properties.Settings.Default.MainFormHeight;
            }

            set
            {
                Properties.Settings.Default.MainFormHeight = value;
                HaveBeenSet = true;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Width of form
        /// </summary>
        public static int Width
        {
            get
            {
                return Properties.Settings.Default.MainFormWidth;
            }

            set
            {
                Properties.Settings.Default.MainFormWidth = value;
                HaveBeenSet = true;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Code column width
        /// </summary>
        public static int ColumnCodeWidth
        {
            get
            {
                return Properties.Settings.Default.ColumnCodeWidth;
            }

            set
            {
                Properties.Settings.Default.ColumnCodeWidth = value;
                HaveBeenSet = true;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Time column width
        /// </summary>
        public static int ColumnTimeWidth
        {
            get
            {
                return Properties.Settings.Default.ColumnTimeWidth;
            }

            set
            {
                Properties.Settings.Default.ColumnTimeWidth = value;
                HaveBeenSet = true;
                Properties.Settings.Default.Save();
            }
        }
    }
}
