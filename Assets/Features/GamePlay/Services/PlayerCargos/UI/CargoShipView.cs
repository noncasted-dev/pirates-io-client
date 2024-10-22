﻿using System;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Services.Reputation.Runtime;
using Global.Services.MessageBrokers.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Services.PlayerCargos.UI
{
    [DisallowMultipleComponent]
    public class CargoShipView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _reputation;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _weight;
        [SerializeField] private TMP_Text _cannons;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _team;

        private IDisposable _healthListener;
        private IReputation _playerReputation;
        private IDisposable _reputationListener;

        private IShipResources _resources;

        private void OnEnable()
        {
            _healthListener = Msg.Listen<HealthChangeEvent>(OnHealthChanged);
            _reputationListener = Msg.Listen<ReputationChangedEvent>(OnReputationChanged);
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
        }

        public void Setup(
            IShipResources shipResources,
            IReputation reputation)
        {
            _playerReputation = reputation;
            _resources = shipResources;

            _name.text = _resources.Name;
            _icon.sprite = shipResources.Icon;

            _resources.WeightChanged += OnResourcesWeightChanged;
            _resources.CannonsChanged += OnResourcesCannonsChanged;
            _resources.TeamChanged += OnResourcesTeamChanged;
            _resources.SpeedChanged += OnSpeedChanged;

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
            SetStat(_cannons, _resources.Cannons, _resources.MaxCannons);
        }

        private void OnResourcesCannonsChanged(int current, int max)
        {
            SetStat(_cannons, _resources.Cannons, _resources.MaxCannons);
        }

        private void OnResourcesTeamChanged(int current, int max)
        {
            SetStat(_team, _resources.Team, _resources.MaxTeam);
        }

        private void OnSpeedChanged(int current, int max)
        {
            _speed.text = $"{current}/{max}";
        }

        private void SetStat(TMP_Text target, int resources, int max)
        {
            target.text = $"{resources}/{max}";
        }

        private void SetReputation()
        {
            _reputation.text = $"{_playerReputation.Value}";
        }

        private void SetWeight()
        {
            if (_resources.Weight > _resources.MaxWeight)
                _weight.text = $"<color=#91171b>{_resources.Weight}</color>/{_resources.MaxWeight}";
            else
                _weight.text = $"{_resources.Weight}/{_resources.MaxWeight}";
        }
    }
}