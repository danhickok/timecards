using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core = TimecardsCore.Models;

namespace TimecardsData
{
    public class Repository : IDisposable
    {
        private TimecardsContext _context = null;

        public Repository() : this("TimecardsDb")
        {
        }

        public Repository(string connectionStringName)
        {
            _context = new TimecardsContext(connectionStringName);
        }

        public core.Timecard GetTimecard(int ID)
        {
            var timecard = _context.Timecards
                .Where(t => t.ID == ID)
                .Select(t => t.ToCore())
                .FirstOrDefault();

            return timecard;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
