using System;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Services.Reputation.Runtime;
using Global.Services.MessageBrokers.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Views
{
    [DisallowMultipleComponent]
    public class PortShipView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _reputation;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _weight;
        [SerializeField] private TMP_Text _cannons;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _team;

        [SerializeField] private TradeHandler _handler;

        private int _tradeWeight;
        private int _tradeCannons;
        private int _tradeTeam;
        private int _tradeReputation;

        private IDisposable _healthListener;
        private IDisposable _reputationListener;

        private IShipResources _resources;
        private IReputation _playerReputation;

        private void OnEnable()
        {
            _healthListener = Msg.Listen<HealthChangeEvent>(OnHealthChanged);
            _reputationListener =
                Msg.Listen<ReputationChangedEvent>(OnReputationChanged);
        }

        private void OnDisable()
        {
            _healthListener?.Dispose();
            _reputationListener?.Dispose();

            if (_resources == null)
                return;

            _resources.WeightChanged -= OnResourcesWeightChanged;
            _resources.CannonsChanged -= OnResourcesCannonsChanged;
            _resources.TeamChanged -= OnResourcesTeamChanged;
            _resources.SpeedChanged -= OnSpeedChanged;
            _handler.WeightUpdated -= OnTradeWeightChanged;
            _handler.CannonsUpdated -= OnTradeCannonsChanged;
            _handler.TeamUpdated -= OnTradeTeamChanged;
            _handler.ReputationUpdated -= OnTradeReputationChanged;
        }

        public void Setup(IShipResources shipResources, IReputation reputation)
        {
            _playerReputation = reputation;
            _resources = shipResources;

            _name.text = _resources.Name;
            _icon.sprite = shipResources.Icon;

            _resources.WeightChanged += OnResourcesWeightChanged;
            _resources.CannonsChanged += OnResourcesCannonsChanged;
            _resources.TeamChanged += OnResourcesTeamChanged;
            _resources.SpeedChanged += OnSpeedChanged;

            _handler.WeightUpdated += OnTradeWeightChanged;
            _handler.CannonsUpdated += OnTradeCannonsChanged;
            _handler.TeamUpdated += OnTradeTeamChanged;
            _handler.ReputationUpdated += OnTradeReputationChanged;

            ResetStats();

        }

        public void ResetStats()
        {
            _reputation.text = $"{_playerReputation.Value}";
            _health.text = $"{_resources.Health}/{_resources.MaxHealth}";
            _weight.text = $"{_resources.Weight}/{_resources.MaxWeight}";
            _cannons.text = $"{_resources.Cannons}/{_resources.MaxCannons}";
            _team.text = $"{_resources.Team}/{_resources.MaxTeam}";
            _speed.text = $"{_resources.Speed}/{_resources.MaxSpeed}";

            _tradeCannons = 0;
            _tradeReputation = 0;
            _tradeTeam = 0;
            _tradeWeight = 0;
        }

        private void OnHealthChanged(HealthChangeEvent data)
        {
            SetWeight();
        }

        private void OnReputationChanged(ReputationChangedEvent data)
        {
            SetReputation();
        }

        private void OnResourcesWeightChanged(int current, int max)
        {
            SetStat(_cannons, _resources.Cannons, _resources.MaxCannons, _tradeCannons);
        }

        private void OnResourcesCannonsChanged(int current, int max)
        {
            SetStat(_cannons, _resources.Cannons, _resources.MaxCannons, _tradeCannons);
        }

        private void OnResourcesTeamChanged(int current, int max)
        {
            SetStat(_team, _resources.Team, _resources.MaxTeam, _tradeTeam);
        }

        private void OnSpeedChanged(int current, int max)
        {
            _speed.text = $"{current}/{max}";
        }

        private void OnTradeWeightChanged(int weight)
        {
            _tradeWeight = weight;

            SetWeight();
        }

        private void OnTradeCannonsChanged(int cannons)
        {
            _tradeCannons = cannons;

            SetStat(_cannons, _resources.Cannons, _resources.MaxCannons, cannons);
        }

        private void OnTradeTeamChanged(int team)
        {
            _tradeTeam = team;

            SetStat(_team, _resources.Team, _resources.MaxTeam, team);
        }

        private void OnTradeReputationChanged(int reputation)
        {
            _tradeReputation = reputation;

            SetReputation();
        }

        private void SetStat(TMP_Text target, int resources, int max, int additional)
        {
            if (additional > 0)
                target.text = $"{resources} + <color=#2c9958>{additional}</color>/{max}";
            else
                target.text = $"{resources}/{max}";
        }

        private void SetReputation()
        {
            if (_tradeReputation > 0)
                _reputation.text = $"{_playerReputation.Value} + <color=#2c9958>{_tradeReputation}</color>";
            else
                _reputation.text = $"{_playerReputation.Value}";
        }

        private void SetWeight()
        {
            var current = "";

            if (_tradeWeight > 0)
                current = $"{_resources.Weight} + {_tradeWeight}";
            else
                current = $"{_resources.Weight}";

            if (_tradeWeight + _resources.Weight > _resources.MaxWeight)
                _weight.text = $"<color=#91171b>{current}</color>/{_resources.MaxWeight}";
            else
                _weight.text = $"{current}/{_resources.MaxWeight}";
        }
    }
}