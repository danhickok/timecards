namespace TimecardsCore.Interfaces
{
    /// <summary>
    /// The Factory class is the inversion-of-control (IOC) container that provides access to objects from
    /// other application layers without requiring a reference to that layer
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Returns a new object or a singleton corresponding to the given interface type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// Releases all references to singletons held by this object
        /// </summary>
        void Dispose();
    }
}
