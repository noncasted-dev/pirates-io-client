using System;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Services.Reputation.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Views
{
    [DisallowMultipleComponent]
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _reputation;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _weight;
        [SerializeField] private TMP_Text _cannons;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _team;

        private IDisposable _healthListener;
        private IDisposable _reputationListener;

        private IShipResources _resources;
        
        private void OnEnable()
        {
            _healthListener = MessageBroker.Default.Receive<HealthChangedEvent>().Subscribe(OnHealthChanged);
            _reputationListener = MessageBroker.Default.Receive<ReputationChangedEvent>().Subscribe(OnReputationChanged);
        }

        private void OnDisable()
        {
            _healthListener?.Dispose();
            _reputationListener?.Dispose();
            
            if (_resources == null)
                return;
            
            _resources.WeightChanged += OnWeightChanged;
            _resources.CannonsChanged += OnCannonsChanged;
            _resources.TeamChanged += OnTeamChanged;
            _resources.SpeedChanged += OnSpeedChanged;
        }

        public void Setup(IShipResources shipResources, IReputation reputation)
        {
            _resources = shipResources;
            
            _name.text = _resources.Name;
            _icon.sprite = shipResources.Icon;

            _resources.WeightChanged += OnWeightChanged;
            _resources.CannonsChanged += OnCannonsChanged;
            _resources.TeamChanged += OnTeamChanged;
            _resources.SpeedChanged += OnSpeedChanged;
            
            _reputation.text = $"{reputation.Value}";
            _health.text = $"{shipResources.Health}/{shipResources.MaxHealth}";
            _weight.text = $"{shipResources.Weight}/{shipResources.MaxWeight}";
            _cannons.text = $"{shipResources.Cannons}/{shipResources.MaxCannons}";
            _team.text = $"{shipResources.Team}/{shipResources.MaxTeam}";
            _speed.text = $"{shipResources.Speed}/{shipResources.MaxSpeed}";
        }

        private void OnHealthChanged(HealthChangedEvent data)
        {
            _health.text = $"{data.Current}/{data.Max}";
        }

        private void OnReputationChanged(ReputationChangedEvent data)
        {
            _reputation.text = $"{data.Reputation}";
        }

        private void OnWeightChanged(int current, int max)
        {
            _weight.text = $"{current}/{max}";
        }
        
        private void OnCannonsChanged(int current, int max)
        {
            _cannons.text = $"{current}/{max}";
        }
        
        private void OnTeamChanged(int current, int max)
        {
            _team.text = $"{current}/{max}";
        }
        
        private void OnSpeedChanged(int current, int max)
        {
            _speed.text = $"{current}/{max}";
        }
    }
}