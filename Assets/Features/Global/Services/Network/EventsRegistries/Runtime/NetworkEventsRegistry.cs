using Features.GamePlay.Player.Entity.Network.Common.Events;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime;
using GamePlay.Services.DroppedObjects.Network.Runtime;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using Ragon.Client;

namespace Global.Services.Network.EventsRegistries.Runtime
{
    public class NetworkEventsRegistry
    {
        public void Register()
        {
            RagonNetwork.Event.Register<ProjectileInstantiateEvent>();
            RagonNetwork.Event.Register<DamageEvent>();
            RagonNetwork.Event.Register<ItemDropEvent>();
            RagonNetwork.Event.Register<ItemCollectedEvent>();
            RagonNetwork.Event.Register<HealthChangeNetworkEvent>();
            RagonNetwork.Event.Register<DeathNetworkEvent>();
            RagonNetwork.Event.Register<StartBoardingNetworkEvent>();
            RagonNetwork.Event.Register<StopBoardingNetworkEvent>();
        }
    }
}