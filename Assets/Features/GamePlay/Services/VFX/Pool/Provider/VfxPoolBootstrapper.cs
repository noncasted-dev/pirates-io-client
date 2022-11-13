using Common.ObjectsPools.Runtime;
using Cysharp.Threading.Tasks;
using Local.Services.Abstract.Callbacks;
using Local.Services.DependenciesResolve;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace GamePlay.Services.VFX.Pool.Provider
{
    public class VfxPoolBootstrapper : MonoBehaviour, ILocalAsyncAwakeListener, IDependencyResolver
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

        public void OnSceneLoaded(Scene targetScene)
        {
            _targetScene = targetScene;
        }
    }
}