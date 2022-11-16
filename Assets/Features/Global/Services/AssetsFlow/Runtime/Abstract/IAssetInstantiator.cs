using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Global.Services.AssetsFlow.Runtime.Abstract
{
    public interface IAssetInstantiator<T>
    {
        UniTask PreloadAsync();
        UniTask<T> InstantiateAsync(Vector2 position);
        T Instantiate(Vector2 position, float angle = 0f, Transform parent = null);
        void Release();
    }
}