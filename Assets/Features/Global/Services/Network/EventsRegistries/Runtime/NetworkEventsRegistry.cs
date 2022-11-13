using GamePlay.Services.Projectiles.Replicator.Runtime;
using Ragon.Client;

namespace Global.Services.Network.EventsRegistries.Runtime
{
    public class NetworkEventsRegistry
    {
        public void Register()
        {
            RagonNetwork.Event.Register<ProjectileInstantiateEvent>();
        }
    }
}