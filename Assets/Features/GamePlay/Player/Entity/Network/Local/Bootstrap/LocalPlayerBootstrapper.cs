using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Network.Local.Replicators.Canons.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Network.Views.Transforms.Runtime;
using GamePlay.Player.Entity.Setup.Bootstrap;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Local.Bootstrap
{
    [DisallowMultipleComponent]
    public class LocalPlayerBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField] private NetworkTransform _transform;
        [SerializeField] private LocalPlayerRoot _root;

        public void OnBuild(IDependencyRegister builder)
        {
            builder.RegisterComponent(_transform)
                .As<INetworkTransform>();

            builder.RegisterComponent(_root)
                .As<IPlayerEventSender>()
                .As<IPlayerEventListener>()
                .AsSelf();

            builder.Register<CannonReplicator>()
                .As<ICannonReplicator>();
        }
    }
}