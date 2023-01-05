using System;
using System.Collections.Generic;
using GamePlay.Common.Damages;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Events;
using GamePlay.Services.Projectiles.Entity;
using Global.Services.MessageBrokers.Runtime;
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
            
            Select(ProjectileType.Fishnet);
        }

        private void OnEnable()
        {
            _cargoListener = Msg.Listen<CargoChangedEvent>(OnCargoChanged);
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

        public bool CanSelect(ProjectileType type)
        {
            var amount = GetAmount(type);
            
            if (amount == 0)
                return false;

            return true;
        }

        public void Select(ProjectileType type)
        {
            if (CanSelect(type) == false)
                return;
            
            _selected = type;
            
            var select = new ProjectileSelectedEvent(type);
            Msg.Publish(select);
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
            CheckType(data.Items, ProjectileType.Shrapnel);

            if (CanShoot() == false)
                Select(ProjectileType.Fishnet);
        }

        private void CheckType(IReadOnlyDictionary<ItemType, IItem> items, ProjectileType type)
        {
            var item = type.ConvertToItemType();

            if (items.ContainsKey(item) == false)
                return;

            var count = items[item].Count;
            _projectiles[type] = count;
            
            Msg.Publish(new ProjectileAmountChangedEvent(type, count));
        }
    }
}