namespace TimecardsUI
{
    public static class MainFormSettings
    {
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
    }
}
