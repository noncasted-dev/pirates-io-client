using Ragon.Client;
using UnityEngine;

namespace Global.Services.Network.Connection.Runtime
{
    public class NetworkDisconnectHandler : MonoBehaviour, IRagonListener
    {
        private void OnEnable()
        {
            RagonNetwork.AddListener(this);
        }

        private void OnDisable()
        {
            RagonNetwork.RemoveListener(this);
        }

        public void OnAuthorized(string playerId, string playerName)
        {
        }

        public void OnJoined()
        {
        }

        public void OnFailed(string message)
        {
        }

        public void OnLeaved()
        {
        }

        public void OnConnected()
        {
        }

        public void OnDisconnected()
        {
        }

        public void OnPlayerJoined(RagonPlayer player)
        {
        }

        public void OnPlayerLeft(RagonPlayer player)
        {
        }

        public void OnOwnerShipChanged(RagonPlayer player)
        {
        }

        public void OnLevel(string sceneName)
        {
        }
    }
}