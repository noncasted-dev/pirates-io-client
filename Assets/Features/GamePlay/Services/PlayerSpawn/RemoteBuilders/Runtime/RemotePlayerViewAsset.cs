using Common.ObjectsPools.Runtime;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Paths;
using GamePlay.Player.Entity.Network.Remote.Bootstrap;
using GamePlay.Services.Projectiles.Factory;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    [CreateAssetMenu(fileName = "PoolEntry_RemotePlayerVew",
        menuName = GamePlayAssetsPaths.RemotePlayerBuilder + "View")]
    public class RemotePlayerViewAsset : PoolEntryAsset
    {
        public override string Name => "RemotePlayerView";

        public override IObjectsPool Create(IObjectResolver resolver, Transform parent)
        {
            var projectiles = resolver.Resolve<IProjectilesPoolProvider>();
            var instantiatorFactory = resolver.Resolve<IAssetInstantiatorFactory>();
            var logger = resolver.Resolve<ILogger>();

            var factory = new RemoteViewFactory(
                Reference,
                parent,
                projectiles,
                logger,
                instantiatorFactory);
            
            var provider = new ObjectProvider<PlayerRemoteView>(
                null,
                factory,
                StartupInstances,
                parent);
            return provider;
        }
    }
}