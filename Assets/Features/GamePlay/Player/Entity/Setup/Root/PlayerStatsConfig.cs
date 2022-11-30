using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Setup.Root
{
    [DisallowMultipleComponent]
    public class PlayerStatsConfig : MonoBehaviour, IAwakeCallback
    {
        [Inject]
        private void Construct(
            IShipResourcesPresenter resources,
            IHealth health,
            IPlayerCargoStorage storage)
        {
            _storage = storage;
            _health = health;
            _resources = resources;
        }

        [SerializeField] private string _name = "Frigate";
        [SerializeField] private Sprite _icon;
        
        [SerializeField] private int _baseHealth;
        [SerializeField] private int _regenerationInTick = 30;

        [SerializeField] private float _baseMaxSpeed;

        [SerializeField] private int _baseMaxWeight;

        [SerializeField] private int _baseMaxCannons;
        [SerializeField] private int _baseCannons;

        [SerializeField] private int _baseTeam;
        [SerializeField] private int _baseMaxTeam;
        
        [SerializeField] private int _shallowDamage = 5;
        [SerializeField] private bool _isShallowIgnored;
        
        private IShipResourcesPresenter _resources;
        private IHealth _health;
        private IPlayerCargoStorage _storage;

        public void OnAwake()
        {
            if (_storage.Items.ContainsKey(ItemType.Cannon) == true)
                _resources.SetCannons(_storage.Items[ItemType.Cannon].Count);
            
            _health.SetMaxHealth(_baseHealth, _regenerationInTick);

            _resources.SetName(_name);
            _resources.SetIcon(_icon);
            
            _resources.SetMaxWeight(_baseMaxWeight);
            _resources.SetMaxSpeed(_baseMaxSpeed);

            _resources.SetMaxCannons(_baseMaxCannons);

            _resources.SetTeam(_baseTeam);
            _resources.SetMaxTeam(_baseMaxTeam);

            _resources.SetShallowIgnorance(_isShallowIgnored);
            _resources.SetShallowDamage(_shallowDamage);
        }
    }
}