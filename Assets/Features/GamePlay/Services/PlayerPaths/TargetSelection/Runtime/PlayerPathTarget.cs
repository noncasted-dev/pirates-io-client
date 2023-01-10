using System;
using Common.Local.Services.Abstract.Callbacks;
using Features.GamePlay.Services.PlayerPaths.Builder.Runtime;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Services.Maps.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using Global.Services.MessageBrokers.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Features.GamePlay.Services.PlayerPaths.TargetSelection.Runtime
{
    [DisallowMultipleComponent]
    public class PlayerPathTarget : MonoBehaviour, ILocalSwitchListener
    {
        [SerializeField] private ShipType _maxAvailableShip;
        [SerializeField] private MapEntry _entry;

        [SerializeField] private Button _button;
        [SerializeField] private Image _selectedIcon;

        private IDisposable _spawnListener;
        private IDisposable _cancelListener;
        private IDisposable _buildListener;

        public void OnEnabled()
        {
            _spawnListener = Msg.Listen<PlayerSpawnedEvent>(OnPlayerSpawned);
            _cancelListener = Msg.Listen<PlayerPathCancelEvent>(OnCanceled);
            _buildListener = Msg.Listen<PlayerPathBuildEvent>(OnBuild);

            _button.onClick.AddListener(OnClicked);
            
            Disable();
        }

        public void OnDisabled()
        {
            _spawnListener?.Dispose();
            _cancelListener?.Dispose();
            _buildListener?.Dispose();

            _button.onClick.RemoveListener(OnClicked);
        }

        private void Enable()
        {
            _button.gameObject.SetActive(true);
            _selectedIcon.gameObject.SetActive(false);
        }

        private void Disable()
        {
            _button.gameObject.SetActive(false);
            _selectedIcon.gameObject.SetActive(false);
        }

        private void OnClicked()
        {
            Msg.Publish(new TargetRequestedEvent(_entry.Type));
        }

        private void OnPlayerSpawned(PlayerSpawnedEvent data)
        {
            if (data.ShipType < _maxAvailableShip)
                Enable();
            else
                Disable();
        }
        
        private void OnBuild(PlayerPathBuildEvent data)
        {
            if (data.Target != _entry.Type)
            {
                _selectedIcon.gameObject.SetActive(false);
                return;
            }

            _selectedIcon.gameObject.SetActive(true);
        }

        private void OnCanceled(PlayerPathCancelEvent data)
        {
            _selectedIcon.gameObject.SetActive(false);
        }
    }
}