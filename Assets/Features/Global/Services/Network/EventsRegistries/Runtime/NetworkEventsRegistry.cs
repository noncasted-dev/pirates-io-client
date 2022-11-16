#region

using GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime;
using GamePlay.Services.ObjectDroppers.Network.Runtime;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using Ragon.Client;

#endregion

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
        }
    }
}