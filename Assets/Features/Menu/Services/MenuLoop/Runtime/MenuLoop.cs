using System;
using Cysharp.Threading.Tasks;
using Global.Services.Network.Connection.Runtime;
using Global.Services.Network.Session.Join.Runtime;
using Local.Services.Abstract.Callbacks;
using Menu.Services.UI.Runtime;
using UniRx;
using UnityEngine;
using VContainer;

namespace Menu.Services.MenuLoop.Runtime
{
    [DisallowMultipleComponent]
    public class MenuLoop : MonoBehaviour, ILocalLoadCallbackListener, IMenuLoop
    {
        [Inject]
        private void Construct(
            INetworkConnector connector,
            INetworkSessionJoiner sessionJoiner,
            IMenuUI menuUI)
        {
            _sessionJoiner = sessionJoiner;
            _menuUI = menuUI;
            _connector = connector;
        }

        private IDisposable _playEvent;
        private INetworkConnector _connector;
        private IMenuUI _menuUI;
        private INetworkSessionJoiner _sessionJoiner;

        private void OnEnable()
        {
            _playEvent = MessageBroker.Default.Receive<PlayClickedEvent>().Subscribe(OnPlayClicked);
        }

        private void OnDisable()
        {
            _playEvent.Dispose();
        }

        public void OnLoaded()
        {
            _menuUI.OnLogin();
        }

        private void OnPlayClicked(PlayClickedEvent data)
        {
            TryConnect(data.Name).Forget();
        }

        private async UniTaskVoid TryConnect(string userName)
        {
            _menuUI.OnLoading();

            var result = await _connector.Connect(userName);

            switch (result)
            {
                case NetworkConnectResultType.Success:
                {
                    TryJoin().Forget();
                    break;
                }
                case NetworkConnectResultType.Fail:
                {
                    _menuUI.OnLoginWithError("Connection error");
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        private async UniTaskVoid TryJoin()
        {
            var result = await _sessionJoiner.JoinRandom();

            switch (result)
            {
                case NetworkSessionJoinResultType.Success:
                {
                    var playFromMenu = new PlayFromMenuEvent();
                    MessageBroker.Default.Publish(playFromMenu);
                    break;
                }
                case NetworkSessionJoinResultType.Fail:
                {
                    _menuUI.OnLoginWithError("Session join error");
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