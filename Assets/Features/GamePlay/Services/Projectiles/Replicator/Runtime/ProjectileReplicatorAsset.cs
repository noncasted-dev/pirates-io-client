using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Replicator.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "ProjectilesReplicator",
        menuName = GamePlayAssetsPaths.ProjectilesReplicator + "Service")]
    public class ProjectileReplicatorAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private ProjectileReplicatorConfigAsset _config;
        [SerializeField] [Indent] private ProjectileReplicator _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var replicator = Instantiate(_prefab);
            replicator.name = "ProjectilesReplicator";

            builder.RegisterComponent(replicator)
                .WithParameter(_config)
                .As<IProjectileReplicator>()
                .AsCallbackListener();

            serviceBinder.AddToModules(replicator);
        }
    }
}