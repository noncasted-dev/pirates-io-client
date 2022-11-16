#region

using System;
using Ragon.Client;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Network.Root.Runtime
{
    [DisallowMultipleComponent]
    public class PlayerNetworkRoot : RagonBehaviour, IPlayerEventSender, IPlayerEventListener
    {
        public bool IsLocal => Entity.IsMine;
        public int Id => Entity.Id;

        public void AddListener<TEvent>(Action<RagonPlayer, TEvent> callback) where TEvent : IRagonEvent, new()
        {
            OnEvent(callback);
        }

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