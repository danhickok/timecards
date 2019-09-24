namespace TimecardsCore.Interfaces
{
    public interface IFactory
    {
        T Resolve<T>();
        void Dispose();
    }
}
