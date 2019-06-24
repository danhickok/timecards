using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsIOC
{
    public class Factory
    {
        private static Factory _self = null;
        private static Factory Self
        {
            get
            {
                if (_self == null)
                    _self = new Factory();
                return _self;
            }
        }

        private Dictionary<Type, RegisteredType> Registry { get; }
        private Dictionary<Type, object> Singletons { get; }

        private Factory()
        {
            Registry = new Dictionary<Type, RegisteredType>();
            Singletons = new Dictionary<Type, object>();
        }

        public static void Register<TTypeToResolve>(Type concreteType, bool isSingleton = false, params Type[] constructorParameterTypes)
        {
            Self.Registry[typeof(TTypeToResolve)] = new RegisteredType
            {
                ConcreteType = concreteType,
                IsSingleton = isSingleton,
                ConstructorParameterTypes = constructorParameterTypes,
            };
        }

        public static TTypeToResolve Resolve<TTypeToResolve>()
        {
            TTypeToResolve result = default;

            if (!Self.Registry.ContainsKey(typeof(TTypeToResolve)))
                throw new Exception($"Unregistered type {typeof(TTypeToResolve)}");

            var registeredType = Self.Registry[typeof(TTypeToResolve)];

            //TODO: if singleton, check Singletons; if there, return it; if not, create one, add it, and return it
            //TODO: if not singleton, create type and return it

            return result;
        }

        private TTypeToResolve ObjectFromRegisteredType<TTypeToResolve>(RegisteredType registeredType)
        {
            //TODO: make an object - do we need reflection?
            return default;
        }

        // nested class
        private class RegisteredType
        {
            public Type ConcreteType { get; set; }
            public bool IsSingleton { get; set; }
            public Type[] ConstructorParameterTypes { get; set; }
        }
    }
}
