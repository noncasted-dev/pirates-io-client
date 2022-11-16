using System;
using Cysharp.Threading.Tasks;
using Global.Services.Network.Session.Leave.Logs;
using Ragon.Client;
using UnityEngine;
using VContainer;

namespace Global.Services.Network.Session.Leave.Runtime
{
    public class NetworkSessionLeaver : MonoBehaviour, INetworkSessionLeaver
    {
        [Inject]
        private void Construct(NetworkSessionLeaveLogger joinLogger)
        {
            _logger = joinLogger;
        }

        private NetworkSessionLeaveLogger _logger;

        public async UniTask Leave()
        {
            _logger.OnAttempted();

            RagonNetwork.Session.Leave();

            _logger.OnSuccess();

            var attempt = new SessionLeaveAttempt();

            var result = await attempt.Leave();

            switch (result)
            {
                case NetworkSessionLeaveResultType.Success:
                {
                    _logger.OnSuccess();
                    break;
                }
                case NetworkSessionLeaveResultType.Fail:
                {
                    _logger.OnFailed(attempt.FailMessage);
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}