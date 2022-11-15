using System;
using Cysharp.Threading.Tasks;
using Global.Services.Common.Abstract;
using Global.Services.Network.Common;
using Global.Services.Network.Connection.Logs;
using Ragon.Client;
using UnityEngine;
using VContainer;

namespace Global.Services.Network.Connection.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RagonEntityManager))]
    public class NetworkConnector : MonoBehaviour, INetworkConnector, IGlobalAwakeListener
    {
        [Inject]
        private void Construct(
            NetworkConnectionConfigAsset config,
            NetworkConnectorLogger logger)
        {
            _logger = logger;
            _config = config;
        }

        [SerializeField] private RagonEntityManager _entityManager;

        private readonly NetworkEvents _events = new();

        private NetworkConnectionConfigAsset _config;
        private NetworkConnectorLogger _logger;

        public void OnAwake()
        {
            RagonNetwork.SetManager(_entityManager);
        }

        public async UniTask<NetworkConnectResultType> Connect(string userName)
        {
            _logger.OnAttempt(_config.Ip, _config.Port);

            var attempt = new ConnectionAttempt(_config.Ip, _config.Port);

            var result = await attempt.Connect(userName);

            switch (result)
            {
                case NetworkConnectResultType.Success:
                {
                    _logger.OnSuccess();
                    return NetworkConnectResultType.Success;
                }
                case NetworkConnectResultType.Fail:
                {
                    _logger.OnFailed(attempt.FailMessage);
                    return NetworkConnectResultType.Fail;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}