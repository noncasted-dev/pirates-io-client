namespace Common.ObjectsPools.Runtime.Abstract
{
    public interface IObjectReturner<T>
    {
        void Return(T poolObject);
    }
}