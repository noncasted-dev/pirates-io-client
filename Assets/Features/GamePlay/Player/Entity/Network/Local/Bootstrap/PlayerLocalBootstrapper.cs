using GamePlay.Player.Entity.Network.Local.Replicators.Canons.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Network.Views.Transforms.Runtime;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Player.Entity.Network.Local.Bootstrap
{
    [DisallowMultipleComponent]
    public class PlayerLocalBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField] private NetworkTransform _transform;
        [SerializeField] private PlayerNetworkRoot _root;

        public void OnBuild(IContainerBuilder builder)
        {
            builder.RegisterComponent(_transform)
                .As<INetworkTransform>();

            builder.RegisterComponent(_root)
                .As<IPlayerEventSender>()
                .As<IPlayerEventListener>()
                .AsSelf();

            builder.Register<CannonReplicator>(Lifetime.Singleton)
                .As<ICannonReplicator>();
        }

        public void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
        }
    }
}