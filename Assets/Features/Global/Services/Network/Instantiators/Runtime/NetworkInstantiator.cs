using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Global.Services.Common.Abstract.Callbacks;
using Global.Services.Network.Instantiators.Logs;
using Ragon.Client;
using UnityEngine;
using VContainer;

namespace Global.Services.Network.Instantiators.Runtime
{
    public class NetworkInstantiator : MonoBehaviour, INetworkInstantiator, IGlobalAwakeListener
    {
        [Inject]
        private void Construct(NetworkInstantiatorLogger logger)
        {
            _logger = logger;
        }

        private static NetworkInstantiator _instance;

        private readonly Dictionary<int, UniTaskCompletionSource<GameObject>> _completions = new();
        private int _counter;
        private NetworkInstantiatorLogger _logger;

        public static NetworkInstantiator Instance => _instance;

        public void OnAwake()
        {
            _instance = this;
        }

        public async UniTask<T1> Instantiate<T1, T2>(GameObject prefab, Vector2 position, T2 payload)
            where T2 : NetworkPayload
            where T1 : class
        {
            _counter++;
            payload.SetData(_counter, position);

            _logger.OnRequested(position, prefab.name);

            _completions.Add(_counter, new UniTaskCompletionSource<GameObject>());

            RagonNetwork.Room.CreateEntity(prefab, payload);

            var result = await _completions[_counter].Task;

            if (result.TryGetComponent(out T1 searched) == false)
            {
                _logger.OnGetComponentException<T1>(result.name);
                return null;
            }

            return searched;
        }

        public void OnInstantiated(int id, GameObject instantiated)
        {
            _logger.OnReturned(id, instantiated.name);
            _completions[id].TrySetResult(instantiated);
        }
    }
}