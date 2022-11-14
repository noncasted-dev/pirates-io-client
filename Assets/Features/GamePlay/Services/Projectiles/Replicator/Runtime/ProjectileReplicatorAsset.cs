using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.Projectiles.Replicator.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "ProjectilesReplicator",
        menuName = GamePlayAssetsPaths.ProjectilesReplicator + "Service")]
    public class ProjectileReplicatorAsset : LocalServiceAsset
    {
        [SerializeField] private ProjectileReplicator _prefab;
        [SerializeField] private ProjectileReplicatorConfigAsset _config;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var replicator = Instantiate(_prefab);
            replicator.name = "ProjectilesReplicator";

            serviceBinder.RegisterComponent(replicator)
                .WithParameter<ProjectileReplicatorConfigAsset>(_config)
                .As<IProjectileReplicator>();

            serviceBinder.AddToModules(replicator);
            callbacksRegister.ListenLoopCallbacks(replicator);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            resolver.Resolve<IProjectileReplicator>();
        }
    }
}