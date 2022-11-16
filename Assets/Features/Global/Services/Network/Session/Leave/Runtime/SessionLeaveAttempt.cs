using Cysharp.Threading.Tasks;
using Ragon.Client;

namespace Global.Services.Network.Session.Leave.Runtime
{
    public class SessionLeaveAttempt : IRagonListener
    {
        private readonly UniTaskCompletionSource<NetworkSessionLeaveResultType> _completion = new();

        private string _failMessage = string.Empty;

        public string FailMessage => _failMessage;

        public void OnLeaved()
        {
            _completion.TrySetResult(NetworkSessionLeaveResultType.Success);
        }

        public void OnFailed(string message)
        {
            _failMessage = message;

            _completion.TrySetResult(NetworkSessionLeaveResultType.Fail);
        }

        public void OnAuthorized(string playerId, string playerName)
        {
        }

        public void OnJoined()
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

        public async UniTask<NetworkSessionLeaveResultType> Leave()
        {
            RagonNetwork.AddListener(this);

            RagonNetwork.Session.Leave();

            var result = await _completion.Task;
            await UniTask.Yield();

            RagonNetwork.RemoveListener(this);

            return result;
        }
    }
}