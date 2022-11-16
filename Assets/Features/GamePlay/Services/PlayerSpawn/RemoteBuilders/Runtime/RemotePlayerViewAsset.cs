#region

using Common.ObjectsPools.Runtime;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Paths;
using GamePlay.Player.Entity.Network.Remote.Bootstrap;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    [CreateAssetMenu(fileName = "PoolEntry_RemotePlayerVew",
        menuName = GamePlayAssetsPaths.RemotePlayerBuilder + "View")]
    public class RemotePlayerViewAsset : PoolEntryAsset
    {
        public override IObjectsPool Create(IObjectResolver resolver, Transform parent)
        {
            var instantiatorFactory = resolver.Resolve<IAssetInstantiatorFactory>();

            var factory = new RemoteViewFactory(
                Reference,
                parent,
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