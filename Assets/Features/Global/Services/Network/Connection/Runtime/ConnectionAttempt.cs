using System;
using Cysharp.Threading.Tasks;
using Ragon.Client;
using UnityEngine;

namespace Global.Services.Network.Connection.Runtime
{
    public class ConnectionAttempt : IRagonListener
    {
        public ConnectionAttempt(string ip, ushort port, RagonSocketType socketType)
        {
            _ip = ip;
            _port = port;
            _socketType = socketType;
        }

        private readonly UniTaskCompletionSource<NetworkConnectResultType> _authorizationCompletion = new();

        private readonly UniTaskCompletionSource<NetworkConnectResultType> _connectionCompletion = new();

        private readonly string _ip;
        private readonly ushort _port;
        private readonly RagonSocketType _socketType;

        private string _failMessage = string.Empty;
        private bool _isConnected = false;

        public string FailMessage => _failMessage;

        public void OnConnected()
        {
            _connectionCompletion.TrySetResult(NetworkConnectResultType.Success);
        }
        
        public async UniTask<NetworkConnectResultType> Connect(string userName)
        {
            RagonNetwork.AddListener(this);

            switch (_socketType)
            {
                case RagonSocketType.UDP:
                    RagonNetwork.Connect(_ip, _port);
                    break;
                case RagonSocketType.WebSocket:
                    Debug.Log("Connect connect wss");
                    RagonNetwork.Connect($"ws://{_ip}", _port);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var result = await _connectionCompletion.Task;

            if (result == NetworkConnectResultType.Fail)
            {
                RagonNetwork.RemoveListener(this);

                return result;
            }

            _isConnected = true;

            RagonNetwork.Session.AuthorizeWithKey("defaultkey", userName, Array.Empty<byte>());

            result = await _authorizationCompletion.Task;
            await UniTask.Yield();

            RagonNetwork.RemoveListener(this);

            return result;
        }

        public void OnFailed(string message)
        {
            _failMessage = message;

            if (_isConnected == true)
                _authorizationCompletion.TrySetResult(NetworkConnectResultType.Fail);
            else
                _connectionCompletion.TrySetResult(NetworkConnectResultType.Fail);
        }

        public void OnAuthorized(string playerId, string playerName)
        {
            _authorizationCompletion.TrySetResult(NetworkConnectResultType.Success);
        }

        public void OnJoined()
        {
        }

        public void OnLeaved()
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