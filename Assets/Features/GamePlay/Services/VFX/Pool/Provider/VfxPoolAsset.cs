using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace GamePlay.Services.VFX.Pool.Provider
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "VfxPool",
        menuName = GamePlayAssetsPaths.VFX + "Pool")]
    public class VfxPoolAsset : LocalServiceAsset
    {
        [SerializeField] private AssetReference _poolScene;
        [SerializeField] private VfxPoolBootstrapper _prefab;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var pool = Instantiate(_prefab);
            pool.name = "Pool_Vfx";

            serviceBinder.Register<VfxPoolProvider>()
                .As<IVfxPoolProvider>()
                .WithParameter<IPoolProvider>(pool.Handler);

            var scene = await sceneLoader.Load(new EmptySceneLoadData(_poolScene));
            SceneManager.MoveGameObjectToScene(pool.gameObject, scene.Instance.Scene);

            callbacksRegister.ListenLoopCallbacks(pool);
            callbacksRegister.ListenContainerCallbacks(pool);

            pool.OnSceneLoaded(scene.Instance.Scene);
        }
    }
}