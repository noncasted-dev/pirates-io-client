using System;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Services.PlayerCargos.Storage.Events;
using Global.Services.MessageBrokers.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public class ShipResources : IShipResources, IShipResourcesPresenter, IPlayerSwitchListener
    {
        public ShipResources(
            IHealth health,
            ISail sail)
        {
            _sail = sail;
            _health = health;
        }

        private readonly IHealth _health;
        private readonly ISail _sail;
        private int _cannons;

        private IDisposable _cargoChangedListener;
        private Sprite _icon;
        private bool _isIgnored;

        private int _maxCannons;

        private float _maxSpeed;

        private int _maxTeam;

        private int _maxWeight;

        private string _name;
        private int _shallowDamage;
        private int _speed;
        private int _team;
        private int _weight;

        public string Name => _name;
        public Sprite Icon => _icon;
        public int MaxHealth => _health.Max;
        public int Health => _health.Amount;
        public int Weight => _weight;
        public int MaxWeight => _maxWeight;

        public int MaxCannons => _maxCannons;
        public int Cannons => _cannons;

        public float MaxSpeed => _maxSpeed;
        public int Speed => _speed;

        public int MaxTeam => _maxTeam;
        public int Team => _team;
        public int Sail => _sail.Strength;
        public bool IsShallowIgnored => _isIgnored;
        public int ShallowDamage => _shallowDamage;
        public event Action<int, int> HealthChanged;
        public event Action<int, int> WeightChanged;
        public event Action<int, int> CannonsChanged;
        public event Action<int, int> SpeedChanged;
        public event Action<int, int> TeamChanged;

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetIcon(Sprite icon)
        {
            _icon = icon;
        }

        public void SetMaxWeight(int maxWeight)
        {
            _maxWeight = maxWeight;

            WeightChanged?.Invoke(_weight, _maxWeight);

            Msg.Publish(new ResourcesChangedEvent(this));
        }

        public void SetWeight(int weight)
        {
            _weight = weight;

            WeightChanged?.Invoke(_weight, _maxWeight);
            Msg.Publish(new ResourcesChangedEvent(this));
        }

        public void SetMaxCannons(int maxCannons)
        {
            _maxCannons = maxCannons;

            CannonsChanged?.Invoke(_cannons, _maxCannons);
            Msg.Publish(new ResourcesChangedEvent(this));
        }

        public void SetCannons(int cannons)
        {
            _cannons = cannons;

            CannonsChanged?.Invoke(_cannons, _maxCannons);
            Msg.Publish(new ResourcesChangedEvent(this));
        }

        public void SetMaxTeam(int maxTeam)
        {
            _maxTeam = maxTeam;

            TeamChanged?.Invoke(_team, _maxTeam);
            Msg.Publish(new ResourcesChangedEvent(this));
        }

        public void SetTeam(int team)
        {
            _team = team;

            TeamChanged?.Invoke(_team, _maxTeam);
            Msg.Publish(new ResourcesChangedEvent(this));
        }

        public void SetMaxSpeed(float maxSpeed)
        {
            _maxSpeed = maxSpeed;

            SpeedChanged?.Invoke(_speed, (int)_maxSpeed);
            Msg.Publish(new ResourcesChangedEvent(this));
        }

        public void SetSpeed(int speed)
        {
            _speed = speed;

            SpeedChanged?.Invoke(_speed, (int)_maxSpeed);
            Msg.Publish(new ResourcesChangedEvent(this));
        }

        public void SetShallowIgnorance(bool isIgnored)
        {
            _isIgnored = isIgnored;
        }

        public void SetShallowDamage(int damage)
        {
            _shallowDamage = damage;
        }

        public void OnEnabled()
        {
            _cargoChangedListener = Msg.Listen<CargoChangedEvent>(OnCargoChanged);
            _sail.Changed += OnSailChanged;
        }

        public void OnDisabled()
        {
            _cargoChangedListener?.Dispose();
            _sail.Changed -= OnSailChanged;
        }

        private void OnSailChanged()
        {
            Msg.Publish(new ResourcesChangedEvent(this));
        }

        private void OnCargoChanged(CargoChangedEvent data)
        {
            SetWeight(data.Weight);

            foreach (var (type, item) in data.Items)
                switch (type)
                {
                    case ItemType.Cannon:
                        SetCannons(item.Count);
                        break;
                    case ItemType.Team:
                        SetTeam(item.Count);
                        break;
                }
        }
    }
}