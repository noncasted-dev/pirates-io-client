﻿using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Services.PlayerPositionProviders.Runtime
{
    public class PlayerEntityProvider :
        MonoBehaviour,
        IPlayerEntityPresenter,
        IPlayerEntityProvider
    {
        private RagonEntity _entity;
        private Vector2 _lastPosition = Vector2.zero;
        private IShipResources _resources;
        private Transform _transform;

        public void AssignPlayer(
            RagonEntity entity,
            Transform playerTransform,
            IShipResources resources)
        {
            _entity = entity;
            _transform = playerTransform;
            _resources = resources;
        }

        public void DestroyPlayer()
        {
            RagonNetwork.Room.DestroyEntity(_entity.gameObject);
        }

        public Vector2 Position => GetPosition();
        public IShipResources Resources => _resources;

        private Vector2 GetPosition()
        {
            if (_transform == null) return _lastPosition;

            _lastPosition = _transform.position;

            return _lastPosition;
        }
    }
}