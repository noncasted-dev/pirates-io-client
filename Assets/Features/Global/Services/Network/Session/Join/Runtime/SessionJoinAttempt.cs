#region

using Cysharp.Threading.Tasks;
using Ragon.Client;

#endregion

namespace Global.Services.Network.Session.Join.Runtime
{
    public class SessionJoinAttempt : IRagonListener
    {
        private readonly UniTaskCompletionSource<NetworkSessionJoinResultType> _completion = new();

        private string _failMessage = string.Empty;

        public string FailMessage => _failMessage;

        public void OnLevel(string sceneName)
        {
            _completion.TrySetResult(NetworkSessionJoinResultType.Success);
        }

        public void OnFailed(string message)
        {
            _failMessage = message;

            _completion.TrySetResult(NetworkSessionJoinResultType.Fail);
        }

        public async UniTask<NetworkSessionJoinResultType> Join()
        {
            RagonNetwork.AddListener(this);

            RagonNetwork.Session.CreateOrJoin("game", 1, 300);

            var result = await _completion.Task;
            await UniTask.Yield();

            RagonNetwork.RemoveListener(this);

            return result;
        }

        #region Unused

        public void OnAuthorized(string playerId, string playerName)
        {
        }

        public void OnJoined()
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

        #endregion
    }
}