using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.Projectiles.Factory;
using GamePlay.Services.Projectiles.Logs;
using GamePlay.Services.Projectiles.Mover;
using GamePlay.Services.Projectiles.Mover.Abstract;
using GamePlay.Services.Projectiles.Selector.Runtime;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace GamePlay.Services.Projectiles.Bootstrap
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "Projectiles",
        menuName = GamePlayAssetsPaths.Projectiles + "Service")]
    public class ProjectilesAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private ProjectilesLogSettings _logSettings;
        [SerializeField] [Indent] private ProjectilesMoverConfigAsset _moverConfig;
        [SerializeField] [Indent] private AssetReference _poolScene;
        [SerializeField] [Indent] private ProjectilesBootstrapper _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var pool = Instantiate(_prefab);
            pool.name = "Pool_Projectiles";
            var selector = pool.GetComponent<ProjectilesSelector>();

            builder.Register<ProjectilesPoolProvider>()
                .As<IProjectilesPoolProvider>()
                .WithParameter<IPoolProvider>(pool.Handler);

            builder.Register<ProjectilesLogger>()
                .WithParameter(_logSettings)
                .AsSelf();

            builder.Register<ProjectilesMover>()
                .WithParameter(_moverConfig)
                .As<IProjectilesMover>()
                .AsCallbackListener();

            builder.Register<ProjectilesSelectorInput>()
                .AsCallbackListener();

            builder.RegisterComponent(selector)
                .As<IProjectileSelector>();

            var scene = await sceneLoader.Load(new EmptySceneLoadData(_poolScene));
            SceneManager.MoveGameObjectToScene(pool.gameObject, scene.Instance.Scene);

            callbacks.Listen(pool);

            pool.OnSceneLoaded(scene.Instance.Scene);
        }
    }
}