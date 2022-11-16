#region

using System;
using Ragon.Client;

#endregion

namespace Global.Services.Network.Common
{
    public class NetworkEvents : IRagonListener
    {
        public void OnAuthorized(string playerId, string playerName)
        {
            Authorized?.Invoke(playerId, playerName);
        }

        public void OnJoined()
        {
            Joined?.Invoke();
        }

        public void OnFailed(string message)
        {
            Failed?.Invoke(message);
        }

        public void OnLeaved()
        {
            Leaved?.Invoke();
        }

        public void OnConnected()
        {
            Connected?.Invoke();
        }

        public void OnDisconnected()
        {
            Disconnected?.Invoke();
        }

        public void OnPlayerJoined(RagonPlayer player)
        {
            PlayerJoined?.Invoke(player);
        }

        public void OnPlayerLeft(RagonPlayer player)
        {
            PlayerLeft?.Invoke(player);
        }

        public void OnOwnerShipChanged(RagonPlayer player)
        {
            OwnerShipChanged?.Invoke(player);
        }

        public void OnLevel(string sceneName)
        {
            LevelReceived?.Invoke(sceneName);
        }

        public event Action<string, string> Authorized;
        public event Action Joined;
        public event Action<string> Failed;
        public event Action Leaved;
        public event Action Connected;
        public event Action Disconnected;
        public event Action<RagonPlayer> PlayerJoined;
        public event Action<RagonPlayer> PlayerLeft;
        public event Action<RagonPlayer> OwnerShipChanged;
        public event Action<string> LevelReceived;

        public void Setup()
        {
            RagonNetwork.AddListener(this);
        }

        public void Dispose()
        {
            RagonNetwork.RemoveListener(this);
        }
    }
}