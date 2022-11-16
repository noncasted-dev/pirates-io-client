#region

using Cysharp.Threading.Tasks;

#endregion

namespace Common.ObjectsPools.Runtime.Abstract
{
    public interface IObjectsPool
    {
        IObjectProvider<T> GetProvider<T>();
        UniTask PreloadAsync();
        void InstantiateStartupInstances();
        void Unload();
    }
}