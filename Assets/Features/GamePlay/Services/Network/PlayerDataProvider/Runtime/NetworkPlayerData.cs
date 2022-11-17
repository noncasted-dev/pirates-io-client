using Ragon.Client;
using UnityEngine;

namespace GamePlay.Services.Network.PlayerDataProvider.Runtime
{
    public class NetworkPlayerData :
        MonoBehaviour,
        INetworkPlayerDataProvider
    {
        private const int _multiplier = 1000_000;
        private int _generatedCounter;

        private int _playerEntityId;

        public int GenerateUniqueId()
        {
            if (_generatedCounter >= _multiplier)
                _generatedCounter = 0;

            _generatedCounter++;

            var baseValue = (int)RagonNetwork.Room.LocalPlayer.PeerId * _multiplier;
            var id = baseValue + _generatedCounter;

            return id;
        }
    }
}