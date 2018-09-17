using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore
{
    public static class Configuration
    {
        public static int RoundTimeToMinutes { get; set; }
        public static string TicketNumberMask { get; set; }

        public static Dictionary<string, string> DefaultCodes { get; private set; }

        static Configuration()
        {
            DefaultCodes = new Dictionary<string, string>();
        }

        public static void Load()
        {
            //TODO: call something to load settings
        }

        public static void Save()
        {
            //TODO: call something to save settings
        }
    }
}
