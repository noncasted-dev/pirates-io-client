using Features.GamePlay.Player.Entity.Network.Common.Events;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Services.Network.RemoteEntities.Storage;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Boardings.Local
{
    public class BoardingDefence : IPlayerSwitchListener
    {
        public BoardingDefence(
            IPlayerEventListener listener,
            IRemotePlayersRegistry remotePlayersRegistry,
            BoardingConfigAsset config)
        {
            _listener = listener;
            _remotePlayersRegistry = remotePlayersRegistry;
            _config = config;
        }
        
        private readonly IPlayerEventListener _listener;
        private readonly IRemotePlayersRegistry _remotePlayersRegistry;
        private readonly BoardingConfigAsset _config;

        public void OnEnabled()
        {
            _listener.AddListener<StartBoardingNetworkEvent>(OnBoardingStarted);
        }

        public void OnDisabled()
        {
        }

        private void OnBoardingStarted(RagonPlayer player, StartBoardingNetworkEvent data)
        {
            if (_remotePlayersRegistry.TryGet(player.Id, out var opponent) == false)
            {
                Debug.LogError($"No boarding opponent found with id: {player.Id}");
                return;
            }
            
            
        }
    }
}