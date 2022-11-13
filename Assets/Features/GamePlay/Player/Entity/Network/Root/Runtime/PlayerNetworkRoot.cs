using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Root.Runtime
{
    [DisallowMultipleComponent]
    public class PlayerNetworkRoot : RagonBehaviour, INetworkEventSender
    {
        public override void OnCreatedEntity()
        {
            var payload = Entity.GetSpawnPayload<PlayerPayload>();
            var userName = payload.UserName;

            if (IsMine == true)
                gameObject.name = "LocalPlayer";
            else
                gameObject.name = $"RemotePlayer_{userName}";
        }
    }
}