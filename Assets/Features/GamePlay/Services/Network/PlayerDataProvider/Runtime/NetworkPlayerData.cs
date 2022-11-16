#region

using UnityEngine;

#endregion

namespace GamePlay.Services.Network.PlayerDataProvider.Runtime
{
    public class NetworkPlayerData :
        MonoBehaviour,
        INetworkPlayerDataPresenter,
        INetworkPlayerDataProvider
    {
        private const int _multiplier = 1000_000;

        private int _playerEntityId;
        private int _generatedCounter;

        public void SetEntityId(int id)
        {
            _playerEntityId = id;
        }

        public int GenerateUniqueId()
        {
            if (_generatedCounter >= _multiplier)
                _generatedCounter = 0;

            _generatedCounter++;

            var baseValue = _playerEntityId * _multiplier;
            var id = baseValue + _generatedCounter;

            return id;
        }
    }
}