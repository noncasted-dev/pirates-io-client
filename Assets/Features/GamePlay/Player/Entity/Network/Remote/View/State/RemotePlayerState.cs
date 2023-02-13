using System;
using Features.GamePlay.Player.Entity.Network.Common.Events;
using Ragon.Client;

namespace Features.GamePlay.Player.Entity.Network.Remote.View.State
{
    public class RemotePlayerState : RagonBehaviour, IRemoteState
    {
        public event Action Died;

        public override void OnCreatedEntity()
        {
            OnEvent<DeathNetworkEvent>(OnDiedNetwork);
        }

        private void OnDiedNetwork(RagonPlayer player, DeathNetworkEvent data)
        {
            Died?.Invoke();
        }
    }
}