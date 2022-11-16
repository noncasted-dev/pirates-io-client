using System;
using Cysharp.Threading.Tasks;
using Ragon.Client;

namespace Global.Services.Network.Connection.Runtime
{
    public class ConnectionAttempt : IRagonListener
    {
        public ConnectionAttempt(string ip, ushort port)
        {
            _ip = ip;
            _port = port;
        }

        private readonly UniTaskCompletionSource<NetworkConnectResultType> _authorizationCompletion = new();

        private readonly UniTaskCompletionSource<NetworkConnectResultType> _connectionCompletion = new();

        private readonly string _ip;
        private readonly ushort _port;

        private string _failMessage = string.Empty;
        private bool _isConnected = false;

        public string FailMessage => _failMessage;

        public void OnConnected()
        {
            _connectionCompletion.TrySetResult(NetworkConnectResultType.Success);
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

        public async UniTask<NetworkConnectResultType> Connect(string userName)
        {
            RagonNetwork.AddListener(this);

            RagonNetwork.Connect(_ip, _port);

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
    }
}