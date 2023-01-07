using System;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.States.Respawns.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime.Health
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class HealthBar : MonoBehaviour
    {
        [Inject]
        private void Construct(
            IUpdater updater,
            IPlayerEntityProvider entityProvider)
        {
            _entityProvider = entityProvider;
            _update = new HealthUpdate(updater, _actual, _damage, _root.sizeDelta.x, _speed);
        }

        [SerializeField] private float _speed = 1f;

        [SerializeField] private RectTransform _actual;
        [SerializeField] private RectTransform _damage;
        [SerializeField] private RectTransform _root;
        private IPlayerEntityProvider _entityProvider;

        private IDisposable _healthListener;
        private IDisposable _respawnListener;

        private HealthUpdate _update;

        private void OnEnable()
        {
            _healthListener = Msg.Listen<HealthChangeEvent>(OnHealthChange);
            _respawnListener = Msg.Listen<PlayerRespawnedEvent>(OnPlayerRespawned);

            _update?.Start();

            if (_entityProvider == null || _entityProvider.Resources == null)
                return;

            var resources = _entityProvider.Resources;
            _update?.OnHealthChanged(resources.Health, resources.MaxHealth);
        }

        private void OnDisable()
        {
            _healthListener?.Dispose();
            _respawnListener?.Dispose();

            _update?.Stop();
        }

        private void OnHealthChange(HealthChangeEvent data)
        {
            _update?.OnHealthChanged(data.Current, data.Max);
            _update?.Start();
        }

        private void OnPlayerRespawned(PlayerRespawnedEvent data)
        {
            _update?.OnHealthChanged(data.Health, data.MaxHealth);
            _update?.Start();
        }
    }
}