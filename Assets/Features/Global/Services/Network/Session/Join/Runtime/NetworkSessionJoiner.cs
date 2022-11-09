using System;
using Cysharp.Threading.Tasks;
using Global.Services.Network.Session.Join.Logs;
using UnityEngine;
using VContainer;

namespace Global.Services.Network.Session.Join.Runtime
{
    [DisallowMultipleComponent]
    public class NetworkSessionJoiner : MonoBehaviour, INetworkSessionJoiner
    {
        [Inject]
        private void Construct(NetworkSessionJoinLogger joinLogger)
        {
            _joinLogger = joinLogger;
        }

        private NetworkSessionJoinLogger _joinLogger;

        public async UniTask<NetworkSessionJoinResultType> JoinRandom()
        {
            _joinLogger.OnAttempted();
            
            var attempt = new SessionJoinAttempt();

            var result = await attempt.Join();

            switch (result)
            {
                case NetworkSessionJoinResultType.Success:
                {
                    _joinLogger.OnSuccess();
                    return NetworkSessionJoinResultType.Success;
                }
                case NetworkSessionJoinResultType.Fail:
                {
                    _joinLogger.OnFailed(attempt.FailMessage);
                    return NetworkSessionJoinResultType.Fail;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}