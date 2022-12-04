using System;
using Ragon.Client;
using UnityEngine;

namespace Global.Services.Network.Connection.Runtime
{
    public class NetworkDisconnectHandler : MonoBehaviour, IRagonListener
    {
        [SerializeField] private GameObject _disconnectNotification;

        private void Awake()
        {
            _disconnectNotification.SetActive(false);
        }

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
            _disconnectNotification.SetActive(true);
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