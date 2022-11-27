using System;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Services.PlayerCargos.Storage.Events;
using UniRx;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public class ShipResources : IShipResources, IShipResourcesPresenter, ISwitchCallbacks
    {
        public ShipResources(IHealth health)
        {
            _health = health;
        }

        private readonly IHealth _health;

        private string _name;
        private Sprite _icon;

        private int _maxWeight;
        private int _weight;

        private int _maxCannons;
        private int _cannons;

        private int _maxSpeed;
        private int _speed;

        private int _maxTeam;
        private int _team;

        private IDisposable _cargoChangedListener;

        public string Name => _name;
        public Sprite Icon => _icon;
        public int MaxHealth => _health.Max;
        public int Health => _health.Amount;
        public int Weight => _weight;
        public int MaxWeight => _maxWeight;

        public int MaxCannons => _maxCannons;
        public int Cannons => _cannons;

        public int MaxSpeed => _maxSpeed;
        public int Speed => _speed;

        public int MaxTeam => _maxTeam;
        public int Team => _team;
        public event Action<int, int> HealthChanged;
        public event Action<int, int> WeightChanged;
        public event Action<int, int> CannonsChanged;
        public event Action<int, int> SpeedChanged;
        public event Action<int, int> TeamChanged;
        
        public void OnEnabled()
        {
            _cargoChangedListener = MessageBroker.Default.Receive<CargoChangedEvent>().Subscribe(OnCargoChanged);
        }
        
        public void OnDisabled()
        {
            _cargoChangedListener?.Dispose();
        }

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
            
            MessageBroker.Default.Publish(new ResourcesChangedEvent(this));
        }

        public void SetWeight(int weight)
        {
            _weight = weight;

            WeightChanged?.Invoke(_weight, _maxWeight);
            MessageBroker.Default.Publish(new ResourcesChangedEvent(this));
        }

        public void SetMaxCannons(int maxCannons)
        {
            _maxCannons = maxCannons;

            CannonsChanged?.Invoke(_cannons, _maxCannons);
            MessageBroker.Default.Publish(new ResourcesChangedEvent(this));
        }

        public void SetCannons(int cannons)
        {
            _cannons = cannons;

            CannonsChanged?.Invoke(_cannons, _maxCannons);
            MessageBroker.Default.Publish(new ResourcesChangedEvent(this));
        }

        public void SetMaxTeam(int maxTeam)
        {
            _maxTeam = maxTeam;

            TeamChanged?.Invoke(_team, _maxTeam);
            MessageBroker.Default.Publish(new ResourcesChangedEvent(this));
        }

        public void SetTeam(int team)
        {
            _team = team;
            
            TeamChanged?.Invoke(_team, _maxTeam);
            MessageBroker.Default.Publish(new ResourcesChangedEvent(this));
        }

        public void SetMaxSpeed(int maxSpeed)
        {
            _maxSpeed = maxSpeed;
            
            SpeedChanged?.Invoke(_speed, _maxSpeed);
            MessageBroker.Default.Publish(new ResourcesChangedEvent(this));
        }

        public void SetSpeed(int speed)
        {
            _maxSpeed = speed;
            
            SpeedChanged?.Invoke(_speed, _maxSpeed);
            MessageBroker.Default.Publish(new ResourcesChangedEvent(this));
        }
        
        private void OnCargoChanged(CargoChangedEvent data)
        {
            SetWeight(data.Weight);

            foreach (var (type, item) in data.Items)
            {
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
}