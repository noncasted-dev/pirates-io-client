using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Global.Services.Network.Instantiators.Runtime
{
    public interface INetworkInstantiator
    {
        public UniTask<T1> Instantiate<T1, T2>(GameObject prefab, Vector2 position, T2 payload)
            where T1 : class
            where T2 : NetworkPayload;
    }
}