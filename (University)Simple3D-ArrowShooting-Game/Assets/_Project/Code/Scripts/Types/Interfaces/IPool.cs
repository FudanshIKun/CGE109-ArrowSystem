namespace Wonderland.Types
{
    public interface IPool<T>
    {
        T Pull();
        void Push(T t);
    }
}
