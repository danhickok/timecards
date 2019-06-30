using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardsCore.Interfaces;

namespace TimecardsIOC
{
    public class Factory : IFactory
    {
        private Dictionary<Type, RegisteredType> _registry { get; }
        private Dictionary<Type, object> _singletons { get; }

        public Factory()
        {
            _registry = new Dictionary<Type, RegisteredType>();
            _singletons = new Dictionary<Type, object>();
        }

        public void Register<TTypeToResolve>(Type concreteType, bool isSingleton = false, params Type[] constructorParameterTypes)
        {
            _registry[typeof(TTypeToResolve)] = new RegisteredType
            {
                ConcreteType = concreteType,
                IsSingleton = isSingleton,
                ConstructorParameterTypes = constructorParameterTypes,
            };
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            TTypeToResolve result = default;

            if (!_registry.ContainsKey(typeof(TTypeToResolve)))
                throw new Exception($"Unregistered type {typeof(TTypeToResolve)}");

            var registeredType = _registry[typeof(TTypeToResolve)];

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
