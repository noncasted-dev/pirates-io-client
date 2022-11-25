using GamePlay.Player.Entity.Components.ShipResources.Runtime;
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
        private Transform _transform;
        private IShipResources _resources;

        public Vector2 Position => GetPosition();
        public IShipResources Resources => _resources;

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

        private Vector2 GetPosition()
        {
            if (_transform == null)
            {
                Debug.Log("No player assigned");
                return Vector2.zero;
            }

            return _transform.position;
        }
    }
}