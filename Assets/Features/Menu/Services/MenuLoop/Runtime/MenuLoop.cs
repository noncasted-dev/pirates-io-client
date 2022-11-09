using System;
using Cysharp.Threading.Tasks;
using Global.Services.Network.Connection.Runtime;
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
            IMenuUI menuUI)
        {
            _menuUI = menuUI;
            _connector = connector;
        }

        private IDisposable _playEvent;
        private INetworkConnector _connector;
        private IMenuUI _menuUI;

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
                    var playFromMenu = new PlayFromMenuEvent();
                    MessageBroker.Default.Publish(playFromMenu);
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
    }
}