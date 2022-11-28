using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime;
using Ragon.Client;

namespace GamePlay.Player.Entity.Network.Remote.BuildInvoker
{
    public class RemotePlayerBuildInvoker : RagonBehaviour
    {
        public override void OnCreatedEntity()
        {
            if (IsMine == true)
                return;

            var payload = Entity.GetSpawnPayload<PlayerPayload>();
            RemotePlayerBuilder.Instance.Build(gameObject, payload.ShipType, payload.Faction);
        }
    }
}