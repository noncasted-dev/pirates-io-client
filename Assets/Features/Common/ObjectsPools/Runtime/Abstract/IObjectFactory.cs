#region

using Cysharp.Threading.Tasks;
using UnityEngine;

#endregion

namespace Common.ObjectsPools.Runtime.Abstract
{
    public interface IObjectFactory<T>
    {
        UniTask PreloadAsync();
        void Unload();
        T Create(Vector2 position, float angle = 0f);
    }
}