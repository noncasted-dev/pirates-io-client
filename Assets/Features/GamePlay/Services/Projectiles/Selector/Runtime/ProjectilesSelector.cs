using System;
using System.Collections.Generic;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Events;
using UniRx;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Selector.Runtime
{
    public class ProjectilesSelector : MonoBehaviour, IProjectileSelector
    {
        private readonly Dictionary<ProjectileType, int> _projectiles = new();

        private ProjectileType _selected = ProjectileType.Ball;
        
        private IDisposable _cargoListener;

        public ProjectileType Selected => _selected;
        
        private void Awake()
        {
            _projectiles[ProjectileType.Fishnet] = 999999;
        }

        private void OnEnable()
        {
            _cargoListener = MessageBroker.Default.Receive<CargoChangedEvent>().Subscribe(OnCargoChanged);
        }

        private void OnDisable()
        {
            _cargoListener?.Dispose();
        }

        public int GetAmount(ProjectileType type)
        {
            if (_projectiles.ContainsKey(type) == false)
                return 0;

            return _projectiles[type];
        }

        public bool CanShoot()
        {
            if (_projectiles.ContainsKey(_selected) == false)
                return false;

            if (_projectiles[_selected] == 0)
                return false;

            return true;
        }

        private void OnCargoChanged(CargoChangedEvent data)
        {
            CheckType(data.Items, ProjectileType.Ball);
            CheckType(data.Items, ProjectileType.Knuppel);
            CheckType(data.Items, ProjectileType.Ball);
        }

        private void CheckType(IReadOnlyDictionary<ItemType, IItem> items, ProjectileType type)
        {
            var item = ConvertToItemType(type);
            
            if (items.ContainsKey(item) == false)
                return;

            var count = items[item].Count;
            _projectiles[type] = count;

            MessageBroker.Default.Publish(new ProjectileAmountChanged(type, count));
        }

        private ItemType ConvertToItemType(ProjectileType type)
        {
            return type switch
            {
                ProjectileType.Ball => ItemType.CannonBall,
                ProjectileType.Knuppel => ItemType.CannonKnuppel,
                ProjectileType.Shrapnel => ItemType.CannonShrapnel,
                ProjectileType.Fishnet => ItemType.CannonFishnet,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}