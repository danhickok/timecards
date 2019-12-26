using System;
using System.Collections.Generic;
using System.Linq;
using TimecardsCore.Interfaces;

namespace TimecardsIOC
{
    /// <summary>
    /// This is the inversion-of-control (IOC) container that makes it possible for an inner-layer object to retrieve
    /// an instance of an outer-layer class even though it knows only about the interface for that class
    /// </summary>
    public class Factory : IFactory, IDisposable
    {
        private readonly Dictionary<Type, RegisteredType> _registry;
        private readonly Dictionary<Type, object> _singletons;

        #region Constructor

        public Factory()
        {
            _registry = new Dictionary<Type, RegisteredType>();
            _singletons = new Dictionary<Type, object>();
        }

        #endregion

        /// <summary>
        /// Registers a concrete type for a given interface type
        /// </summary>
        /// <typeparam name="TTypeToResolve">Interface type to be fulfilled</typeparam>
        /// <param name="concreteType">Concrete (actual) type fulfilling the given interface type</param>
        /// <param name="isSingleton">True if the factory is to make only one of these contrete types</param>
        /// <param name="constructorParameterTypes">Interface types that serve as constructor parameters for this type;
        /// they must be resolvable by this factory</param>
        public void Register<TTypeToResolve>(Type concreteType, bool isSingleton = false, params Type[] constructorParameterTypes)
        {
            _registry[typeof(TTypeToResolve)] = new RegisteredType
            {
                ConcreteType = concreteType,
                IsSingleton = isSingleton,
                ConstructorParameterTypes = constructorParameterTypes,
            };
        }

        /// <summary>
        /// Returns an instance of the corresponding concrete type for the given interface type
        /// </summary>
        /// <typeparam name="TTypeToResolve">Interface type</typeparam>
        /// <returns>Instance of concrete type</returns>
        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)Resolve(typeof(TTypeToResolve));
        }

        #region Private methods and nested classes

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

        #endregion

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
