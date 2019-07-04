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
        private readonly Dictionary<Type, RegisteredType> _registry;
        private readonly Dictionary<Type, object> _singletons;

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

            if (registeredType.IsSingleton)
            {
                if (_singletons.ContainsKey(typeof(TTypeToResolve)))
                {
                    result = (TTypeToResolve)_singletons[typeof(TTypeToResolve)];
                }
                else
                {
                    result = ObjectFromRegisteredType<TTypeToResolve>(registeredType);
                    _singletons[typeof(TTypeToResolve)] = result;
                }
            }
            else
            {
                result = ObjectFromRegisteredType<TTypeToResolve>(registeredType);
            }

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
