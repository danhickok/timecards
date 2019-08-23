using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardsCore.Interfaces;

namespace TimecardsIOC
{
    public class Factory : IFactory, IDisposable
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
            return (TTypeToResolve)Resolve(typeof(TTypeToResolve));
        }

        private object Resolve(Type typeToResolve)
        {
            if (!_registry.ContainsKey(typeToResolve))
                throw new Exception($"Requested unregistered type {typeToResolve}");

            var registeredType = _registry[typeToResolve];

            object result;

            if (registeredType.IsSingleton)
            {
                if (_singletons.ContainsKey(typeToResolve))
                {
                    result = _singletons[typeToResolve];
                }
                else
                {
                    result = ObjectFromRegisteredType(registeredType);
                    _singletons[typeToResolve] = result;
                }
            }
            else
            {
                result = ObjectFromRegisteredType(registeredType);
            }

            return result;
        }

        private object ObjectFromRegisteredType(RegisteredType registeredType)
        {
            var parameterList = new List<object>();
            foreach (var constructorParameterType in registeredType.ConstructorParameterTypes)
                parameterList.Add(Resolve(constructorParameterType));

            return Activator.CreateInstance(registeredType.ConcreteType, parameterList.ToArray());
        }

        // nested class
        private class RegisteredType
        {
            public Type ConcreteType { get; set; }
            public bool IsSingleton { get; set; }
            public Type[] ConstructorParameterTypes { get; set; }
        }

        #region IDisposable Support
        private bool isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    var keys = _singletons.Keys.ToArray();
                    foreach (var key in keys)
                    {
                        if (_singletons[key] is IDisposable)
                            ((IDisposable)_singletons[key]).Dispose();
                        _singletons.Remove(key);
                    }
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
