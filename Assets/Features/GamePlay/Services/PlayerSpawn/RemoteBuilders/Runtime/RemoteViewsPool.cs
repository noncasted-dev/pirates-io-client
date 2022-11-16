#region

using Common.ObjectsPools.Runtime;
using Cysharp.Threading.Tasks;
using Local.Services.Abstract.Callbacks;
using Local.Services.DependenciesResolve;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

#endregion

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    [DisallowMultipleComponent]
    public class RemoteViewsPool : MonoBehaviour,
        ILocalAsyncAwakeListener,
        ILocalBootstrappedListener,
        IDependencyResolver
    {
        [SerializeField] private ObjectsPoolsHandler _handler;

        private Scene _targetScene;

        public ObjectsPoolsHandler Handler => _handler;

        public void Resolve(IObjectResolver resolver)
        {
            _handler.Setup(resolver, _targetScene);
        }

        public async UniTask OnAwakeAsync()
        {
            await _handler.Prepare();
        }

        public void OnBootstrapped()
        {
            _handler.InstantiateStartupInstances();
        }

        public void OnSceneLoaded(Scene targetScene)
        {
            _targetScene = targetScene;
        }
    }
}