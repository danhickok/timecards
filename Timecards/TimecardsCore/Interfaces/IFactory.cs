using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Interfaces
{
    public interface IFactory
    {
        T Resolve<T>();
        void Dispose();
    }
}
