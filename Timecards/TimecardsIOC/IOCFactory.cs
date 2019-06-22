using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsIOC
{
    public class IOCFactory : TimecardsCore.Interfaces.IIOCFactory
    {
        // class member
        private static IOCFactory _self = null;

        // instance member
        private List<object> _registry;

        // private constructor
        private IOCFactory()
        {
            _registry = new List<object>();
        }

        // singleton
        public static IOCFactory Self()
        {
            if (_self == null)
                _self = new IOCFactory();
            return _self;
        }
    }
}
