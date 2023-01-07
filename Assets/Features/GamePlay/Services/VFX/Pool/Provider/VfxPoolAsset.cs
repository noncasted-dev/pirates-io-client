using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace GamePlay.Services.VFX.Pool.Provider
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "VfxPool",
        menuName = GamePlayAssetsPaths.VFX + "Pool")]
    public class VfxPoolAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private AssetReference _poolScene;
        [SerializeField] [Indent] private VfxPoolBootstrapper _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var pool = Instantiate(_prefab);
            pool.name = "Pool_Vfx";

            builder.Register<VfxPoolProvider>()
                .As<IVfxPoolProvider>()
                .WithParameter<IPoolProvider>(pool.Handler);

            var scene = await sceneLoader.Load(new EmptySceneLoadData(_poolScene));
            SceneManager.MoveGameObjectToScene(pool.gameObject, scene.Instance.Scene);

            callbacks.Listen(pool);

            pool.OnSceneLoaded(scene.Instance.Scene);
        }
    }
}