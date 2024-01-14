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
            _registry = [];
            _singletons = [];
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
            _registry[typeof(TTypeToResolve)] = new RegisteredType(concreteType, isSingleton, constructorParameterTypes);
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
            if (!_registry.TryGetValue(typeToResolve, out RegisteredType? registeredType))
                throw new Exception($"Requested unregistered type {typeToResolve}");

            object result;

            if (registeredType.IsSingleton)
            {
                if (_singletons.TryGetValue(typeToResolve, out object? value))
                {
                    result = value;
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

            var returnValue = Activator.CreateInstance(registeredType.ConcreteType, [.. parameterList]);

            return returnValue ?? throw new Exception($"Unable to instantiate requested type {registeredType}");
        }

        // nested class holds type and how to instantiate it
        private class RegisteredType(Type concreteType, bool isSingleton, Type[] constructorParameters)
        {
            public Type ConcreteType { get; set; } = concreteType;
            public bool IsSingleton { get; set; } = isSingleton;
            public Type[] ConstructorParameterTypes { get; set; } = constructorParameters;
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
                        if (_singletons[key] is IDisposable singleton)
                            singleton.Dispose();
                        _singletons.Remove(key);
                    }
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        #endregion
    }
}
